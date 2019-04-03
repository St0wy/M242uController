/* Programme RTCRamSpider2 
 * JFG 23.02.16 Version 1.0
 * JFG 26.10.17 Version 2.0

 * Description: Pilote les Leds d'un module LEDStrip pas l'intermédiaire
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
 * Description matériel:
 * Carte mère: Spider II Version firmware: 4.3.8.1 	Version Loader:4.3.8.1  (MAJ: 4381)
 * Module utilisé	Connecteur utilisé	Fonction
 *	usbClientDP				1               Connexion au PC
 *  LEDStrip    			14	uniquement	Connecteur branché sur le port 2 
 */

/* Référence à ajouter:
 *     GHI.Pins                     pour GHI.Pins.EMX ou ...
 *     GHI.Hardware									pour GHI.Processor
 *     Microsoft.SPOT.Hardware      pour Cpu.Pin et OutputPort
 */

// Par défaut
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
                    stringValBin += "1";
                else
                    stringValBin += "0";
                if (y++ == 8 && x != 0) // Met une séparation tout les 8 caractères
                {
                    stringValBin += "'";
                    y = 1;
                }
            }

            return stringValBin;
        }
        //-------------------------------------------------------------------------

        // Déclaration des variables globales
        // Déclaration des ports du module LedStrip sur le connecteur 14 
        // Evite de configurerles ports directement depuis les registres GPIO
        static OutputPort LEDStrip1 = new OutputPort(FEZSpiderII.Socket14.Pin3, true); // Port2.12
        static OutputPort LEDStrip2 = new OutputPort(FEZSpiderII.Socket14.Pin4, false);// Port2.06
        static OutputPort LEDStrip3 = new OutputPort(FEZSpiderII.Socket14.Pin5, true); // Port2.07
        static OutputPort LEDStrip4 = new OutputPort(FEZSpiderII.Socket14.Pin6, false);// Port2.08
        static OutputPort LEDStrip5 = new OutputPort(FEZSpiderII.Socket14.Pin7, true); // Port2.09
        static OutputPort LEDStrip6 = new OutputPort(FEZSpiderII.Socket14.Pin8, false);// Port2.03
        static OutputPort LEDStrip7 = new OutputPort(FEZSpiderII.Socket14.Pin9, true); // Port2.05

        //-------------------------------------------------------------------------

        public static void Main()
        {
            Debug.Print(Resources.GetString(Resources.StringResources.String1));

            // Configure l'Adresse du registre de base du post 2 (DIR2)de la carte Spider 1
            const uint RegAdrPort2 = xxx;

            // Adresses des registres de contrôle du port 2
            Register DIR2 = new Register(yyy); // Détermine la direction de chaque broche du port: Entrée=0 Sortie=1
            Register MASK2 = new Register(yyy); // Masque, les bits à 1 sont masqués
            Register PIN2 = new Register(yyy);	// Lecture/écriture du port au travers de MASK2
            Register SET2 = new Register(yyy);	// Met les bits du port, défini par le reg. SET, à 1 si non masqués par MASK2 	
            Register CLR2 = new Register(yyy);	// Met les bits du port, défini par le reg. CLR, à 0 si non masqués par MASK2

            // Affiche le contenu des registres du port 2
            Debug.Print("DIR2  = " + DIR2.ToString() + "\t" + ExToBinToString(DIR2.Value));
            Debug.Print("PIN2  = " + PIN2.ToString() + "\t" + ExToBinToString(PIN2.Value));
            Debug.Print("SET2  = " + SET2.ToString() + "\t" + ExToBinToString(SET2.Value));
            Debug.Print("CLR2  = " + CLR2.ToString() + "\t" + ExToBinToString(CLR2.Value));
            Debug.Print("MASK2 = " + MASK2.ToString() + "\t" + ExToBinToString(MASK2.Value));

            // Emplacement du bit du port 2 correspondant à chaque LED du module LEDStrip
            // Pour configurer et manpuler les registres Mask, Set et Cls  du port 2
            const uint BITLEDStrip1 = (uint)(1 << 12); // Port2.12
            const uint BITLEDStrip2 = (uint)(1 << 6);  // Port2.06
            const uint BITLEDStrip3 = (uint)(1 << 7);  // Port2.07
            const uint BITLEDStrip4 = (uint)(1 << 8);  // Port2.08
            const uint BITLEDStrip5 = (uint)(1 << 9);  // Port2.09
            const uint BITLEDStrip6 = (uint)(1 << 3);  // Port2.03
            const uint BITLEDStrip7 = (uint)(1 << 5);  // Port2.05

            // Crée un masque de toutes les IO du port 2 à l'exception des sorties du module LEDStrip 
            const uint MaskP2LedStrip = ~(BITLEDStrip1 + BITLEDStrip2 + BITLEDStrip3 + BITLEDStrip4 +
                                                                        BITLEDStrip5 + BITLEDStrip6 + BITLEDStrip7);

            // Applique le masque 
            MASK2.Value = MaskP2LedStrip;
            Debug.Print("MLED  = \t\t\t0x" + MaskP2LedStrip.ToString("X") + "\t" + ExToBinToString(MaskP2LedStrip));


            while (true)
            {

                // Votre code ...

            }

        }
    }
}
