using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Linq;
using System;

public class Ai : MonoBehaviour
{
    public GameObject OdigranoPozicija1;
    public GameObject OdigranoPozicija2;
    public GameObject OdigranoPozicija3;
    public GameObject OdigranoPozicija4;
    public GameObject Igrac1;
    public GameObject Igrac2;
    public GameObject Igrac3;
    public GameObject Igrac4;
    public GameObject IgraciJedanITri;
    public GameObject IgraciDvaiCetri;    
    GameObject PrvaOdigranaKarta;
    public GameObject TkoJeZvao;
    public TextMeshProUGUI IgracKojiJeZvao;
    public Image SlikaAduta;
    public Sprite[] SlikeAduta;

    public string BojaKarte;
    public string NazivKarte;
    public int RangZvanja;
    public int RangKarte;
    public int VrijednostKarte;
    public string BojaAduta;
    public bool Adut = false;
    public bool NaRedu = true;
    public int BrojacRunda = 0;
    public int Zvanja = 0;

    public GameObject IgracKojiJeZvaoURundiPrije = null;
    public GameObject IgracKojiPrviZove;
    public GameObject IgracKojiTrenutnoZove = null;
    public GameObject IgracKojiZoveSljedeci = null;
    public string SljedecaScena = "BelaScen1";

    public TextMeshProUGUI Rezultat1i3;
    public TextMeshProUGUI Rezultat2i4;
    // Start is called before the first frame update
    void Start()
    {
        
        PostaviVrijednostiKarataPriPokretanju();
        OnMouseDown();
        
        

    }

    // Update is called once per frame
    void Update()
    {
        
        if (Igrac1.transform.childCount != 0 || Igrac2.transform.childCount != 0 || Igrac3.transform.childCount != 0 || Igrac4.transform.childCount != 0)
        {
            OdluciKojaKartaJeDoSadNajvecaIStoTrebaOdbaciti();

            if (OdigranoPozicija1.transform.childCount == 1 && OdigranoPozicija2.transform.childCount == 1 && OdigranoPozicija3.transform.childCount == 1 && OdigranoPozicija4.transform.childCount == 1)
            {
                StartCoroutine(PocistiPlocuNakonIgreDelay());
            }
        }
        else if(Igrac1.transform.childCount == 0 && Igrac2.transform.childCount == 0 && Igrac3.transform.childCount == 0 && Igrac4.transform.childCount == 0)
        {
          
            StartCoroutine(RestartajIgruZasljedeciKrugPartije());
            

        }
    }

    public void ZoviZvanja()
    {
        
        
        AutomatskiProvjeriZvanjaIZovi();
    }

    public void AutomatskiProvjeriZvanjaIZovi()
    {
        int NajveceZvanjeIgrac1 = VratiZvanjeAkoPostojiKodPojedinogIgraca(Igrac1).NajveceZvanje;
        int NajveceZvanjeIgrac2 = VratiZvanjeAkoPostojiKodPojedinogIgraca(Igrac2).NajveceZvanje;
        int NajveceZvanjeIgrac3 = VratiZvanjeAkoPostojiKodPojedinogIgraca(Igrac3).NajveceZvanje;
        int NajveceZvanjeIgrac4 = VratiZvanjeAkoPostojiKodPojedinogIgraca(Igrac4).NajveceZvanje;
        int[] SvaZvanja = { NajveceZvanjeIgrac1, NajveceZvanjeIgrac2, NajveceZvanjeIgrac3, NajveceZvanjeIgrac4 };
        int NajaceZvanje = 0;
        int BrojacJednakihZvanja = 0;
         print(NajveceZvanjeIgrac1 + "TO JEZVANJE IGRACA 1");
        GameObject NajaciIgrac = null;
        GameObject NajacaKartaUZvanju = null;

        NajaceZvanje = SvaZvanja.Max();
        
        for(int i = 0; i < SvaZvanja.Length;i++)
        {
            if(NajaceZvanje == SvaZvanja[i])
            {
                BrojacJednakihZvanja = BrojacJednakihZvanja + 1;                
            }
        }

        if (BrojacJednakihZvanja == 1)
        {

            int IndeksNajacegIgraca = Array.IndexOf(SvaZvanja, NajaceZvanje);
            if (SvaZvanja[IndeksNajacegIgraca] == NajveceZvanjeIgrac1)
            {
                NajaciIgrac = Igrac1;
            }
            if (SvaZvanja[IndeksNajacegIgraca] == NajveceZvanjeIgrac2)
            {
                NajaciIgrac = Igrac2;
            }
            if (SvaZvanja[IndeksNajacegIgraca] == NajveceZvanjeIgrac3)
            {
                NajaciIgrac = Igrac3;
            }
            if (SvaZvanja[IndeksNajacegIgraca] == NajveceZvanjeIgrac4)
            {
                NajaciIgrac = Igrac4;
            }
        } 
        else if (BrojacJednakihZvanja != 0)
        {
            GameObject NajcaKartaZvanjaIgrac1 = VratiZvanjeAkoPostojiKodPojedinogIgraca(Igrac1).ZadnjaKartaZvanja;
            GameObject NajcaKartaZvanjaIgrac2 = VratiZvanjeAkoPostojiKodPojedinogIgraca(Igrac2).ZadnjaKartaZvanja;
            GameObject NajcaKartaZvanjaIgrac3 = VratiZvanjeAkoPostojiKodPojedinogIgraca(Igrac3).ZadnjaKartaZvanja;
            GameObject NajcaKartaZvanjaIgrac4 = VratiZvanjeAkoPostojiKodPojedinogIgraca(Igrac4).ZadnjaKartaZvanja;
            GameObject[] NajaceKarteZvanja = { NajcaKartaZvanjaIgrac1, NajcaKartaZvanjaIgrac2, NajcaKartaZvanjaIgrac3, NajcaKartaZvanjaIgrac4 };

            int BrojacKartaJednakogRanga = 1;
            
            
            for (int i = 0; i < NajaceKarteZvanja.Length; i++)
            {
                if(NajacaKartaUZvanju == null)
                {
                    NajacaKartaUZvanju = NajaceKarteZvanja[i];
                }
                else if( NajacaKartaUZvanju != null)
                {
                    GameObject PrivremenaKarta = NajaceKarteZvanja[i];
                    Ai AiPrivremenaKarta = PrivremenaKarta.GetComponent<Ai>();
                    Ai AiNajacaKataUZvanju = NajacaKartaUZvanju.GetComponent<Ai>();
                    if( AiPrivremenaKarta.RangZvanja > AiNajacaKataUZvanju.RangZvanja)
                    {
                        NajacaKartaUZvanju = PrivremenaKarta;
                    }
                    else if (AiPrivremenaKarta.RangZvanja == AiNajacaKataUZvanju.RangZvanja)
                    {
                        BrojacKartaJednakogRanga = BrojacKartaJednakogRanga + 1;
                        /*Tu fali cijeli dio što bi se trebalo dogoditi ako igrač imaju isto zvanje sa istom jačinom znači treba igrač koje je prije od njega na redu imati zvanje*/
                    }
                }
            }

            int IndeksNajacegIgracaKarta = Array.IndexOf(NajaceKarteZvanja, NajacaKartaUZvanju);
            if (NajaceKarteZvanja[IndeksNajacegIgracaKarta] == NajcaKartaZvanjaIgrac1)
            {
                NajaciIgrac = Igrac1;
            }
            if (NajaceKarteZvanja[IndeksNajacegIgracaKarta] == NajcaKartaZvanjaIgrac2)
            {
                NajaciIgrac = Igrac2;
            }
            if (NajaceKarteZvanja[IndeksNajacegIgracaKarta] == NajcaKartaZvanjaIgrac3)
            {
                NajaciIgrac = Igrac3;
            }
            if (NajaceKarteZvanja[IndeksNajacegIgracaKarta] == NajcaKartaZvanjaIgrac4)
            {
                NajaciIgrac = Igrac4;
            }


        }
        GameObject SuigracNajacegaIgraca = null ;

        if (NajaciIgrac == Igrac1)
        {
            SuigracNajacegaIgraca = Igrac3;
        }
        if (NajaciIgrac == Igrac2 )
        {
            SuigracNajacegaIgraca = Igrac4;
        }
        if (NajaciIgrac == Igrac3)
        {
            SuigracNajacegaIgraca = Igrac1;
        }
        if (NajaciIgrac == Igrac4)
        {
            SuigracNajacegaIgraca = Igrac2;
        }


/*-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------*/


        List<GameObject> KarteKojeSeZovu = VratiZvanjeAkoPostojiKodPojedinogIgraca(NajaciIgrac).KarteKojeSuUZvanju;
        List<GameObject> KarteKojeSeZovu2 = VratiZvanjeAkoPostojiKodPojedinogIgraca(SuigracNajacegaIgraca).KarteKojeSuUZvanju;
        KarteKojeSeZovu.AddRange(KarteKojeSeZovu2);
        
        for(int i = 0; i < KarteKojeSeZovu.Count; i++)
        {
            GameObject Karta = KarteKojeSeZovu[i];
            Selectable SelectebleKarta = Karta.GetComponent<Selectable>();
            SelectebleKarta.KartaOkrenutaPremaGore = true;

            Karta.transform.position = new Vector3(Karta.transform.position.x , Karta.transform.position.y, Karta.transform.position.z-0.9f );
        }
        /*
        yield return new WaitForSecondsRealtime(5);
        

        for (int i = 0; i < KarteKojeSeZovu.Count; i++)
        {
            GameObject Karta = KarteKojeSeZovu[i];
            Selectable SelectebleKarta = Karta.GetComponent<Selectable>();
            SelectebleKarta.KartaOkrenutaPremaGore = false;

            Karta.transform.position = new Vector3(Karta.transform.position.x - 5f, Karta.transform.position.y, Karta.transform.position.z + 5f);
        }*/

        int SveukupnoZvanjaMi = VratiZvanjeAkoPostojiKodPojedinogIgraca(Igrac1).SveukupnaZvanja + VratiZvanjeAkoPostojiKodPojedinogIgraca(Igrac3).SveukupnaZvanja;
        int SveukupnoZvanjaVi = VratiZvanjeAkoPostojiKodPojedinogIgraca(Igrac2).SveukupnaZvanja + VratiZvanjeAkoPostojiKodPojedinogIgraca(Igrac4).SveukupnaZvanja;

        if( NajaciIgrac == Igrac1 || NajaciIgrac == Igrac3)
        {
           Rezultat1i3.SetText(SveukupnoZvanjaMi.ToString());
            Rezultat2i4.SetText("0");
        }
        else if(NajaciIgrac == Igrac2 || NajaciIgrac == Igrac4)
        {
           Rezultat2i4.SetText(SveukupnoZvanjaVi.ToString());
           Rezultat1i3.SetText("0");
        }
        
       
       for(int i = 0; i< KarteKojeSeZovu.Count; i++)
        {
            print(KarteKojeSeZovu[i].name);
        }

        print("Najace Zvanje = " + NajaceZvanje);
        print("Najaci Igrac = " +  NajaciIgrac);
        print("Najaca Karta u Zvanju" + NajacaKartaUZvanju);
        print("Najaci Suigrac " + SuigracNajacegaIgraca);

    }

