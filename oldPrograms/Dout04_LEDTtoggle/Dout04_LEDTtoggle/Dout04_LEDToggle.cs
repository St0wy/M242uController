/* Programme  Dout04_LEDToggle
 * Hbr Fabian 20.03.17 Version 1.0
 * Description
 * Fonction pour changer l'�tat de la LED a chaque appel
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

namespace Dout04_LEDTtoggle
{
    public class Dout04_LEDToggle
    {
        static bool EtatLed;
        static OutputPort LedSpider = new OutputPort(FEZSpiderII.DebugLed, false);
        public static void Main()
        {
            while (true)
            {
                LedToggle();
                Thread.Sleep(100);
            }
        }
        public static bool LedToggle()
        {
            if (EtatLed == true)
            {
                LedSpider.Write(false);
                EtatLed = false;
            }
            else if (EtatLed == false)
            {
                LedSpider.Write(true);
                EtatLed = true;
            }
            return EtatLed;
        }
    }
}
