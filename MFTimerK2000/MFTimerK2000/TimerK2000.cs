/* Programme  WFTimerK2000
 * Hbr Fabian 13.03.2019 Version 1.0
 * Description
 * Allumer des leds avec des timers a la K2000
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


namespace MFTimerK2000
{
    public class TimerK2000
    {
        public static int ledIndex = 0;
        public static bool isGoingUp = true;
        public static LedStrip ls = new LedStrip();

        public static void Main()
        {
            bool isTimerActive = true;
            int dueTime = 0;
            int period = 250;

            TimerCallback TC = new TimerCallback(BlinkLed);
            Button btnStart = new Button(true, new InputPort(GHICard.Socket12.Pin3, true, Port.ResistorMode.Disabled), 100);

            Timer timer = new Timer(TC, null, dueTime, period);

            while (true)
            {
                if (btnStart.isPressed())
                {
                    if (isTimerActive)
                    {
                        dueTime = -1;
                    }
                    else
                    {
                        dueTime = 0;
                    }

                    isTimerActive = !isTimerActive;

                }
                timer.Change(dueTime, period);
            }
        }

        public static void BlinkLed(object obj)
        {
            if (isGoingUp)
            {
                ledIndex++;
            }
            else
            {
                ledIndex--;
            }

            if (ledIndex == 6)
            {
                isGoingUp = false;
            }
            else if (ledIndex == 0)
            {
                isGoingUp = true;
            }

            foreach (OutputPort op in ls.TabLed)
            {
                if (op == ls.TabLed[ledIndex])
                {
                    op.Write(true);
                }
                else
                {
                    op.Write(false);
                }
            }
        }
    }
}
