﻿using System.Collections;
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
    public GameObject TkoJeZvao = null;
    public TextMeshProUGUI IgracKojiJeZvao;
    public Image SlikaAduta;
    public Sprite[] SlikeAduta;
    public TextMeshProUGUI TextDalje;
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
    public GameObject IgracKojiPrviZove;
    public GameObject IgracKojiTrenutnoZove;
    public GameObject IgracKojiZoveSljedeci = null;
    public GameObject IgracNaMusu;
    public GameObject IgracKojiJeSadaNaRedu = null;
    public string SljedecaScena = "BelaScen1";
    public TextMeshProUGUI Rezultat1i3;
    public TextMeshProUGUI Rezultat2i4;
    private bool IzvrsavanjeJednom ;  
    public static int ZvanjaMi ;
    public static int ZvanjaVi ;
    public GameObject ZoviAdutPloca;
    public GameObject ZoviDaljeButton;
    public GameObject PrvaOdigranaKartaURundi = null;
    public AudioSource OdbaciKartuAudio;
    private GameObject KojiIgracJeZvao = null;  
   
    public GameObject ZoviBelu;
    public GameObject IgracZvaoJeBelu;
    public TextMeshProUGUI IgracKojiJeZvaoBelu;

    private GameObject IgracKojiPrviBacaKartu;
    
    void Start()
    {
        PostaviVrijednostiKarataPriPokretanju();
        OdlučiKojiIgracSadaPrviZove();
        ZvanjaMi = 0;
        ZvanjaVi = 0;
        OnMouseDown();
        BojaAduta = null;
        IzvrsavanjeJednom = false;
        IgracKojiTrenutnoZove = null;
        StartCoroutine(AiZoviAdutSaZakasnjenjem());       
      
    }
    void Update()
    {
        if (IgracKojiJeSadaNaRedu != null)
        {
           
            Spremanje.IgracKojiJeSadNaRedu = IgracKojiJeSadaNaRedu;
            
        }

        if(IgracKojiPrviBacaKartu == null && IgracKojiJeSadaNaRedu != null)
        {
            IgracKojiPrviBacaKartu = IgracKojiJeSadaNaRedu;
        }
        
        if(BojaAduta !=null && IzvrsavanjeJednom == false)
        {
            AutomatskiProvjeriZvanjaIZovi();  
            StartCoroutine(PrviNaReduODigraKartuPrviPut());              
            IzvrsavanjeJednom =  true;
        }
        StartCoroutine(AiIgraciZovuBelu());         
        ProvjeriDaliIgrac1ImaBelu();     

        if (Igrac1.transform.childCount != 0 || Igrac2.transform.childCount != 0 || Igrac3.transform.childCount != 0 || Igrac4.transform.childCount != 0)
        {
            if (OdigranoPozicija1.transform.childCount ==0 || OdigranoPozicija2.transform.childCount == 0 || OdigranoPozicija3.transform.childCount == 0 || OdigranoPozicija4.transform.childCount == 0 )
            {
                OdbaciKartuAkoNaRedu();
            }
            else if (OdigranoPozicija1.transform.childCount == 1 && OdigranoPozicija2.transform.childCount == 1 && OdigranoPozicija3.transform.childCount == 1 && OdigranoPozicija4.transform.childCount == 1)
            {
              Spremanje.PrvaOdigranaKarta = null;
              Spremanje.NajacaKartaDoSad = null;             

            }
           
                
            
            }
        else if(Igrac1.transform.childCount == 0 && Igrac2.transform.childCount == 0 && Igrac3.transform.childCount == 0 && Igrac4.transform.childCount == 0)
        {
            if (OdigranoPozicija1.transform.childCount == 0 && OdigranoPozicija2.transform.childCount == 0 && OdigranoPozicija3.transform.childCount == 0 && OdigranoPozicija4.transform.childCount == 0)
            {
                
                GameObject PobjednikZadnjegStiha = PocistiPlocuIDajKartePobjedniku();
                if (PobjednikZadnjegStiha == Igrac1||PobjednikZadnjegStiha == Igrac3)
                {
                    ZvanjaMi = ZvanjaMi + 10;
                }
                else if (PobjednikZadnjegStiha == Igrac2 || PobjednikZadnjegStiha == Igrac4)
                {
                    ZvanjaVi = ZvanjaVi + 10;
                }               
                StartCoroutine(RestartajIgruZasljedeciKrugPartije());  
            }
                    
        }
        
        
    }
    public IEnumerator AiIgraciZovuBelu()
    {
            
        if (Spremanje.BelaJeZvana == false)
        {         
            GameObject[] PozicijeAi = { OdigranoPozicija2, OdigranoPozicija3, OdigranoPozicija4 };
            GameObject[] IgraciAi = { Igrac2, Igrac3, Igrac4 };
            for (int i = 0; i < PozicijeAi.Length; i++)
            {
                GameObject TrenutnaPozicija = PozicijeAi[i];
                if (TrenutnaPozicija.transform.childCount == 1)
                {
                    GameObject KartaNaTojPoziciji = TrenutnaPozicija.transform.GetChild(0).gameObject;
                    Ai KartaNaTojPozicijiAi = KartaNaTojPoziciji.GetComponent<Ai>();
                    GameObject IgracNaTojPoziciji = IgraciAi[i];
                    if (KartaNaTojPozicijiAi.Adut == true && KartaNaTojPozicijiAi.RangKarte == 5 || KartaNaTojPozicijiAi.Adut && KartaNaTojPozicijiAi.RangKarte == 6)
                    {
                        if (IgracNaTojPoziciji.transform.childCount != 0)
                        {
                            for (int a = 0; a < IgracNaTojPoziciji.transform.childCount; a++)
                            {

                                GameObject KartaURuciAiIgraca = IgracNaTojPoziciji.transform.GetChild(a).gameObject;
                                Ai KartaURuciAi = KartaURuciAiIgraca.GetComponent<Ai>();
                                if (KartaURuciAi.Adut == true && KartaURuciAi.RangKarte == 5 || KartaURuciAi.Adut == true && KartaURuciAi.RangKarte == 6)
                                {
                                   if(IgracNaTojPoziciji == Igrac2 || Igrac4)
                                    {
                                        ZvanjaVi = ZvanjaVi + 20;
                                    }
                                    else if (IgracNaTojPoziciji == Igrac3)
                                    {
                                        ZvanjaMi = ZvanjaMi + 20;
                                    }

                                    IgracZvaoJeBelu.SetActive(true);
                                    IgracKojiJeZvaoBelu.SetText(IgracNaTojPoziciji.name);
                                    Spremanje. BelaJeZvana = true;
                                    yield return new WaitForSecondsRealtime(3);
                                    IgracZvaoJeBelu.SetActive(false);
                                    break;

                                }
                            }
                        }

                    }
                }
            }
        }
            
          
        
    }
    public void ProvjeriDaliIgrac1ImaBelu()
    {
        if (OdigranoPozicija1.transform.childCount == 1)
        {
           GameObject Karta = OdigranoPozicija1.transform.GetChild(0).gameObject;
            Ai KartaAi = Karta.GetComponent<Ai>();
            if(KartaAi.Adut == true && KartaAi.RangKarte == 5 || KartaAi.Adut == true && KartaAi.RangKarte == 6)
            {
                if (Igrac1.transform.childCount != 0)
                {                
                    for (int i = 0; i < Igrac1.transform.childCount; i++)
                    {                         
                        GameObject KartaURuci = Igrac1.transform.GetChild(i).gameObject;                       
                        Ai KartaURuciAi = KartaURuci.GetComponent<Ai>();
                        if(KartaURuciAi.Adut == true &&KartaURuciAi.RangKarte ==5 || KartaURuciAi.Adut == true && KartaURuciAi.RangKarte == 6)
                        {
                            ZoviBelu.SetActive(true);
                        }
                    }
                }
            }
        }
    }
    public void Igrac1ZoveBelu()
    {
        ZvanjaMi = ZvanjaMi + 20;
    }
    
    public void OdbaciKartuAkoNaRedu()
    {
                     
        if (Spremanje.PozicijaKojaMoraBitiPopunjenaPrvo != null)
        {
          
            if (Spremanje.PozicijaKojaMoraBitiPopunjenaPrvo == OdigranoPozicija1)
            {
                if (OdigranoPozicija1.transform.childCount != 0 && OdigranoPozicija2.transform.childCount == 0)
                {
                    OdluciKojaKartaJeDoSadNajvecaIStoTrebaOdbaciti();                 
                }
            }
            else if (Spremanje.PozicijaKojaMoraBitiPopunjenaPrvo == OdigranoPozicija2)
            {
                if (OdigranoPozicija2.transform.childCount != 0 && OdigranoPozicija3.transform.childCount == 0)
                {                 
                    OdluciKojaKartaJeDoSadNajvecaIStoTrebaOdbaciti();
                }              
            }
            else if (Spremanje.PozicijaKojaMoraBitiPopunjenaPrvo == OdigranoPozicija3 && OdigranoPozicija1.transform.childCount == 0)
            {                
                if (OdigranoPozicija3.transform.childCount != 0 && OdigranoPozicija4.transform.childCount == 0 ) 
                {                   
                    OdluciKojaKartaJeDoSadNajvecaIStoTrebaOdbaciti();
                }
            }
           else if (Spremanje.PozicijaKojaMoraBitiPopunjenaPrvo == OdigranoPozicija4)
            {
                if (OdigranoPozicija4.transform.childCount != 0 && OdigranoPozicija1.transform.childCount != 0)
                {
                   OdluciKojaKartaJeDoSadNajvecaIStoTrebaOdbaciti();
                    
                }
            }
            else if(Spremanje.PozicijaKojaMoraBitiPopunjenaPrvo != OdigranoPozicija1 && OdigranoPozicija1.transform.childCount ==1)
            {
                OdluciKojaKartaJeDoSadNajvecaIStoTrebaOdbaciti();
            }
        }
        else if(OdigranoPozicija1.transform.childCount == 1 && Spremanje.PozicijaKojaMoraBitiPopunjenaPrvo == null)
        {
       
            OdluciKojaKartaJeDoSadNajvecaIStoTrebaOdbaciti();
        }
    }
    public IEnumerator PrviNaReduODigraKartuPrviPut()
    {
        yield return new WaitForSecondsRealtime(3);
        if (Igrac1.transform.childCount == 8 && Igrac2.transform.childCount == 8 && Igrac3.transform.childCount == 8 && Igrac4.transform.childCount == 8)
        {
            OdluciKojiIgracJeNaReduITajIgracIgra(IgracKojiPrviZove);
            OdbaciKartuAudio.Play();
        }
    }
    public void OdlučiKojiIgracSadaPrviZove()
    {
        if(Spremanje.IgracKojiSadaZove == 1)
        {
            IgracKojiPrviZove = Igrac1;
            IgracNaMusu = Igrac4;
            
        }
        else if (Spremanje.IgracKojiSadaZove == 2)
        {
            IgracKojiPrviZove = Igrac2;
            IgracNaMusu = Igrac1;
            
        }
        else if (Spremanje.IgracKojiSadaZove == 3)
        {
            IgracKojiPrviZove = Igrac3;
            IgracNaMusu = Igrac2;
          
        }
        else if (Spremanje.IgracKojiSadaZove == 4)
        {
            IgracKojiPrviZove = Igrac4;
            IgracNaMusu = Igrac3;
            
        }
        
    }
    public IEnumerator AiZoviAdutSaZakasnjenjem()
    {
        yield return new WaitForSecondsRealtime(5);
        StartCoroutine(AiZovi(IgracKojiPrviZove));
            
        
    }
    public void AutomatskiProvjeriZvanjaIZovi()
    {
        //print("Funkcija Se Ponovila");
        int NajveceZvanjeIgrac1 = VratiZvanjeAkoPostojiKodPojedinogIgraca(Igrac1).NajveceZvanje;
        int NajveceZvanjeIgrac2 = VratiZvanjeAkoPostojiKodPojedinogIgraca(Igrac2).NajveceZvanje;
        int NajveceZvanjeIgrac3 = VratiZvanjeAkoPostojiKodPojedinogIgraca(Igrac3).NajveceZvanje;
        int NajveceZvanjeIgrac4 = VratiZvanjeAkoPostojiKodPojedinogIgraca(Igrac4).NajveceZvanje;
        int[] SvaZvanja = { NajveceZvanjeIgrac1, NajveceZvanjeIgrac2, NajveceZvanjeIgrac3, NajveceZvanjeIgrac4 };
        int NajaceZvanje = 0;
        int BrojacJednakihZvanja = 0;
        // print(NajveceZvanjeIgrac1 + "TO JEZVANJE IGRACA 1");
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
                if(NajacaKartaUZvanju == null && NajaceKarteZvanja[i] != null)
                {
                    NajacaKartaUZvanju = NajaceKarteZvanja[i];
                }
                else if( NajacaKartaUZvanju != null && NajaceKarteZvanja[i] != null)
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
                        
                        // to be continue
                    }
                }
            }

            int IndeksNajacegIgracaKarta = Array.IndexOf(NajaceKarteZvanja, NajacaKartaUZvanju);
            if (NajaceKarteZvanja[IndeksNajacegIgracaKarta] == NajcaKartaZvanjaIgrac1)
            {
                NajaciIgrac = Igrac1;
            }
            else if (NajaceKarteZvanja[IndeksNajacegIgracaKarta] == NajcaKartaZvanjaIgrac2)
            {
                NajaciIgrac = Igrac2;
            }
            else if (NajaceKarteZvanja[IndeksNajacegIgracaKarta] == NajcaKartaZvanjaIgrac3)
            {
                NajaciIgrac = Igrac3;
            }
           else if (NajaceKarteZvanja[IndeksNajacegIgracaKarta] == NajcaKartaZvanjaIgrac4)
            {
                NajaciIgrac = Igrac4;
            }else if( BrojacKartaJednakogRanga > 1)
            {

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

            //Karta.transform.position = new Vector3(Karta.transform.position.x , Karta.transform.position.y, Karta.transform.position.z-0.9f );
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
            if (ZvanjaMi == 0)
            {
                ZvanjaMi = SveukupnoZvanjaMi;
            }
        }
        else if(NajaciIgrac == Igrac2 || NajaciIgrac == Igrac4)
        {
           if(ZvanjaMi == 0)
            { 
           ZvanjaVi = SveukupnoZvanjaVi;
           }
        }
        
       /*
       for(int i = 0; i< KarteKojeSeZovu.Count; i++)
        {
            print(KarteKojeSeZovu[i].name);
        }
       */
       // print("Najace Zvanje = " + NajaceZvanje);
       // print("Najaci Igrac = " +  NajaciIgrac);
       // print("Najaca Karta u Zvanju" + NajacaKartaUZvanju);
       // print("Najaci Suigrac " + SuigracNajacegaIgraca);

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
            if (KojiIgracJeZvao == Igrac1 || KojiIgracJeZvao == Igrac3 && VratiVrijednostKarataIgraci(IgraciJedanITri) + ZvanjaMi <= VratiVrijednostKarataIgraci(IgraciDvaiCetri) + ZvanjaVi)
            {
                Rezultati.PostaviNoviRezultatMi(0);
                Rezultati.PostaviNoviRezultatVi(VratiVrijednostKarataIgraci(IgraciJedanITri) + ZvanjaMi + VratiVrijednostKarataIgraci(IgraciDvaiCetri) + ZvanjaVi);
            }
            else if (KojiIgracJeZvao == Igrac2 || KojiIgracJeZvao == Igrac4 && VratiVrijednostKarataIgraci(IgraciJedanITri) + ZvanjaMi >= VratiVrijednostKarataIgraci(IgraciDvaiCetri) + ZvanjaVi)
            {
                Rezultati.PostaviNoviRezultatMi(VratiVrijednostKarataIgraci(IgraciJedanITri) + ZvanjaMi + VratiVrijednostKarataIgraci(IgraciDvaiCetri) + ZvanjaVi);
                Rezultati.PostaviNoviRezultatVi(0);
            }
            else 
            {
                Rezultati.PostaviNoviRezultatMi(VratiVrijednostKarataIgraci(IgraciJedanITri)+ZvanjaMi);
                Rezultati.PostaviNoviRezultatVi(VratiVrijednostKarataIgraci(IgraciDvaiCetri)+ZvanjaVi);
            }

            
            if(Spremanje.IgracKojiSadaZove != 4)
            {
                Spremanje.IgracKojiSadaZove = Spremanje.IgracKojiSadaZove + 1;
            }
            else if (Spremanje.IgracKojiSadaZove == 4)
            {
                Spremanje.IgracKojiSadaZove = 1;
            }
            
            SceneManager.LoadScene(SljedecaScena);
           // enabled = false;
        }
         
       
    }
    private void PokaziImeIgracaKojiJeZvao(GameObject ImeIgracaKojJeZvao)
    {
        if(ImeIgracaKojJeZvao.name == Igrac1.name)
        {
            if (Spremanje.Igrac1 != null)
            {
                IgracKojiJeZvao.SetText(Spremanje.Igrac1);
            }
            else
            {
                IgracKojiJeZvao.SetText(ImeIgracaKojJeZvao.name);
            }
        }
        else if (ImeIgracaKojJeZvao.name == Igrac2.name)
        {
            if (Spremanje.Igrac2 != null)
            {
                IgracKojiJeZvao.SetText(Spremanje.Igrac2);
            }
            else
            {
                IgracKojiJeZvao.SetText(ImeIgracaKojJeZvao.name);
            }
        }
        else if (ImeIgracaKojJeZvao.name == Igrac3.name)
        {
            if (Spremanje.Igrac3 != null)
            {
                IgracKojiJeZvao.SetText(Spremanje.Igrac3);
            }
            else
            {
                IgracKojiJeZvao.SetText(ImeIgracaKojJeZvao.name);
            }
        }
        else if (ImeIgracaKojJeZvao.name == Igrac4.name)
        {
            if (Spremanje.Igrac4 != null)
            {
                IgracKojiJeZvao.SetText(Spremanje.Igrac4);
            }
            else
            {
                IgracKojiJeZvao.SetText(ImeIgracaKojJeZvao.name);
            }
        }
        
    }
    public void PozvanoSrce(GameObject IgracKojiJeZvaoSrce)
    {
        if (BojaAduta == "Src") {
            PokaziImeIgracaKojiJeZvao(IgracKojiJeZvaoSrce);
            SlikaAduta.sprite = SlikeAduta[0];
            TkoJeZvao.SetActive(true);
            TextDalje.SetText("");
            KojiIgracJeZvao = IgracKojiJeZvaoSrce;
        }
    }
    public void PozvanoTikva(GameObject IgracKojiJeZvaoTikvu)
    {
        if (BojaAduta == "Tik")
        {
            PokaziImeIgracaKojiJeZvao(IgracKojiJeZvaoTikvu);
            SlikaAduta.sprite = SlikeAduta[1];
            TkoJeZvao.SetActive(true);
            TextDalje.SetText("");
            KojiIgracJeZvao = IgracKojiJeZvaoTikvu;
        }
    }
    public void PozvanoZelje(GameObject IgracKojiJeZvaoZelje)
    {
        if (BojaAduta == "Zel")
        {
            PokaziImeIgracaKojiJeZvao(IgracKojiJeZvaoZelje);
            SlikaAduta.sprite = SlikeAduta[2];
            TkoJeZvao.SetActive(true);
            TextDalje.SetText("");
            KojiIgracJeZvao = IgracKojiJeZvaoZelje;
        }
    }
    public void PozvanoZir(GameObject IgracKojiJeZvaoZir)
    {
        if (BojaAduta == "Zir")
        {
            PokaziImeIgracaKojiJeZvao(IgracKojiJeZvaoZir);
            SlikaAduta.sprite = SlikeAduta[3];
            TkoJeZvao.SetActive(true);
            TextDalje.SetText("");
            KojiIgracJeZvao = IgracKojiJeZvaoZir;
        }
    }
    public void PozvanoDalje(GameObject IgracKojiJeZvaoDalje)
    {
        if (BojaAduta == null)
        {
            PokaziImeIgracaKojiJeZvao(IgracKojiJeZvaoDalje);
            TkoJeZvao.SetActive(true);
            TextDalje.SetText("DALJE");
        }
    }
    public IEnumerator PocistiPlocuNakonIgreDelay()
    {       
            GameObject Igrac = PocistiPlocuIDajKartePobjedniku();
            IgracKojiPrviBacaKartu = Igrac;
            
            yield return new WaitForSecondsRealtime(1);
            OdluciKojiIgracJeNaReduITajIgracIgra(Igrac);
        
    }
    public void IgracZoveDalje(GameObject ImeSljedecegIgraca)
    {
        StartCoroutine(AiZovi(ImeSljedecegIgraca));
    }
    public IEnumerator AiZovi(GameObject IgracKojiZove)
    {
        IgracKojiTrenutnoZove = IgracKojiZove;
        for (int i = 0; i < 4; i++)
        {         
            if (IgracKojiTrenutnoZove != Igrac1 && BojaAduta != "Src" && BojaAduta != "Tik" && BojaAduta != "Zel" && BojaAduta != "Zir")
            {
                yield return new WaitForSecondsRealtime(2);
                AiAkoJeNaReduZaZvanjeZove(IgracKojiTrenutnoZove);               
                if (IgracKojiTrenutnoZove != Igrac1 && BojaAduta != "Src" && BojaAduta != "Tik" && BojaAduta != "Zel" && BojaAduta != "Zir")
                {
                    ZoviDalje(IgracKojiTrenutnoZove);
                    IgracKojiTrenutnoZove = IgracKojiZoveSljedeci;
                    continue;
                }
                else if (BojaAduta == "Src" || BojaAduta == "Tik" || BojaAduta == "Zel" || BojaAduta == "Zir")
                {                  
                    break;
                }
            }
            else if (IgracKojiTrenutnoZove == Igrac1)
            {              
                ZoviAdutPloca.SetActive(true);
                if (IgracNaMusu == Igrac1)
                {
                    ZoviDaljeButton.SetActive(false);
                }
                continue;
            }
            
        }
    }  
    public void ZoviDalje(GameObject IgracKojiJeSadNaReduZaZvanje )
    {
        GameObject SljedeciIgrac = null;
        
        if (IgracKojiJeSadNaReduZaZvanje == Igrac1)
        {
            SljedeciIgrac = Igrac2;
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

        for (int i = 0; i < IgracNaReduZaZvanje.transform.childCount - 2; i++)
        {
            GameObject KartaURuci = IgracNaReduZaZvanje.transform.GetChild(i).gameObject;
            SveKarteURuciIgraca.Add(KartaURuci);

        }
        foreach (GameObject Karta in SveKarteURuciIgraca) {
            Ai KartaAi = Karta.GetComponent<Ai>();
            if (KartaAi.BojaKarte == "Src")
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

        else if (IgracNaReduZaZvanje == IgracNaMusu)
        {
            int[] SveVrijednosti = { VrijednostSrc, VrijednostTik, VrijednostZel, VrijednostZir };
            int NajvecaVrijednost = 0;
            int IndexNajveceVrijednosti = 0;
            for (int i = 0; i < SveVrijednosti.Length; i++)
            {
                if (SveVrijednosti[i] > NajvecaVrijednost)
                {
                    NajvecaVrijednost = SveVrijednosti[i];
                    IndexNajveceVrijednosti = i;
                }
            }

            if (IndexNajveceVrijednosti == 0)
            {
                PostaviVrijednostiAduta("Src");
                PozvanoSrce(IgracKojiTrenutnoZove);
            }
            if (IndexNajveceVrijednosti == 1)
            {
                PostaviVrijednostiAduta("Tik");
                PozvanoTikva(IgracKojiTrenutnoZove);
            }
            if (IndexNajveceVrijednosti == 2)
            {
                PostaviVrijednostiAduta("Zel");
                PozvanoZelje(IgracKojiTrenutnoZove);
            }
            if (IndexNajveceVrijednosti == 3)
            {
                PostaviVrijednostiAduta("Zir");
                PozvanoZir(IgracKojiTrenutnoZove);
            }
            
        }
        else
        {
            PozvanoDalje(IgracKojiTrenutnoZove);         
        }       

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
            PozicijaNaKojuIgracOdigra = GameObject.Find(ImePozicije).gameObject;

            Spremanje.PozicijaKojaMoraBitiPopunjenaPrvo = PozicijaNaKojuIgracOdigra;
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
                }
                else if(NajvecaKarta != null)
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
                
                if (NajveciAdut == null)
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
            if (NajvecaKarta != null)
            {
            print(IgracKojiIgra.name + " Ima najvecu kartu" + NajvecaKarta.name);
            }
            if(NajmanjaKarta != null)
            {
            print(IgracKojiIgra.name + " Ima najmanju kartu" + NajmanjaKarta.name);
            }
            print("-----------------------");
            if(NajveciAdut != null)
            {
            print(IgracKojiIgra.name + " Ima najvecu kartu u adutu" + NajveciAdut.name);
            }
            if(NajmanjiAdut != null)
            {
            print(IgracKojiIgra.name + " Ima najmanju kartu u adutu" + NajmanjiAdut.name);
            }

            
             if (NajvecaKarta != null && NajmanjaKarta != null )
            {
                Ai NajvecaKartaAi = NajvecaKarta.GetComponent<Ai>();
                if(NajvecaKartaAi.VrijednostKarte == 11)
                {
                    Selectable PokaziKartu = NajvecaKarta.GetComponent<Selectable>();
                    PokaziKartu.KartaOkrenutaPremaGore = true;
                 
                    NajvecaKarta.transform.SetParent(PozicijaNaKojuIgracOdigra.transform);
                    NajvecaKarta.transform.position = PozicijaNaKojuIgracOdigra.transform.position;
                }
                else
                {
                    Selectable PokaziKartu = NajmanjaKarta.GetComponent<Selectable>();
                    PokaziKartu.KartaOkrenutaPremaGore = true;
                  
                    NajmanjaKarta.transform.SetParent(PozicijaNaKojuIgracOdigra.transform);
                    NajmanjaKarta.transform.position = PozicijaNaKojuIgracOdigra.transform.position;
                }
            }
            else if(NajmanjaKarta == null && NajvecaKarta != null)
            {
                Selectable PokaziKartu = NajvecaKarta.GetComponent<Selectable>();
                PokaziKartu.KartaOkrenutaPremaGore = true;
              
                NajvecaKarta.transform.SetParent(PozicijaNaKojuIgracOdigra.transform);
                NajvecaKarta.transform.position = PozicijaNaKojuIgracOdigra.transform.position;
            } 
            else if ( NajvecaKarta == null && NajmanjaKarta == null && NajveciAdut != null || NajmanjiAdut != null)
            {               
                 Ai NajveciAdutAi = NajveciAdut.GetComponent<Ai>();
                if (NajveciAdutAi.VrijednostKarte == 20)
                {
                    Selectable PokaziKartu = NajveciAdut.GetComponent<Selectable>();
                    PokaziKartu.KartaOkrenutaPremaGore = true;
                    NajveciAdut.transform.SetParent(PozicijaNaKojuIgracOdigra.transform);
                    NajveciAdut.transform.position = PozicijaNaKojuIgracOdigra.transform.position;
                }               
                else if (NajmanjiAdut == null && NajveciAdut != null)
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
    public  void OdluciKojiIgracJeNaReduITajIgracIgra(GameObject IgracNaRedu)
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
        IgracKojiJeSadaNaRedu = IgracNaRedu;
    }
    private void OnMouseDown() 
    {
        if (OdigranoPozicija1.transform.childCount == 1 && OdigranoPozicija2.transform.childCount == 1 && OdigranoPozicija3.transform.childCount == 1 && OdigranoPozicija4.transform.childCount == 1)
        {
            Spremanje.PozicijaKojaMoraBitiPopunjenaPrvo = null;
            StartCoroutine(PocistiPlocuNakonIgreDelay());
        }
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
        GameObject OdigranaKartaPozicija1 = null;
        GameObject OdigranaKartaPozicija2= null;
        GameObject OdigranaKartaPozicija3= null;
        GameObject OdigranaKartaPozicija4 = null;
        GameObject PrvaOdigrana = null;
        GameObject NajacaPozicija = null;
        GameObject NajaciIgrac = null;

        GameObject[] PozicijePoReduOdigrane = PozicijePoreduOdPRvogaIgraca();
        if (PozicijePoReduOdigrane != null)
        {
            if (OdigranoPozicija1.transform.childCount == 1 && OdigranoPozicija2.transform.childCount == 1 && OdigranoPozicija3.transform.childCount == 1 && OdigranoPozicija4.transform.childCount == 1)
            {
                List<GameObject> SveOdigraneKarte = new List<GameObject>();
                for (int a = 0; a < PozicijePoReduOdigrane.Length; a++)
                {
                    if (PozicijePoReduOdigrane[a] == OdigranoPozicija1)
                    {
                        OdigranaKartaPozicija1 = PozicijePoReduOdigrane[a].transform.GetChild(0).gameObject;
                        SveOdigraneKarte.Add(OdigranaKartaPozicija1);
                    }
                    else if (PozicijePoReduOdigrane[a] == OdigranoPozicija2)
                    {
                        OdigranaKartaPozicija2 = PozicijePoReduOdigrane[a].transform.GetChild(0).gameObject;
                        SveOdigraneKarte.Add(OdigranaKartaPozicija2);
                    }
                    else if (PozicijePoReduOdigrane[a] == OdigranoPozicija3)
                    {
                        OdigranaKartaPozicija3 = PozicijePoReduOdigrane[a].transform.GetChild(0).gameObject;
                        SveOdigraneKarte.Add(OdigranaKartaPozicija3);
                    }
                    else if (PozicijePoReduOdigrane[a] == OdigranoPozicija4)
                    {
                        OdigranaKartaPozicija4 = PozicijePoReduOdigrane[a].transform.GetChild(0).gameObject;
                        SveOdigraneKarte.Add(OdigranaKartaPozicija4);
                    }
                }


                for (int i = 0; i < SveOdigraneKarte.Count; i++)
                {
                    Ai TrenutnaPozicijaAi = SveOdigraneKarte[i].GetComponent<Ai>();
                    if (PrvaOdigrana == null)
                    {
                        PrvaOdigrana = SveOdigraneKarte[i];
                        NajacaPozicija = SveOdigraneKarte[i];
                    }
                    else if (PrvaOdigrana != null)
                    {
                        Ai PrvaOdigranaAi = PrvaOdigrana.GetComponent<Ai>();
                        Ai NajacaPozicijaAi = NajacaPozicija.GetComponent<Ai>();
                        if (NajacaPozicijaAi.Adut == false && TrenutnaPozicijaAi.Adut == false && NajacaPozicijaAi.RangKarte < TrenutnaPozicijaAi.RangKarte && NajacaPozicijaAi.BojaKarte == TrenutnaPozicijaAi.BojaKarte && PrvaOdigranaAi.BojaKarte == TrenutnaPozicijaAi.BojaKarte)
                        {
                            NajacaPozicija = SveOdigraneKarte[i];
                        }
                        else if (NajacaPozicijaAi.Adut == false && TrenutnaPozicijaAi.Adut == true)
                        {
                            NajacaPozicija = SveOdigraneKarte[i];
                        }
                        else if (NajacaPozicijaAi.Adut == true && TrenutnaPozicijaAi.Adut == true && NajacaPozicijaAi.RangKarte < TrenutnaPozicijaAi.RangKarte)
                        {
                            NajacaPozicija = SveOdigraneKarte[i];
                        }
                        else if (NajacaPozicijaAi.Adut == false && TrenutnaPozicijaAi.Adut == false && NajacaPozicijaAi.BojaKarte != TrenutnaPozicijaAi.BojaKarte)
                        {
                            NajacaPozicija = PrvaOdigrana;
                        }

                    }
                }


                if (OdigranaKartaPozicija1 != null && OdigranaKartaPozicija2 != null && OdigranaKartaPozicija3 != null & OdigranaKartaPozicija4 != null)
                {
                    if (NajacaPozicija == OdigranaKartaPozicija1)
                    {
                        PremjestiViseKarataNaJeduLokaciju(OdigranaKartaPozicija1, OdigranaKartaPozicija2, OdigranaKartaPozicija3, OdigranaKartaPozicija4, IgraciJedanITri);
                        return NajaciIgrac = Igrac1;
                    }
                    else if (NajacaPozicija == OdigranaKartaPozicija2)
                    {
                        PremjestiViseKarataNaJeduLokaciju(OdigranaKartaPozicija1, OdigranaKartaPozicija2, OdigranaKartaPozicija3, OdigranaKartaPozicija4, IgraciDvaiCetri);
                        return NajaciIgrac = Igrac2;
                    }
                    else if (NajacaPozicija == OdigranaKartaPozicija3)
                    {
                        PremjestiViseKarataNaJeduLokaciju(OdigranaKartaPozicija1, OdigranaKartaPozicija2, OdigranaKartaPozicija3, OdigranaKartaPozicija4, IgraciJedanITri);
                        return NajaciIgrac = Igrac3;
                    }
                    else if (NajacaPozicija == OdigranaKartaPozicija4)
                    {
                        PremjestiViseKarataNaJeduLokaciju(OdigranaKartaPozicija1, OdigranaKartaPozicija2, OdigranaKartaPozicija3, OdigranaKartaPozicija4, IgraciDvaiCetri);
                        return NajaciIgrac = Igrac4;
                    }
                }
            }
        }
        return null;
        
    }
    public GameObject[] PozicijePoreduOdPRvogaIgraca()
    {
       
        if(Spremanje.PozicijaKojaMoraBitiPopunjenaPrvo == OdigranoPozicija1)
        {
           
            GameObject[] Igrac1Pozicije = { OdigranoPozicija1, OdigranoPozicija2, OdigranoPozicija3, OdigranoPozicija4 };
            return Igrac1Pozicije;

        }
        else if (Spremanje.PozicijaKojaMoraBitiPopunjenaPrvo == OdigranoPozicija2)
        {
            
            GameObject[] Igrac2Pozicije = {OdigranoPozicija2, OdigranoPozicija3, OdigranoPozicija4, OdigranoPozicija1};
            return Igrac2Pozicije;
        }
        else  if (Spremanje.PozicijaKojaMoraBitiPopunjenaPrvo == OdigranoPozicija3)
        {
            
            GameObject[] Igrac3Pozicije = {  OdigranoPozicija3, OdigranoPozicija4, OdigranoPozicija1, OdigranoPozicija2 };
            return Igrac3Pozicije;
        }
        else if (Spremanje.PozicijaKojaMoraBitiPopunjenaPrvo == OdigranoPozicija4)
        {
           
            GameObject[] Igrac4Pozicije = { OdigranoPozicija4, OdigranoPozicija1, OdigranoPozicija2, OdigranoPozicija3 };
            return Igrac4Pozicije;
        }
        else if(Spremanje.PozicijaKojaMoraBitiPopunjenaPrvo == null)
        {
           
            GameObject[] IgracPozicije = { OdigranoPozicija1, OdigranoPozicija2, OdigranoPozicija3, OdigranoPozicija4 };
            return IgracPozicije;
        }
        else{
          
            return null;
        }

    }
    public void OdluciKojaKartaJeDoSadNajvecaIStoTrebaOdbaciti() {

        GameObject[] SveOdigranePozicije = PozicijePoreduOdPRvogaIgraca();
        GameObject PrvaOdigrana= null;
        GameObject NajcaKarta=null;
        GameObject SljedeciIgrac=null;
        if (SveOdigranePozicije != null)
        {
            for (int i = 0; i < SveOdigranePozicije.Length - 1; i++)
            {

                if (SveOdigranePozicije[i] == OdigranoPozicija1)
                {
                    Spremanje.IgracKojiJeSadNaRedu = Igrac1;
                }
                GameObject Pozicija = SveOdigranePozicije[i];
                GameObject SljedecaPozicija = SveOdigranePozicije[i + 1];


                if (Pozicija.transform.GetChildCount() == 1)
                {
                    // print(SveOdigranePozicije[i].name);
                    if (PrvaOdigrana == null)
                    {
                        PrvaOdigrana = Pozicija.transform.GetChild(0).gameObject;
                        NajcaKarta = PrvaOdigrana;
                        PrvaOdigranaKartaURundi = PrvaOdigrana;
                        Spremanje.PrvaOdigranaKarta = PrvaOdigrana;
                        Spremanje.NajacaKartaDoSad = NajcaKarta;
                    }
                    if (PrvaOdigrana != null)
                    {
                        string ImeSljedecegIgraca = "Igrac" + SljedecaPozicija.name.Substring(SljedecaPozicija.name.Length - 1);
                        SljedeciIgrac = GameObject.Find(ImeSljedecegIgraca);
                        GameObject TrenutnaKarta = Pozicija.transform.GetChild(0).gameObject;
                        Ai TrenutnaKartaAi = TrenutnaKarta.GetComponent<Ai>();
                        Ai PrvaOdigranaAi = PrvaOdigrana.GetComponent<Ai>();
                        Ai NajacaKartaAi = NajcaKarta.GetComponent<Ai>();
                        if (SljedeciIgrac != Igrac1)
                        {
                            if (SljedecaPozicija.transform.GetChildCount() == 0)
                            {
                                if (PrvaOdigrana == TrenutnaKarta)
                                {
                                    AiOdluciKojuKartuBacaIzRuke(SljedeciIgrac, SljedecaPozicija, PrvaOdigrana, PrvaOdigrana);
                                }
                                else if (PrvaOdigranaAi.RangKarte < TrenutnaKartaAi.RangKarte && PrvaOdigranaAi.BojaKarte == TrenutnaKartaAi.BojaKarte)
                                {
                                    NajcaKarta = TrenutnaKarta;
                                    Spremanje.NajacaKartaDoSad = NajcaKarta;
                                    AiOdluciKojuKartuBacaIzRuke(SljedeciIgrac, SljedecaPozicija, NajcaKarta, PrvaOdigrana);
                                }
                                else if (PrvaOdigranaAi.RangKarte > TrenutnaKartaAi.RangKarte && PrvaOdigranaAi.BojaKarte == TrenutnaKartaAi.BojaKarte)
                                {
                                    AiOdluciKojuKartuBacaIzRuke(SljedeciIgrac, SljedecaPozicija, PrvaOdigrana, PrvaOdigrana);
                                }
                                else if (PrvaOdigranaAi.BojaKarte != TrenutnaKartaAi.BojaKarte && TrenutnaKartaAi.Adut == true)
                                {
                                    NajcaKarta = TrenutnaKarta;
                                    Spremanje.NajacaKartaDoSad = NajcaKarta;
                                    AiOdluciKojuKartuBacaIzRuke(SljedeciIgrac, SljedecaPozicija, NajcaKarta, PrvaOdigrana);
                                }
                                else if (NajacaKartaAi.RangKarte < TrenutnaKartaAi.RangKarte && PrvaOdigranaAi.BojaKarte == TrenutnaKartaAi.BojaKarte)
                                {
                                    NajcaKarta = TrenutnaKarta;
                                    Spremanje.NajacaKartaDoSad = NajcaKarta;
                                    AiOdluciKojuKartuBacaIzRuke(SljedeciIgrac, SljedecaPozicija, NajcaKarta, PrvaOdigrana);
                                }
                                else if (NajacaKartaAi.RangKarte > TrenutnaKartaAi.RangKarte && PrvaOdigranaAi.BojaKarte == TrenutnaKartaAi.BojaKarte)
                                {
                                    AiOdluciKojuKartuBacaIzRuke(SljedeciIgrac, SljedecaPozicija, NajcaKarta, PrvaOdigrana);
                                }
                                else if (PrvaOdigranaAi.Adut == false && NajacaKartaAi.Adut == false && TrenutnaKartaAi.Adut == true)
                                {
                                    NajcaKarta = TrenutnaKarta;
                                    Spremanje.NajacaKartaDoSad = NajcaKarta;
                                    AiOdluciKojuKartuBacaIzRuke(SljedeciIgrac, SljedecaPozicija, NajcaKarta, PrvaOdigrana);
                                }
                                else if (NajacaKartaAi.Adut == true && NajacaKartaAi.RangKarte < TrenutnaKartaAi.RangKarte && TrenutnaKartaAi.Adut == true)
                                {
                                    NajcaKarta = TrenutnaKarta;
                                    Spremanje.NajacaKartaDoSad = NajcaKarta;
                                    AiOdluciKojuKartuBacaIzRuke(SljedeciIgrac, SljedecaPozicija, NajcaKarta, PrvaOdigrana);
                                }
                                else if (NajacaKartaAi.Adut == true && NajacaKartaAi.RangKarte > TrenutnaKartaAi.RangKarte && TrenutnaKartaAi.Adut == true)
                                {
                                    AiOdluciKojuKartuBacaIzRuke(SljedeciIgrac, SljedecaPozicija, NajcaKarta, PrvaOdigrana);
                                }
                                else if (NajacaKartaAi.Adut == true && TrenutnaKartaAi.Adut == false)
                                {
                                    AiOdluciKojuKartuBacaIzRuke(SljedeciIgrac, SljedecaPozicija, NajcaKarta, PrvaOdigrana);
                                }
                                else if (PrvaOdigranaAi.BojaKarte != TrenutnaKartaAi.BojaKarte && TrenutnaKartaAi.Adut == false)
                                {
                                    AiOdluciKojuKartuBacaIzRuke(SljedeciIgrac, SljedecaPozicija, NajcaKarta, PrvaOdigrana);
                                }
                            }
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
    public void AiOdluciKojuKartuBacaIzRuke(GameObject SljedeciNaReduIgrac, GameObject SljedecaPozicijaNaReduOdigranoIgrac, GameObject NajvecaOdigranaKartaDoSad,GameObject PrvaOdigranaKarta)
    {
                
        Ai PrvaOdigranaKartaAi = PrvaOdigranaKarta.GetComponent<Ai>();
        Ai NajvecaKartaDoSad = NajvecaOdigranaKartaDoSad.GetComponent<Ai>();
        
        GameObject NajvecaKartaUIstojBoji = null;           
        GameObject NajmanjaKartaUIstojBoji = null;        
        GameObject NajveciAdut = null;      
        GameObject NajmaniAdut = null;       
        GameObject NajmanjaKatraVanBoje = null;        

        for (int i = 0; i < SljedeciNaReduIgrac.transform.childCount; i++)
        {

            GameObject TrenutnaKartaUPetlji = SljedeciNaReduIgrac.transform.GetChild(i).gameObject;
            Ai TrenutnaKarta = TrenutnaKartaUPetlji.GetComponent<Ai>();

            if (PrvaOdigranaKartaAi.BojaKarte == TrenutnaKarta.BojaKarte)
            {
                if (NajvecaKartaUIstojBoji == null)
                {
                    NajvecaKartaUIstojBoji = TrenutnaKartaUPetlji;

                }

                else if (NajvecaKartaUIstojBoji != null)
                {
                    Ai NajvecaKartaUIstojBojiAi = NajvecaKartaUIstojBoji.GetComponent<Ai>();

                    if (NajvecaKartaUIstojBojiAi.RangKarte < TrenutnaKarta.RangKarte /*|| PrvaOdigranaKartaAi.BojaKarte == TrenutnaKarta.BojaKarte*/)
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
            else if (TrenutnaKarta.Adut != true && PrvaOdigranaKartaAi.BojaKarte != TrenutnaKarta.BojaKarte)
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
            if (NajvecaKartaDoSad.RangKarte > NajvecaKartaUIstojBojiAi.RangKarte && NajvecaKartaDoSad.BojaKarte == PrvaOdigranaKartaAi.BojaKarte || NajvecaKartaDoSad.BojaKarte != PrvaOdigranaKartaAi.BojaKarte && NajvecaKartaDoSad.Adut == true)
            {
                if (NajmanjaKartaUIstojBoji != null)
                {

                    Selectable PokaziKartu = NajmanjaKartaUIstojBoji.GetComponent<Selectable>();
                    PokaziKartu.KartaOkrenutaPremaGore = true;
                    NajmanjaKartaUIstojBoji.transform.SetParent(SljedecaPozicijaNaReduOdigranoIgrac.transform);
                    NajmanjaKartaUIstojBoji.transform.position = SljedecaPozicijaNaReduOdigranoIgrac.transform.position;
                    // print("Odbaci Najmanju kartu u istoj boji " + SljedecaPozicijaNaReduOdigranoIgrac.name);
                }
                else
                {
                    Selectable PokaziKartu = NajvecaKartaUIstojBoji.GetComponent<Selectable>();
                    PokaziKartu.KartaOkrenutaPremaGore = true;
                    NajvecaKartaUIstojBoji.transform.SetParent(SljedecaPozicijaNaReduOdigranoIgrac.transform);
                    NajvecaKartaUIstojBoji.transform.position = SljedecaPozicijaNaReduOdigranoIgrac.transform.position;
                    // print("Odbaci nema najmanje karte u istoj boji odbaci veču " + SljedecaPozicijaNaReduOdigranoIgrac.name);
                }
            }
            else if (NajvecaKartaDoSad.RangKarte < NajvecaKartaUIstojBojiAi.RangKarte && PrvaOdigranaKartaAi.BojaKarte == NajvecaKartaDoSad.BojaKarte)
            {
                Selectable PokaziKartu = NajvecaKartaUIstojBoji.GetComponent<Selectable>();
                PokaziKartu.KartaOkrenutaPremaGore = true;
                NajvecaKartaUIstojBoji.transform.SetParent(SljedecaPozicijaNaReduOdigranoIgrac.transform);
                NajvecaKartaUIstojBoji.transform.position = SljedecaPozicijaNaReduOdigranoIgrac.transform.position;
                // print("Odbaci najvecu kartu u istoj boji " + SljedecaPozicijaNaReduOdigranoIgrac.name);

            }

        }
        else if (NajveciAdut != null)
        {
            Ai NajaciAdutAi = NajveciAdut.GetComponent<Ai>();
            if (NajvecaKartaDoSad.BojaKarte == PrvaOdigranaKartaAi.BojaKarte || NajvecaKartaDoSad.Adut == true && PrvaOdigranaKartaAi.Adut ==false && NajvecaKartaDoSad.RangKarte < NajaciAdutAi.RangKarte)
            {
                Selectable PokaziKartu = NajveciAdut.GetComponent<Selectable>();
                PokaziKartu.KartaOkrenutaPremaGore = true;
                NajveciAdut.transform.SetParent(SljedecaPozicijaNaReduOdigranoIgrac.transform);
                NajveciAdut.transform.position = SljedecaPozicijaNaReduOdigranoIgrac.transform.position;
                //print("Odbaci najvecu kartu u adutu " + SljedecaPozicijaNaReduOdigranoIgrac.name);
            }
            else if(NajvecaKartaDoSad.Adut == true && PrvaOdigranaKartaAi.Adut == false && NajvecaKartaDoSad.RangKarte > NajaciAdutAi.RangKarte)
            {

                if (NajmaniAdut != null)
                {
                    Selectable PokaziKartu = NajmaniAdut.GetComponent<Selectable>();
                    PokaziKartu.KartaOkrenutaPremaGore = true;
                    NajmaniAdut.transform.SetParent(SljedecaPozicijaNaReduOdigranoIgrac.transform);
                    NajmaniAdut.transform.position = SljedecaPozicijaNaReduOdigranoIgrac.transform.position;
                    //print("Odbaci najmanju kartu u adutu " + SljedecaPozicijaNaReduOdigranoIgrac.name);
                
                }
                else if (NajaciAdutAi != null)
                {
                    Selectable PokaziKartu = NajveciAdut.GetComponent<Selectable>();
                    PokaziKartu.KartaOkrenutaPremaGore = true;
                    NajveciAdut.transform.SetParent(SljedecaPozicijaNaReduOdigranoIgrac.transform);
                    NajveciAdut.transform.position = SljedecaPozicijaNaReduOdigranoIgrac.transform.position;
                }
            }


        }
         else if (NajmanjaKatraVanBoje != null)
         {
            
                Selectable PokaziKartu = NajmanjaKatraVanBoje.GetComponent<Selectable>();
                PokaziKartu.KartaOkrenutaPremaGore = true;
                NajmanjaKatraVanBoje.transform.SetParent(SljedecaPozicijaNaReduOdigranoIgrac.transform);
                NajmanjaKatraVanBoje.transform.position = SljedecaPozicijaNaReduOdigranoIgrac.transform.position;
               // print("Odbaci najmanju kartu u različitoj boji " + SljedecaPozicijaNaReduOdigranoIgrac.name);
            
        }

        

    }
}



    



    

