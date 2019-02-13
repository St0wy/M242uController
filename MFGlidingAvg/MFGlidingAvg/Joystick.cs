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

namespace MFGlidingAvg
{
    class Joystick
    {
        //Fields
        private AnalogInput x;
        private AnalogInput y;

        //Properties
        public AnalogInput X
        {
            get { return x; }
            set { x = value; }
        }

        public AnalogInput Y
        {
            get { return y; }
            set { y = value; }
        }

        //Constructors
        public Joystick(AnalogInput x, AnalogInput y)
        {
            this.X = x;
            this.Y = y;
        }

        public Joystick() : this(new AnalogInput(GHICard.Socket10.AnalogInput4), new AnalogInput(GHICard.Socket10.AnalogInput5))
        {

        }

        //Methods

    }
}