   public (int SveukupnaZvanja, int NajveceZvanje, GameObject ZadnjaKartaZvanja, bool ZvanjaPostoje, List<GameObject> KarteKojeSuUZvanju) VratiZvanjeAkoPostojiKodPojedinogIgraca(GameObject Igrac)
    {
        
        List<GameObject> SveKarteURuciIgraca = new List<GameObject>();
        List<GameObject> KarteKojeSuUZvanju = new List<GameObject>();

        for (int i = 0; i < Igrac.transform.childCount; i++)
        {
            GameObject KartaURuci = Igrac.transform.GetChild(i).gameObject;
            SveKarteURuciIgraca.Add(KartaURuci);
        }
        List<GameObject> KartaSrc= new List<GameObject>();
        List<GameObject> KartaTik = new List<GameObject>();
        List<GameObject> KartaZel = new List<GameObject>();
        List<GameObject> KartaZir = new List<GameObject>();
        for( int i=0;i<SveKarteURuciIgraca.Count;i++)
        {
            GameObject Karta = SveKarteURuciIgraca[i];
            Ai KartaAi = Karta.GetComponent<Ai>();
            if (KartaAi.BojaKarte == "Src")
            {
                KartaSrc.Add(Karta);
               // print("Karte Srce " + Karta.name);
            }
            else if (KartaAi.BojaKarte == "Tik")
            {
                KartaTik.Add(Karta);
            }
            else if (KartaAi.BojaKarte == "Zel")
            {
                KartaZel.Add(Karta);
            }
            else if (KartaAi.BojaKarte == "Zir")
            {
                KartaZir.Add(Karta);
            }

        }
        //print(Igrac.name);
        //KartaSrc.ForEach(print);
        int NajveceZvanje=0;       
        int SveukupnaZvanja=0;
        GameObject NajvecaKartaZvanja = null;
        bool ZvanjaPostoje = false;

        if(ZvanjaUIstojBoji(KartaSrc).ZvanjaPostoje == true)
        {
            SveukupnaZvanja = SveukupnaZvanja + ZvanjaUIstojBoji(KartaSrc).SveukupnaZvanja;
            if (ZvanjaUIstojBoji(KartaSrc).NajveceZvanje > NajveceZvanje)
            {
                NajveceZvanje = ZvanjaUIstojBoji(KartaSrc).NajveceZvanje;
                if (NajvecaKartaZvanja == null)
                {
                    NajvecaKartaZvanja = ZvanjaUIstojBoji(KartaSrc).ZadnjaKartaZvanja;
                }
                else
                {
                    Ai AiNajvecaKartaZvanja = NajvecaKartaZvanja.GetComponent<Ai>();
                    GameObject PrivremenaKarta = ZvanjaUIstojBoji(KartaSrc).ZadnjaKartaZvanja;
                    Ai AiPrivremenaKarta = PrivremenaKarta.GetComponent<Ai>();
                    if (AiNajvecaKartaZvanja.RangZvanja < AiPrivremenaKarta.RangZvanja)
                    {
                        NajvecaKartaZvanja = PrivremenaKarta;
                    }
                }

            }
            List<GameObject> PrivremenaListaKarta = ZvanjaUIstojBoji(KartaSrc).KarteKojeSuUZvanju;
            for(int i = 0; i < PrivremenaListaKarta.Count;i++)
            {
                KarteKojeSuUZvanju.Add(PrivremenaListaKarta[i]);
            }
            
        }
        if (ZvanjaUIstojBoji(KartaTik).ZvanjaPostoje == true)
        {
            SveukupnaZvanja = SveukupnaZvanja + ZvanjaUIstojBoji(KartaTik).SveukupnaZvanja;
            if (ZvanjaUIstojBoji(KartaTik).NajveceZvanje > NajveceZvanje)
            {
                NajveceZvanje = ZvanjaUIstojBoji(KartaTik).NajveceZvanje;
                if (NajvecaKartaZvanja == null)
                {
                    NajvecaKartaZvanja = ZvanjaUIstojBoji(KartaTik).ZadnjaKartaZvanja;
                }
                else
                {
                    Ai AiNajvecaKartaZvanja = NajvecaKartaZvanja.GetComponent<Ai>();
                    GameObject PrivremenaKarta = ZvanjaUIstojBoji(KartaTik).ZadnjaKartaZvanja;
                    Ai AiPrivremenaKarta = PrivremenaKarta.GetComponent<Ai>();
                    if (AiNajvecaKartaZvanja.RangZvanja < AiPrivremenaKarta.RangZvanja)
                    {
                        NajvecaKartaZvanja = PrivremenaKarta;
                    }
                }

            }
            List<GameObject> PrivremenaListaKarta = ZvanjaUIstojBoji(KartaTik).KarteKojeSuUZvanju;
            for (int i = 0; i < PrivremenaListaKarta.Count; i++)
            {
                KarteKojeSuUZvanju.Add(PrivremenaListaKarta[i]);
            }
        }
        if (ZvanjaUIstojBoji(KartaZel).ZvanjaPostoje == true)
        {
            SveukupnaZvanja = SveukupnaZvanja + ZvanjaUIstojBoji(KartaZel).SveukupnaZvanja;
            if (ZvanjaUIstojBoji(KartaZel).NajveceZvanje > NajveceZvanje)
            {
                NajveceZvanje = ZvanjaUIstojBoji(KartaZel).NajveceZvanje;
                if (NajvecaKartaZvanja == null)
                {
                    NajvecaKartaZvanja = ZvanjaUIstojBoji(KartaZel).ZadnjaKartaZvanja;
                }
                else
                {
                    Ai AiNajvecaKartaZvanja = NajvecaKartaZvanja.GetComponent<Ai>();
                    GameObject PrivremenaKarta = ZvanjaUIstojBoji(KartaZel).ZadnjaKartaZvanja;
                    Ai AiPrivremenaKarta = PrivremenaKarta.GetComponent<Ai>();
                    if (AiNajvecaKartaZvanja.RangZvanja < AiPrivremenaKarta.RangZvanja)
                    {
                        NajvecaKartaZvanja = PrivremenaKarta;
                    }
                }

            }
            List<GameObject> PrivremenaListaKarta = ZvanjaUIstojBoji(KartaZel).KarteKojeSuUZvanju;
            for (int i = 0; i < PrivremenaListaKarta.Count; i++)
            {
                KarteKojeSuUZvanju.Add(PrivremenaListaKarta[i]);
            }
        }
        if (ZvanjaUIstojBoji(KartaZir).ZvanjaPostoje == true)
        {
            SveukupnaZvanja = SveukupnaZvanja + ZvanjaUIstojBoji(KartaZir).SveukupnaZvanja;
            if (ZvanjaUIstojBoji(KartaZir).NajveceZvanje > NajveceZvanje)
            {
                NajveceZvanje = ZvanjaUIstojBoji(KartaZir).NajveceZvanje;
                if (NajvecaKartaZvanja == null)
                {
                    NajvecaKartaZvanja = ZvanjaUIstojBoji(KartaZir).ZadnjaKartaZvanja;
                }
                else
                {
                    Ai AiNajvecaKartaZvanja = NajvecaKartaZvanja.GetComponent<Ai>();
                    GameObject PrivremenaKarta = ZvanjaUIstojBoji(KartaZir).ZadnjaKartaZvanja;
                    Ai AiPrivremenaKarta = PrivremenaKarta.GetComponent<Ai>();
                    if (AiNajvecaKartaZvanja.RangZvanja < AiPrivremenaKarta.RangZvanja)
                    {
                        NajvecaKartaZvanja = PrivremenaKarta;
                    }
                }

            }
            List<GameObject> PrivremenaListaKarta = ZvanjaUIstojBoji(KartaZir).KarteKojeSuUZvanju;
            for (int i = 0; i < PrivremenaListaKarta.Count; i++)
            {
                KarteKojeSuUZvanju.Add(PrivremenaListaKarta[i]);
            }
        }
        if (ZvanjaCetriISteKarte(KartaSrc, KartaTik, KartaZel, KartaZir).ZvanjaPostoje == true)
        {
            SveukupnaZvanja =SveukupnaZvanja+ ZvanjaCetriISteKarte(KartaSrc, KartaTik, KartaZel, KartaZir).SveukupnaZvanja;
            

            if(ZvanjaCetriISteKarte(KartaSrc, KartaTik, KartaZel, KartaZir).NajveceZvanje > NajveceZvanje)
            {
                NajveceZvanje = ZvanjaCetriISteKarte(KartaSrc, KartaTik, KartaZel, KartaZir).NajveceZvanje;
                NajvecaKartaZvanja = ZvanjaCetriISteKarte(KartaSrc, KartaTik, KartaZel, KartaZir).NajacaKartaZvanja;
            }

            List<GameObject> PrivremenaListaKarta = ZvanjaCetriISteKarte(KartaSrc, KartaTik, KartaZel, KartaZir).KarteKojeSuUZvanju;
            for (int i = 0; i < PrivremenaListaKarta.Count; i++)
            {
                KarteKojeSuUZvanju.Add(PrivremenaListaKarta[i]);
            }
        }

        if (SveukupnaZvanja != 0)
        {
            ZvanjaPostoje = true;
        }

        return (SveukupnaZvanja,NajveceZvanje,NajvecaKartaZvanja, ZvanjaPostoje,KarteKojeSuUZvanju);
    }
    // Treba doraditi da u istoj boji se može dva put zvati 20 ili više ako dođe do toga
   public (int SveukupnaZvanja, int NajveceZvanje, GameObject ZadnjaKartaZvanja, bool ZvanjaPostoje,List<GameObject> KarteKojeSuUZvanju) ZvanjaUIstojBoji(List<GameObject> UnesiListuKarata)
    {
        int NajveceZvanje =0;
        int TrenutnoZvanje = 0;
        int SveukupnaZvanja =0;
        GameObject ZadnjaKartaZvanja=null;
        int ZadnjaKartaZvanjaRang =0;
        bool ZvanjaPostoje = false;
        List<GameObject> KarteKojeSuUZvanju = new List<GameObject>();

        List<int> KarteRang = new List<int>();
        foreach (GameObject Karta in UnesiListuKarata)
        {
            Ai KartaAi = Karta.GetComponent<Ai>();
            KarteRang.Add(KartaAi.RangZvanja);
        }
        KarteRang.Sort();

        if(KarteRang.Count < 3)
        {
            return (0, 0, null,false,null);
        }
     

        // Dvije liste za slučaj duplog zvanja u istoj boji
            List<int> KarteZaZvanje = new List<int>();
            List<int> KarteZaZvanje2 = new List<int>();

        
        int BrojcKartaZaZvanje = 0;
        int BrojcKartaZaZvanje2 = 0;
        for (int i = 0; i < KarteRang.Count-1; i++)
        {
            
            int Rang1 = KarteRang[i];          
            int Rang2 = KarteRang[i+1];
            if (Rang1 + 1 == Rang2 && BrojcKartaZaZvanje < 6)
            {
                KarteZaZvanje.Add(Rang1);
                KarteZaZvanje.Add(Rang2);
                BrojcKartaZaZvanje = BrojcKartaZaZvanje + 1;
            }
            else if (BrojcKartaZaZvanje < 3 )
            { 
                    KarteZaZvanje.Clear();
                    BrojcKartaZaZvanje = 0;
              
            }
            else if (BrojcKartaZaZvanje < 6 && BrojcKartaZaZvanje >2 && Rang1 + 1 == Rang2)
            {
                KarteZaZvanje2.Add(Rang1);
                KarteZaZvanje2.Add(Rang2);
                BrojcKartaZaZvanje2 = BrojcKartaZaZvanje2 + 1;
            }
            else if (BrojcKartaZaZvanje2 < 3)
            {
                KarteZaZvanje2.Clear();
                BrojcKartaZaZvanje2 = 0;

            }

        }
       
        for (int i = 0; i < KarteZaZvanje.Count - 1; i++)
        {

            int Rang1 = KarteZaZvanje[i];
            int Rang2 = KarteZaZvanje[i + 1];
            if (Rang1 == Rang2)
            {
                KarteZaZvanje.RemoveAt(i);

            }

        }

        for (int i = 0; i < KarteZaZvanje2.Count - 1; i++)
        {

            int Rang1 = KarteZaZvanje2[i];
            int Rang2 = KarteZaZvanje2[i + 1];
            if (Rang1 == Rang2)
            {
                KarteZaZvanje2.RemoveAt(i);

            }

        }

        if (KarteZaZvanje.Count == 3 )
        {
            TrenutnoZvanje = 20;
            if (NajveceZvanje < TrenutnoZvanje)
            {
                NajveceZvanje = 20;
                ZadnjaKartaZvanjaRang = KarteZaZvanje[2];
            }
            SveukupnaZvanja = SveukupnaZvanja + 20;
        }
        else if (KarteZaZvanje.Count == 4)
        {
            TrenutnoZvanje = 50;
            if (NajveceZvanje < TrenutnoZvanje)
            {
                NajveceZvanje = 50;
                ZadnjaKartaZvanjaRang = KarteZaZvanje[3];
            }
            SveukupnaZvanja = SveukupnaZvanja + 50;
        }
        else if (KarteZaZvanje.Count == 5)
        {
            TrenutnoZvanje = 100;
            if (NajveceZvanje < TrenutnoZvanje)
            {
                NajveceZvanje = 100;
                ZadnjaKartaZvanjaRang = KarteZaZvanje[4];
            }
            SveukupnaZvanja = SveukupnaZvanja + 100;
        }
        else if (KarteZaZvanje.Count == 8)
        {
            TrenutnoZvanje = 1001;
            if (NajveceZvanje < TrenutnoZvanje)
            {
                NajveceZvanje = 1001;
                ZadnjaKartaZvanjaRang = KarteZaZvanje[7];
            }
            SveukupnaZvanja = SveukupnaZvanja + 1001;
        }

        if (KarteZaZvanje2.Count != 0)
        {
            if (KarteZaZvanje2.Count == 3)
            {
                TrenutnoZvanje = 20;
                if (NajveceZvanje < TrenutnoZvanje)
                {
                    NajveceZvanje = 20;
                    ZadnjaKartaZvanjaRang = KarteZaZvanje2[2];
                }
                SveukupnaZvanja = SveukupnaZvanja + 20;
            }
            else if (KarteZaZvanje.Count == 4)
            {
                TrenutnoZvanje = 50;
                if (NajveceZvanje < TrenutnoZvanje)
                {
                    NajveceZvanje = 50;
                    ZadnjaKartaZvanjaRang = KarteZaZvanje2[3];
                }
                SveukupnaZvanja = SveukupnaZvanja + 50;
            }
            else if (KarteZaZvanje.Count == 5)
            {
                TrenutnoZvanje = 100;
                if (NajveceZvanje < TrenutnoZvanje)
                {
                    NajveceZvanje = 100;
                    ZadnjaKartaZvanjaRang = KarteZaZvanje2[4];
                }
                SveukupnaZvanja = SveukupnaZvanja + 100;
            }
            else if (KarteZaZvanje.Count == 8)
            {
                TrenutnoZvanje = 1001;
                if (NajveceZvanje < TrenutnoZvanje)
                {
                    NajveceZvanje = 1001;
                    ZadnjaKartaZvanjaRang = KarteZaZvanje2[7];
                }
                SveukupnaZvanja = SveukupnaZvanja + 1001;
            }
        }


        KarteZaZvanje.AddRange(KarteZaZvanje2);

        for (int i = 0; i < KarteZaZvanje.Count; i++)
        {
            int rang = KarteZaZvanje[i];
            for (int a = 0; a < UnesiListuKarata.Count; a++)
            {
                GameObject Karta = UnesiListuKarata[a];
                Ai KartaAi = Karta.GetComponent<Ai>();
                if (rang == KartaAi.RangZvanja)
                {
                    KarteKojeSuUZvanju.Add(Karta);
                }
            }

        }

        if (SveukupnaZvanja != 0)
        {
            ZvanjaPostoje = true;
        }

        for (int a = 0; a < UnesiListuKarata.Count; a++)
        {
            GameObject Karta = UnesiListuKarata[a];
            Ai KartaAi = Karta.GetComponent<Ai>();
            if (ZadnjaKartaZvanjaRang == KartaAi.RangZvanja)
            {
                ZadnjaKartaZvanja = Karta;
            }
        }

        return (SveukupnaZvanja, NajveceZvanje, ZadnjaKartaZvanja,ZvanjaPostoje,KarteKojeSuUZvanju);

    }

