/**
 * Fabian Huber
 * Classe bouton pour carte FEZ Spider
 * 27.03.2019
 * v2.0
 */
// Par défaut
using System;
using Microsoft.SPOT;
// A ajouter
using Microsoft.SPOT.Hardware;      // Pour OutputPort
using GHI.Pins;					    // Pour GHI.Pins
using System.Threading;             // Pour Thread.Sleep(1000)
using GHICard = GHI.Pins.FEZSpiderII; // Défini la carte principale utilisée

namespace YOURNAMESPACE
{
    class Button
    {
        //Constants
        const long TIME_MS_TO_TICK = 10000;
        const int DEFAULT_TIMER_DOUBLE_PRESS = 250;
        const bool DEFAULT_EDGE_DETECTION = true;

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
        public Button(InputPort button, bool edgeDetection, int timerDoublePressInMs)
        {
            oldIsButtonPressed = false;
            EdgeDetection = edgeDetection;
            this.button = button;
            this.timeDoublePressInMs = timerDoublePressInMs;
            timerCheck = 0;
        }

        public Button(InputPort button, bool edgeDetection) : this(button, edgeDetection, DEFAULT_TIMER_DOUBLE_PRESS)
        {

        }

        public Button(InputPort button) : this(button, DEFAULT_EDGE_DETECTION)
        {

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
