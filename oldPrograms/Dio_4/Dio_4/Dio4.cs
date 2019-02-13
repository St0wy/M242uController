/* Programme  Dio_4
 * Hbr Fabian 24.04.17 Version 1.0
 * Description
 * Comptage du nombre de clique sur un boutton
 */

/* Description matériel et librairies:
 * 
 * Version: GHI SDK:2016.1.2.0; NETMF:4.3.8.1; Gadgeteer core:2.44.1100.0
 * Carte mère: Spider 2	Version firmware: 4.3.8.1 	Version Loader:4.3.8.1  (MAJ: 4381)
 * Module utilisé		Connecteur utilisé	Fonction
 *	usbClientDP     1                   Connexion au PC
 *  Button          1                   +1
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

namespace Dio_4
{
    public class Dio4
    {
        public static void Main()
        {
            InputPort btn1 = new InputPort(G120.P2_13, true, Port.ResistorMode.Disabled);
            InputPort btn2 = new InputPort(FEZSpiderII.Socket14.Pin3, true, Port.ResistorMode.Disabled);
            OutputPort LedLigne0 = new OutputPort(FEZSpiderII.Socket8.Pin3, false);
            OutputPort LedLigne1 = new OutputPort(FEZSpiderII.Socket8.Pin4, false);
            OutputPort LedLigne2 = new OutputPort(FEZSpiderII.Socket8.Pin5, false);
            OutputPort LedLigne3 = new OutputPort(FEZSpiderII.Socket8.Pin6, false);
            OutputPort LedLigne4 = new OutputPort(FEZSpiderII.Socket8.Pin7, false);
            OutputPort LedLigne5 = new OutputPort(FEZSpiderII.Socket8.Pin8, false);
            OutputPort LedLigne6 = new OutputPort(FEZSpiderII.Socket8.Pin9, false);

            bool cur1, old1 = false, cur2, old2 = false;
            int comptage = 0;
            while (true)
            {
                cur1 = !btn1.Read();
                cur2 = !btn2.Read();
                if (cur1 == true && old1 == false)
                {
                    comptage++;
                }
                if (cur2 == true && old2 == false)
                {
                    comptage--;
                }
                if (comptage == 0)
                {
                    LedLigne0.Write(false);
                    LedLigne1.Write(false);
                    LedLigne2.Write(false);
                    LedLigne3.Write(false);
                    LedLigne4.Write(false);
                    LedLigne5.Write(false);
                    LedLigne6.Write(false);
                }
                if (comptage == 1)
                {
                    LedLigne0.Write(false);
                    LedLigne1.Write(false);
                    LedLigne2.Write(false);
                    LedLigne3.Write(false);
                    LedLigne4.Write(false);
                    LedLigne5.Write(false);
                    LedLigne6.Write(true);
                }
                if (comptage == 2)
                {
                    LedLigne0.Write(false);
                    LedLigne1.Write(false);
                    LedLigne2.Write(false);
                    LedLigne3.Write(false);
                    LedLigne4.Write(false);
                    LedLigne5.Write(true);
                    LedLigne6.Write(true);
                }
                if (comptage == 3)
                {
                    LedLigne0.Write(false);
                    LedLigne1.Write(false);
                    LedLigne2.Write(false);
                    LedLigne3.Write(false);
                    LedLigne4.Write(true);
                    LedLigne5.Write(true);
                    LedLigne6.Write(true);
                }
                if (comptage == 4)
                {
                    LedLigne0.Write(false);
                    LedLigne1.Write(false);
                    LedLigne2.Write(false);
                    LedLigne3.Write(true);
                    LedLigne4.Write(true);
                    LedLigne5.Write(true);
                    LedLigne6.Write(true);
                }
                if (comptage == 5)
                {
                    LedLigne0.Write(false);
                    LedLigne1.Write(false);
                    LedLigne2.Write(true);
                    LedLigne3.Write(true);
                    LedLigne4.Write(true);
                    LedLigne5.Write(true);
                    LedLigne6.Write(true);
                }
                if (comptage == 6)
                {
                    LedLigne0.Write(false);
                    LedLigne1.Write(true);
                    LedLigne2.Write(true);
                    LedLigne3.Write(true);
                    LedLigne4.Write(true);
                    LedLigne5.Write(true);
                    LedLigne6.Write(true);
                }
                if (comptage == 7)
                {
                    LedLigne0.Write(true);
                    LedLigne1.Write(true);
                    LedLigne2.Write(true);
                    LedLigne3.Write(true);
                    LedLigne4.Write(true);
                    LedLigne5.Write(true);
                    LedLigne6.Write(true);
                }

                old1 = cur1;
                old2 = cur2;
            }
        }
    }
}