    public (int SveukupnaZvanja, int NajveceZvanje, GameObject NajacaKartaZvanja, bool ZvanjaPostoje, List<GameObject> KarteKojeSuUZvanju) ZvanjaCetriISteKarte(List<GameObject> Src, List<GameObject> Tik, List<GameObject> Zel, List<GameObject> Zir)
    {
        int NajveceZvanje = 0;
        int TrenutnoZvanje = 0;
        int SveukupnaZvanja = 0;
        GameObject NajacaKartaZvanja = null;
        bool ZvanjaPostoje = false;
        List<GameObject> KarteKojeSuUZvanju = new List<GameObject>();

        if (Src.Contains(GameObject.Find("SrcDecko")) && Tik.Contains(GameObject.Find("TikDecko")) && Zel.Contains(GameObject.Find("ZelDecko")) && Zir.Contains(GameObject.Find("ZirDecko")))
        {
            TrenutnoZvanje = 200;                         
            NajacaKartaZvanja = GameObject.Find("SrcDecko");

            SveukupnaZvanja = SveukupnaZvanja + TrenutnoZvanje;

            KarteKojeSuUZvanju.Add(GameObject.Find("SrcDecko"));
            KarteKojeSuUZvanju.Add(GameObject.Find("TikDecko"));
            KarteKojeSuUZvanju.Add(GameObject.Find("ZelDecko"));
            KarteKojeSuUZvanju.Add(GameObject.Find("ZirDecko"));
        }
        if (Src.Contains(GameObject.Find("SrcKralj")) && Tik.Contains(GameObject.Find("TikKralj")) && Zel.Contains(GameObject.Find("ZelKralj")) && Zir.Contains(GameObject.Find("ZirKralj")))
        {
            TrenutnoZvanje = 100;
            if (TrenutnoZvanje > NajveceZvanje)
            {
                NajveceZvanje = TrenutnoZvanje;
                NajacaKartaZvanja = GameObject.Find("SrcKralj");
            }

            SveukupnaZvanja = SveukupnaZvanja + TrenutnoZvanje;

            KarteKojeSuUZvanju.Add(GameObject.Find("SrcKralj"));
            KarteKojeSuUZvanju.Add(GameObject.Find("TikKralj"));
            KarteKojeSuUZvanju.Add(GameObject.Find("ZelKralj"));
            KarteKojeSuUZvanju.Add(GameObject.Find("ZirKralj"));
        }
        if (Src.Contains(GameObject.Find("SrcBaba")) && Tik.Contains(GameObject.Find("TikBaba")) && Zel.Contains(GameObject.Find("ZelBaba")) && Zir.Contains(GameObject.Find("ZirBaba")))
        {
            TrenutnoZvanje = 100;
            if (TrenutnoZvanje > NajveceZvanje)
            {
                NajveceZvanje = TrenutnoZvanje;
                NajacaKartaZvanja = GameObject.Find("SrcBaba");
            }
            SveukupnaZvanja = SveukupnaZvanja + TrenutnoZvanje;

            KarteKojeSuUZvanju.Add(GameObject.Find("SrcBaba"));
            KarteKojeSuUZvanju.Add(GameObject.Find("TikBaba"));
            KarteKojeSuUZvanju.Add(GameObject.Find("ZelBaba"));
            KarteKojeSuUZvanju.Add(GameObject.Find("ZirBaba"));
        }
        if (Src.Contains(GameObject.Find("Src10")) && Tik.Contains(GameObject.Find("Tik10")) && Zel.Contains(GameObject.Find("Zel10")) && Zir.Contains(GameObject.Find("Zir10")))
        {
            TrenutnoZvanje = 100;
            if (TrenutnoZvanje > NajveceZvanje)
            {
                NajveceZvanje = TrenutnoZvanje;
                NajacaKartaZvanja = GameObject.Find("Src10");
            }
            SveukupnaZvanja = SveukupnaZvanja + TrenutnoZvanje;

            KarteKojeSuUZvanju.Add(GameObject.Find("Src10"));
            KarteKojeSuUZvanju.Add(GameObject.Find("Tik10"));
            KarteKojeSuUZvanju.Add(GameObject.Find("Zel10"));
            KarteKojeSuUZvanju.Add(GameObject.Find("Zir10"));
        }
        if (Src.Contains(GameObject.Find("Src9")) && Tik.Contains(GameObject.Find("Tik9")) && Zel.Contains(GameObject.Find("Zel9")) && Zir.Contains(GameObject.Find("Zir9")))
        {         
            TrenutnoZvanje = 150;
            if (TrenutnoZvanje > NajveceZvanje)
            {
                NajveceZvanje = TrenutnoZvanje;
            
            }
            SveukupnaZvanja = SveukupnaZvanja + TrenutnoZvanje;

            KarteKojeSuUZvanju.Add(GameObject.Find("Src9"));
            KarteKojeSuUZvanju.Add(GameObject.Find("Tik9"));
            KarteKojeSuUZvanju.Add(GameObject.Find("Zel9"));
            KarteKojeSuUZvanju.Add(GameObject.Find("Zir9"));
        }
        if(SveukupnaZvanja != 0)
        {
            ZvanjaPostoje = true;
        }

        return (SveukupnaZvanja, NajveceZvanje, NajacaKartaZvanja, ZvanjaPostoje, KarteKojeSuUZvanju);
    }

