/**
 * Fabian Huber
 * Classe LCD pour carte FEZ Spider
 * 27.03.2019
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

namespace MFRegMemRTC_01
{
    class LCD
    {
        //Constants
        const int COMMAND_SET_CURSOR = 0x80;
        const int COMMAND_WRITE_IN_8_BIT = 0x33;
        const int COMMAND_WRITE_IN_4_BIT = 0x32;
        const int COMMAND_DISPLAY_ON = 0x0C;
        const int COMMAND_EMPTY_DISPLAY = 0x01;
        const int ADRESSE_START_SECOND_LINE = 0x40;

        //Fields
        private OutputPort registerSelect;
        private OutputPort enable;
        private OutputPort backLight;

        private OutputPort d7;
        private OutputPort d6;
        private OutputPort d5;
        private OutputPort d4;

        //Properties
        public OutputPort RegisterSelect
        {
            get { return registerSelect; }
            set { registerSelect = value; }
        }
        public OutputPort BackLight
        {
            get { return backLight; }
            set { backLight = value; }
        }

        //Constructors
        public LCD(OutputPort rs, OutputPort e, OutputPort bl, OutputPort d7, OutputPort d6, OutputPort d5, OutputPort d4)
        {
            this.registerSelect = rs;
            this.enable = e;
            this.BackLight = bl;
            this.d7 = d7;
            this.d6 = d6;
            this.d5 = d5;
            this.d4 = d4;

            InitLCD();
        }

        public LCD()
            : this(
                new OutputPort(FEZSpiderII.Socket5.Pin4, false),
                new OutputPort(FEZSpiderII.Socket5.Pin3, true),
                new OutputPort(FEZSpiderII.Socket5.Pin8, false),
                new OutputPort(FEZSpiderII.Socket5.Pin6, false),
                new OutputPort(FEZSpiderII.Socket5.Pin9, false),
                new OutputPort(FEZSpiderII.Socket5.Pin7, false),
                new OutputPort(FEZSpiderII.Socket5.Pin5, false))
        {

        }

        //Methods
        public void SetCursor(int line, int column)
        {
            int adress = 0;
            if (line == 0)
            {
                adress = column;
            }
            else
            {
                adress = (ADRESSE_START_SECOND_LINE + column);
            }

            SendData((byte)(COMMAND_SET_CURSOR + adress));
        }

        public void Write(string text)
        {
            RegisterSelect.Write(true);
            foreach (char c in text)
            {
                SendData((byte)c);
            }
            RegisterSelect.Write(false);
        }

        public void WriteChar(char charToWrite)
        {
            //Indicate the writing of a character
            RegisterSelect.Write(true);
            SendData((byte)charToWrite);
            RegisterSelect.Write(false);
        }

        public void SendData(byte value)
        {
            byte strongBits = (byte)((value & 0xF0) >> 4);
            byte weakBits = (byte)(value & 0x0F);

            Send4Bits(strongBits);
            ValidateEnable();
            Send4Bits(weakBits);
            ValidateEnable();
        }

        private void Send4Bits(byte value)
        {
            d7.Write((value & 1 << 3) > 0);
            d6.Write((value & 1 << 2) > 0);
            d5.Write((value & 1 << 1) > 0);
            d4.Write((value & 1 << 0) > 0);
        }

        private void ValidateEnable()
        {
            enable.Write(true);
            enable.Write(false);
            Thread.Sleep(1);
        }

        private void InitLCD()
        {
            //indique l'envois d'une commande
            this.RegisterSelect.Write(false);
            //active l'eclairage de l'ecran
            this.BackLight.Write(true);
            //ecrit en 8 bit
            SendData(COMMAND_WRITE_IN_8_BIT);
            //ecrit en 4 bit
            SendData(COMMAND_WRITE_IN_4_BIT);
            //display on
            SendData(COMMAND_DISPLAY_ON);
            //vide l'affichage
            SendData(COMMAND_EMPTY_DISPLAY);
        }
    }
}
