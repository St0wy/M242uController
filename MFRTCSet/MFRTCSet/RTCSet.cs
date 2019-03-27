/* Programme  Test
 * Hbr Fabian 27.03.2019 Version 1.0
 * Description
 * Programme qui sert a definir la valeur du RTC
 */

/* Description matériel et librairies:
 * 
 * Version: GHI SDK:2016.1.2.0; NETMF:4.3.8.1; Gadgeteer core:2.44.1100.0
 * Carte mère: Spider 2	Version firmware: 4.3.8.1 	Version Loader:4.3.8.1  (MAJ: 4381)
 */

/* Référence à ajouter:
 *     GHI.Pins                     pour GHI.Pins.EMX ou ...
 *     GHI.Hardware                 pour GHI.Processor.RealTimeClock
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
using GHIRTC = GHI.Processor.RealTimeClock;


namespace MFRTCSet
{
    public class RTCSet
    {
        public static void Main()
        {
            DateTime rtc = new DateTime(2019, 03, 27, 11, 0, 0);
            Button btn = new Button(new InputPort(GHICard.Socket12.Pin3, true, Port.ResistorMode.Disabled));

            while (true)
            {
                if (btn.isPressed())
                {
                    GHIRTC.SetDateTime(rtc);
                }

                try
                {
                    Debug.Print("RTC: " + GHIRTC.GetDateTime().ToString());
                }
                catch
                {
                    Debug.Print("RTC NOT SET");
                }
            }
        }
    }
}
