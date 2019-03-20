/* Programme  MFTextExtendedTimer
 * Hbr Fabian 13.03.2019 Version 1.0
 * Description
 * Champ de test pour Spider 2
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


namespace MFTestExtendedTimer
{
    public class TestExtendedTimer
    {
        public static OutputPort op = new OutputPort(GHICard.Socket9.Pin4, true);

        public static void Main()
        {
            Utility.SetLocalTime(new DateTime(2019, 3, 13, 23, 59, 30));

            TimerCallback TC = new TimerCallback(BlinkLed);

            ExtendedTimer eTimer = new ExtendedTimer(TC, null, ExtendedTimer.TimeEvents.Second);

            while (true)
            {
                
            }
        }

        public static void BlinkLed(object obj)
        {
            op.Write(!op.Read());
        }
    }
}
