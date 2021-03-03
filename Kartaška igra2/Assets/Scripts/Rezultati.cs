using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class Rezultati : MonoBehaviour
{
    public GameObject IgraciDvaiCetri;
    public TextMeshProUGUI Rezultat2i4;
    public GameObject IgraciJedaniTri;
    public TextMeshProUGUI Rezultat1i3;
    public TextMeshProUGUI RezultatMi;
    public TextMeshProUGUI RezultatVi;
    public TextMeshProUGUI RezultatMiSveukupno;
    public TextMeshProUGUI RezultatViSveukupno;

    public TextMeshProUGUI TextIgrac1;
    public TextMeshProUGUI TextIgrac2;
    public TextMeshProUGUI TextIgrac3;
    public TextMeshProUGUI TextIgrac4;
 

  
    void Start()
    {
        ProvjeriDaliNaziviIgracaPostojeIDodaj();

        Spremanje.StariRezultatMi = Spremanje.RezultatMi;
        Spremanje.StariRezultatVi = Spremanje.RezultatVi;
        
        RezultatMi.SetText(Spremanje.RezultatMi.ToString());
        RezultatVi.SetText(Spremanje.RezultatVi.ToString());

        Spremanje.StariRezultatMiSveukupno = Spremanje.RezultatMiSveukupno;
        Spremanje.StariRezultatViSveukupno = Spremanje.RezultatViSveukupno;

        RezultatMiSveukupno.SetText(Spremanje.RezultatMiSveukupno.ToString());
        RezultatViSveukupno.SetText(Spremanje.RezultatViSveukupno.ToString());
        
    }

    void Update()
    {

        BrojiRezultatStalnoVi(IgraciDvaiCetri, Rezultat2i4);
        BrojiRezultatStalnoMi(IgraciJedaniTri, Rezultat1i3);
    }

    /*Ako je korisnik igre promjenio nazive igrača u postavcima u glavnom meniu doda te nazive igračima.*/
    private void ProvjeriDaliNaziviIgracaPostojeIDodaj()
    {
        if(Spremanje.Igrac1 != null)
        {
            TextIgrac1.SetText(Spremanje.Igrac1);
        }
        else
        {
            TextIgrac1.SetText("Igrac 1");
        }

        if (Spremanje.Igrac2 != null)
        {
            TextIgrac2.SetText(Spremanje.Igrac2);
        }
        else
        {
            TextIgrac2.SetText("Igrac 2");
        }

        if (Spremanje.Igrac3 != null)
        {
            TextIgrac3.SetText(Spremanje.Igrac3);
        }
        else
        {
            TextIgrac3.SetText("Igrac 3");
        }

        if (Spremanje.Igrac4 != null)
        {
            TextIgrac4.SetText(Spremanje.Igrac4);
        }
        else
        {
            TextIgrac4.SetText("Igrac 4");
        }

    }
    /*Stalno broji osvojene karte kod igrača koji igra i Ai igrača 3*/
    public void BrojiRezultatStalnoMi(GameObject Igraci, TextMeshProUGUI Rezultat)
    {
       
           int rezultat = 0;
        if (Rezultat != null)
        {
            for (int i = 0; i < Igraci.transform.childCount; i++)
            {
                GameObject OdigranKarta = Igraci.transform.GetChild(i).gameObject;
                Ai OdigranaKartaAi = OdigranKarta.GetComponent<Ai>();
                rezultat = rezultat + OdigranaKartaAi.VrijednostKarte;
                
            }
 
            Rezultat.SetText(Ai.ZvanjaMi.ToString()+" + "+rezultat.ToString());
        }
    }
    /*Stalno broji osvojene karte kod igrača koji Ai igrača 2 i Ai igrača 4*/
    public void BrojiRezultatStalnoVi(GameObject Igraci, TextMeshProUGUI Rezultat)
    {

        int rezultat = 0;
        if (Rezultat != null)
        {
            for (int i = 0; i < Igraci.transform.childCount; i++)
            {
                GameObject OdigranKarta = Igraci.transform.GetChild(i).gameObject;
                Ai OdigranaKartaAi = OdigranKarta.GetComponent<Ai>();
                rezultat = rezultat + OdigranaKartaAi.VrijednostKarte;

            }           
            Rezultat.SetText(Ai.ZvanjaVi.ToString()+" + "+rezultat.ToString());
        }
    }
    /*Na kraju partije postavlja rezultat sa zvanjima i svime za igrača koji igra i Ai igrača 3.*/
    public static void PostaviNoviRezultatMi(int VrijednostTrenutnogRezultataIgraci1i3)
    {
        int stariRezultat = Spremanje.StariRezultatMi;         
        int noviRezultat = stariRezultat + VrijednostTrenutnogRezultataIgraci1i3;
        Spremanje.RezultatMi = noviRezultat;
        if(noviRezultat >= 1001)
        {
            int stariRezultatiMiSveukupno = Spremanje.StariRezultatMiSveukupno;
            int noviRezultatMiSveukupno = stariRezultatiMiSveukupno + 1;   
            Spremanje.RezultatMiSveukupno = noviRezultatMiSveukupno;  
            Spremanje.RezultatVi = 0; 
            Spremanje.RezultatMi = 0;
                   
        }
    }

    /*Na kraju partije postavlja rezultat sa zvanjima i svime za Ai igrača 2 Ai igrača 4.*/
    public static void PostaviNoviRezultatVi(int VrijednostTrenutnogRezultataIgraci1i3)
    {
        int stariRezultat = Spremanje.StariRezultatVi;
        int noviRezultat = stariRezultat + VrijednostTrenutnogRezultataIgraci1i3;
        Spremanje.RezultatVi = noviRezultat;
        if (noviRezultat >= 1001)
        {
            int stariRezultatiViSveukupno = Spremanje.StariRezultatViSveukupno;
            int noviRezultatViSveukupno = stariRezultatiViSveukupno + 1;
            Spremanje.RezultatViSveukupno = noviRezultatViSveukupno;
            Spremanje.RezultatMi = 0;
            Spremanje.RezultatVi = 0;
        }
    }
}
