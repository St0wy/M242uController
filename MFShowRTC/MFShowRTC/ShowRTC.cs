/* Programme  Test
 * Hbr Fabian 27.03.2019 Version 1.0
 * Description
 * Programme qui sert a definir la valeur du RTC
 */

/* Description mat�riel et librairies:
 * 
 * Version: GHI SDK:2016.1.2.0; NETMF:4.3.8.1; Gadgeteer core:2.44.1100.0
 * Carte m�re: Spider 2	Version firmware: 4.3.8.1 	Version Loader:4.3.8.1  (MAJ: 4381)
 */

/* R�f�rence � ajouter:
 *     GHI.Pins                     pour GHI.Pins.EMX ou ...
 *     GHI.Hardware                 pour GHI.Processor.RealTimeClock
 *     Microsoft.SPOT.Hardware      pour Cpu.Pin et OutputPort
 */

// Par d�faut
using System;
using Microsoft.SPOT;
// A ajouter
using Microsoft.SPOT.Hardware;      // Pour OutputPort
using GHI.Pins;					    // Pour GHI.Pins
using System.Threading;             // Pour Thread.Sleep()
using GHICard = GHI.Pins.FEZSpiderII; // D�fini la carte principale utilis�e
using GHIRTC = GHI.Processor.RealTimeClock;

namespace MFShowRTC
{
    public class ShowRTC
    {
        public static void Main()
        {
            const string DATE_FORMAT = "dd.MM.yyyy";
            const string HOUR_FORMAT = "HH:mm:ss";

            Thread.Sleep(40);
            Utility.SetLocalTime(GHIRTC.GetDateTime());
            LCD lcd = new LCD();

            string date = "";
            string hour = "";

            while (true)
            {
                date = DateTime.Now.ToString(DATE_FORMAT);
                hour = DateTime.Now.ToString(HOUR_FORMAT);

                lcd.SetCursor(0, 0);
                lcd.Write(date);
                lcd.SetCursor(1, 0);
                lcd.Write(hour);
            }
        }
    }
}
