/* Programme  TrueLedsAsychrones
 * Hbr Fabian 28.02.17 Version 1.0
 * Description
 * CLignoter des leds de façon asychrones
 */

/* Description matériel et librairies:
 * 
 * Version: GHI SDK:2016.1.2.0; NETMF:4.3.8.1; Gadgeteer core:2.44.1100.0
 * Carte mère: Spider 2	Version firmware: 4.3.8.1 	Version Loader:4.3.8.1  (MAJ: 4381)
 * Module utilisé		Connecteur utilisé	Fonction
 *	usbClientDP     1                   Connexion au PC
 *  LED strip       1                   Show the action of the Joystick
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

namespace TrueLedsAsychrones
{
    public class Program
    {
        public static void Main()
        {
            OutputPort LedLigne1 = new OutputPort(FEZSpiderII.Socket8.Pin4, false);
            OutputPort LedLigne2 = new OutputPort(FEZSpiderII.Socket8.Pin5, false);
            OutputPort LedLigne3 = new OutputPort(FEZSpiderII.Socket8.Pin6, false);
            OutputPort LedLigne4 = new OutputPort(FEZSpiderII.Socket8.Pin7, false);
            OutputPort LedLigne5 = new OutputPort(FEZSpiderII.Socket8.Pin8, false);
            OutputPort LedLigne6 = new OutputPort(FEZSpiderII.Socket8.Pin9, false);

            int compteurLed1 = 0, compteurLed2 = 0, compteurLed3 = 0;
            while (true)
            {
                #region Led1
                if (compteurLed1 < 15)
                {
                    LedLigne5.Write(true);
                }
                else
                {
                    if (compteurLed1 < 100)
                    {
                        LedLigne5.Write(false);
                    }
                    else
                    {
                        compteurLed1 = 0;
                    }
                }
                #endregion

                #region Led2
                if (compteurLed2 < 27)
                {
                    LedLigne3.Write(true);
                }
                else
                {
                    if (compteurLed2 < 33)
                    {
                        LedLigne3.Write(false);
                    }
                    else
                    {
                        compteurLed2 = 0;
                    }
                }
                #endregion

                #region Led3
                if (compteurLed3 < 6)
                {
                    LedLigne1.Write(true);
                }
                else
                {
                    if (compteurLed3 < 18)
                    {
                        LedLigne1.Write(false);
                    }
                    else
                    {
                        compteurLed3 = 0;
                    }
                }
                #endregion

                Thread.Sleep(10);
                compteurLed1++;
                compteurLed2++;
                compteurLed3++;
            }
        }
    }
}
