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
    class LedStrip
    {
        //Fileds
        OutputPort led0;
        OutputPort led1;
        OutputPort led2;
        OutputPort led3;
        OutputPort led4;
        OutputPort led5;
        OutputPort led6;

        //Properties
        public OutputPort Led0
        {
            get { return led0; }
            set { led0 = value; }
        }
        public OutputPort Led1
        {
            get { return led1; }
            set { led1 = value; }
        }
        public OutputPort Led2
        {
            get { return led2; }
            set { led2 = value; }
        }
        public OutputPort Led3
        {
            get { return led3; }
            set { led3 = value; }
        }
        public OutputPort Led4
        {
            get { return led4; }
            set { led4 = value; }
        }
        public OutputPort Led5
        {
            get { return led5; }
            set { led5 = value; }
        }
        public OutputPort Led6
        {
            get { return led6; }
            set { led6 = value; }
        }

        //Constructors
        public LedStrip()
        {
            led0 = new OutputPort(GHICard.Socket9.Pin3, false);
            led1 = new OutputPort(GHICard.Socket9.Pin4, false);
            led2 = new OutputPort(GHICard.Socket9.Pin5, false);
            led3 = new OutputPort(GHICard.Socket9.Pin6, false);
            led4 = new OutputPort(GHICard.Socket9.Pin7, false);
            led5 = new OutputPort(GHICard.Socket9.Pin8, false);
            led6 = new OutputPort(GHICard.Socket9.Pin9, false);
        }
    }
}
