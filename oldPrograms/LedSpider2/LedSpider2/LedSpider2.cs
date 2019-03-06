/* Programme  LedSpider2
 * Hbr Fabian 28.02.17 Version 1.0
 * Description
 * Champ de test pour Spider 2
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



namespace LedSpider2
{
    public class LedSpider2
    {
        public static void Main()
        {
            int intx, inty, mode=0, light;
            bool cur, old=false;
            #region MoulteDeclaration
            OutputPort LedSpider = new OutputPort(FEZSpiderII.DebugLed, false);
            OutputPort LedBtn1 = new OutputPort(FEZSpiderII.Socket12.Pin4, false);
            OutputPort LedBtn2 = new OutputPort(FEZSpiderII.Socket14.Pin4, false);

            OutputPort LedLigne0 = new OutputPort(FEZSpiderII.Socket8.Pin3, false);
            OutputPort LedLigne1 = new OutputPort(FEZSpiderII.Socket8.Pin4, false);
            OutputPort LedLigne2 = new OutputPort(FEZSpiderII.Socket8.Pin5, false);
            OutputPort LedLigne3 = new OutputPort(FEZSpiderII.Socket8.Pin6, false);
            OutputPort LedLigne4 = new OutputPort(FEZSpiderII.Socket8.Pin7, false);
            OutputPort LedLigne5 = new OutputPort(FEZSpiderII.Socket8.Pin8, false);
            OutputPort LedLigne6 = new OutputPort(FEZSpiderII.Socket8.Pin9, false);

            OutputPort LedRond1 = new OutputPort(FEZSpiderII.Socket5.Pin3, false);
            OutputPort LedRond2 = new OutputPort(FEZSpiderII.Socket5.Pin4, false);
            OutputPort LedRond3 = new OutputPort(FEZSpiderII.Socket5.Pin5, false);
            OutputPort LedRond4 = new OutputPort(FEZSpiderII.Socket5.Pin6, false);
            OutputPort LedRond5 = new OutputPort(FEZSpiderII.Socket5.Pin7, false);
            OutputPort LedRond6 = new OutputPort(FEZSpiderII.Socket5.Pin8, false);
            OutputPort LedRond7 = new OutputPort(FEZSpiderII.Socket5.Pin9, false);

            OutputPort Led7Color1 = new OutputPort(FEZSpiderII.Socket4.Pin3, false);
            OutputPort Led7Color2 = new OutputPort(FEZSpiderII.Socket4.Pin4, false);
            OutputPort Led7Color3 = new OutputPort(FEZSpiderII.Socket4.Pin5, false);

            InputPort btn1 = new InputPort(G120.P2_13, true, Port.ResistorMode.Disabled);
            InputPort btn2 = new InputPort(FEZSpiderII.Socket14.Pin3, true, Port.ResistorMode.Disabled);
            InputPort btnJoy = new InputPort(FEZSpiderII.Socket10.Pin3, true, Port.ResistorMode.Disabled);

            AnalogInput joyx = new AnalogInput(FEZSpiderII.Socket10.AnalogInput4);
            AnalogInput joyy = new AnalogInput(FEZSpiderII.Socket10.AnalogInput5);
            AnalogInput lightSense = new AnalogInput(FEZSpiderII.Socket9.AnalogInput3);
            #endregion

            while (true)
            {
                intx = joyx.ReadRaw();
                inty = joyy.ReadRaw();
                light = lightSense.ReadRaw();

                cur = btn2.Read();
                if (cur == false && old == true)
                {
                    if (mode == 0)
                    {
                        mode = 1;
                    }
                    else if (mode == 1)
                    {
                        mode = 0;
                    }
                    
                }
                if (mode == 0)
                {
                    Led7Color1.Write(true);
                    Led7Color2.Write(false);
                    Led7Color3.Write(false);


                    #region MoulteIfPlat
                
                if (intx < 585 && intx > -1)
                {
                    LedLigne0.Write(false);
                    LedLigne1.Write(false);
                    LedLigne2.Write(false);
                    LedLigne3.Write(false);
                    LedLigne4.Write(false);
                    LedLigne5.Write(false);
                    LedLigne6.Write(true);
                }
                else if (intx > 584 && intx < 1170)
                {
                    LedLigne0.Write(false);
                    LedLigne1.Write(false);
                    LedLigne2.Write(false);
                    LedLigne3.Write(false);
                    LedLigne4.Write(false);
                    LedLigne5.Write(true);
                    LedLigne6.Write(true);
                }
                else if (intx > 1169 && intx < 1755)
                {
                    LedLigne0.Write(false);
                    LedLigne1.Write(false);
                    LedLigne2.Write(false);
                    LedLigne3.Write(false);
                    LedLigne4.Write(true);
                    LedLigne5.Write(true);
                    LedLigne6.Write(true);
                }
                else if (intx > 1754 && intx < 2340)
                {
                    LedLigne0.Write(false);
                    LedLigne1.Write(false);
                    LedLigne2.Write(false);
                    LedLigne3.Write(true);
                    LedLigne4.Write(true);
                    LedLigne5.Write(true);
                    LedLigne6.Write(true);
                }
                else if (intx > 2339 && intx < 2925)
                {
                    LedLigne0.Write(false);
                    LedLigne1.Write(false);
                    LedLigne2.Write(true);
                    LedLigne3.Write(true);
                    LedLigne4.Write(true);
                    LedLigne5.Write(true);
                    LedLigne6.Write(true);
                }
                else if (intx > 2924 && intx < 3510)
                {
                    LedLigne0.Write(false);
                    LedLigne1.Write(true);
                    LedLigne2.Write(true);
                    LedLigne3.Write(true);
                    LedLigne4.Write(true);
                    LedLigne5.Write(true);
                    LedLigne6.Write(true);
                }
                else if (intx > 3509 && intx < 4096)
                {
                    LedLigne0.Write(true);
                    LedLigne1.Write(true);
                    LedLigne2.Write(true);
                    LedLigne3.Write(true);
                    LedLigne4.Write(true);
                    LedLigne5.Write(true);
                    LedLigne6.Write(true);
                } 
                #endregion

                    #region MoulteIfRond
                if (inty == 0)
                {
                    LedRond1.Write(true);
                    LedRond2.Write(false);
                    LedRond4.Write(false);
                    LedRond5.Write(false);
                    LedRond6.Write(false);
                }
                else if (inty == 4095)
                {
                    LedRond1.Write(false);
                    LedRond2.Write(false);
                    LedRond3.Write(false);
                    LedRond4.Write(true);
                    LedRond5.Write(false);
                    LedRond6.Write(false);
                }
                else
                {
                    LedRond1.Write(false);
                    LedRond2.Write(false);
                    LedRond3.Write(false);
                    LedRond4.Write(false);
                    LedRond5.Write(false);
                    LedRond6.Write(false);
                }
                if (intx == 4095)
                {
                    LedRond1.Write(false);
                    LedRond2.Write(true);
                    LedRond3.Write(true);
                    LedRond4.Write(false);
                    LedRond5.Write(false);
                    LedRond6.Write(false);
                }
                if (intx == 0)
                {
                    LedRond1.Write(false);
                    LedRond2.Write(false);
                    LedRond4.Write(false);
                    LedRond5.Write(true);
                    LedRond6.Write(true);
                } 
                #endregion
                }
                else if (mode == 1)
                {
                    Led7Color1.Write(false);
                    Led7Color2.Write(true);
                    Led7Color3.Write(false);

                    #region MoulteIfPlatV2

                    if (intx < 585 && intx > -1)
                    {
                        LedLigne0.Write(false);
                        LedLigne1.Write(false);
                        LedLigne2.Write(false);
                        LedLigne3.Write(false);
                        LedLigne4.Write(false);
                        LedLigne5.Write(false);
                        LedLigne6.Write(true);
                    }
                    else if (intx > 584 && intx < 1170)
                    {
                        LedLigne0.Write(false);
                        LedLigne1.Write(false);
                        LedLigne2.Write(false);
                        LedLigne3.Write(false);
                        LedLigne4.Write(false);
                        LedLigne5.Write(true);
                        LedLigne6.Write(false);
                    }
                    else if (intx > 1169 && intx < 1755)
                    {
                        LedLigne0.Write(false);
                        LedLigne1.Write(false);
                        LedLigne2.Write(false);
                        LedLigne3.Write(false);
                        LedLigne4.Write(true);
                        LedLigne5.Write(false);
                        LedLigne6.Write(false);
                    }
                    else if (intx > 1754 && intx < 2340)
                    {
                        LedLigne0.Write(false);
                        LedLigne1.Write(false);
                        LedLigne2.Write(false);
                        LedLigne3.Write(true);
                        LedLigne4.Write(false);
                        LedLigne5.Write(false);
                        LedLigne6.Write(false);
                    }
                    else if (intx > 2339 && intx < 2925)
                    {
                        LedLigne0.Write(false);
                        LedLigne1.Write(false);
                        LedLigne2.Write(true);
                        LedLigne3.Write(false);
                        LedLigne4.Write(false);
                        LedLigne5.Write(false);
                        LedLigne6.Write(false);
                    }
                    else if (intx > 2924 && intx < 3510)
                    {
                        LedLigne0.Write(false);
                        LedLigne1.Write(true);
                        LedLigne2.Write(false);
                        LedLigne3.Write(false);
                        LedLigne4.Write(false);
                        LedLigne5.Write(false);
                        LedLigne6.Write(false);
                    }
                    else if (intx > 3509 && intx < 4096)
                    {
                        LedLigne0.Write(true);
                        LedLigne1.Write(false);
                        LedLigne2.Write(false);
                        LedLigne3.Write(false);
                        LedLigne4.Write(false);
                        LedLigne5.Write(false);
                        LedLigne6.Write(false);
                    }
                    #endregion

                    #region MoulteLumiere
                    if (light == 0)
                    {
                        LedRond1.Write(false);
                        LedRond2.Write(false);
                        LedRond3.Write(false);
                        LedRond4.Write(false);
                        LedRond5.Write(false);
                        LedRond6.Write(false);
                        LedRond7.Write(true);
                    }
                    if (light < 682 && light > 0)
                    {
                        LedRond1.Write(true);
                        LedRond2.Write(false);
                        LedRond3.Write(false);
                        LedRond4.Write(false);
                        LedRond5.Write(false);
                        LedRond6.Write(false);
                        LedRond7.Write(false);
                    }
                    if (light < 1365 && light > 681)
                    {
                        LedRond1.Write(true);
                        LedRond2.Write(true);
                        LedRond3.Write(false);
                        LedRond4.Write(false);
                        LedRond5.Write(false);
                        LedRond6.Write(false);
                        LedRond7.Write(false);
                    }
                    if (light < 2047 && light > 1364)
                    {
                        LedRond1.Write(true);
                        LedRond2.Write(true);
                        LedRond3.Write(true);
                        LedRond4.Write(false);
                        LedRond5.Write(false);
                        LedRond6.Write(false);
                        LedRond7.Write(false);
                    }
                    if (light < 2730 && light > 2046)
                    {
                        LedRond1.Write(true);
                        LedRond2.Write(true);
                        LedRond3.Write(true);
                        LedRond4.Write(true);
                        LedRond5.Write(false);
                        LedRond6.Write(false);
                        LedRond7.Write(false);
                    }
                    if (light < 3412 && light > 2729)
                    {
                        LedRond1.Write(true);
                        LedRond2.Write(true);
                        LedRond3.Write(true);
                        LedRond4.Write(true);
                        LedRond5.Write(true);
                        LedRond6.Write(false);
                        LedRond7.Write(false);
                    }
                    if (light < 4096 && light > 3411)
                    {
                        LedRond1.Write(true);
                        LedRond2.Write(true);
                        LedRond3.Write(true);
                        LedRond4.Write(true);
                        LedRond5.Write(true);
                        LedRond6.Write(true);
                        LedRond7.Write(false);
                    } 
                    #endregion
                }
                #region MoulteBoutton
                if (!btnJoy.Read() == true)
                {
                    LedRond7.Write(true);
                }
                else
                {
                    LedRond7.Write(false);
                }
                if (!btn1.Read() == true)
                {
                    LedBtn1.Write(true);
                    LedSpider.Write(true);
                    Thread.Sleep(125);
                    LedSpider.Write(false);
                    Thread.Sleep(125);
                }
                else
                {
                    LedBtn1.Write(false);
                }
                if (!btn2.Read() == true)
                {
                    LedBtn2.Write(true);
                }
                else
                {
                    LedBtn2.Write(false);
                } 
                #endregion
                old = cur;

            }
        }
    }
}
