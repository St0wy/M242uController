/* Programme  MFChariot
 * Hbr Fabian 06.02.2019 Version 1.0
 * Description
 * Machine d'etat chariot
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

namespace MFChariot
{
    public class MFChariot
    {
        public static void Main()
        {
            #region constants
            const int STATE_STOP = 0;
            const int STATE_GO_LEFT = 1;
            const int STATE_LOADING = 2;
            const int STATE_GO_RIGHT = 3;
            const int STATE_UNLOADING = 4;
            const int STATE_GO_BACK_STOP = 5;
            const long TIME_1_MS_IN_TICKS = 10000;
            const long LOADING_TIME_IN_MS = 3000;
            const long UNLOADING_TIME_IN_MS = 1000;
            const long GO_BACK_STOP_TIME_IN_MS = 2000;
            #endregion

            LedStrip ledStrip = new LedStrip();
            Button btnStart = new Button(true, new InputPort(GHICard.Socket10.Pin3, false, Port.ResistorMode.Disabled), 100);
            Button btnEndLeft = new Button(true, new InputPort(GHICard.Socket12.Pin3, false, Port.ResistorMode.Disabled), 100);
            Button btnEndRight = new Button(true, new InputPort(GHICard.Socket14.Pin3, false, Port.ResistorMode.Disabled), 100);
            int state = 0;
            bool leftMotor = false;
            bool rightMotor = false;
            long checkTime = 0;
            long elapsedTime = 0;

            //Main Loop
            while (true)
            {
                //State management
                switch (state)
                {
                    case STATE_STOP:
                        leftMotor = false;
                        rightMotor = false;

                        if (btnStart.isDoublePressed())
                        {
                            state = STATE_GO_LEFT;
                        }
                        break;
                    case STATE_GO_LEFT:
                        leftMotor = true;
                        rightMotor = false;

                        if (btnEndLeft.isPressed())
                        {
                            state = STATE_LOADING;
                            checkTime = Utility.GetMachineTime().Ticks;
                        }
                        break;
                    case STATE_LOADING:
                        leftMotor = false;
                        rightMotor = false;
                        
                        elapsedTime = Utility.GetMachineTime().Ticks - checkTime;
                        if (elapsedTime >= LOADING_TIME_IN_MS * TIME_1_MS_IN_TICKS)
                        {
                            state = STATE_GO_RIGHT;
                        }
                        break;
                    case STATE_GO_RIGHT:
                        leftMotor = false;
                        rightMotor = true;

                        if (btnEndRight.isPressed())
                        {
                            state = STATE_UNLOADING;
                            checkTime = Utility.GetMachineTime().Ticks;
                        }
                        break;
                    case STATE_UNLOADING:
                        leftMotor = false;
                        rightMotor = false;

                        elapsedTime = Utility.GetMachineTime().Ticks - checkTime;
                        if (elapsedTime >= UNLOADING_TIME_IN_MS * TIME_1_MS_IN_TICKS)
                        {
                            state = STATE_GO_BACK_STOP;
                        }
                        break;
                    case STATE_GO_BACK_STOP:
                        leftMotor = true;
                        rightMotor = false;

                        elapsedTime = Utility.GetMachineTime().Ticks - checkTime;
                        if (elapsedTime >= GO_BACK_STOP_TIME_IN_MS * TIME_1_MS_IN_TICKS)
                        {
                            state = STATE_STOP;
                        }
                        break;
                    default:
                        state = STATE_STOP;
                        break;
                }

                //View management
                if (leftMotor)
                {
                    ledStrip.Led5.Write(true);
                    ledStrip.Led6.Write(true);
                }
                else
                {
                    ledStrip.Led5.Write(false);
                    ledStrip.Led6.Write(false);
                }

                if (rightMotor)
                {
                    ledStrip.Led0.Write(true);
                    ledStrip.Led1.Write(true);
                }
                else
                {
                    ledStrip.Led0.Write(false);
                    ledStrip.Led1.Write(false);
                }

                if (!leftMotor && !rightMotor)
                {
                    ledStrip.Led2.Write(true);
                    ledStrip.Led3.Write(true);
                    ledStrip.Led4.Write(true);
                }
                else
                {
                    ledStrip.Led2.Write(false);
                    ledStrip.Led3.Write(false);
                    ledStrip.Led4.Write(false);
                }
            }
        }
    }
}
