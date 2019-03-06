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