/* Programme  SlindingLed
 * Hbr Fabian 02.05.17 Version 1.0
 * Description
 * se faire deplacer une led sur le led strip
 */

/* Description matériel et librairies:
 * 
 * Version: GHI SDK:2016.1.2.0; NETMF:4.3.8.1; Gadgeteer core:2.44.1100.0
 * Carte mère: Spider 2	Version firmware: 4.3.8.1 	Version Loader:4.3.8.1  (MAJ: 4381)
 * Module utilisé		Connecteur utilisé	Fonction
 *	usbClientDP     1                   Connexion au PC
 *  Button          2                   Start/Stop
 *  Joystick        1                   "Mooving" LED in the LED strip
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

namespace SlidingLed
{
    public class SlidingLed
    {
        enum ETATS { restart, descend, monte , nul};
        public static void Main()
        {
            InputPort btn1 = new InputPort(G120.P2_13, true, Port.ResistorMode.Disabled);
            InputPort btn2 = new InputPort(FEZSpiderII.Socket14.Pin3, true, Port.ResistorMode.Disabled);
            InputPort btnJoy = new InputPort(FEZSpiderII.Socket10.Pin3, true, Port.ResistorMode.Disabled);

            OutputPort LedLigne0 = new OutputPort(FEZSpiderII.Socket8.Pin3, false);
            OutputPort LedLigne1 = new OutputPort(FEZSpiderII.Socket8.Pin4, false);
            OutputPort LedLigne2 = new OutputPort(FEZSpiderII.Socket8.Pin5, false);
            OutputPort LedLigne3 = new OutputPort(FEZSpiderII.Socket8.Pin6, false);
            OutputPort LedLigne4 = new OutputPort(FEZSpiderII.Socket8.Pin7, false);
            OutputPort LedLigne5 = new OutputPort(FEZSpiderII.Socket8.Pin8, false);
            OutputPort LedLigne6 = new OutputPort(FEZSpiderII.Socket8.Pin9, false);

            bool cur1, old1 = false, cur2, old2 = false, curJoy, oldJoy = false;
            ETATS EtatMachine = ETATS.nul;
            int comptage = 4;
            while (true)
            {
                #region def etat
                cur1 = !btn1.Read();
                cur2 = !btn2.Read();
                curJoy = !btnJoy.Read();
                if (cur1 == true && old1 == false)
                {
                    EtatMachine = ETATS.restart;
                }
                if (cur2 == true && old2 == false)
                {
                    EtatMachine = ETATS.descend;
                }
                if (curJoy == true && oldJoy == false)
                {
                    EtatMachine = ETATS.monte;
                } 
                #endregion

                #region action etat

                switch (EtatMachine)
                {
                    case ETATS.descend:
                        if (comptage == 7)
                        {
                            comptage = 0;
                        }
                        else
                        {
                            comptage++;
                        }
                        break;
                    case ETATS.monte:
                        if (comptage == 0)
                        {
                            comptage = 7;
                        }
                        else
                        {
                            comptage--;
                        }
                        break;
                    case ETATS.restart:
                        if (comptage == 7)
                        {
                            comptage = 0;
                        }
                        else
                        {
                            comptage++;
                        }
                        if (comptage == 4)
                        {
                            EtatMachine = ETATS.nul;
                        }
                        break;
                }
                #endregion

                #region Affichage
                switch (comptage)
                {
                    case 0:
                        LedLigne0.Write(false);
                        LedLigne1.Write(false);
                        LedLigne2.Write(false);
                        LedLigne3.Write(false);
                        LedLigne4.Write(false);
                        LedLigne5.Write(false);
                        LedLigne6.Write(false);
                        break;

                    case 1:
                        LedLigne0.Write(false);
                        LedLigne1.Write(false);
                        LedLigne2.Write(false);
                        LedLigne3.Write(false);
                        LedLigne4.Write(false);
                        LedLigne5.Write(false);
                        LedLigne6.Write(true);
                        break;

                    case 2:
                        LedLigne0.Write(false);
                        LedLigne1.Write(false);
                        LedLigne2.Write(false);
                        LedLigne3.Write(false);
                        LedLigne4.Write(false);
                        LedLigne5.Write(true);
                        LedLigne6.Write(false);
                        break;

                    case 3:
                        LedLigne0.Write(false);
                        LedLigne1.Write(false);
                        LedLigne2.Write(false);
                        LedLigne3.Write(false);
                        LedLigne4.Write(true);
                        LedLigne5.Write(false);
                        LedLigne6.Write(false);
                        break;

                    case 4:
                        LedLigne0.Write(false);
                        LedLigne1.Write(false);
                        LedLigne2.Write(false);
                        LedLigne3.Write(true);
                        LedLigne4.Write(false);
                        LedLigne5.Write(false);
                        LedLigne6.Write(false);
                        break;

                    case 5:
                        LedLigne0.Write(false);
                        LedLigne1.Write(false);
                        LedLigne2.Write(true);
                        LedLigne3.Write(false);
                        LedLigne4.Write(false);
                        LedLigne5.Write(false);
                        LedLigne6.Write(false);
                        break;

                    case 6:
                        LedLigne0.Write(false);
                        LedLigne1.Write(true);
                        LedLigne2.Write(false);
                        LedLigne3.Write(false);
                        LedLigne4.Write(false);
                        LedLigne5.Write(false);
                        LedLigne6.Write(false);
                        break;

                    case 7:
                        LedLigne0.Write(true);
                        LedLigne1.Write(false);
                        LedLigne2.Write(false);
                        LedLigne3.Write(false);
                        LedLigne4.Write(false);
                        LedLigne5.Write(false);
                        LedLigne6.Write(false);
                        break;
                } 
                #endregion

                old1 = cur1;
                old2 = cur2;
                oldJoy = curJoy;
                Thread.Sleep(500);
            }
        }
    }
}
