/* Programme  Dout08_Bougie
 * Hbr Fabian 27.03.17 Version 1.0
 * Description
 * Faire moduler la luminositee aleatoirement d'une led qui a une frequence de 100Hz.
 */

/* Description matériel et librairies:
 * 
 * Version: GHI SDK:2016.1.2.0; NETMF:4.3.8.1; Gadgeteer core:2.44.1100.0
 * Carte mère: Spider 2	Version firmware: 4.3.8.1 	Version Loader:4.3.8.1  (MAJ: 4381)
 * Module utilisé		Connecteur utilisé	Fonction
 *	usbClientDP     1                   Connexion au PC
 */

/* Référence à ajouter:
 *     GHI.Pins                     pour GHI.Pins.EMX ou ...
 *     Microsoft.SPOT.Hardware      pour Cpu.Pin et OutputPort
 */

// Par défaut
using System;
using Microsoft.SPOT;
// A ajouter
using Microsoft.SPOT.Hardware;  // Pour OutputPort
using GHI.Pins;									// Pour GHI.Pins
using System.Threading;         // Pour Thread.Sleep(1000)
using GHICard = GHI.Pins.FEZSpider; // Défini la carte principale utilisée

namespace DOut07_LedModulation
{
    public class Program
    {
        public static void Main()
        {
            int thaut = 0, tbas = 10, compteur = 0, etat = 0;
            Random rnd = new Random();
            OutputPort LedSpider = new OutputPort(FEZSpiderII.DebugLed, false);
            while (true)
            {
                LedSpider.Write(true);
                Thread.Sleep(thaut);
                LedSpider.Write(false);
                Thread.Sleep(tbas);
                compteur++;
                etat = rnd.Next(2);
                if (compteur == 3)
                {
                    compteur = 0;
                    if (etat == 0)
                    {
                        if (thaut < 8)
                        {
                            thaut += rnd.Next(4);
                        }
                        else if (thaut == 8) 
                        {
                            thaut += rnd.Next(3);
                        }
                        else if (thaut == 9)
                        {
                            
                        }
                        else if (thaut == 10)
                        {

                        }
                            tbas = 10 - thaut;
                    }
                    else if (etat == 1)
                    {
                        if (thaut > 2)
                        {
                            thaut -= rnd.Next(4);
                        }
                        else if (thaut == 2) 
                        {
                            thaut -= rnd.Next(3);
                        }
                        else if (thaut == 1)
                        {
                            
                        }
                        else if (thaut == 0)
                        {

                        }
                        tbas = 10 - thaut;
                    }
                }
            }
        }
    }
}
