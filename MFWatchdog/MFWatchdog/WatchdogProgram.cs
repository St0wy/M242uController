/* Programme  MFWatchdog
 * Hbr Fabian 06.03.2019 Version 1.0
 * Description
 * Programme apprentisage watchdog
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
using Microsoft.SPOT.Hardware;      // Pour OutputPort
using GHI.Pins;					    // Pour GHI.Pins
using System.Threading;             // Pour Thread.Sleep(1000)
using GHICard = GHI.Pins.FEZSpiderII; // Défini la carte principale utilisée
using GHIWatchdog = GHI.Processor.Watchdog;

namespace MFWatchdog
{
    public class WatchdogProgram
    {
        enum State { Wait, EnableWD, Count, ReloadWD, Alarm };

        public static void Main()
        {
            const bool DEFAULT_LEDSTART_INITIAL_STATE = true;
            const bool DEFAULT_LEDRESTART_INITIAL_STATE = false;
            const bool DEFAULT_LEDFEZ_INITIAL_STATE = true;
            const int DEFAULT_WATCHDOG_TIMEOUT_PERIOD = 5000;
            const int TIME_BLINK_LED = 100;
            const int ONE_MS_IN_TICK = 10000;

            Button btnStart = new Button(true, new InputPort(GHICard.Socket12.Pin3, true, Port.ResistorMode.Disabled), 250);
            Button btnRestart = new Button(true, new InputPort(GHICard.Socket14.Pin3, true, Port.ResistorMode.Disabled), 250);
            OutputPort ledStart = new OutputPort(GHICard.Socket12.Pin4, DEFAULT_LEDSTART_INITIAL_STATE);
            OutputPort ledRestart = new OutputPort(GHICard.Socket14.Pin4, DEFAULT_LEDRESTART_INITIAL_STATE);
            OutputPort ledFez = new OutputPort(GHICard.DebugLed, DEFAULT_LEDFEZ_INITIAL_STATE);

            int state = (int)State.Wait;
            long oldTicksFezLed = 0;
            long oldTicksRestartLed = 0;
            long oldTicksAlarm = 0;
            bool isLedFezOn = false;
            bool isLedRestartOn = false;

            //main loop
            while (true)
            {
                //State Management
                long ticks = Utility.GetMachineTime().Ticks;

                switch (state)
                {
                    case (int)State.Wait:
                        ledStart.Write(true);
                        ledRestart.Write(false);
                        long elapsedTicksFezLed = ticks - oldTicksFezLed;
                        if (elapsedTicksFezLed >= TIME_BLINK_LED * ONE_MS_IN_TICK)
                        {
                            isLedFezOn = !isLedFezOn;
                            oldTicksFezLed = ticks;
                        }
                        ledFez.Write(isLedFezOn);

                        if (btnStart.isPressed())
                        {
                            state = (int)State.EnableWD;
                        }
                        break;
                    case (int)State.EnableWD:
                        ledStart.Write(false);
                        ledRestart.Write(false);
                        ledFez.Write(false);
                        GHIWatchdog.Enable(DEFAULT_WATCHDOG_TIMEOUT_PERIOD);
                        state = (int)State.Count;
                        oldTicksAlarm = ticks;
                        break;
                    case (int)State.Count:
                        ledStart.Write(false);
                        ledRestart.Write(true);
                        ledFez.Write(true);
                        long elapsedTicksAlarm = ticks - oldTicksAlarm;
                        if (elapsedTicksAlarm >= (DEFAULT_WATCHDOG_TIMEOUT_PERIOD / 2) * ONE_MS_IN_TICK)
                        {
                            state = (int)State.Alarm;
                        }

                        if (btnRestart.isPressed())
                        {
                            state = (int)State.ReloadWD;
                        }
                        break;
                    case (int)State.Alarm:
                        ledStart.Write(false);
                        long elapsedTicksRestartLed = ticks - oldTicksRestartLed;
                        if (elapsedTicksRestartLed >= TIME_BLINK_LED * ONE_MS_IN_TICK)
                        {
                            isLedRestartOn = !isLedRestartOn;
                            oldTicksRestartLed = ticks;
                        }
                        ledRestart.Write(isLedRestartOn);
                        ledFez.Write(true);

                        if (btnRestart.isPressed())
                        {
                            state = (int)State.ReloadWD;
                        }
                        break;
                    case (int)State.ReloadWD:
                        ledStart.Write(false);
                        ledRestart.Write(true);
                        ledFez.Write(true);
                        GHIWatchdog.ResetCounter();
                        oldTicksAlarm = ticks;

                        state = (int)State.Count;
                        break;
                    default:
                        break;
                }
            }
        }
    }
}