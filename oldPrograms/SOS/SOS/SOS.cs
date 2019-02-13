/* Programme  SOS
 * Hbr Fabian 27.03.17 Version 1.0
 * Description
 * Cree un signal morse SOS.
 */

/* Description matériel et librairies:
 * 
 * Version: GHI SDK:2016.1.2.0; NETMF:4.3.8.1; Gadgeteer core:2.44.1100.0
 * Carte mère: Spider 2	Version firmware: 4.3.8.1 	Version Loader:4.3.8.1  (MAJ: 4381)
 * Module utilisé		Connecteur utilisé	Fonction
 *	usbClientDP     1                   Connexion au PC
 */

/* Référence à ajouter:
 *     GHI.Pins                     pour GHI.Pins.EMX ou ...
 *     Microsoft.SPOT.Hardware      pour Cpu.Pin et OutputPort
 */

// Par défaut
using System;
using Microsoft.SPOT;
// A ajouter
using Microsoft.SPOT.Hardware;  // Pour OutputPort
using GHI.Pins;									// Pour GHI.Pins
using System.Threading;         // Pour Thread.Sleep(1000)
using GHICard = GHI.Pins.FEZSpider; // Défini la carte principale utilisée

namespace SOS
{
    public class SOS
    {
        static OutputPort LedSpider = new OutputPort(FEZSpiderII.DebugLed, false);
        public static void Main()
        {
            while (true)
            {
                S();
                separateur();
                O();
                separateur();
                S();
                espace();
            }
            
        }

        public static void tmpDot()
        {
            Thread.Sleep(200);
        }

        public static void tmpDash()
        {
            tmpDot();
            tmpDot();
            tmpDot();
        }

        public static void dot()
        {
            LedSpider.Write(true);
            tmpDot();
            LedSpider.Write(false);
        }

        public static void dash()
        {
            LedSpider.Write(true);
            tmpDash();
            LedSpider.Write(false);
        }

        public static void separateur()
        {
            tmpDash();
        }

        public static void espace()
        {
            for (int i = 0; i < 7; i++)
            {
                tmpDot();
            }
        }

        public static void S()
        {
            dot();
            tmpDot();
            dot();
            tmpDot();
            dot();
            tmpDot();
        }

        public static void O()
        {
            dash();
            tmpDot();
            dash();
            tmpDot();
            dash();
            tmpDot();
        }
    }
}
