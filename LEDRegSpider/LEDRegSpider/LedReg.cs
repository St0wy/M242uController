/* Programme RTCRamSpider2 
 * JFG 23.02.16 Version 1.0
 * JFG 26.10.17 Version 2.0

 * Description: Pilote les Leds d'un module LEDStrip pas l'interm�diaire
 *							de registres du port 2 des cartes Spider2
 *
 *		!!!!!!!!	Le programme fonctionne uniquement sur le socket 14 !!!!!!!!
 * 
 * Adresses de bases des registres des ports GPIO des cartes Spider II
 * 
 *		Adresse DIR port 0 = 0x20098000;
 *		Adresse DIR port 1 = 0x20098020;
 *		Adresse DIR port 2 = 0x20098040;
 *		Adresse DIR port 3 = 0x20098060;
 *		Adresse DIR port 4 = 0x20098080;
 *		Adresse DIR port 5 = 0x200980A0;
 */

/* Version SDK et librairies: GHI SDK:2016.1.2.0  NETMF:4.3.8.1 Gadgeteer core:2.44.1100.0
 * Description mat�riel:
 * Carte m�re: Spider II Version firmware: 4.3.8.1 	Version Loader:4.3.8.1  (MAJ: 4381)
 * Module utilis�	Connecteur utilis�	Fonction
 *	usbClientDP				1               Connexion au PC
 *  LEDStrip    			14	uniquement	Connecteur branch� sur le port 2 
 */

/* R�f�rence � ajouter:
 *     GHI.Pins                     pour GHI.Pins.EMX ou ...
 *     GHI.Hardware									pour GHI.Processor
 *     Microsoft.SPOT.Hardware      pour Cpu.Pin et OutputPort
 */

// Par d�faut
using System;
using Microsoft.SPOT;
// A ajouter
using Microsoft.SPOT.Hardware;  // Pour OutputPort
using GHI.Processor;			// Pour GHI.Processor
using GHI.Pins;
using System.Threading;         // Pour Thread.Sleep(1000)

namespace LEDRegSpider
{
    public class LedReg
    {
        //-------------------------------------------------------------------------
        public static string ExToBinToString(uint ValExa)
        {
            // Converti une valeur exa 32bits en une chaine de bits
            // Exemple ValExa=0xFFFFEC17 retourne "11111111'11111111'11101100'00010111"
            string stringValBin = "";
            uint masque = 0;
            int x = 0;
            int y = 1;
            for (x = 31; x >= 0; x--)
            {
                masque = (uint)1 << x;
                if ((ValExa & masque) > 0)
                {
                    stringValBin += "1";
                }
                else
                {
                    stringValBin += "0";
                }
                if (y++ == 8 && x != 0) // Met une s�paration tout les 8 caract�res
                {
                    stringValBin += "'";
                    y = 1;
                }
            }

            return stringValBin;
        }
        //-------------------------------------------------------------------------

        // D�claration des variables globales
        // D�claration des ports du module LedStrip sur le connecteur 14 
        // Evite de configurerles ports directement depuis les registres GPIO
        static OutputPort LEDStrip1 = new OutputPort(FEZSpiderII.Socket14.Pin3, false); // Port2.12
        static OutputPort LEDStrip2 = new OutputPort(FEZSpiderII.Socket14.Pin4, false);// Port2.06
        static OutputPort LEDStrip3 = new OutputPort(FEZSpiderII.Socket14.Pin5, false); // Port2.07
        static OutputPort LEDStrip4 = new OutputPort(FEZSpiderII.Socket14.Pin6, false);// Port2.08
        static OutputPort LEDStrip5 = new OutputPort(FEZSpiderII.Socket14.Pin7, false); // Port2.09
        static OutputPort LEDStrip6 = new OutputPort(FEZSpiderII.Socket14.Pin8, false);// Port2.03
        static OutputPort LEDStrip7 = new OutputPort(FEZSpiderII.Socket14.Pin9, false); // Port2.05

        //-------------------------------------------------------------------------

