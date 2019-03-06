/* Programme  BlinkingLed
 * Hbr Fabian 28.02.17 Version 1.0
 * Description
 * Utilisation de la led de la carte Spider 2
 */

/* Description matériel et librairies:
 * 
 * Version: GHI SDK:2016.1.2.0; NETMF:4.3.8.1; Gadgeteer core:2.44.1100.0
 * Carte mère: Spider 2	Version firmware: 4.3.8.1 	Version Loader:4.3.8.1  (MAJ: 4381)
 * Module utilisé		Connecteur utilisé	Fonction
 *	usbClientDP     1                   Connexion au PC
 *  Button          2                   Start/Stop
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

namespace BlinkingLed
{
    public class BlinkingLed
    {
        public static void Main()
        {
            bool start=false;
            OutputPort LedSpider = new OutputPort(FEZSpiderII.DebugLed, false);
            InputPort btn1 = new InputPort(G120.P2_13, true, Port.ResistorMode.Disabled);
            InputPort btn2 = new InputPort(FEZSpiderII.Socket14.Pin3, true, Port.ResistorMode.Disabled);

            while (true)
            {
                if (!btn1.Read() == true)
                {
                    start = true;
                }
                if (!btn2.Read() == true)
                {
                    start = false;
                }
                if (start == true)
                {
                    LedSpider.Write(true);
                    Thread.Sleep(125);
                    LedSpider.Write(false);
                    Thread.Sleep(125);
                }
                if (start == false)
                {
                    LedSpider.Write(false);
                }
            }
        }
    }
}