    public int VratiVrijednostKarataIgraci(GameObject Igraci)
    {
       int  UkupaVrijednostKarataNaKaraju=0;
        if( Igraci.transform.childCount != null)
        {
            for(int i=0;i< Igraci.transform.childCount; i++)
            {
                GameObject Karta = Igraci.transform.GetChild(i).gameObject;
                Ai VrijednostTrenutne = Karta.GetComponent<Ai>();
                UkupaVrijednostKarataNaKaraju = UkupaVrijednostKarataNaKaraju + VrijednostTrenutne.VrijednostKarte;

            }
        }

        return UkupaVrijednostKarataNaKaraju;
    }
    
    public IEnumerator RestartajIgruZasljedeciKrugPartije()
    {
        yield return new WaitForSecondsRealtime(2);

        if (Igrac1.transform.childCount == 0 && Igrac2.transform.childCount == 0 && Igrac3.transform.childCount == 0 && Igrac4.transform.childCount == 0)
        {
            Rezultati.PostaviNoviRezultatMi(VratiVrijednostKarataIgraci(IgraciJedanITri));
            Rezultati.PostaviNoviRezultatVi(VratiVrijednostKarataIgraci(IgraciDvaiCetri));
            SceneManager.LoadScene(SljedecaScena);
            enabled = false;
        }
         
       
    }
    public void PozvanoSrce(GameObject IgracKojiJeZvaoSrce)
    {
        if (BojaAduta == "Src") {
        IgracKojiJeZvao.SetText(IgracKojiJeZvaoSrce.name);
        SlikaAduta.sprite = SlikeAduta[0];
        TkoJeZvao.SetActive(true); 
        }
    }
    public void PozvanoTikva(GameObject IgracKojiJeZvaoTikvu)
    {
        if (BojaAduta == "Tik")
        {
            IgracKojiJeZvao.SetText(IgracKojiJeZvaoTikvu.name);
            SlikaAduta.sprite = SlikeAduta[1];
            TkoJeZvao.SetActive(true);
        }
    }
    public void PozvanoZelje(GameObject IgracKojiJeZvaoZelje)
    {
        if (BojaAduta == "Zel")
        {
            IgracKojiJeZvao.SetText(IgracKojiJeZvaoZelje.name);
            SlikaAduta.sprite = SlikeAduta[2];
            TkoJeZvao.SetActive(true);
        }
    }
    public void PozvanoZir(GameObject IgracKojiJeZvaoZir)
    {
        if (BojaAduta == "Zir")
        {
            IgracKojiJeZvao.SetText(IgracKojiJeZvaoZir.name);
            SlikaAduta.sprite = SlikeAduta[3];
            TkoJeZvao.SetActive(true);
        }
    }

    public IEnumerator PocistiPlocuNakonIgreDelay()
    {
        yield return new WaitForSecondsRealtime(1);
        OdluciKojiIgracJeNaReduITajIgracIgra(PocistiPlocuIDajKartePobjedniku());
    }
    
    
    public void AiZovi(GameObject IgracKojiZove)
    {
        IgracKojiPrviZove = IgracKojiZove;
        AiAkoJeNaReduZaZvanjeZove(IgracKojiZove);     
        
        if(BojaAduta != "Src" && BojaAduta != "Tik" && BojaAduta != "Zel" && BojaAduta != "Zir")
        {
         AiAkoJeNaReduZaZvanjeZove(IgracKojiZoveSljedeci);
            if (BojaAduta != "Src" && BojaAduta != "Tik" && BojaAduta != "Zel" && BojaAduta != "Zir")
            {
                AiAkoJeNaReduZaZvanjeZove(IgracKojiZoveSljedeci);
                if (BojaAduta != "Src" && BojaAduta != "Tik" && BojaAduta != "Zel" && BojaAduta != "Zir")
                {
                    AiAkoJeNaReduZaZvanjeZove(IgracKojiZoveSljedeci);

                }
            }
        }
        
        
    }
    
    public void ZoviDalje(GameObject IgracKojiJeSadNaReduZaZvanje )
    {
        if (IgracKojiTrenutnoZove != null)
        {
            IgracKojiJeSadNaReduZaZvanje = IgracKojiTrenutnoZove;
        }
        GameObject SljedeciIgrac = null;
       
            if (IgracKojiJeSadNaReduZaZvanje == Igrac1)
            {               
                    GameObject PlocaZaZvanje = GameObject.Find("ZoviAdut");
                    PlocaZaZvanje.SetActive(true);
            }
           else if (IgracKojiJeSadNaReduZaZvanje == Igrac2)
            {
                SljedeciIgrac = Igrac3;
            }
            else if (IgracKojiJeSadNaReduZaZvanje == Igrac3)
            {
                SljedeciIgrac = Igrac4;
            }
            else if (IgracKojiJeSadNaReduZaZvanje == Igrac4)
            {
                SljedeciIgrac = Igrac1;
            }
        IgracKojiZoveSljedeci = SljedeciIgrac;      
               
    }

