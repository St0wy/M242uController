/**
 * Fabian Huber
 * Classe bouton pour carte FEZ Spider
 * 06.03.2019
 * v1.0
 */
// Par défaut
using System;
using Microsoft.SPOT;
// A ajouter
using Microsoft.SPOT.Hardware;      // Pour OutputPort
using GHI.Pins;					    // Pour GHI.Pins
using System.Threading;             // Pour Thread.Sleep(1000)
using GHICard = GHI.Pins.FEZSpiderII; // Défini la carte principale utilisée

namespace MFTimerK2000
{
    class Button
    {
        const long TIME_MS_TO_TICK = 10000;
        //Fields
        bool oldIsButtonPressed;
        bool edgeDetection;
        InputPort button;
        long timeDoublePressInMs;
        long timerCheck;

        //Properties
        public bool EdgeDetection
        {
            get { return edgeDetection; }
            set { edgeDetection = value; }
        }

        //Constructors
        public Button(bool edgeDetection, InputPort button, int timerDoublePressInMs)
        {
            oldIsButtonPressed = false;
            EdgeDetection = edgeDetection;
            this.button = button;
            this.timeDoublePressInMs = timerDoublePressInMs;
            timerCheck = 0;
        }

        //Methods
        public bool isPressed()
        {
            bool isButtonPressed = !button.Read();
            if (EdgeDetection)
            {
                if (!oldIsButtonPressed && isButtonPressed)
                {
                    isButtonPressed = true;
                }
            }

            oldIsButtonPressed = isButtonPressed;
            return isButtonPressed;
        }

        public bool isDoublePressed()
        {
            bool isDoublePressed = false;
            if (isPressed())
            {
                long ticks = Utility.GetMachineTime().Ticks;
                if (timerCheck == 0)
                {
                    timerCheck = ticks;
                }
                else 
                {
                    long elapsedTime = ticks - timerCheck;
                    if (elapsedTime <= timeDoublePressInMs * TIME_MS_TO_TICK)
                    {
                        isDoublePressed = true;
                    }
                    else
                    {
                        timerCheck = 0;
                    }
                }
            }
            
            return isDoublePressed;
        }
    }
}
