/* Programme  Dout03_BlinkLED_2Hz
 * Hbr Fabian 06.03.17 Version 1.0
 * Description
 * Clignotement de LED a 2Hz
 */

/* Description mat�riel et librairies:
 * 
 * Version: GHI SDK:2016.1.2.0; NETMF:4.3.8.1; Gadgeteer core:2.44.1100.0
 * Carte m�re: Spider 2	Version firmware: 4.3.8.1 	Version Loader:4.3.8.1  (MAJ: 4381)
 * Module utilis�		Connecteur utilis�	Fonction
 *	usbClientDP     1                   Connexion au PC
 */

/* R�f�rence � ajouter:
 *     GHI.Pins                     pour GHI.Pins.EMX ou ...
 *     Microsoft.SPOT.Hardware      pour Cpu.Pin et OutputPort
 */

// Par d�faut
using System;
using Microsoft.SPOT;
// A ajouter
using Microsoft.SPOT.Hardware;  // Pour OutputPort
using GHI.Pins;									// Pour GHI.Pins
using System.Threading;         // Pour Thread.Sleep(1000)
using GHICard = GHI.Pins.FEZSpider; // D�fini la carte principale utilis�e

namespace Dout03_BlinkLED_2Hz
{
    public class Dout03_BlinkLED_2Hz
    {
        public static void Main()
        {
            OutputPort LedSpider = new OutputPort(FEZSpiderII.DebugLed, false);
            while (true)
            {
                LedSpider.Write(true);
                Thread.Sleep(500);
                LedSpider.Write(false);
                Thread.Sleep(500);
            }
        }
    }
}