    public void AiAkoJeNaReduZaZvanjeZove(GameObject IgracNaReduZaZvanje)
    {
        IgracKojiTrenutnoZove = IgracNaReduZaZvanje;
        int VrijednostSrc = 0;
        int VrijednostZir = 0;
        int VrijednostTik = 0;
        int VrijednostZel = 0;

        int BrojKartaSrc = 0;
        int BrojKartaZir = 0;
        int BrojKartaZel = 0;
        int BrojKartaTik = 0;
        

        
        List<GameObject> SveKarteURuciIgraca = new List<GameObject>();
        
        for(int i = 0; i < IgracNaReduZaZvanje.transform.childCount-2; i++)
        {
            GameObject KartaURuci = IgracNaReduZaZvanje.transform.GetChild(i).gameObject;
            SveKarteURuciIgraca.Add(KartaURuci);
            
        }
        foreach(GameObject Karta in SveKarteURuciIgraca) {
            Ai KartaAi = Karta.GetComponent<Ai>();
            if(KartaAi.BojaKarte == "Src")
            {
                BrojKartaSrc = BrojKartaSrc + 1;
                if (KartaAi.NazivKarte != "Decko" && KartaAi.NazivKarte != "9")
                {
                    VrijednostSrc = VrijednostSrc + KartaAi.VrijednostKarte;
                }
                else if (KartaAi.NazivKarte == "Decko")
                {
                    VrijednostSrc = VrijednostSrc + 20;
                }
                else if (KartaAi.NazivKarte == "9")
                {
                    VrijednostSrc = VrijednostSrc + 14;
                }
            }
            else if (KartaAi.BojaKarte == "Zir")
            {
                BrojKartaZir = BrojKartaZir + 1;
                if (KartaAi.NazivKarte != "Decko" && KartaAi.NazivKarte != "9")
                {
                    VrijednostZir = VrijednostZir + KartaAi.VrijednostKarte;
                }
                else if (KartaAi.NazivKarte == "Decko")
                {
                    VrijednostZir = VrijednostZir + 20;
                }
                else if (KartaAi.NazivKarte == "9")
                {
                    VrijednostZir = VrijednostZir + 14;
                }
            }
            else if (KartaAi.BojaKarte == "Tik")
            {
                BrojKartaTik = BrojKartaTik + 1;
                if (KartaAi.NazivKarte != "Decko" && KartaAi.NazivKarte != "9")
                {
                    VrijednostTik = VrijednostTik + KartaAi.VrijednostKarte;
                }
                else if (KartaAi.NazivKarte == "Decko")
                {
                    VrijednostTik = VrijednostTik + 20;
                }
                else if (KartaAi.NazivKarte == "9")
                {
                    VrijednostTik = VrijednostTik + 14;
                }
            }
            else if (KartaAi.BojaKarte == "Zel")
            {
                BrojKartaZel = BrojKartaZel + 1;
                if (KartaAi.NazivKarte != "Decko" && KartaAi.NazivKarte != "9")
                {
                    VrijednostZel = VrijednostZel + KartaAi.VrijednostKarte;
                }
                else if (KartaAi.NazivKarte == "Decko")
                {
                    VrijednostZel = VrijednostZel + 20;
                }
                else if (KartaAi.NazivKarte == "9")
                {
                    VrijednostZel = VrijednostZel + 14;
                }
            }
        }
        if (IgracKojiTrenutnoZove != Igrac1)
        {
            if (VrijednostSrc > 40 || BrojKartaSrc > 4)
            {
                PostaviVrijednostiAduta("Src");
                PozvanoSrce(IgracKojiTrenutnoZove);
            }
            else if (VrijednostTik > 40 || BrojKartaTik > 5)
            {
                PostaviVrijednostiAduta("Tik");
                PozvanoTikva(IgracKojiTrenutnoZove);

            }
            else if (VrijednostZel > 40 || BrojKartaZel > 5)
            {
                PostaviVrijednostiAduta("Zel");
                PozvanoZelje(IgracKojiTrenutnoZove);

            }
            else if (VrijednostZir > 40 || BrojKartaZir > 5)
            {
                PostaviVrijednostiAduta("Zir");
                PozvanoZir(IgracKojiTrenutnoZove);

            }

            else
            {
                ZoviDalje(IgracNaReduZaZvanje);
                if (IgracKojiZoveSljedeci == Igrac4)
                {
                    int[] SveVrijednosti = { VrijednostSrc, VrijednostTik, VrijednostZel, VrijednostZir };
                    int NajvecaVrijednost = 0;
                    int IndexNajveceVrijednosti = 0;
                    for(int i=0;i < SveVrijednosti.Length; i++)
                    {
                        if (SveVrijednosti[i] > NajvecaVrijednost)
                        {
                            NajvecaVrijednost = SveVrijednosti[i];
                            IndexNajveceVrijednosti = i;
                        }
                    }
                    if(IndexNajveceVrijednosti == 1)
                    {
                        PostaviVrijednostiAduta("Src");
                        PozvanoSrce(IgracKojiTrenutnoZove);
                    }
                    else if (IndexNajveceVrijednosti == 2)
                    {
                        PostaviVrijednostiAduta("Tik");
                        PozvanoTikva(IgracKojiTrenutnoZove);
                    }
                    else if (IndexNajveceVrijednosti == 3)
                    {
                        PostaviVrijednostiAduta("Zel");
                        PozvanoZelje(IgracKojiTrenutnoZove);
                    }
                    else if (IndexNajveceVrijednosti == 4)
                    {
                        PostaviVrijednostiAduta("Zir");
                        PozvanoZir(IgracKojiTrenutnoZove);
                    }
                    print("Zvao je na mus");
                }
                
                  
                
            }
        }
        else if (IgracKojiTrenutnoZove == Igrac1)
        {
            GameObject PlocaZaZvanje = GameObject.Find("ZoviAdut");
            PlocaZaZvanje.SetActive(true);
        }
        print(IgracNaReduZaZvanje+" Zvao Je >>>>" + BojaAduta +"<<<<<<" );
    }

    public void AiIgracIgraSamPrvu(GameObject IgracKojiIgra)
    {

        if(IgracKojiIgra != null)
        {
            GameObject PozicijaNaKojuIgracOdigra = null;
            GameObject NajvecaKarta=null;
            GameObject NajmanjaKarta = null;
            GameObject NajveciAdut = null;
            GameObject NajmanjiAdut = null;
            string ImePozicije = "OdigranoPozicija" + IgracKojiIgra.name.Substring(IgracKojiIgra.name.Length - 1);
            print("Ime pozicije koja se trenutno igra:" + ImePozicije);          
            PozicijaNaKojuIgracOdigra = GameObject.Find(ImePozicije).gameObject;                     
            print("Ime pozicije koja se trenutno igra:" + PozicijaNaKojuIgracOdigra.name);
            for (int i=0;i< IgracKojiIgra.transform.childCount; i++)
            {
                GameObject TrenutnaKarta = IgracKojiIgra.transform.GetChild(i).gameObject;
                Ai TrenutnaKartaAi = TrenutnaKarta.GetComponent<Ai>();
                if(NajvecaKarta == null)
                {
                    if (TrenutnaKartaAi.Adut == false)
                    {
                        NajvecaKarta = TrenutnaKarta;
                    }                  
                }else if(NajvecaKarta != null)
                {
                    Ai NajvecaKartaAi = NajvecaKarta.GetComponent<Ai>();
                    if(TrenutnaKartaAi.Adut== false && NajvecaKartaAi.RangKarte > TrenutnaKartaAi.RangKarte)
                    {
                       if( NajmanjaKarta == null)
                        {
                            NajmanjaKarta = TrenutnaKarta;
                        }
                       else if( NajmanjaKarta != null)
                        {
                            Ai NajmnjaKartaAi = NajmanjaKarta.GetComponent<Ai>();
                            if(NajmnjaKartaAi.RangKarte > TrenutnaKartaAi.RangKarte)
                            {
                                NajmanjaKarta = TrenutnaKarta;
                            }
                        }
                    }
                    else if (TrenutnaKartaAi.Adut == false && NajvecaKartaAi.RangKarte < TrenutnaKartaAi.RangKarte)
                    {
                        if (NajmanjaKarta == null)
                        {
                            NajmanjaKarta = NajvecaKarta;
                        }
                        else if (NajmanjaKarta != null)
                        {
                            Ai NajmnjaKartaAi = NajmanjaKarta.GetComponent<Ai>();
                            if (NajmnjaKartaAi.RangKarte > NajvecaKartaAi.RangKarte)
                            {
                                NajmanjaKarta = TrenutnaKarta;
                            }
                        }
                        NajvecaKarta = TrenutnaKarta;
                    }
                    
                }
                else if (NajveciAdut == null)
                {
                    if (TrenutnaKartaAi.Adut == true)
                    {
                        NajveciAdut = TrenutnaKarta;
                    }
                }
                else if (NajveciAdut != null)
                {
                    Ai NajveciAdutAi = NajveciAdut.GetComponent<Ai>();
                    if (TrenutnaKartaAi.Adut == true && NajveciAdutAi.RangKarte > TrenutnaKartaAi.RangKarte)
                    {
                        if (NajmanjiAdut == null)
                        {
                            NajmanjiAdut = TrenutnaKarta;
                        }
                        else if (NajmanjiAdut != null)
                        {
                            Ai NajmnjiAdutAi = NajmanjiAdut.GetComponent<Ai>();
                            if (NajmnjiAdutAi.RangKarte > TrenutnaKartaAi.RangKarte)
                            {
                                NajmanjiAdut = TrenutnaKarta;
                            }
                        }
                    }
                    else if (TrenutnaKartaAi.Adut == false && NajveciAdutAi.RangKarte < TrenutnaKartaAi.RangKarte)
                    {
                        if (NajmanjiAdut == null)
                        {
                            NajmanjiAdut = NajveciAdut;
                        }
                        else if (NajmanjiAdut != null)
                        {
                            Ai NajmnjiAdutAi = NajmanjiAdut.GetComponent<Ai>();
                            if (NajmnjiAdutAi.RangKarte > NajveciAdutAi.RangKarte)
                            {
                                NajmanjiAdut = TrenutnaKarta;
                            }
                        }
                        NajveciAdut = TrenutnaKarta;
                    }

                }

            }
            if( NajveciAdut != null)
            {
                Ai NajveciAdutAi = NajveciAdut.GetComponent<Ai>();
                if (NajveciAdutAi.VrijednostKarte == 20)
                {
                    Selectable PokaziKartu = NajveciAdut.GetComponent<Selectable>();
                    PokaziKartu.KartaOkrenutaPremaGore = true;
                    NajveciAdut.transform.SetParent(PozicijaNaKojuIgracOdigra.transform);
                    NajveciAdut.transform.position = PozicijaNaKojuIgracOdigra.transform.position;
                }                
            }
            else if (NajvecaKarta != null && NajmanjaKarta != null )
            {
                Ai NajvecaKartaAi = NajvecaKarta.GetComponent<Ai>();
                if(NajvecaKartaAi.VrijednostKarte == 11)
                {
                    Selectable PokaziKartu = NajvecaKarta.GetComponent<Selectable>();
                    PokaziKartu.KartaOkrenutaPremaGore = true;
                    print("Pozicija koja bi se sad trebala odigrati" + PozicijaNaKojuIgracOdigra);
                    NajvecaKarta.transform.SetParent(PozicijaNaKojuIgracOdigra.transform);
                    NajvecaKarta.transform.position = PozicijaNaKojuIgracOdigra.transform.position;
                }
                else
                {
                    Selectable PokaziKartu = NajmanjaKarta.GetComponent<Selectable>();
                    PokaziKartu.KartaOkrenutaPremaGore = true;
                    print("Pozicija koja bi se sad trebala odigrati" + PozicijaNaKojuIgracOdigra);
                    NajmanjaKarta.transform.SetParent(PozicijaNaKojuIgracOdigra.transform);
                    NajmanjaKarta.transform.position = PozicijaNaKojuIgracOdigra.transform.position;
                }
            }
            else if(NajmanjaKarta == null && NajvecaKarta != null)
            {
                Selectable PokaziKartu = NajvecaKarta.GetComponent<Selectable>();
                PokaziKartu.KartaOkrenutaPremaGore = true;
                print("Pozicija koja bi se sad trebala odigrati" + PozicijaNaKojuIgracOdigra);
                NajvecaKarta.transform.SetParent(PozicijaNaKojuIgracOdigra.transform);
                NajvecaKarta.transform.position = PozicijaNaKojuIgracOdigra.transform.position;
            } 
            else if ( NajvecaKarta == null && NajvecaKarta == null && NajveciAdut != null)
            {
                if(NajmanjiAdut == null && NajveciAdut != null)
                {
                    Selectable PokaziKartu = NajveciAdut.GetComponent<Selectable>();
                    PokaziKartu.KartaOkrenutaPremaGore = true;
                    NajveciAdut.transform.SetParent(PozicijaNaKojuIgracOdigra.transform);
                    NajveciAdut.transform.position = PozicijaNaKojuIgracOdigra.transform.position;
                }
                else if ( NajmanjiAdut != null && NajveciAdut != null)
                {
                    Selectable PokaziKartu = NajmanjiAdut.GetComponent<Selectable>();
                    PokaziKartu.KartaOkrenutaPremaGore = true;
                    NajmanjiAdut.transform.SetParent(PozicijaNaKojuIgracOdigra.transform);
                    NajmanjiAdut.transform.position = PozicijaNaKojuIgracOdigra.transform.position;
                }
            }
            
        }
    }

