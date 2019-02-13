/* Programme  Reveil
 * Hbr Fabian 14.11.17 Version 1.0
 * Description
 * Reveile qui fait bip + affichage
 */

/* Description matériel et librairies:
 * 
 * Version: GHI SDK:2016.1.2.0; NETMF:4.3.8.1; Gadgeteer core:2.44.1100.0
 * Carte mère: Spider 2	Version firmware: 4.3.8.1 	Version Loader:4.3.8.1  (MAJ: 4381)
 * Module utilisé		Connecteur utilisé	Fonction
 *	usbClientDP         1                   Connexion au PC
 *  LCD                 8                   Affichage de l'heure
 *  Tunes               11                  Reveil
 */

/* Référence à ajouter:
 *     GHI.Pins                     pour GHI.Pins.EMX ou ...
 *     Microsoft.SPOT.Hardware      pour Cpu.Pin et OutputPort
 */

// Par défaut
using System;
using Microsoft.SPOT;
// A ajouter
using GHI.Processor;
using GHI.Utilities;
using Microsoft.SPOT.Hardware;                   // Pour OutputPort
using GHI.Pins;									 // Pour GHI.Pins
using System.Threading;         // Pour Thread.Sleep(1000)
using GHICard = GHI.Pins.FEZSpider; // Défini la carte principale utilisée

namespace Reveil
{
    public class Reveil
    {
        #region Declaration
        //DECLARATION, INITIALISATION
        static OutputPort rs = new OutputPort(FEZSpiderII.Socket8.Pin4, false);
        static OutputPort e = new OutputPort(FEZSpiderII.Socket8.Pin3, true);
        static OutputPort backLight = new OutputPort(FEZSpiderII.Socket8.Pin8, false);

        static OutputPort d7 = new OutputPort(FEZSpiderII.Socket8.Pin6, false);
        static OutputPort d6 = new OutputPort(FEZSpiderII.Socket8.Pin9, false);
        static OutputPort d5 = new OutputPort(FEZSpiderII.Socket8.Pin7, false);
        static OutputPort d4 = new OutputPort(FEZSpiderII.Socket8.Pin5, false);

        static AnalogInput lightSense = new AnalogInput(FEZSpiderII.Socket9.AnalogInput3);

        static InputPort btn1 = new InputPort(FEZSpiderII.Socket12.Pin3, true, Port.ResistorMode.Disabled);

        static string motHaut;
        static string motBas;
        static bool alarm;
        static double frequence = 500;
        static string[] joursemaine = { "Lundi", "Mardi", "Mercredi", "Jeudi", "Vendredi", "Samedi", "Dimanche" };

        static string HHverif = "";
        static string mmverif = "";
        static string ssverif = "";
        static string Dverif = "";
        static string MMverif = "";
        static string YYverif = "";

        static string HH = "";
        static string mm = "";
        static string ss = "";
        static string dd = "";
        static string MM = "";
        static string YY = "";
        static string ddd = "";
        #endregion

        static double Sol0 = 196.00;

        static double Do = 261.63;
        static double Re = 293.66;
        static double Mi = 329.63;
        static double Mid = 369.99;
        static double Fa = 349.23;
        static double Sol = 392.00;
        static double La = 440.00;
        static double Sib = 466.16;
        static double Si = 493.88;
        static double Do1 = 523.25;
        static double Do1d = 554.37;

        static double Re1d = 622.25;




