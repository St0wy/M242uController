/* Programme  serrure
 * Hbr Fabian 08.05.17 Version 1.0
 * Description
 * Faire une serrure
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

namespace serrure
{
    public class serrure
    {
        public static void Main()
        {
            bool etatBtn1, etatBtn2;
            int etat=0, timer=0;
            InputPort btn1 = new InputPort(G120.P2_13, true, Port.ResistorMode.Disabled);
            InputPort btn2 = new InputPort(FEZSpiderII.Socket14.Pin3, true, Port.ResistorMode.Disabled);

            OutputPort LedRond1 = new OutputPort(FEZSpiderII.Socket5.Pin3, false);
            OutputPort LedRond2 = new OutputPort(FEZSpiderII.Socket5.Pin4, false);
            OutputPort LedRond3 = new OutputPort(FEZSpiderII.Socket5.Pin5, false);
            OutputPort LedRond4 = new OutputPort(FEZSpiderII.Socket5.Pin6, false);
            OutputPort LedRond5 = new OutputPort(FEZSpiderII.Socket5.Pin7, false);
            OutputPort LedRond6 = new OutputPort(FEZSpiderII.Socket5.Pin8, false);
            OutputPort LedRond7 = new OutputPort(FEZSpiderII.Socket5.Pin9, false);

            while (true)
            {
                etatBtn1 = !btn1.Read();
                etatBtn2 = !btn2.Read();

                if (etatBtn1 == true && etatBtn2 == false && etat == 0) 
                { 
                    etat = 1;

                    LedRond1.Write(true);
                    LedRond2.Write(false);
                    LedRond3.Write(false);
                    LedRond4.Write(false);
                    LedRond5.Write(false);
                    LedRond6.Write(false);
                    LedRond7.Write(false);
                }
                else if (etatBtn2 == true && etat == 0)
                { etat = 7; }

                switch (etat)
                {
                    case 0:
                        //etat innitial
                        LedRond1.Write(false);
                        LedRond2.Write(false);
                        LedRond3.Write(false);
                        LedRond4.Write(false);
                        LedRond5.Write(false);
                        LedRond6.Write(false);
                        LedRond7.Write(false);
                        break;
                    case 1:
                        if (etatBtn1 == false && etatBtn2 == false) 
                        { 
                            etat = 2;

                            LedRond1.Write(true);
                            LedRond2.Write(true);
                            LedRond3.Write(false);
                            LedRond4.Write(false);
                            LedRond5.Write(false);
                            LedRond6.Write(false);
                            LedRond7.Write(false);
                        }
                        else if(etatBtn2 == true)
                        { etat = 7; }
                        break;

                    case 2:
                        if (etatBtn1 == true && etatBtn2 == false) 
                        { 
                            etat = 3;

                            LedRond1.Write(true);
                            LedRond2.Write(true);
                            LedRond3.Write(true);
                            LedRond4.Write(false);
                            LedRond5.Write(false);
                            LedRond6.Write(false);
                            LedRond7.Write(false);
                        }
                        else if (etatBtn2 == true) 
                        { etat = 7; }
                        break;

                    case 3:
                        if (etatBtn1 == true && etatBtn2 == true) 
                        { 
                            etat = 4;

                            LedRond1.Write(true);
                            LedRond2.Write(true);
                            LedRond3.Write(true);
                            LedRond4.Write(true);
                            LedRond5.Write(false);
                            LedRond6.Write(false);
                            LedRond7.Write(false);
                        }
                        else if (etatBtn1 == false)
                        { etat = 7; }
                        break;

                    case 4:
                        if (etatBtn1 == true && etatBtn2 == false) 
                        { 
                            etat = 5;

                            LedRond1.Write(true);
                            LedRond2.Write(true);
                            LedRond3.Write(true);
                            LedRond4.Write(true);
                            LedRond5.Write(true);
                            LedRond6.Write(false);
                            LedRond7.Write(false);
                        }
                        else if (etatBtn1 == false) 
                        { etat = 7; }
                        break;

                    case 5:
                        if (etatBtn1 == false && etatBtn2 == false) 
                        { 
                            etat = 6;

                            LedRond1.Write(true);
                            LedRond2.Write(true);
                            LedRond3.Write(true);
                            LedRond4.Write(true);
                            LedRond5.Write(true);
                            LedRond6.Write(true);
                            LedRond7.Write(false);
                        }
                        else if (etatBtn2 == true)  
                        { etat = 7; }
                        break;

                    case 6:
                        //victoire
                        LedRond1.Write(true);
                        LedRond2.Write(false);
                        LedRond3.Write(false);
                        LedRond4.Write(false);
                        LedRond5.Write(false);
                        LedRond6.Write(false);
                        LedRond7.Write(false);
                        Thread.Sleep(250);
                        LedRond1.Write(true);
                        LedRond2.Write(true);
                        LedRond3.Write(false);
                        LedRond4.Write(false);
                        LedRond5.Write(false);
                        LedRond6.Write(false);
                        LedRond7.Write(false);
                        Thread.Sleep(250);
                        LedRond1.Write(true);
                        LedRond2.Write(true);
                        LedRond3.Write(true);
                        LedRond4.Write(false);
                        LedRond5.Write(false);
                        LedRond6.Write(false);
                        LedRond7.Write(false);
                        Thread.Sleep(250);
                        LedRond1.Write(true);
                        LedRond2.Write(true);
                        LedRond3.Write(true);
                        LedRond4.Write(true);
                        LedRond5.Write(false);
                        LedRond6.Write(false);
                        LedRond7.Write(false);
                        Thread.Sleep(250);
                        LedRond1.Write(true);
                        LedRond2.Write(true);
                        LedRond3.Write(true);
                        LedRond4.Write(true);
                        LedRond5.Write(true);
                        LedRond6.Write(false);
                        LedRond7.Write(false);
                        Thread.Sleep(250);
                        LedRond1.Write(true);
                        LedRond2.Write(true);
                        LedRond3.Write(true);
                        LedRond4.Write(true);
                        LedRond5.Write(true);
                        LedRond6.Write(true);
                        LedRond7.Write(false);
                        Thread.Sleep(250);
                        LedRond1.Write(true);
                        LedRond2.Write(true);
                        LedRond3.Write(true);
                        LedRond4.Write(true);
                        LedRond5.Write(true);
                        LedRond6.Write(true);
                        LedRond7.Write(true);
                        Thread.Sleep(250);
                        etat = 0;
                        break;

                    case 7:
                        //Echec
                        timer += 1000;
                        LedRond1.Write(false);
                        LedRond2.Write(false);
                        LedRond3.Write(false);
                        LedRond4.Write(false);
                        LedRond5.Write(false);
                        LedRond6.Write(false);
                        LedRond7.Write(false);
                        Thread.Sleep(timer / 4);
                        LedRond1.Write(true);
                        LedRond2.Write(true);
                        LedRond3.Write(true);
                        LedRond4.Write(true);
                        LedRond5.Write(true);
                        LedRond6.Write(true);
                        LedRond7.Write(true);
                        Thread.Sleep(timer / 4);
                        LedRond1.Write(false);
                        LedRond2.Write(false);
                        LedRond3.Write(false);
                        LedRond4.Write(false);
                        LedRond5.Write(false);
                        LedRond6.Write(false);
                        LedRond7.Write(false);
                        Thread.Sleep(timer / 4);
                        LedRond1.Write(true);
                        LedRond2.Write(true);
                        LedRond3.Write(true);
                        LedRond4.Write(true);
                        LedRond5.Write(true);
                        LedRond6.Write(true);
                        LedRond7.Write(true);
                        Thread.Sleep(timer / 4);
                        etat = 0;
                        break;

                }

            }
        }
    }
}
