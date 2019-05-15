/* Programme:  MFRegulateurTemp2
 * Hbr Fabian 15.05.2019 Version 1.0
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
        #region Constants
        const int VOLTAGE_0_DEGREE = 500; //in mV
        const int TEMPERATURE_COEFFICIENT = 10; //in mV
        const int HIGHEST_VOLTAGE = 330;
        const int DEFAULT_REQUESTED_TEMPERATURE = 80;
        const int PRINT_VALUES_PERIOD = 100;
        const int DEFAULT_RESISTOR_FREQUENCY = 1000;
        const double DEFAULT_RESISTOR_DUTY_CYCLE = 0.50;
        const double DEFAULT_GAIN = 0.2;
        const double MAX_GAIN = 1;
        const double MIN_GAIN = 0.001;
        const double GAIN_STEP = 0.001;
        const long TIME_ONE_MS_IN_TICKS = 10000;
        const long TIMER_LENGHT = 20000;
        const string GAIN_FORMAT = "D3";
        const string TEMPERATURE_FORMAT = "F2";
        #endregion

        #region Declarations
        enum State { SettingsOff, SettingsTemperature, SettingsGain, IncrementTemperature, DecrementTemperature, IncrementGain, DecrementGain };

        static Button btnSetting = new Button(new InputPort(GHICard.Socket9.Pin3, true, Port.ResistorMode.Disabled));
        static Button btnRemove = new Button(new InputPort(GHICard.Socket12.Pin3, true, Port.ResistorMode.Disabled));
        static Button btnAdd = new Button(new InputPort(GHICard.Socket14.Pin3, true, Port.ResistorMode.Disabled));
        static LCD lcd = new LCD();
        static AnalogInput heatSensor = new AnalogInput(GHICard.Socket10.AnalogInput3);
        //static OutputPort fan = new OutputPort(GHICard.Socket11.Pin9, false);
        static Timer printValues = new Timer(new TimerCallback(PrintValues), null, 0, PRINT_VALUES_PERIOD);
        static PWM resistor = new PWM(GHICard.Socket11.Pwm7, DEFAULT_RESISTOR_FREQUENCY, DEFAULT_RESISTOR_DUTY_CYCLE, false);
        static State state = State.SettingsOff;
        static int requestedTemperature = DEFAULT_REQUESTED_TEMPERATURE;
        static double gain = DEFAULT_GAIN;
        static double temperature = 0;
        static long checkTime = 0;
        static long elapsedTime = 0;
        static double resistorDutyCycle = 0;
        #endregion

        public static void Main()
        {
            //Time for the screen to turn on
            Thread.Sleep(40);

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
                double error = requestedTemperature - temperature;

                #region STATE MANAGEMENT
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
                            state = State.SettingsGain;
                        }
                        break;
                    case State.SettingsGain:
                        elapsedTime = ticks - checkTime;

                        if (btnAdd.isPressed())
                        {
                            state = State.IncrementGain;
                        }
                        if (btnRemove.isPressed())
                        {
                            state = State.DecrementGain;
                        }
                        if (btnSetting.isPressed() || elapsedTime >= TIMER_LENGHT * TIME_ONE_MS_IN_TICKS)
                        {
                            state = State.SettingsOff;
                        }
                        break;
                    case State.IncrementTemperature:
                        requestedTemperature++;

                        checkTime = ticks;
                        state = State.SettingsTemperature;
                        break;
                    case State.DecrementTemperature:
                        requestedTemperature--;

                        checkTime = ticks;
                        state = State.SettingsTemperature;
                        break;
                    case State.IncrementGain:
                        if (gain < MAX_GAIN)
                        {
                            gain += GAIN_STEP;
                        }

                        checkTime = ticks;
                        state = State.SettingsGain;
                        break;
                    case State.DecrementGain:
                        if (gain > MIN_GAIN)
                        {
                            gain -= GAIN_STEP;
                        }

                        checkTime = ticks;
                        state = State.SettingsGain;
                        break;
                    default:
                        break;
                }
                #endregion

                #region HEAT MANAGEMENT
                resistorDutyCycle = gain * error;
                if (resistorDutyCycle > 1)
                {
                    resistorDutyCycle = 1;
                }
                else if (resistorDutyCycle < 0)
                {
                    resistorDutyCycle = 0;
                }

                resistor.DutyCycle = resistorDutyCycle;
                #endregion

                #region OUTPUT MANAGEMENT
                lcd.SetCursor(0, 0);
                lcd.Write(temperature.ToString(TEMPERATURE_FORMAT));
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
                    case State.SettingsGain:
                        settingsText = "Gain       ";
                        break;
                }
                lcd.Write(settingsText);
                lcd.SetCursor(0, 7);
                lcd.Write(gain.ToString(GAIN_FORMAT));
                lcd.SetCursor(1, 12);
                lcd.Write(requestedTemperature.ToString(TEMPERATURE_FORMAT));
                #endregion
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

        private static void PrintValues(object obj)
        {
            Debug.Print(temperature.ToString(TEMPERATURE_FORMAT) + ";" + gain.ToString(GAIN_FORMAT) + ";" + requestedTemperature.ToString(TEMPERATURE_FORMAT) + ";" + resistorDutyCycle.ToString());
        }
    }
}
