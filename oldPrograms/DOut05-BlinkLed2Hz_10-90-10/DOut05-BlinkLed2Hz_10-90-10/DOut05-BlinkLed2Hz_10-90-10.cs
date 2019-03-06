/* Programme  DOut05-BlinkLed2Hz_10-90-10
 * Hbr Fabian 20.03.17 Version 1.0
 * Description
 * Faire clignoter une LED a 2Hz mais a avec un rapport ciclique de 10%
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

namespace DOut05_BlinkLed2Hz_10_90_10
{
    public class Program
    {
        public static void Main()
        {
            OutputPort LedSpider = new OutputPort(FEZSpiderII.DebugLed, false);
            while (true)
            {
                LedSpider.Write(true);
                Thread.Sleep(50);
                LedSpider.Write(false);
                Thread.Sleep(450);
            }
        }
    }
}
