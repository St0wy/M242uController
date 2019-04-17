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
        const int TIMER_LENGHT = 20000;
        const int LOOP_LENGHT = 20;
        const int DEFAULT_LIMIT_TEMPERATURE = 20;
        const int DEFAULT_HYSTERESIS = 10;

        enum State { SettingsOff, SettingsTemperature, SettingsHysteresis, IncrementTemperature, DecrementTemperature, IncrementHysteresis, DecrementHysteresis };

        static Button btnSetting = new Button(new InputPort(GHICard.Socket9.Pin3, true, Port.ResistorMode.Disabled));
        static Button btnRemove = new Button(new InputPort(GHICard.Socket12.Pin3, true, Port.ResistorMode.Disabled));
        static Button btnAdd = new Button(new InputPort(GHICard.Socket14.Pin3, true, Port.ResistorMode.Disabled));

        static State state = State.SettingsOff;

        static AnalogInput heatSensor = new AnalogInput(GHICard.Socket10.AnalogInput3);
        static double temperature = 0;
        static int timer = 0;
        static int limitTemperature = DEFAULT_LIMIT_TEMPERATURE;
        static int hysteresis = DEFAULT_HYSTERESIS;

        public static void Main()
        {
            double oldtemp = 0;
            double oldoldtemp = 0;

            while (true)
            {
                int heatSensorValue = heatSensor.ReadRaw();
                oldoldtemp = oldtemp;
                oldtemp = temperature;
                temperature = (heatSensorValue * HIGHEST_VOLTAGE) / System.Math.Pow(2, GHICard.SupportedAnalogInputPrecision) - (VOLTAGE_0_DEGREE / TEMPERATURE_COEFFICIENT);
                temperature = moyDouble(new double[] { oldoldtemp, oldtemp, temperature });

                switch (state)
                {
                    case State.SettingsOff:
                        if (btnSetting.isPressed())
                        {
                            timer = 0;
                            state = State.IncrementTemperature;
                        }
                        break;
                    case State.SettingsTemperature:
                        timer += LOOP_LENGHT;

                        if (btnAdd.isPressed())
                        {
                            state = State.IncrementTemperature;
                        }
                        if (btnRemove.isPressed())
                        {
                            state = State.DecrementTemperature;
                        }
                        if (timer >= TIMER_LENGHT)
                        {
                            state = State.SettingsOff;
                        }
                        if (btnSetting.isPressed())
                        {
                            timer = 0;
                            state = State.SettingsHysteresis;
                        }
                        break;
                    case State.SettingsHysteresis:
                        timer += LOOP_LENGHT;

                        if (btnAdd.isPressed())
                        {
                            state = State.IncrementHysteresis;
                        }
                        if (btnRemove.isPressed())
                        {
                            state = State.DecrementHysteresis;
                        }
                        if (btnSetting.isPressed() || timer >= TIMER_LENGHT)
                        {
                            state = State.SettingsOff;
                        }
                        break;
                    case State.IncrementTemperature:
                        limitTemperature++;

                        state = State.SettingsTemperature;
                        break;
                    case State.DecrementTemperature:
                        limitTemperature--;

                        state = State.SettingsTemperature;
                        break;
                    case State.IncrementHysteresis:
                        hysteresis++;

                        state = State.SettingsHysteresis;
                        break;
                    case State.DecrementHysteresis:
                        hysteresis--;

                        state = State.SettingsHysteresis;
                        break;
                    default:
                        break;
                }

                Debug.Print(temperature.ToString());

                Thread.Sleep(LOOP_LENGHT);
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
