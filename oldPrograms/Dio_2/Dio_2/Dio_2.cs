/* Programme  LedSpider2
 * Hbr Fabian 28.02.17 Version 1.0
 * Description
 * Allumer une led avec un boutton et l'éteindre avec un autre
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

namespace Dio_2
{
    public class Dio_2
    {
        public static void Main()
        {
            InputPort btn1 = new InputPort(G120.P2_13, true, Port.ResistorMode.Disabled);
            InputPort btn2 = new InputPort(FEZSpiderII.Socket14.Pin3, true, Port.ResistorMode.Disabled);

            OutputPort LedSpider = new OutputPort(FEZSpiderII.DebugLed, false);
            OutputPort LedLigne0 = new OutputPort(FEZSpiderII.Socket8.Pin3, false);
            OutputPort LedLigne1 = new OutputPort(FEZSpiderII.Socket8.Pin4, false);
            OutputPort LedLigne2 = new OutputPort(FEZSpiderII.Socket8.Pin5, false);
            OutputPort LedLigne3 = new OutputPort(FEZSpiderII.Socket8.Pin6, false);
            OutputPort LedLigne4 = new OutputPort(FEZSpiderII.Socket8.Pin7, false);
            OutputPort LedLigne5 = new OutputPort(FEZSpiderII.Socket8.Pin8, false);
            OutputPort LedLigne6 = new OutputPort(FEZSpiderII.Socket8.Pin9, false);

            bool old1 = false, old2 = false, etatBtn1, etatBtn2;
            int comptage=0;
            while (true)
            {
                etatBtn1 = !btn1.Read();
                etatBtn2 = !btn2.Read();
                if (etatBtn1 == true && old1 == false)
                {
                    if (comptage < 7)
                    {
                        comptage+=1;
                    }
                }
                else if (etatBtn1 == false)
                {
                    old1 = false;
                }

                if (etatBtn2 == true && old2 == false)
                {
                    if (comptage > -1)
                    {
                        comptage-=1;
                    }
                }
                else if (etatBtn2 == false)
                {
                    old2 = false;
                }

                if (comptage == 0)
                {
                    LedLigne0.Write(true);
                    LedLigne1.Write(false);
                    LedLigne2.Write(false);
                    LedLigne3.Write(false);
                    LedLigne4.Write(false);
                    LedLigne5.Write(false);
                    LedLigne6.Write(false);
                }

                if (comptage == 1)
                {
                    LedLigne0.Write(true);
                    LedLigne1.Write(true);
                    LedLigne2.Write(false);
                    LedLigne3.Write(false);
                    LedLigne4.Write(false);
                    LedLigne5.Write(false);
                    LedLigne6.Write(false);
                }

                if (comptage == 2)
                {
                    LedLigne0.Write(true);
                    LedLigne1.Write(true);
                    LedLigne2.Write(true);
                    LedLigne3.Write(false);
                    LedLigne4.Write(false);
                    LedLigne5.Write(false);
                    LedLigne6.Write(false);
                }

                if (comptage == 3)
                {
                    LedLigne0.Write(true);
                    LedLigne1.Write(true);
                    LedLigne2.Write(true);
                    LedLigne3.Write(true);
                    LedLigne4.Write(false);
                    LedLigne5.Write(false);
                    LedLigne6.Write(false);
                }

                if (comptage == 4)
                {
                    LedLigne0.Write(true);
                    LedLigne1.Write(true);
                    LedLigne2.Write(true);
                    LedLigne3.Write(true);
                    LedLigne4.Write(true);
                    LedLigne5.Write(false);
                    LedLigne6.Write(false);
                }

                if (comptage == 5)
                {
                    LedLigne0.Write(true);
                    LedLigne1.Write(true);
                    LedLigne2.Write(true);
                    LedLigne3.Write(true);
                    LedLigne4.Write(true);
                    LedLigne5.Write(true);
                    LedLigne6.Write(false);
                }

                if (comptage == 6)
                {
                    LedLigne0.Write(true);
                    LedLigne1.Write(true);
                    LedLigne2.Write(true);
                    LedLigne3.Write(true);
                    LedLigne4.Write(true);
                    LedLigne5.Write(true);
                    LedLigne6.Write(true);
                }
            }
        }
    }
}
