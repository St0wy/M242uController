/* Programme:  MFRegulateurTemp
 * Hbr Fabian 17.04.2019 Version 1.0
 * Description
 * 
 */

/* Description matériel et librairies:
 * 
 * Version: GHI SDK:2016.1.2.0; NETMF:4.3.8.1; Gadgeteer core:2.44.1100.0
 * Carte mère: Spider 2	Version firmware: 4.3.8.1 	Version Loader:4.3.8.1  (MAJ: 4381)
 * Module utilisé		Connecteur utilisé	Fonction
 * 
 */

/* Référence à ajouter:
 *     GHI.Pins                     pour GHI.Pins.EMX ou ...
 *     Microsoft.SPOT.Hardware      pour Cpu.Pin et OutputPort
 */

// Par défaut
using System;
using Microsoft.SPOT;
// A ajouter
using Microsoft.SPOT.Hardware;         // Pour OutputPort
using GHI.Pins;					       // Pour GHI.Pins
using System.Threading;                // Pour Thread.Sleep(1000)
using GHICard = GHI.Pins.FEZSpiderII;  // Défini la carte principale utilisée

namespace MFRegulateurTemp
{
    public class RegulateurTemp
    {
        const int VOLTAGE_0_DEGREE = 500; //in mV
        const int TEMPERATURE_COEFFICIENT = 10; //in mV
        const int HIGHEST_VOLTAGE = 330;
        const int DEFAULT_LIMIT_TEMPERATURE = 80;
        const int DEFAULT_HYSTERESIS = 10;
        const int MAX_HYSTERESIS = 10;
        const int MIN_HYSTERESIS = 0;
        const long TIME_ONE_MS_IN_TICKS = 10000;
        const long TIMER_LENGHT = 20000;

        enum State { SettingsOff, SettingsTemperature, SettingsHysteresis, IncrementTemperature, DecrementTemperature, IncrementHysteresis, DecrementHysteresis };

        static Button btnSetting = new Button(new InputPort(GHICard.Socket9.Pin3, true, Port.ResistorMode.Disabled));
        static Button btnRemove = new Button(new InputPort(GHICard.Socket12.Pin3, true, Port.ResistorMode.Disabled));
        static Button btnAdd = new Button(new InputPort(GHICard.Socket14.Pin3, true, Port.ResistorMode.Disabled));
        static LCD lcd = new LCD();
        static AnalogInput heatSensor = new AnalogInput(GHICard.Socket10.AnalogInput3);
        static OutputPort resistor = new OutputPort(GHICard.Socket11.Pin7, false);
        static OutputPort fan = new OutputPort(GHICard.Socket11.Pin9, false);
        static State state = State.SettingsOff;
        static double temperature = 0;
        static long checkTime = 0;
        static long elapsedTime = 0;
        static int limitTemperature = DEFAULT_LIMIT_TEMPERATURE;
        static int hysteresis = DEFAULT_HYSTERESIS;
        static bool isResistorOn = false;
        static bool isFanOn = false;

        public static void Main()
        {
            double oldtemp = 0;
            double oldoldtemp = 0;

            while (true)
            {
                //Gets the temperature
                long ticks = Utility.GetMachineTime().Ticks;
                int heatSensorValue = heatSensor.ReadRaw();
                oldoldtemp = oldtemp;
                oldtemp = temperature;
                temperature = (heatSensorValue * HIGHEST_VOLTAGE) / System.Math.Pow(2, GHICard.SupportedAnalogInputPrecision) - (VOLTAGE_0_DEGREE / TEMPERATURE_COEFFICIENT);
                temperature = moyDouble(new double[] { oldoldtemp, oldtemp, temperature });

                //STATE MANAGEMENT
                switch (state)
                {
                    case State.SettingsOff:
                        if (btnSetting.isPressed())
                        {
                            checkTime = ticks;
                            state = State.SettingsTemperature;
                        }
                        break;
                    case State.SettingsTemperature:
                        elapsedTime = ticks - checkTime;
                        
                        if (btnAdd.isPressed())
                        {
                            state = State.IncrementTemperature;
                        }
                        if (btnRemove.isPressed())
                        {
                            state = State.DecrementTemperature;
                        }
                        if (elapsedTime >= TIMER_LENGHT * TIME_ONE_MS_IN_TICKS)
                        {
                            state = State.SettingsOff;
                        }
                        if (btnSetting.isPressed())
                        {
                            checkTime = ticks;
                            state = State.SettingsHysteresis;
                        }
                        break;
                    case State.SettingsHysteresis:
                        elapsedTime = ticks - checkTime;

                        if (btnAdd.isPressed())
                        {
                            state = State.IncrementHysteresis;
                        }
                        if (btnRemove.isPressed())
                        {
                            state = State.DecrementHysteresis;
                        }
                        if (btnSetting.isPressed() || elapsedTime >= TIMER_LENGHT * TIME_ONE_MS_IN_TICKS)
                        {
                            state = State.SettingsOff;
                        }
                        break;
                    case State.IncrementTemperature:
                        limitTemperature++;

                        checkTime = ticks;
                        state = State.SettingsTemperature;
                        break;
                    case State.DecrementTemperature:
                        limitTemperature--;

                        checkTime = ticks;
                        state = State.SettingsTemperature;
                        break;
                    case State.IncrementHysteresis:
                        if (hysteresis < MAX_HYSTERESIS)
                        {
                            hysteresis++;
                        }

                        checkTime = ticks;
                        state = State.SettingsHysteresis;
                        break;
                    case State.DecrementHysteresis:
                        if (hysteresis > MIN_HYSTERESIS)
                        {
                            hysteresis--;
                        }

                        checkTime = ticks;
                        state = State.SettingsHysteresis;
                        break;
                    default:
                        break;
                }

                
                //HEAT MANAGEMENT
                if (temperature <= limitTemperature - (hysteresis / 2))
                {
                    isResistorOn = true;
                    isFanOn = false;
                }

                if (temperature >= limitTemperature + (hysteresis / 2))
                {
                    isFanOn = true;
                    isResistorOn = false;
                }

                fan.Write(isFanOn);
                resistor.Write(isResistorOn);

                //OUTPUT MANAGEMENT
                lcd.SetCursor(0, 0);
                lcd.Write(temperature.ToString("F2"));
                lcd.SetCursor(1, 0);
                string settingsText = "";
                switch (state)
                {
                    case State.SettingsOff:
                        settingsText = "Off        ";
                        break;
                    case State.SettingsTemperature:
                        settingsText = "Temperature";
                        break;
                    case State.SettingsHysteresis:
                        settingsText = "Hysteresis ";
                        break;
                }
                lcd.Write(settingsText);
                lcd.SetCursor(0, 7);
                lcd.Write(hysteresis.ToString("D2"));
                lcd.SetCursor(0, 10);
                lcd.Write(limitTemperature.ToString("D2"));

                Debug.Print(temperature.ToString("F2") + ";" + (limitTemperature + hysteresis / 2).ToString("D2") + ";" + (limitTemperature - hysteresis / 2).ToString("D2") + ";" + limitTemperature.ToString("D2") + ";" + isResistorOn.ToString());
            }
        }

        public static double moyDouble(double[] tab)
        {
            double moy = 0;
            foreach (double nbr in tab)
            {
                moy += nbr;
            }
            moy /= tab.Length;

            return moy;
        }
    }
}
