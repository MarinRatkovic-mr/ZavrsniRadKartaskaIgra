using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class  Spremanje
{
    /*----------*/
    /*Statičke varijable korištene tijekom sam igre za spremanje različitih vrijednosti.*/
    public static int StariRezultatMi = 0; 
    public static int RezultatMi = 0;
    public static int StariRezultatVi = 0;
    public static int RezultatVi = 0;
    public static int RezultatMiSveukupno = 0;
    public static int StariRezultatMiSveukupno = 0;   
    public static int RezultatViSveukupno = 0;
    public static int StariRezultatViSveukupno = 0;
    public static int IgracKojiSadaZove = 2;
    public static GameObject NajacaKartaDoSad;
    public static GameObject PrvaOdigranaKarta;
    public static GameObject IgracKojiJeSadNaRedu;
    public static GameObject KojiIgracJeZvao;
    public static string Igrac1 ;
    public static string Igrac2 ;
    public static string Igrac3 ;
    public static string Igrac4 ;
    public static Sprite IgracaPloca = Resources.Load<Sprite>("IgracaPloca1");
    public static GameObject PozicijaKojaMoraBitiPopunjenaPrvo = null;
    public static bool BelaJeZvana = false;
    public static bool PosloziKarte=false;
    public static bool ZoviJednom=false;
    public static bool PoravnajKarte=false;
    /*------------*/

}