   public void OdluciKojiIgracJeNaReduITajIgracIgra(GameObject IgracNaRedu)
    {
        
        if (IgracNaRedu == Igrac2)
        {
            AiIgracIgraSamPrvu(Igrac2);
        }
        if (IgracNaRedu == Igrac3)
        {
            AiIgracIgraSamPrvu(Igrac3);
        }
        if (IgracNaRedu == Igrac4)
        {
            AiIgracIgraSamPrvu(Igrac4);
        }

    }


    private void OnMouseDown()
    {       
        PocistiPlocuIDajKartePobjedniku();            
    }

    IEnumerator ZadrziKod3Sekunde()
    {
        yield return new WaitForSeconds(3f);
    }
   

    public void PremjestiViseKarataNaJeduLokaciju(GameObject KartaJedan, GameObject KartaDva, GameObject KartaTri, GameObject KartaCetri,GameObject LokacijaPrebacivanja)
    {        
        KartaJedan.transform.SetParent(LokacijaPrebacivanja.transform);
        KartaJedan.transform.position = LokacijaPrebacivanja.transform.position;
      
        KartaDva.transform.SetParent(LokacijaPrebacivanja.transform);
        KartaDva.transform.position = LokacijaPrebacivanja.transform.position;
       
        KartaTri.transform.SetParent(LokacijaPrebacivanja.transform);
        KartaTri.transform.position = LokacijaPrebacivanja.transform.position;
        
        KartaCetri.transform.SetParent(LokacijaPrebacivanja.transform);
        KartaCetri.transform.position = LokacijaPrebacivanja.transform.position;
        
        for (int i=0;i < LokacijaPrebacivanja.transform.GetChildCount(); i++)
        {
            GameObject Karta = LokacijaPrebacivanja.transform.GetChild(i).gameObject;
            Selectable SakriKarte = Karta.GetComponent<Selectable>();
            SakriKarte.KartaOkrenutaPremaGore = false;
        }
    }

