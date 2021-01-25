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
    
    
    // Start is called before the first frame update
    void Start()
    {
        Spremanje.StariRezultatMi = Spremanje.RezultatMi;
        Spremanje.StariRezultatVi = Spremanje.RezultatVi;
        
        RezultatMi.SetText(Spremanje.RezultatMi.ToString());
        RezultatVi.SetText(Spremanje.RezultatVi.ToString());

        Spremanje.StariRezultatMiSveukupno = Spremanje.RezultatMiSveukupno;
        Spremanje.StariRezultatViSveukupno = Spremanje.RezultatViSveukupno;

        RezultatMiSveukupno.SetText(Spremanje.RezultatMiSveukupno.ToString());
        RezultatViSveukupno.SetText(Spremanje.RezultatViSveukupno.ToString());
        
    }

    // Update is called once per frame
    void Update()
    {

        BrojiRezultatStalnoVi(IgraciDvaiCetri, Rezultat2i4);
        BrojiRezultatStalnoMi(IgraciJedaniTri, Rezultat1i3);
    }
    
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
 
            Rezultat.SetText(Ai.ZvanjaVi.ToString()+" + "+rezultat.ToString());
        }
    }
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

    public static void PostaviNoviRezultatMi(int VrijednostTrenutnogRezultataIgraci1i3)
    {
        int stariRezultat = Spremanje.StariRezultatMi;         
        int noviRezultat = stariRezultat + VrijednostTrenutnogRezultataIgraci1i3;
        Spremanje.RezultatMi = noviRezultat;
        if(noviRezultat >= 1001)
        {
            int stariRezultatiMiSveukupno = Spremanje.StariRezultatMiSveukupno;
            int noviRezultatMiSveukupno = stariRezultatiMiSveukupno + 1;
            Spremanje.RezultatMi = 0;
            Spremanje.RezultatVi = 0;
            Spremanje.RezultatMiSveukupno = noviRezultatMiSveukupno;         
        }
    }

    public static void PostaviNoviRezultatVi(int VrijednostTrenutnogRezultataIgraci1i3)
    {
        int stariRezultat = Spremanje.StariRezultatVi;
        int noviRezultat = stariRezultat + VrijednostTrenutnogRezultataIgraci1i3;
        Spremanje.RezultatVi = noviRezultat;
        if (noviRezultat >= 1001)
        {
            int stariRezultatiViSveukupno = Spremanje.StariRezultatViSveukupno;
            int noviRezultatViSveukupno = stariRezultatiViSveukupno + 1;
            Spremanje.RezultatMi = 0;
            Spremanje.RezultatVi = 0;
            Spremanje.RezultatViSveukupno = noviRezultatViSveukupno;
        }
    }
}
