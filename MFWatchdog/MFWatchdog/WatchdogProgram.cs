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

            Button btnStart = new Button(true, new InputPort(GHICard.Socket12.Pin3, true, Port.ResistorMode.Disabled), 250);
            Button btnRestart = new Button(true, new InputPort(GHICard.Socket14.Pin3, true, Port.ResistorMode.Disabled), 250);
            OutputPort ledStart = new OutputPort(GHICard.Socket12.Pin4, DEFAULT_LEDSTART_INITIAL_STATE);
            OutputPort ledRestart = new OutputPort(GHICard.Socket14.Pin4, DEFAULT_LEDRESTART_INITIAL_STATE);
            OutputPort ledFez = new OutputPort(GHICard.DebugLed, DEFAULT_LEDFEZ_INITIAL_STATE);

            int state = 0;

            //main loop
            while (true)
            {
                switch (state)
                {
                    case (int)State.Wait:
                        ledStart.Write(true);
                        break;
                    case (int)State.EnableWD:
                        GHIWatchdog.Enable(DEFAULT_WATCHDOG_TIMEOUT_PERIOD);
                        break;
                    case (int)State.Count:
                        break;
                    case (int)State.ReloadWD:
                        break;
                    case (int)State.Alarm:
                        break;
                    default:
                        break;
                }


            }
        }
    }
}
