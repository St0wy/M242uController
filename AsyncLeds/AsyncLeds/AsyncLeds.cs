/* Programme  AsyncLeds
 * Hbr Fabian 30.01.2019 Version 1.0
 * Description
 * Faire varier la luminositee de leds de facon asynchrone
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

namespace AsyncLeds
{
    public class AsyncLeds
    {
        public static void Main()
        {
            #region Constants
            const int STEP_DURATION = 20;
            const double LED_1_FREQUENCY = 1;
            const double LED_2_FREQUENCY = 3.3;
            const double LED_3_FREQUENCY = 5.5;
            const int LED_1_CYCLICAL_REPORT = 15;
            const int LED_2_CYCLICAL_REPORT = 80;
            const int LED_3_CYCLICAL_REPORT = 33; 
            #endregion

            LedStrip ledStrip = new LedStrip();

            AsynchronousLed AsyncLed1 = new AsynchronousLed(LED_1_FREQUENCY, ledStrip.Led0, LED_1_CYCLICAL_REPORT);
            AsynchronousLed AsyncLed2 = new AsynchronousLed(LED_2_FREQUENCY, ledStrip.Led1, LED_2_CYCLICAL_REPORT);
            AsynchronousLed AsyncLed3 = new AsynchronousLed(LED_3_FREQUENCY, ledStrip.Led2, LED_3_CYCLICAL_REPORT);

            //Main Loop
            while (true)
            {
                AsyncLed1.Blink();
                AsyncLed2.Blink();
                AsyncLed3.Blink();
                
                Thread.Sleep(STEP_DURATION);
            }
        }
    }
}
