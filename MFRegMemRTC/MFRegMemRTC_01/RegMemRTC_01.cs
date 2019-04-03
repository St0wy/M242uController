/* Programme:  Test
 * Hbr Fabian 03.04.2019 Version 1.0
 * Description:
 * Créez un programme qui, lorsque l'on appuie sur un bouton, on sauvegarde la valeur d'un compteur et l'heure système
 * dans la zone mémoire du RTC.
 * Lors de la mise sous tension, le programme récupère les données sauvegardée, les affiches et l'on réinitialise le compteur
 * avec sa valeur mémorisée.
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
 *     GHI.Hardware                 pour GHI.Processor.RealTimeClock
 *     Microsoft.SPOT.Hardware      pour Cpu.Pin et OutputPort
 */

// Par défaut
using System;
using Microsoft.SPOT;
// A ajouter
using Microsoft.SPOT.Hardware;              // Pour OutputPort
using GHI.Pins;					            // Pour GHI.Pins
using System.Threading;                     // Pour Thread.Sleep(1000)
using GHICard = GHI.Pins.FEZSpiderII;       // Défini la carte principale utilisée
using GHIRTC = GHI.Processor.RealTimeClock; // Pour utiliser le RTC


namespace MFRegMemRTC_01
{
    public class RegMemRTC_01
    {
        public static void Main()
        {
            Debug.Print(Resources.GetString(Resources.StringResources.String1));
        }
    }
}
