/* Programme  LedsAsynchrones
 * Hbr Fabian 03.04.17 Version 1.0
 * Description
 * Fait clignoter 3 leds de facon asychrones
 */

/* Description matériel et librairies:
 * 
 * Version: GHI SDK:2016.1.2.0; NETMF:4.3.8.1; Gadgeteer core:2.44.1100.0
 * Carte mère: Spider 2	Version firmware: 4.3.8.1 	Version Loader:4.3.8.1  (MAJ: 4381)
 * Module utilisé		Connecteur utilisé	Fonction
 *	usbClientDP     1                   Connexion au PC
 *	LED Strip 1.2                       Affichage de 7 leds
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

namespace LedsAsychrones
{
    public class LedsAsynchrones
    {
        static OutputPort LedLigne0 = new OutputPort(FEZSpiderII.Socket8.Pin3, false);
        static OutputPort LedLigne1 = new OutputPort(FEZSpiderII.Socket8.Pin4, false);
        static OutputPort LedLigne2 = new OutputPort(FEZSpiderII.Socket8.Pin5, false);
        static OutputPort LedLigne3 = new OutputPort(FEZSpiderII.Socket8.Pin6, false);
        static OutputPort LedLigne4 = new OutputPort(FEZSpiderII.Socket8.Pin7, false);
        static OutputPort LedLigne5 = new OutputPort(FEZSpiderII.Socket8.Pin8, false);
        static OutputPort LedLigne6 = new OutputPort(FEZSpiderII.Socket8.Pin9, false);

        public static void Main()
        {
            int compteur=0;
            while (true)
            {
                FeuRouge();
                FeuRougeOrange();
                if (compteur % 2 == 0) { FeuVert(); }
                else { FeuVertOrange(); }
                FeuOrange();
                compteur++;
            }
        }

        public static void FeuRouge()
        {
            LedLigne0.Write(false);
            LedLigne1.Write(false);
            LedLigne2.Write(false);
            LedLigne3.Write(false);
            LedLigne4.Write(false);
            LedLigne5.Write(true);
            LedLigne6.Write(true);
            Thread.Sleep(3000);
        }
        public static void FeuRougeOrange()
        {
            LedLigne0.Write(false);
            LedLigne1.Write(false);
            LedLigne2.Write(true);
            LedLigne3.Write(true);
            LedLigne4.Write(true);
            LedLigne5.Write(true);
            LedLigne6.Write(true);
            Thread.Sleep(1000);
        }
        public static void FeuVert()
        {
            LedLigne0.Write(true);
            LedLigne1.Write(true);
            LedLigne2.Write(false);
            LedLigne3.Write(false);
            LedLigne4.Write(false);
            LedLigne5.Write(false);
            LedLigne6.Write(false);
            Thread.Sleep(3000);
        }
        public static void ClignottementOrange()
        {
            for (int i = 0; i < 6; i++)
            {
                LedLigne2.Write(true);
                LedLigne3.Write(true);
                LedLigne4.Write(true);
                Thread.Sleep(250);
                LedLigne2.Write(false);
                LedLigne3.Write(false);
                LedLigne4.Write(false);
                Thread.Sleep(250);
            }
        }
        public static void FeuVertOrange()
        {
            LedLigne0.Write(true);
            LedLigne1.Write(true);
            LedLigne5.Write(false);
            LedLigne6.Write(false);
            ClignottementOrange();
        }
        public static void FeuOrange()
        {
            LedLigne0.Write(false);
            LedLigne1.Write(false);
            LedLigne2.Write(true);
            LedLigne3.Write(true);
            LedLigne4.Write(true);
            LedLigne5.Write(false);
            LedLigne6.Write(false);
            Thread.Sleep(1000);
        }
    }
}
