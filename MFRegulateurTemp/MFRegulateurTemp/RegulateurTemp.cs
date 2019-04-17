/* Programme:  MFRegulateurTemp
 * Hbr Fabian 17.04.2019 Version 1.0
 * Description
 * 
 */

/* Description mat�riel et librairies:
 * 
 * Version: GHI SDK:2016.1.2.0; NETMF:4.3.8.1; Gadgeteer core:2.44.1100.0
 * Carte m�re: Spider 2	Version firmware: 4.3.8.1 	Version Loader:4.3.8.1  (MAJ: 4381)
 * Module utilis�		Connecteur utilis�	Fonction
 * 
 */

/* R�f�rence � ajouter:
 *     GHI.Pins                     pour GHI.Pins.EMX ou ...
 *     Microsoft.SPOT.Hardware      pour Cpu.Pin et OutputPort
 */

// Par d�faut
using System;
using Microsoft.SPOT;
// A ajouter
using Microsoft.SPOT.Hardware;         // Pour OutputPort
using GHI.Pins;					       // Pour GHI.Pins
using System.Threading;                // Pour Thread.Sleep(1000)
using GHICard = GHI.Pins.FEZSpiderII;  // D�fini la carte principale utilis�e

namespace MFRegulateurTemp
{
    public class RegulateurTemp
    {
        public static void Main()
        {
            
        }
    }
}