        public static void Main()
        {
            Initialisation();
            Utility.SetLocalTime(RealTimeClock.GetDateTime());
            
            firstWrite();
            while (true)
            {
                //Set l'heure du RTC
                //if (!btn1.Read())
                //{
                //    DateTime DT;
                //    DT = new DateTime(2017, 12, 05, 08, 12, 00);
                //    RealTimeClock.SetDateTime(DT);
                //}

                if (!btn1.Read())
                {
                    Rick();
                }

                updateHeureVariable();

                #region moultIf
                if (ssverif != ss)  //Test seconde
                {
                    ssverif = ss;
                    SetCursor(0, 6);
                    rs.Write(true);
                    write(ss);
                    rs.Write(false);

                    if (mmverif != mm)  //Test minute
                    {
                        mmverif = mm;
                        SetCursor(0, 3);
                        rs.Write(true);
                        write(mm + ":");
                        rs.Write(false);

                        if (HHverif != HH)  //Test heure
                        {
                            HHverif = HH;
                            SetCursor(0, 0);
                            rs.Write(true);
                            write(HH + ":");
                            rs.Write(false);

                            if (Dverif != dd)   //Test jour
                            {
                                Dverif = dd;
                                SetCursor(0, 9);
                                rs.Write(true);
                                write(ddd);
                                rs.Write(false);
                                SetCursor(1, 0);
                                rs.Write(true);
                                write(dd + "/");
                                rs.Write(false);

                                if (MMverif != MM)  //Test mois
                                {
                                    MMverif = MM;
                                    SetCursor(1, 3);
                                    rs.Write(true);
                                    write(MM + "/");
                                    rs.Write(false);

                                    if (YYverif != YY)  //Test année
                                    {
                                        YYverif = YY;
                                        SetCursor(1, 6);
                                        rs.Write(true);
                                        write(YY);
                                        rs.Write(false);
                                    }//Fin if année
                                }//Fin if mois
                            }//Fin if Jour
                        }//Fin if heure
                    }//Fin if minute
                }//Fin if seconde 
                #endregion

                SetCursor(1, 13);
                if (alarm)
                {
                    write("ON");
                }
                else
                {
                    write("OFF");
                }

                Thread.Sleep(1000);
            }
        }

        public static void Rick()
        {
            PWM myPWM = new PWM(FEZSpiderII.Socket11.Pwm9, frequence, 0.5, false);
            myPWM.Start();
            myPWM.Frequency = Fa;
            Thread.Sleep(750);
            myPWM.Frequency = Sol;
            Thread.Sleep(750);
            myPWM.Frequency = Do;
            Thread.Sleep(500);
            myPWM.Frequency = Sol;
            Thread.Sleep(750);
            myPWM.Frequency = La;
            Thread.Sleep(750);

            myPWM.Frequency = Do1;
            Thread.Sleep(180);
            myPWM.Frequency = Sib;
            Thread.Sleep(180);
            myPWM.Frequency = La;
            Thread.Sleep(300);

            myPWM.Frequency = Fa;
            Thread.Sleep(750);
            myPWM.Frequency = Sol;
            Thread.Sleep(750);
            myPWM.Frequency = Do;
            Thread.Sleep(1000);


            myPWM.Stop();
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

        public static void updateHeureVariable()
        {
            HH = DateTime.Now.ToString("HH");
            mm = DateTime.Now.ToString("mm");
            ss = DateTime.Now.ToString("ss");
            dd = DateTime.Now.ToString("dd");
            ddd = DateTime.Now.ToString("ddd");
            MM = DateTime.Now.ToString("MM");
            YY = DateTime.Now.ToString("yy");
        }

        public static void firstWrite()
        {
            updateHeureVariable();

            SendCmd(0x01);  //Vide l'afficheur
            //Ecrit les secondes
            SetCursor(0, 6);
            rs.Write(true);
            write(ss);
            rs.Write(false);
            //Ecrit les minutes
            SetCursor(0, 3);
            rs.Write(true);
            write(mm + ":");
            rs.Write(false);
            //Ecrit les heures
            SetCursor(0, 0);
            rs.Write(true);
            write(HH + ":");
            rs.Write(false);
            //Ecrit le jour de la semaine et le jour de la date
            SetCursor(0, 9);
            rs.Write(true);
            write(ddd);
            rs.Write(false);
            SetCursor(1, 0);
            rs.Write(true);
            write(dd + "/");
            rs.Write(false);
            //Ecrit le mois
            SetCursor(1, 3);
            rs.Write(true);
            write(MM + "/");
            rs.Write(false);
            //Ecrit l'année
            SetCursor(1, 6);
            rs.Write(true);
            write(YY);
            rs.Write(false);
        }

        public static void write(string mot)
        {
            //Indique l'ecriture de char
            rs.Write(true);
            for (int i = 0; i < mot.Length; i++)
            {
                SendCmd((byte)mot[i]);
            }
            rs.Write(false);
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

        public static void SetCursor(int ligne, int colonne)
        {
            const int CMDSetCursor = 0x80;  //Valeur de la commande qui permet de deplacer le curseur
            int adresse = 0;                //Valeur de la position du curseur

            if (ligne == 0)
            {
                adresse = colonne;
            }
            else
            {
                adresse = (0x40 + colonne);
            }
            SendCmd((byte)(CMDSetCursor + adresse));
        }
    }
}
