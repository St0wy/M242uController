/* Programme:  [NomProgramme]
 * Hbr Fabian jj.mm.2019 Version 1.0
 * Description
 * [Description]
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

namespace MFDetecteurCrepusculaire
{
    public class DetecteurCrepusculaire
    {
        const int TRESHOLD_HIGH = 3000;
        const int TRESHOLD_LOW = 2500;
        const int LIGHT_SENSOR_SCALE = 4095;

        enum Mode { Auto, Manual };
        static LCD lcd = new LCD();
        static Mode mode = Mode.Auto;
        static Button btnMode = new Button(new InputPort(GHICard.Socket12.Pin3, true, Port.ResistorMode.Disabled));
        static Button btnLamp = new Button(new InputPort(GHICard.Socket14.Pin3, true, Port.ResistorMode.Disabled));
        static OutputPort ledLamp = new OutputPort(GHICard.DebugLed, false);
        static AnalogInput lightSensor = new AnalogInput(GHICard.Socket10.AnalogInput3);
        static double lightSensorValue = 0;

        public static void Main()
        {
            lightSensor.Scale = LIGHT_SENSOR_SCALE;
            Debug.Print(mode.ToString());

            //Main loop
            while (true)
            {
                lightSensorValue = lightSensor.Read();
                //Debug.Print(lightSensorValue.ToString());

                switch (mode)
                {
                    case Mode.Auto:
                        if (lightSensorValue > TRESHOLD_HIGH)
                        {
                            ledLamp.Write(false);
                        }
                        else if (lightSensorValue < TRESHOLD_LOW)
                        {
                            ledLamp.Write(true);
                        }

                        if (btnMode.isPressed())
                        {
                            Debug.Print("MODE MANUAL");
                            mode = Mode.Manual;
                        }
                        break;
                    case Mode.Manual:
                        if (btnLamp.isPressed())
                        {
                            Debug.Print("BTNLAMP IN MODE AUTO");
                            ledLamp.Write(!ledLamp.Read());
                        }

                        if (btnMode.isPressed())
                        {
                            Debug.Print("MODE AUTO");
                            mode = Mode.Auto;
                        }
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
