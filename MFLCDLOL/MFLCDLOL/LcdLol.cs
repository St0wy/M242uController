/* Programme  LCDLOL
 * Hbr Fabian 20.03.2019 Version 1.0
 * Description
 * LCD
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

namespace MFLCDLOL
{
    public class LcdLol
    {
        public static void Main()
        {
            Thread.Sleep(40);
            LCD lcd = new LCD();

            lcd.SetCursor(0, 0);
            lcd.RegisterSelect.Write(true);
            lcd.SendData(0xBD);
            lcd.SendData(0xB7);
            lcd.SendData(0xAC);
            lcd.SendData(0xB7);
            lcd.RegisterSelect.Write(false);
            lcd.SetCursor(1, 0);
            lcd.RegisterSelect.Write(true);
            lcd.SendData(0xCC);
            lcd.SendData(0xBF);
            lcd.SendData(0xB9);
            lcd.SendData(0xBF);
            lcd.RegisterSelect.Write(false);
            Thread.Sleep(Timeout.Infinite);
        }
    }
}
