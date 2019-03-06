/* Programme  MFECGSignal
 * Hbr Fabian 13.02.2019 Version 1.0
 * Description
 * Simulator of an ECG signal
 */

/* Description matériel et librairies:
 * 
 * Version: GHI SDK:2016.1.2.0; NETMF:4.3.8.1; Gadgeteer core:2.44.1100.0
 * Carte mère: Spider 2	Version firmware: 4.3.8.1 	Version Loader:4.3.8.1  (MAJ: 4381)
 * Module utilisé		Connecteur utilisé	Fonction
 * Breakout 1.0         9
 * Joystick 1.2         10
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
using System.Threading;             // Pour Thread.Sleep()
using GHICard = GHI.Pins.FEZSpiderII; // Défini la carte principale utilisée

namespace MFECGSignal
{
    public class ECGSignal
    {
        public static void Main()
        {
            //one heartbeat 272.72ms
            int[] ecgValues = new int[]{238, 238, 238, 238, 238, 238, 238, 248, 258, 269, 280, 294, 308, 313, 318, 313, 308, 301, 294, 287, 280, 273, 266, 259,
                                              252, 245, 238, 238, 238, 238, 238, 238, 238, 238, 238, 238, 238, 227, 217, 199, 182, 255, 329, 444, 560, 672, 784, 871,
                                              959, 773, 588, 301, 014, 059, 105, 161, 217, 227, 238, 238, 238, 238, 238, 238, 238, 238, 238, 238, 238, 238, 238, 238,
                                              238, 238, 238, 238, 238, 238, 238, 238, 238, 238, 238, 238, 238, 248, 259, 266, 273, 280, 287, 294, 301, 308, 315, 322,
                                              329, 336, 343, 346, 350, 353, 357, 360, 364, 360, 357, 353, 350, 346, 343, 336, 329, 322, 315, 304, 294, 280, 266, 252,
                                              238, 238, 238, 238, 238, 238, 238, 238, 238, 238, 238, 248, 259, 262, 266, 259, 252, 245, 238, 238, 238, 238, 238, 238, 238};

            const long TIME_MS_TO_TICK = 10000;
            const int STATE_DYNAMIC = 0;
            const int STATE_STATIC = 1;
            const long LENGTH_ONE_HEARTBEAT_IN_TICK = 2727272;

            AnalogOutput analogOut = new AnalogOutput(GHICard.Socket9.AnalogOutput);
            int state = STATE_STATIC;
            long timeToWait = 1000;  //in MS
            int index = 0;
            long ticksAtLastChange = 0;
            long ticksAtLastWait = 0;
            bool isBeating = true;

            //Main Loop
            while (true)
            {
                long ticks = Utility.GetMachineTime().Ticks;

                switch (state)
                {
                    case STATE_DYNAMIC:
                        
                        break;
                    case STATE_STATIC:
                        if (isBeating)
                        {
                            analogOut.WriteRaw(ecgValues[index]);
                        }
                        else
                        {

                        }
                        
                        break;
                    default:
                        break;
                }
                if (isBeating)
                {
                    long elapsedTimeHeartbeat = ticks - ticksAtLastChange;
                    if (elapsedTimeHeartbeat >= LENGTH_ONE_HEARTBEAT_IN_TICK / ecgValues.Length )
                    {
                        if (index == ecgValues.Length - 1)
                        {
                            index = 0;
                            isBeating = false;
                            ticksAtLastWait = ticks;
                        }
                        else
                        {
                            index++;
                        }

                        ticksAtLastChange = ticks;
                    }
                }
                else
                {
                    long elapsedTimeWait = ticks - ticksAtLastWait;
                    if (elapsedTimeWait >= timeToWait * TIME_MS_TO_TICK)
                    {
                        isBeating = true;
                        ticksAtLastChange = ticks;
                    }
                }
            }//End while
        }
    }
}
