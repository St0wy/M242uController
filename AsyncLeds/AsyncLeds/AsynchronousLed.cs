// Par défaut
using System;
using Microsoft.SPOT;
// A ajouter
using Microsoft.SPOT.Hardware;      // Pour OutputPort
using GHI.Pins;					    // Pour GHI.Pins
using System.Threading;             // Pour Thread.Sleep(1000)
using GHICard = GHI.Pins.FEZSpiderII; // Défini la carte principale utilisée

namespace AsyncLeds
{
    class AsynchronousLed
    {
        //Constants
        const int ONE_SECOND_IN_MS = 1000;
        const int STEP_DURATION = 20;

        //Fileds
        int counter;
        double frenquency;
        int cyclicalReport;
        OutputPort led;

        //Properties
        public int Counter
        {
            get { return counter; }
            set { counter = value; }
        }
        public double Frenquency
        {
            get { return frenquency; }
            set { frenquency = value; }
        }
        public OutputPort Led
        {
            get { return led; }
            set { led = value; }
        }
        public int CyclicalReport
        {
            get { return cyclicalReport; }
            set { cyclicalReport = value; }
        }

        //Constructors
        public AsynchronousLed(): this(0, new OutputPort(new Cpu.Pin(), false), 0)
        {

        }

        public AsynchronousLed(double frenquency, OutputPort led, int cyclicalReport)
        {
            Frenquency = frenquency;
            Led = led;
            CyclicalReport = cyclicalReport;
        }

        //Methods
        public double GetPeriod()
        {
            return ONE_SECOND_IN_MS / Frenquency;
        }

        public double GetTUp()
        {
            return GetPeriod() * CyclicalReport / 100;
        }

        public void Blink()
        {
            if (Counter <= GetTUp())
            {
                Led.Write(true);
            }
            else if (Counter <= GetPeriod())
            {
                Led.Write(false);
            }
            else
            {
                Counter = 0;
            }

            Counter += STEP_DURATION;
        }
    }
}