    public GameObject PocistiPlocuIDajKartePobjedniku()
    {
        GameObject OdigranaKartaPozicija1;
        GameObject OdigranaKartaPozicija2;
        GameObject OdigranaKartaPozicija3;
        GameObject OdigranaKartaPozicija4;
        GameObject NajaciIgrac;
        if (OdigranoPozicija1.transform.childCount == 1 && OdigranoPozicija2.transform.childCount == 1 && OdigranoPozicija3.transform.childCount == 1 && OdigranoPozicija4.transform.childCount == 1)
        {
           OdigranaKartaPozicija1 = OdigranoPozicija1.transform.GetChild(0).gameObject;
           OdigranaKartaPozicija2 = OdigranoPozicija2.transform.GetChild(0).gameObject;                      
           OdigranaKartaPozicija3 = OdigranoPozicija3.transform.GetChild(0).gameObject;                       
           OdigranaKartaPozicija4 = OdigranoPozicija4.transform.GetChild(0).gameObject;
           Ai OdigranaKartaPozicija1Ai = OdigranaKartaPozicija1.transform.GetComponent<Ai>();
           Ai OdigranaKartaPozicija2Ai = OdigranaKartaPozicija2.transform.GetComponent<Ai>();
           Ai OdigranaKartaPozicija3Ai = OdigranaKartaPozicija3.transform.GetComponent<Ai>();
           Ai OdigranaKartaPozicija4Ai = OdigranaKartaPozicija4.transform.GetComponent<Ai>();
            if(OdigranaKartaPozicija1Ai.RangKarte > OdigranaKartaPozicija2Ai.RangKarte && OdigranaKartaPozicija2Ai.Adut ==false && OdigranaKartaPozicija1Ai.RangKarte > OdigranaKartaPozicija3Ai.RangKarte && OdigranaKartaPozicija3Ai.Adut == false && OdigranaKartaPozicija1Ai.RangKarte > OdigranaKartaPozicija4Ai.RangKarte && OdigranaKartaPozicija4Ai.Adut == false)
            {
                
                PremjestiViseKarataNaJeduLokaciju(OdigranaKartaPozicija1, OdigranaKartaPozicija2, OdigranaKartaPozicija3, OdigranaKartaPozicija4, IgraciJedanITri);
                return NajaciIgrac = Igrac1;
            }
            else if (OdigranaKartaPozicija1Ai.RangKarte < OdigranaKartaPozicija2Ai.RangKarte && OdigranaKartaPozicija1Ai.BojaKarte == OdigranaKartaPozicija2Ai.BojaKarte && OdigranaKartaPozicija2Ai.RangKarte > OdigranaKartaPozicija3Ai.RangKarte && OdigranaKartaPozicija3Ai.Adut == false && OdigranaKartaPozicija2Ai.RangKarte > OdigranaKartaPozicija4Ai.RangKarte && OdigranaKartaPozicija4Ai.Adut == false)
            {              
                PremjestiViseKarataNaJeduLokaciju(OdigranaKartaPozicija1, OdigranaKartaPozicija2, OdigranaKartaPozicija3, OdigranaKartaPozicija4, IgraciDvaiCetri);
                return NajaciIgrac = Igrac2;
            }
            else if (OdigranaKartaPozicija1Ai.RangKarte < OdigranaKartaPozicija3Ai.RangKarte && OdigranaKartaPozicija1Ai.BojaKarte == OdigranaKartaPozicija3Ai.BojaKarte && OdigranaKartaPozicija2Ai.RangKarte < OdigranaKartaPozicija3Ai.RangKarte && OdigranaKartaPozicija2Ai.Adut == false && OdigranaKartaPozicija3Ai.RangKarte > OdigranaKartaPozicija4Ai.RangKarte && OdigranaKartaPozicija4Ai.Adut == false)
            {
                
                PremjestiViseKarataNaJeduLokaciju(OdigranaKartaPozicija1, OdigranaKartaPozicija2, OdigranaKartaPozicija3, OdigranaKartaPozicija4, IgraciJedanITri);
                return NajaciIgrac = Igrac3;
            }
            else if (OdigranaKartaPozicija1Ai.RangKarte < OdigranaKartaPozicija4Ai.RangKarte && OdigranaKartaPozicija1Ai.BojaKarte == OdigranaKartaPozicija4Ai.BojaKarte && OdigranaKartaPozicija4Ai.RangKarte > OdigranaKartaPozicija3Ai.RangKarte && OdigranaKartaPozicija3Ai.Adut == false && OdigranaKartaPozicija2Ai.RangKarte < OdigranaKartaPozicija4Ai.RangKarte && OdigranaKartaPozicija2Ai.Adut == false)
            {               
                PremjestiViseKarataNaJeduLokaciju(OdigranaKartaPozicija1, OdigranaKartaPozicija2, OdigranaKartaPozicija3, OdigranaKartaPozicija4, IgraciDvaiCetri);
                return NajaciIgrac = Igrac4;
            }

            else if (OdigranaKartaPozicija1Ai.Adut== true && OdigranaKartaPozicija3Ai.Adut==false && OdigranaKartaPozicija2Ai.Adut == false && OdigranaKartaPozicija4Ai.Adut==false)
            {               
                PremjestiViseKarataNaJeduLokaciju(OdigranaKartaPozicija1, OdigranaKartaPozicija2, OdigranaKartaPozicija3, OdigranaKartaPozicija4, IgraciJedanITri);
                return NajaciIgrac = Igrac1;
            }
            else if (OdigranaKartaPozicija1Ai.Adut == false && OdigranaKartaPozicija3Ai.Adut == true && OdigranaKartaPozicija2Ai.Adut == false && OdigranaKartaPozicija4Ai.Adut == false)
            {
             
                PremjestiViseKarataNaJeduLokaciju(OdigranaKartaPozicija1, OdigranaKartaPozicija2, OdigranaKartaPozicija3, OdigranaKartaPozicija4, IgraciJedanITri);
                return NajaciIgrac = Igrac3;
            }
            else if (OdigranaKartaPozicija1Ai.Adut == false && OdigranaKartaPozicija3Ai.Adut==false && OdigranaKartaPozicija2Ai.Adut == true && OdigranaKartaPozicija4Ai.Adut == false)
            {                             
                PremjestiViseKarataNaJeduLokaciju(OdigranaKartaPozicija1, OdigranaKartaPozicija2, OdigranaKartaPozicija3, OdigranaKartaPozicija4, IgraciDvaiCetri);
                return NajaciIgrac = Igrac2;
            }
            else if (OdigranaKartaPozicija1Ai.Adut == false && OdigranaKartaPozicija3Ai.Adut == false && OdigranaKartaPozicija2Ai.Adut == false && OdigranaKartaPozicija4Ai.Adut == true)
            {
                PremjestiViseKarataNaJeduLokaciju(OdigranaKartaPozicija1, OdigranaKartaPozicija2, OdigranaKartaPozicija3, OdigranaKartaPozicija4, IgraciDvaiCetri);
                return NajaciIgrac = Igrac4;
            }
           // treba doraditi
            else if (OdigranaKartaPozicija1Ai.Adut == false && OdigranaKartaPozicija3Ai.Adut == false && OdigranaKartaPozicija2Ai.Adut == false && OdigranaKartaPozicija4Ai.Adut == false)
            {
                PremjestiViseKarataNaJeduLokaciju(OdigranaKartaPozicija1, OdigranaKartaPozicija2, OdigranaKartaPozicija3, OdigranaKartaPozicija4, IgraciDvaiCetri);
                return NajaciIgrac = Igrac1;
            }
            else if (OdigranaKartaPozicija1Ai.Adut == true  | OdigranaKartaPozicija3Ai.Adut == true && OdigranaKartaPozicija2Ai.Adut == true | OdigranaKartaPozicija4Ai.Adut == true)
            {
                if(OdigranaKartaPozicija1Ai.Adut==true && OdigranaKartaPozicija2Ai.Adut == true && OdigranaKartaPozicija1Ai.RangKarte > OdigranaKartaPozicija2Ai.RangKarte && OdigranaKartaPozicija4Ai.Adut == false)
                {
                    PremjestiViseKarataNaJeduLokaciju(OdigranaKartaPozicija1, OdigranaKartaPozicija2, OdigranaKartaPozicija3, OdigranaKartaPozicija4, IgraciJedanITri);
                    return NajaciIgrac = Igrac1;
                }
                else if (OdigranaKartaPozicija1Ai.Adut == true && OdigranaKartaPozicija4Ai.Adut == true && OdigranaKartaPozicija1Ai.RangKarte > OdigranaKartaPozicija4Ai.RangKarte && OdigranaKartaPozicija2Ai.Adut == false)
                {
                    PremjestiViseKarataNaJeduLokaciju(OdigranaKartaPozicija1, OdigranaKartaPozicija2, OdigranaKartaPozicija3, OdigranaKartaPozicija4, IgraciJedanITri);
                    return NajaciIgrac = Igrac1;
                }
                else if (OdigranaKartaPozicija1Ai.Adut == true && OdigranaKartaPozicija4Ai.Adut == true && OdigranaKartaPozicija1Ai.RangKarte > OdigranaKartaPozicija4Ai.RangKarte && OdigranaKartaPozicija2Ai.Adut == true && OdigranaKartaPozicija1Ai.RangKarte> OdigranaKartaPozicija2Ai.RangKarte)
                {
                    PremjestiViseKarataNaJeduLokaciju(OdigranaKartaPozicija1, OdigranaKartaPozicija2, OdigranaKartaPozicija3, OdigranaKartaPozicija4, IgraciJedanITri);
                    return NajaciIgrac = Igrac1;
                }
                else if (OdigranaKartaPozicija3Ai.Adut == true && OdigranaKartaPozicija2Ai.Adut == true && OdigranaKartaPozicija3Ai.RangKarte > OdigranaKartaPozicija2Ai.RangKarte && OdigranaKartaPozicija4Ai.Adut == false)
                {
                    PremjestiViseKarataNaJeduLokaciju(OdigranaKartaPozicija1, OdigranaKartaPozicija2, OdigranaKartaPozicija3, OdigranaKartaPozicija4, IgraciJedanITri);
                    return NajaciIgrac = Igrac3;
                }
                else if (OdigranaKartaPozicija3Ai.Adut == true && OdigranaKartaPozicija4Ai.Adut == true && OdigranaKartaPozicija3Ai.RangKarte > OdigranaKartaPozicija4Ai.RangKarte && OdigranaKartaPozicija2Ai.Adut == false)
                {
                    PremjestiViseKarataNaJeduLokaciju(OdigranaKartaPozicija1, OdigranaKartaPozicija2, OdigranaKartaPozicija3, OdigranaKartaPozicija4, IgraciJedanITri);
                    return NajaciIgrac = Igrac3;
                }
                else if (OdigranaKartaPozicija3Ai.Adut == true && OdigranaKartaPozicija4Ai.Adut == true && OdigranaKartaPozicija3Ai.RangKarte > OdigranaKartaPozicija4Ai.RangKarte && OdigranaKartaPozicija2Ai.Adut == true && OdigranaKartaPozicija3Ai.RangKarte > OdigranaKartaPozicija2Ai.RangKarte)
                {
                    PremjestiViseKarataNaJeduLokaciju(OdigranaKartaPozicija1, OdigranaKartaPozicija2, OdigranaKartaPozicija3, OdigranaKartaPozicija4, IgraciJedanITri);
                    return NajaciIgrac = Igrac3;
                }
                else
                {
                    PremjestiViseKarataNaJeduLokaciju(OdigranaKartaPozicija1, OdigranaKartaPozicija2, OdigranaKartaPozicija3, OdigranaKartaPozicija4, IgraciDvaiCetri);
                    return NajaciIgrac = Igrac4;
                }               
            }
        }
        return null;
    }
    public void OdluciKojaKartaJeDoSadNajvecaIStoTrebaOdbaciti() {       
        GameObject[] SveOdigranePozicije = { OdigranoPozicija1, OdigranoPozicija2, OdigranoPozicija3, OdigranoPozicija4 };
        GameObject PrvaOdigrana= null;
        GameObject NajcaKarta=null;
        GameObject SljedeciIgrac=null;
        for(int i=0; i < SveOdigranePozicije.Length-1; i++)
        {
            
            GameObject Pozicija = SveOdigranePozicije[i];
            GameObject SljedecaPozicija = SveOdigranePozicije[i+1];


            if (Pozicija.transform.GetChildCount() == 1)
            {

                if (PrvaOdigrana == null)
                {
                    PrvaOdigrana = Pozicija.transform.GetChild(0).gameObject;
                    PrvaOdigranaKarta = PrvaOdigranaKarta;
                } 
                                    
                if (PrvaOdigrana != null)
                {                   
                    string ImeSljedecegIgraca = "Igrac" + SljedecaPozicija.name.Substring(SljedecaPozicija.name.Length - 1);
                    SljedeciIgrac = GameObject.Find(ImeSljedecegIgraca);                                      
                    GameObject TrenutnaKarta = Pozicija.transform.GetChild(0).gameObject;
                    Ai TrenutnaKartaAi = TrenutnaKarta.GetComponent<Ai>();
                    print("Prva Odigrana Karta je " + PrvaOdigrana.name);
                    print("Trenutna Karta je " + TrenutnaKarta.name);
                    print("Sljedeci igrac je : " + SljedeciIgrac.name);
                    Ai PrvaOdigranaAi = PrvaOdigrana.GetComponent<Ai>();
                    if (SljedecaPozicija.transform.GetChildCount() == 0)
                    {
                        if (PrvaOdigrana == TrenutnaKarta)
                        {
                            AiOdluciKojuKartuBacaIzRuke(SljedeciIgrac, SljedecaPozicija, PrvaOdigrana, false);
                        }
                        if (PrvaOdigranaAi.RangKarte < TrenutnaKartaAi.RangKarte && PrvaOdigranaAi.BojaKarte == TrenutnaKartaAi.BojaKarte)
                        {
                            NajcaKarta = TrenutnaKarta;
                            AiOdluciKojuKartuBacaIzRuke(SljedeciIgrac, SljedecaPozicija, NajcaKarta, false);
                        }
                        else if (PrvaOdigranaAi.RangKarte > TrenutnaKartaAi.RangKarte && PrvaOdigranaAi.BojaKarte == TrenutnaKartaAi.BojaKarte)
                        {
                            AiOdluciKojuKartuBacaIzRuke(SljedeciIgrac, SljedecaPozicija, PrvaOdigrana, false);
                        }
                        else if ( PrvaOdigranaAi.BojaKarte != TrenutnaKartaAi.BojaKarte && TrenutnaKartaAi.Adut == true )
                        { 
                            NajcaKarta = TrenutnaKarta;
                            AiOdluciKojuKartuBacaIzRuke(SljedeciIgrac, SljedecaPozicija, PrvaOdigrana, true);
                        }
                        else if (NajcaKarta != null)
                        {
                            Ai NajacaKartaAi = NajcaKarta.GetComponent<Ai>();
                            if (NajacaKartaAi.RangKarte < TrenutnaKartaAi.RangKarte && PrvaOdigranaAi.BojaKarte == TrenutnaKartaAi.BojaKarte)
                            {
                                NajcaKarta = TrenutnaKarta;
                                AiOdluciKojuKartuBacaIzRuke(SljedeciIgrac, SljedecaPozicija, NajcaKarta, false);
                            }
                            else if (NajacaKartaAi.RangKarte > TrenutnaKartaAi.RangKarte && PrvaOdigranaAi.BojaKarte == TrenutnaKartaAi.BojaKarte)
                            {                               
                               
                                AiOdluciKojuKartuBacaIzRuke(SljedeciIgrac, SljedecaPozicija, NajcaKarta, false);
                            }
                            else if (PrvaOdigranaAi.Adut == false && NajacaKartaAi.Adut == false && TrenutnaKartaAi.Adut == true)
                            {
                                NajcaKarta = TrenutnaKarta;
                                AiOdluciKojuKartuBacaIzRuke(SljedeciIgrac, SljedecaPozicija, NajcaKarta, false);
                            }
                            else if (NajacaKartaAi.Adut == true && NajacaKartaAi.RangKarte < TrenutnaKartaAi.RangKarte && TrenutnaKartaAi.Adut == true)
                            {
                                NajcaKarta = TrenutnaKarta;
                                AiOdluciKojuKartuBacaIzRuke(SljedeciIgrac, SljedecaPozicija, NajcaKarta, false);
                            }
                            else if (NajacaKartaAi.Adut == true && NajacaKartaAi.RangKarte > TrenutnaKartaAi.RangKarte && TrenutnaKartaAi.Adut == true)
                            {
                                AiOdluciKojuKartuBacaIzRuke(SljedeciIgrac, SljedecaPozicija, NajcaKarta, true);
                            }
                            else if ( NajacaKartaAi.Adut == true && TrenutnaKartaAi.Adut == false)
                            {
                                AiOdluciKojuKartuBacaIzRuke(SljedeciIgrac, SljedecaPozicija, PrvaOdigrana, true);
                            }

                        }
                        else if (PrvaOdigranaAi.BojaKarte != TrenutnaKartaAi.BojaKarte && TrenutnaKartaAi.Adut == false)
                        {
                            NajcaKarta = TrenutnaKarta;
                            AiOdluciKojuKartuBacaIzRuke(SljedeciIgrac, SljedecaPozicija, NajcaKarta, true);
                        }
                    }

                }

            }
            
        }
    
    }

