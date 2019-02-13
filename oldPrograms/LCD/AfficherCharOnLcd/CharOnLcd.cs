/* Programme  AfficherCharOnLcd
 * Hbr Fabian 17.10.17 Version 1.0
 * Description
 * 
 */

/* Description matériel et librairies:
 * 
 * Version: GHI SDK:2016.1.2.0; NETMF:4.3.8.1; Gadgeteer core:2.44.1100.0
 * Carte mère: Spider 2	Version firmware: 4.3.8.1 	Version Loader:4.3.8.1  (MAJ: 4381)
 * Module utilisé		Connecteur utilisé	Fonction
 *	usbClientDP     1                   Connexion au PC
 *  Button          2                   Start/Stop
 *  Joystick        1                   "Mooving" LED in the LED strip
 *  LED strip       1                   Show the action of the Joystick
 */

/* Référence à ajouter:
 *     GHI.Pins                     pour GHI.Pins.EMX ou ...
 *     Microsoft.SPOT.Hardware      pour Cpu.Pin et OutputPort
 */

// Par défaut
using System;
using Microsoft.SPOT;
// A ajouter
using Microsoft.SPOT.Hardware;                   // Pour OutputPort
using GHI.Pins;									 // Pour GHI.Pins
using System.Threading;         // Pour Thread.Sleep(1000)
using GHICard = GHI.Pins.FEZSpider; // Défini la carte principale utilisée

namespace AfficherCharOnLcd
{
    public class CharOnLcd
    {
        //DECLARATION, INITIALISATION
        static OutputPort rs = new OutputPort(FEZSpiderII.Socket8.Pin4, false);
        static OutputPort e = new OutputPort(FEZSpiderII.Socket8.Pin3, true);
        static OutputPort backLight = new OutputPort(FEZSpiderII.Socket8.Pin8, false);

        static OutputPort d7 = new OutputPort(FEZSpiderII.Socket8.Pin6, false);
        static OutputPort d6 = new OutputPort(FEZSpiderII.Socket8.Pin9, false);
        static OutputPort d5 = new OutputPort(FEZSpiderII.Socket8.Pin7, false);
        static OutputPort d4 = new OutputPort(FEZSpiderII.Socket8.Pin5, false);

        static string motHaut;
        static string motBas;
        static int vitesse;

        public static void Main()
        {
            Initialisation();
            //mot affiche en haut
            motHaut = "Bienvenue aux portes ouvertes";
            //mot affiche en bas
            motBas = "du CFPT!";
            //vitesse en milliseconde entre chaque decalage
            vitesse = 200;
            ecrire(motHaut, motBas, vitesse);
        }

        public static void Initialisation() 
        {
            //indique l'envois d'une commande
            rs.Write(false);
            //active l'eclairage de l'ecran
            backLight.Write(true);
            SendCmd(0x33);
            SendCmd(0x32);
            SendCmd(0x0C);
            //vide l'affichage
            SendCmd(0x01);
        }

        public static void ecrire(string motHaut, string motBas, int vitesse)
        {
            //Indique l'ecriture de char
            rs.Write(true);
            //ecrit le mot du haut
            for (int i = 0; i < motHaut.Length; i++)
            {
                SendCmd((byte)motHaut[i]);
            }
            rs.Write(false);
            //Met a la ligne du bas            
            SendCmd(0xA8);

            rs.Write(true);
            //Ecrit le mot du bas
            for (int i = 0; i < motBas.Length; i++)
            {
                SendCmd((byte)motBas[i]);
            }
            rs.Write(false);
            
            while (true)    //Decale le tout
            {
                SendCmd(0x18);
                Thread.Sleep(vitesse);
            }
        }

        public static void SendCmd(byte valeur)
        {
            //Envoie en 2 parties
            //Bits de poids forts
            d7.Write((valeur & 1 << 7) > 0);
            d6.Write((valeur & 1 << 6) > 0);
            d5.Write((valeur & 1 << 5) > 0);
            d4.Write((valeur & 1 << 4) > 0);

            e.Write(true);
            e.Write(false);
            Thread.Sleep(1);
            
            //Bits de poids faibles
            d7.Write((valeur & 1 << 3) > 0);
            d6.Write((valeur & 1 << 2) > 0);
            d5.Write((valeur & 1 << 1) > 0);
            d4.Write((valeur & 1 << 0) > 0);

            e.Write(true);
            e.Write(false);
            Thread.Sleep(1);
        }
    }
}
 