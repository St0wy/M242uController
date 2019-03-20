/* Programme  InteruptTrucMachin
 * Hbr Fabian 20.03.2019 Version 1.0
 * Description
 * Programme d'apprentissage de l'interupt port
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


namespace MFInteruptTrucMachin
{
    public class InteruptTrucMachin
    {
        private static int interuptCounter = 0;
        private static InterruptPort btn1 = new InterruptPort(GHICard.Socket12.Pin3, true, Port.ResistorMode.Disabled, Port.InterruptMode.InterruptEdgeLow);
        private static InterruptPort btn2 = new InterruptPort(GHICard.Socket14.Pin3, true, Port.ResistorMode.Disabled, Port.InterruptMode.InterruptEdgeHigh);
        private static OutputPort ledBtn1 = new OutputPort(GHICard.Socket12.Pin4, false);
        private static OutputPort ledBtn2 = new OutputPort(GHICard.Socket14.Pin4, false);
        private static OutputPort ledSpider = new OutputPort(GHICard.DebugLed, false);

        public static void Main()
        {
            btn1.OnInterrupt += new NativeEventHandler(Btn_OnInterrupt);
            btn2.OnInterrupt += new NativeEventHandler(Btn_OnInterrupt);

            Thread.Sleep(Timeout.Infinite);
        }

        public static void Btn_OnInterrupt(uint port, uint state, DateTime time)
        {
            Thread.Sleep(1);

            if (port == 45)
            {
                if (btn1.Read() && state == 0)
                {
                    AButtonIsPressed();
                    ledBtn1.Write(false);
                    ledBtn2.Write(true);
                }
            }
            else if (port == 44)
            {
                if (btn2.Read() && state == 1)
                {
                    AButtonIsPressed();
                    ledBtn1.Write(true);
                    ledBtn2.Write(false);
                }
            }
        }
        
        public static void AButtonIsPressed()
        {
            interuptCounter++;
            Debug.Print(interuptCounter.ToString());

            ledSpider.Write(!ledSpider.Read());
        }
    }
}
