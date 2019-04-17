/* Programme:  MFRegulateurTemp
 * Hbr Fabian 17.04.2019 Version 1.0
 * Description
 * 
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
using Microsoft.SPOT.Hardware;         // Pour OutputPort
using GHI.Pins;					       // Pour GHI.Pins
using System.Threading;                // Pour Thread.Sleep(1000)
using GHICard = GHI.Pins.FEZSpiderII;  // Défini la carte principale utilisée

namespace MFRegulateurTemp
{
    public class RegulateurTemp
    {
        const int VOLTAGE_0_DEGREE = 500; //in mV
        const int TEMPERATURE_COEFFICIENT = 10; //in mV
        const int HIGHEST_VOLTAGE = 330;
        const int BUFFER_LENGHT = 3;

        static Button btnSetting = new Button(new InputPort(GHICard.Socket9.Pin3, true, Port.ResistorMode.Disabled));
        static Button btnRemove = new Button(new InputPort(GHICard.Socket12.Pin3, true, Port.ResistorMode.Disabled));
        static Button btnAdd = new Button(new InputPort(GHICard.Socket14.Pin3, true, Port.ResistorMode.Disabled));

        static AnalogInput heatSensor = new AnalogInput(GHICard.Socket10.AnalogInput3);
        static double temperature = 0;

        public static void Main()
        {
            double oldtemp = 0;
            double oldoldtemp = 0;

            while (true)
            {
                int heatSensorValue = heatSensor.ReadRaw();
                oldoldtemp = oldtemp;
                oldtemp = temperature;
                temperature = (heatSensorValue * HIGHEST_VOLTAGE) / System.Math.Pow(2, GHICard.SupportedAnalogInputPrecision) - (VOLTAGE_0_DEGREE / TEMPERATURE_COEFFICIENT);
                temperature = moyDouble(new double[] { oldoldtemp, oldtemp, temperature });

                Debug.Print(temperature.ToString());

                Thread.Sleep(10);
            }
        }

        public static double moyDouble(double[] tab)
        {
            double moy = 0;
            foreach (double nbr in tab)
            {
                moy += nbr;
            }
            moy /= tab.Length;

            return moy;
        }
    }
}
