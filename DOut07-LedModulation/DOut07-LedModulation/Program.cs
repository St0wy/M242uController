/* Programme  DOut07-LedModulation
 * Hbr Fabian 23.01.2019 Version 1.0
 * Description
 * Changer le rapport cyclique du clignotement de la led sur la board spider
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
using Microsoft.SPOT.Hardware;        // Pour OutputPort
using GHI.Pins;					      // Pour GHI.Pins
using System.Threading;               // Pour Thread.Sleep(1000)
using GHICard = GHI.Pins.FEZSpiderII; // Défini la carte principale utilisée

namespace DOut07_LedModulation
{
    public class Program
    {
        public static void Main()
        {
            const int STATE_GOING_DOWN = 0;
            const int STATE_GOING_UP = 1;
            const int FREQUENCY = 50;        //Frequency in Hz
            const int ONE_SECOND_IN_MS = 1000;
            const int PERCENT_TO_INCREMENT = 5;
            const int STEP_DURATION = 40;

            int periodTime = ONE_SECOND_IN_MS / FREQUENCY; //20
            int percentOfPeriod = periodTime / PERCENT_TO_INCREMENT; //4

            int duration = 0;
            int state = STATE_GOING_UP;
            int counter = 0;

            OutputPort LedSpider = new OutputPort(GHICard.DebugLed, false);

            //Main loop
            while (true)
            {
                //Turn on and of the led
                LedSpider.Write(true);
                Thread.Sleep(duration);
                LedSpider.Write(false);
                Thread.Sleep(periodTime-duration);

                //Look if one second passed or not
                if (counter >= STEP_DURATION)
                {
                    counter = 0;
                    if (state == STATE_GOING_UP)
                    {
                        duration += percentOfPeriod;
                    }
                    else if (state == STATE_GOING_DOWN)
                    {
                        duration -= percentOfPeriod;
                    }

                    //State management
                    if (duration <= 0)
                    {
                        state = STATE_GOING_UP;
                    }
                    else if (duration >= periodTime)
                    {
                        state = STATE_GOING_DOWN;
                    }
                }
                else
                {
                    counter += periodTime;
                }
            }
        }
    }
}
