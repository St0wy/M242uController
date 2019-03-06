/* Programme  Dout06_ BlinkLed2Hz_10-90-10
 * Hbr Fabian 20.03.17 Version 1.0
 * Description
 * Faire clignoter une LED a 2Hz avec un rapport cyclique de 10% qui augemente de 10% chaque seconde puis a 90% descend de 10% a chaque seconde
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

namespace Dout06__BlinkLed2Hz_10_90_10
{
    public class Program
    {
        public static void Main()
        {
            int thaut = 50, tbas = 450, cmpSeconde=0, etat=0;
            OutputPort LedSpider = new OutputPort(FEZSpiderII.DebugLed, false);
            while (true)
            {
                LedSpider.Write(true);
                Thread.Sleep(thaut);
                LedSpider.Write(false);
                Thread.Sleep(tbas);
                if (thaut == 50) { etat = 0; }
                if (thaut == 450) { etat = 1; }
                if (cmpSeconde == 0)
                {
                    cmpSeconde++;
                }
                else if (cmpSeconde == 1)
                {
                    cmpSeconde = 0;
                    if (etat == 0)
                    {
                        thaut += 50;
                        tbas -= 50;
                    }
                    if (etat == 1)
                    {
                        thaut -= 50;
                        tbas += 50;
                    }
                }
            }
        }
    }
}
