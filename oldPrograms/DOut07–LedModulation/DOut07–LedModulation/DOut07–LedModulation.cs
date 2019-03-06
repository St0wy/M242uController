/* Programme  DOut07–LedModulation
 * Hbr Fabian 20.03.17 Version 1.0
 * Description
 * Faire moduler la luminositee d'une led qui a une frequence de 100Hz.
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
            int thaut=0, tbas=10, compteur=0,etat=0;
            OutputPort LedSpider = new OutputPort(FEZSpiderII.DebugLed, false);
            while (true)
            {
                LedSpider.Write(true);
                Thread.Sleep(thaut);
                LedSpider.Write(false);
                Thread.Sleep(tbas);
                compteur++;
                if (thaut == 10) { etat = 1; }
                if (thaut == 0) { etat = 0; }
                if (compteur == 3)
                {
                    compteur = 0;
                    if (etat == 0)
                    {
                        if (thaut < 10)
                        {
                            if (thaut < 3) { thaut += 1; tbas -= 1;}
                            else
                            {
                                if (thaut == 3)
                                {
                                    thaut = 5;
                                    tbas = 5;
                                }
                                else if (thaut == 5)
                                {
                                    thaut = 10;
                                    tbas = 0;
                                }
                            }
                        }
                        else
                        {
                            etat = 1;
                        }
                    }
                    if (etat == 1)
                    {
                        if (thaut > 5)
                        {
                            thaut /= 2;
                            if (thaut == 5) { tbas = 5; }
                        }
                        else if (thaut == 5) 
                        {
                            thaut = 3;
                            tbas = 7;
                        }
                        else
                        {
                            thaut--;
                            tbas++;
                        }
                    }
                }
            }
        }
    }
}
