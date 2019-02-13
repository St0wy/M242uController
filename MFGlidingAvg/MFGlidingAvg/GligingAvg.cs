/* Programme  MFGligingAvg
 * Hbr Fabian 13.02.2019 Version 1.0
 * Description
 * Travail pour apprendre a utiliser les analogs input (a l'aide un joystick)
 */

/* Description matériel et librairies:
 * 
 * Version: GHI SDK:2016.1.2.0; NETMF:4.3.8.1; Gadgeteer core:2.44.1100.0
 * Carte mère: Spider 2	Version firmware: 4.3.8.1 	Version Loader:4.3.8.1  (MAJ: 4381)
 * Module utilisé		Connecteur utilisé	Fonction
 * Joystick 1.2         10                  entree analogiques pour s'entrainer
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


namespace MFGlidingAvg
{
    public class GligingAvg
    {
        const int NUMBER_OF_MEASURE_FOR_AVG = 4;
        const double SCALE_X = 3.3;
        const double OFFSET_X = 0;
        const double SCALE_Y = 150;
        const double OFFSET_Y = -50;

        public static void Main()
        {
            Joystick joy = new Joystick();

            joy.X.Scale = SCALE_X;
            joy.X.Offset = OFFSET_X;
            joy.Y.Scale = SCALE_Y;
            joy.Y.Offset = OFFSET_Y;

            int rawValueX = joy.X.ReadRaw();
            int minValX = rawValueX;
            int maxValX = rawValueX;
            double voltX = joy.X.Read();
            double glidingAvgX = GetGlidingAvg(voltX, voltX);

            int rawValueY = joy.Y.ReadRaw();
            int minValY = rawValueY;
            int maxValY = rawValueY;
            double tempY = joy.Y.Read();
            double glidingAvgY = GetGlidingAvg(voltX, tempY);


            //Main Loop
            while (true)
            {
                rawValueX = joy.X.ReadRaw();
                if (rawValueX < minValX)
                {
                    minValX = rawValueX;
                }

                if (rawValueX > maxValX)
                {
                    maxValX = rawValueX;
                }
                voltX = joy.X.Read();
                glidingAvgX = GetGlidingAvg(voltX, glidingAvgX);

                rawValueY = joy.Y.ReadRaw();
                if (rawValueY < minValY)
                {
                    minValY = rawValueY;
                }

                if (rawValueY > maxValY)
                {
                    maxValY = rawValueY;
                }
                tempY = joy.Y.Read();
                glidingAvgY = GetGlidingAvg(tempY, glidingAvgY);


                Debug.Print("rawValueX = " + rawValueX.ToString() + "\n");
                Debug.Print("minValX = " + minValX.ToString() + "\n");
                Debug.Print("maxValX = " + maxValX.ToString() + "\n");
                Debug.Print("glidingAvgX = " + glidingAvgX.ToString() + "\n");
                Debug.Print("voltX = " + voltX.ToString() + "\n");

                Debug.Print("rawValueY = " + rawValueY.ToString() + "\n");
                Debug.Print("minValY = " + minValY.ToString() + "\n");
                Debug.Print("maxValY = " + maxValY.ToString() + "\n");
                Debug.Print("glidingAvgY = " + glidingAvgY.ToString() + "\n");
                Debug.Print("tempY = " + tempY.ToString() + "\n");
                Debug.Print("----------------------------------------------------");
                Thread.Sleep(1000);
            }
        }

        public static double GetGlidingAvg(double value, double lastAvg)
        {
            double tmp = lastAvg * (NUMBER_OF_MEASURE_FOR_AVG - 1);

            return (value + tmp) / NUMBER_OF_MEASURE_FOR_AVG;
        }
    }
}