        public static void Main()
        {
            Debug.Print(Resources.GetString(Resources.StringResources.String1));

            // Configure l'Adresse du registre de base du post 2 (DIR2)de la carte Spider 1
            const uint REG_ADR_PORT_2 = 0x20098040;

            // Adresses des registres de contr�le du port 2
            Register dir2 = new Register(REG_ADR_PORT_2); // D�termine la direction de chaque broche du port: Entr�e=0 Sortie=1
            Register mask2 = new Register(REG_ADR_PORT_2 + 0x010); // Masque, les bits � 1 sont masqu�s
            Register pin2 = new Register(REG_ADR_PORT_2 + 0x014);	// Lecture/�criture du port au travers de MASK2
            Register set2 = new Register(REG_ADR_PORT_2 + 0x018);	// Met les bits du port, d�fini par le reg. SET, � 1 si non masqu�s par MASK2 	
            Register clr2 = new Register(REG_ADR_PORT_2 + 0x01C);	// Met les bits du port, d�fini par le reg. CLR, � 0 si non masqu�s par MASK2

            // Affiche le contenu des registres du port 2
            Debug.Print("DIR2  = " + dir2.ToString() + "\t" + ExToBinToString(dir2.Value));
            Debug.Print("PIN2  = " + pin2.ToString() + "\t" + ExToBinToString(pin2.Value));
            Debug.Print("SET2  = " + set2.ToString() + "\t" + ExToBinToString(set2.Value));
            Debug.Print("CLR2  = " + clr2.ToString() + "\t" + ExToBinToString(clr2.Value));
            Debug.Print("MASK2 = " + mask2.ToString() + "\t" + ExToBinToString(mask2.Value));

            // Emplacement du bit du port 2 correspondant � chaque LED du module LEDStrip
            // Pour configurer et manpuler les registres Mask, Set et Cls  du port 2
            const uint BIT_LED_STRIP_1 = (uint)(1 << 12); // Port2.12
            const uint BIT_LED_STRIP_2 = (uint)(1 << 6);  // Port2.06
            const uint BIT_LED_STRIP_3 = (uint)(1 << 7);  // Port2.07
            const uint BIT_LED_STRIP_4 = (uint)(1 << 8);  // Port2.08
            const uint BIT_LED_STRIP_5 = (uint)(1 << 9);  // Port2.09
            const uint BIT_LED_STRIP_6 = (uint)(1 << 3);  // Port2.03
            const uint BIT_LED_STRIP_7 = (uint)(1 << 5);  // Port2.05

            // Cr�e un masque de toutes les IO du port 2 � l'exception des sorties du module LEDStrip 
            const uint MASK_P2_LED_STRIP = ~(BIT_LED_STRIP_1 + BIT_LED_STRIP_2 + BIT_LED_STRIP_3 + BIT_LED_STRIP_4 + BIT_LED_STRIP_5 + BIT_LED_STRIP_6 + BIT_LED_STRIP_7);

            // Applique le masque 
            mask2.Value = MASK_P2_LED_STRIP;
            Debug.Print("MLED  = \t\t\t0x" + MASK_P2_LED_STRIP.ToString("X") + "\t" + ExToBinToString(MASK_P2_LED_STRIP));

            const int DELAY = 75;

            while (true)
            {
                set2.SetBits(BIT_LED_STRIP_4);
                Thread.Sleep(DELAY);
                set2.SetBits(BIT_LED_STRIP_3 + BIT_LED_STRIP_5);
                Thread.Sleep(DELAY);
                set2.SetBits(BIT_LED_STRIP_2 + BIT_LED_STRIP_6);
                Thread.Sleep(DELAY);
                set2.SetBits(BIT_LED_STRIP_1 + BIT_LED_STRIP_7);
                Thread.Sleep(DELAY);
                clr2.SetBits(BIT_LED_STRIP_1 + BIT_LED_STRIP_7);
                Thread.Sleep(DELAY);
                clr2.SetBits(BIT_LED_STRIP_2 + BIT_LED_STRIP_6);
                Thread.Sleep(DELAY);
                clr2.SetBits(BIT_LED_STRIP_3 + BIT_LED_STRIP_5);
            }
        }
    }
}