    public void PostaviVrijednostiAduta(string boja)
    {

        GameObject[] SveKarte = GameObject.FindGameObjectsWithTag("Karta");
        foreach (GameObject Karta in SveKarte  )
            {
            Ai KartaAi = Karta.transform.GetComponent<Ai>();
                KartaAi.BojaAduta = boja;
                if (KartaAi.BojaKarte == boja)
                {
                    
                    KartaAi.Adut = true;
                    if (KartaAi.NazivKarte == "9")
                    {
                    KartaAi.RangKarte = 9;
                    KartaAi.VrijednostKarte = 14;
                    }
                    if (KartaAi.NazivKarte == "Decko")
                    {
                    KartaAi. RangKarte = 10;
                    KartaAi.VrijednostKarte = 20;
                    }
                    
                }
                if (KartaAi.BojaKarte != boja)
                {
                KartaAi.Adut = false;
                }
            }
        for(int i = 0; i < Igrac1.transform.childCount; i++)
        {
            GameObject karte = Igrac1.transform.GetChild(i).gameObject;
            Selectable PokaziKartu = karte.GetComponent<Selectable>();
            PokaziKartu.KartaOkrenutaPremaGore = true;
        }
      
    }

    public void PostaviVrijednostiKarataPriPokretanju()
    {
        if (CompareTag("Karta"))
        {

            BojaKarte = transform.name[0].ToString() + transform.name[1].ToString() + transform.name[2].ToString();
            NazivKarte = transform.name.Substring(3, transform.name.Length - 3);


            if (NazivKarte == "7")
            {
                RangZvanja = 1;
                RangKarte = 1;
                VrijednostKarte = 0;
            }
            if (NazivKarte == "8")
            {
                RangZvanja = 2;
                RangKarte = 2;
                VrijednostKarte = 0;
            }
            if (NazivKarte == "9")
            {
                RangZvanja = 3;
                RangKarte = 3;
                VrijednostKarte = 0;
            }
            if (NazivKarte == "10")
            {
                RangZvanja = 4;
                RangKarte = 7;
                VrijednostKarte = 10;
            }
            if (NazivKarte == "Decko")
            {
                RangZvanja = 5;
                RangKarte = 4;
                VrijednostKarte = 2;
            }
            if (NazivKarte == "Baba")
            {
                RangZvanja = 6;
                RangKarte = 5;
                VrijednostKarte = 3;
            }
            if (NazivKarte == "Kralj")
            {
                RangZvanja = 7;
                RangKarte = 6;
                VrijednostKarte = 4;
            }
            if (NazivKarte == "As")
            {
                RangZvanja = 8;
                RangKarte = 8;
                VrijednostKarte = 11;
            }
        }




    }

    public void AiOdluciKojuKartuBacaIzRuke(GameObject SljedeciNaReduIgrac, GameObject SljedecaPozicijaNaReduOdigranoIgrac, GameObject NajvecaOdigranaKartaDoSad,bool IgrajManjuAkoCijepano)
    {
        Ai NajvecaKartaDoSad = NajvecaOdigranaKartaDoSad.GetComponent<Ai>();
        
        GameObject NajvecaKartaUIstojBoji = null;
        // Ai NajvecaKartaUIstojBojiAi = NajvecaKartaUIstojBoji.GetComponent<Ai>();       
        GameObject NajmanjaKartaUIstojBoji = null;
        //Ai NajmanjaKartaUIstojBojiAi = NajmanjaKartaUIstojBoji.GetComponent<Ai>();
        GameObject NajveciAdut = null;
        // Ai NajveciAdutAi = NajveciAdut.GetComponent<Ai>();
        GameObject NajmaniAdut = null;
        // Ai NajmaniAdutAi = NajmaniAdut.GetComponent<Ai>();
        GameObject NajmanjaKatraVanBoje = null;
        // Ai NajmanjaKatraVanBojeAi = NajmanjaKatraVanBoje.GetComponent<Ai>();

        for (int i = 0; i < SljedeciNaReduIgrac.transform.childCount; i++)
        {
            GameObject TrenutnaKartaUPetlji = SljedeciNaReduIgrac.transform.GetChild(i).gameObject;
            Ai TrenutnaKarta = TrenutnaKartaUPetlji.GetComponent<Ai>();

            if (NajvecaKartaDoSad.BojaKarte == TrenutnaKarta.BojaKarte)
            {
                if (NajvecaKartaUIstojBoji == null)
                {
                    NajvecaKartaUIstojBoji = TrenutnaKartaUPetlji;

                }

                else if (NajvecaKartaUIstojBoji != null)
                {
                    Ai NajvecaKartaUIstojBojiAi = NajvecaKartaUIstojBoji.GetComponent<Ai>();

                    if (NajvecaKartaUIstojBojiAi.RangKarte < TrenutnaKarta.RangKarte)
                    {
                        NajvecaKartaUIstojBoji = TrenutnaKartaUPetlji;
                    }
                    else if (NajmanjaKartaUIstojBoji == null)
                    {
                        NajmanjaKartaUIstojBoji = TrenutnaKartaUPetlji;
                    }
                    else if (NajmanjaKartaUIstojBoji != null)
                    {
                        Ai NajmanjaKartaUIstojBojiAi = NajmanjaKartaUIstojBoji.GetComponent<Ai>();
                        if (NajmanjaKartaUIstojBojiAi.RangKarte > TrenutnaKarta.RangKarte)
                        {
                            NajmanjaKartaUIstojBoji = TrenutnaKartaUPetlji;
                        }
                    }


                }
            }
            else if (TrenutnaKarta.Adut == true)
            {
                if (NajveciAdut == null)
                {
                    NajveciAdut = TrenutnaKartaUPetlji;
                }
                else if (NajveciAdut != null)
                {
                    Ai RangNajveciAdut = NajveciAdut.GetComponent<Ai>();
                    if (RangNajveciAdut.RangKarte < TrenutnaKarta.RangKarte)
                    {
                        NajveciAdut = TrenutnaKartaUPetlji;
                    }
                    else if (NajmaniAdut == null)
                    {
                        NajmaniAdut = TrenutnaKartaUPetlji;
                    }
                    else if (NajmaniAdut != null)
                    {
                        Ai NajmaniAdutAi = NajmaniAdut.GetComponent<Ai>();
                        if (NajmaniAdutAi.RangKarte > TrenutnaKarta.RangKarte)
                        {
                            NajmaniAdut = TrenutnaKartaUPetlji;
                        }
                    }


                }
            }
            else if (TrenutnaKarta.Adut != true && NajvecaKartaDoSad.BojaKarte != TrenutnaKarta.BojaKarte)
            {
                if (NajmanjaKatraVanBoje == null)
                {
                    NajmanjaKatraVanBoje = TrenutnaKartaUPetlji;
                }
                else if (NajmanjaKatraVanBoje != null)
                {
                    Ai NajmanjaKatraVanBojeAi = NajmanjaKatraVanBoje.GetComponent<Ai>();
                    if (NajmanjaKatraVanBojeAi.RangKarte > TrenutnaKarta.RangKarte)
                    {
                        NajmanjaKatraVanBoje = TrenutnaKartaUPetlji;
                    }
                }
            }            
        }             

        
            if (NajvecaKartaUIstojBoji != null)
            {
                Ai NajvecaKartaUIstojBojiAi = NajvecaKartaUIstojBoji.GetComponent<Ai>();
                if (NajvecaKartaDoSad.RangKarte > NajvecaKartaUIstojBojiAi.RangKarte | IgrajManjuAkoCijepano == true)
                {
                    if (NajmanjaKartaUIstojBoji != null )
                    {

                        Selectable PokaziKartu = NajmanjaKartaUIstojBoji.GetComponent<Selectable>();
                        PokaziKartu.KartaOkrenutaPremaGore = true;
                        NajmanjaKartaUIstojBoji.transform.SetParent(SljedecaPozicijaNaReduOdigranoIgrac.transform);
                        NajmanjaKartaUIstojBoji.transform.position = SljedecaPozicijaNaReduOdigranoIgrac.transform.position;
                    print("Odbaci Najmanju kartu u istoj boji " + SljedecaPozicijaNaReduOdigranoIgrac.name);
                    }
                    else
                    {
                        Selectable PokaziKartu = NajvecaKartaUIstojBoji.GetComponent<Selectable>();
                        PokaziKartu.KartaOkrenutaPremaGore = true;
                        NajvecaKartaUIstojBoji.transform.SetParent(SljedecaPozicijaNaReduOdigranoIgrac.transform);
                        NajvecaKartaUIstojBoji.transform.position = SljedecaPozicijaNaReduOdigranoIgrac.transform.position;
                    print("Odbaci nema najmanje karte u istoj boji odbaci veču " + SljedecaPozicijaNaReduOdigranoIgrac.name);
                }
                }
                else if (NajvecaKartaDoSad.RangKarte < NajvecaKartaUIstojBojiAi.RangKarte)
                {
                    Selectable PokaziKartu = NajvecaKartaUIstojBoji.GetComponent<Selectable>();
                    PokaziKartu.KartaOkrenutaPremaGore = true;
                    NajvecaKartaUIstojBoji.transform.SetParent(SljedecaPozicijaNaReduOdigranoIgrac.transform);
                    NajvecaKartaUIstojBoji.transform.position = SljedecaPozicijaNaReduOdigranoIgrac.transform.position;
                    print("Odbaci najvecu kartu u istoj boji " + SljedecaPozicijaNaReduOdigranoIgrac.name);

            }

            }
            else if (NajveciAdut != null)
            {
                Selectable PokaziKartu = NajveciAdut.GetComponent<Selectable>();
                PokaziKartu.KartaOkrenutaPremaGore = true;
                NajveciAdut.transform.SetParent(SljedecaPozicijaNaReduOdigranoIgrac.transform);
                NajveciAdut.transform.position = SljedecaPozicijaNaReduOdigranoIgrac.transform.position;
                print("Odbaci najvecu kartu u adutu " + SljedecaPozicijaNaReduOdigranoIgrac.name);

        }
            else if (NajmanjaKatraVanBoje != null)
            {
            
                Selectable PokaziKartu = NajmanjaKatraVanBoje.GetComponent<Selectable>();
                PokaziKartu.KartaOkrenutaPremaGore = true;
                NajmanjaKatraVanBoje.transform.SetParent(SljedecaPozicijaNaReduOdigranoIgrac.transform);
                NajmanjaKatraVanBoje.transform.position = SljedecaPozicijaNaReduOdigranoIgrac.transform.position;
                print("Odbaci najmanju kartu u različitoj boji " + SljedecaPozicijaNaReduOdigranoIgrac.name);
            
        }

        

    }
}



    



    

