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
        PoravnajKarte();            
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
        
        if(IgracKojiPrviBacaKartu == null && IgracKojiJeSadaNaRedu != null)
        {
            IgracKojiPrviBacaKartu = IgracKojiJeSadaNaRedu;
        }
        
        if(BojaAduta !=null && IzvrsavanjeJednom == false)
        {
            if(Spremanje.PosloziKarte == false)
            {
                PoslozikarteSvimaIgracimaPoveliciniiboji();
                Spremanje.PosloziKarte = true;
            }
           StartCoroutine(AutomatskiProvjeriZvanjaIZovi());  
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
        
    }

    /*Promjeni kut pod kojem je jedna karta kod igrača 2 i igrača 4 okrenuta. Služi kao popravak na prijašnju
     grešku is skripte Bela.cs koju neznam kako drugačije složiti.*/
    public void PoravnajKarte()
    {      
        if (Spremanje.PoravnajKarte == false)
        {
            if (Igrac2.transform.childCount != 0 && Igrac4.transform.childCount != 0)
            {
                GameObject Karta1 = Igrac2.transform.GetChild(0).gameObject;
                GameObject Karta2 = Igrac4.transform.GetChild(0).gameObject;
                Karta1.transform.eulerAngles = new Vector3(Karta1.transform.eulerAngles.x, Karta1.transform.eulerAngles.y, Karta1.transform.eulerAngles.z+90f);
                Karta2.transform.eulerAngles = new Vector3(Karta2.transform.eulerAngles.x, Karta2.transform.eulerAngles.y, Karta2.transform.eulerAngles.z+90f);
                Spremanje.PoravnajKarte = true;
            }
        }   
    }

    /*Provjerava sve karte koje su odbačene od strane igrača te ih usporedi i spremi najaču do sad.*/
    private void PomocnaFunkcijaKojaOdluciKojaJeKartaZaSadNajvecaISpremiJu()
    {
        GameObject[] OdPrvoga= PozicijePoreduOdPRvogaIgraca();
        GameObject PrvaOdigrana = null;
        GameObject NajacaKarta = null;
        for(int i = 0; i < OdPrvoga.Length;i++)
        {
            
            if (OdPrvoga[i].transform.childCount != 0)
            {
               
                GameObject TrenutnaKarta = OdPrvoga[i].transform.GetChild(0).gameObject;
                
                if ( PrvaOdigrana == null)
                {
              
                    Spremanje.PrvaOdigranaKarta = TrenutnaKarta;
                        PrvaOdigrana = TrenutnaKarta;
                    Spremanje.NajacaKartaDoSad = TrenutnaKarta;
                    NajacaKarta = TrenutnaKarta;

                }
                else if (TrenutnaKarta != PrvaOdigrana)
                {
             
                    Ai PrvaOdigranaAi = PrvaOdigrana.GetComponent<Ai>();
                    Ai TrenutnaKartaAi = TrenutnaKarta.GetComponent<Ai>();
                    Ai NajacaKartaAi = NajacaKarta.GetComponent<Ai>();
                    
                    if(TrenutnaKartaAi.RangKarte > NajacaKartaAi.RangKarte && NajacaKartaAi.BojaKarte == PrvaOdigranaAi.BojaKarte && TrenutnaKartaAi.BojaKarte == PrvaOdigranaAi.BojaKarte)
                    {
                    
                        NajacaKarta = TrenutnaKarta;
                        Spremanje.NajacaKartaDoSad = TrenutnaKarta;
                    }
                    else if(NajacaKartaAi.BojaKarte == PrvaOdigranaAi.BojaKarte && NajacaKartaAi.Adut == false && TrenutnaKartaAi.Adut == true)
                    {
                  
                        NajacaKarta = TrenutnaKarta;
                        Spremanje.NajacaKartaDoSad = TrenutnaKarta;
                    }
                    else if( NajacaKartaAi.Adut==true && TrenutnaKartaAi.Adut == true && TrenutnaKartaAi.RangKarte > NajacaKartaAi.RangKarte)
                    {
                    
                        NajacaKarta = TrenutnaKarta;
                        Spremanje.NajacaKartaDoSad = TrenutnaKarta;
                    }

                    
                }
                
            }
        }      
    }
    /*Nakon zvanja aduta posloži karte u ruci igrača radi lakše i ljepše igre, te u ruci drugih igrača 
     da se kod zvanja nalaze jedna pored druge*/
    public void PoslozikarteSvimaIgracimaPoveliciniiboji()
    {     
        PosloziKartePoBojamaIVelicinni(Igrac1);
        PosloziKartePoBojamaIVelicinni(Igrac2);
        PosloziKartePoBojamaIVelicinni(Igrac3);
        PosloziKartePoBojamaIVelicinni(Igrac4);

    }
     /*Premijesti Karte iste boje i sortira ih po rangu karte, potrebno zadati igrača čije karte želimo sortirati*/
    private void PosloziKartePoBojamaIVelicinni(GameObject IgracPosloziKarte)
    {
        List<GameObject> KarteURuci = new List<GameObject>();
        List<Vector3> PozicijeKarataOrginalno = new List<Vector3>();

        
        for (int a=0;a< IgracPosloziKarte.transform.childCount; a++)
        {
          
            KarteURuci.Add(IgracPosloziKarte.transform.GetChild(a).gameObject);
            PozicijeKarataOrginalno.Add(IgracPosloziKarte.transform.GetChild(a).gameObject.transform.position);
        }


        List<GameObject> SortiranoPoBojama = new List<GameObject>();
        
        for(int i = 0; i < KarteURuci.Count; i++)
        {
            GameObject karta = KarteURuci[i];
            Ai kartaAi = karta.GetComponent<Ai>();
            if (SortiranoPoBojama.Contains(karta))
            {
                continue;
            }
            else
            {
                    SortiranoPoBojama.Add(karta);
                foreach (GameObject karta2 in KarteURuci)
                {
                    Ai karta2Ai = karta2.GetComponent<Ai>();
                    if (kartaAi.BojaKarte == karta2Ai.BojaKarte && karta != karta2)
                    {
                        if (SortiranoPoBojama.Contains(karta2))
                        {
                            continue;
                        }
                        else
                        {
                            SortiranoPoBojama.Add(karta2);
                        }
                    }

                }

            }
            

        }

        List<GameObject> SortiranoPoBojamaIRangu = new List<GameObject>();
        

        for (int i = 0; i < SortiranoPoBojama.Count; i++)
        {
            GameObject karta = SortiranoPoBojama[i];
            Ai kartaAi = karta.GetComponent<Ai>();
            List<int> PrivremenaRangLista = new List<int>();
            if (SortiranoPoBojamaIRangu.Contains(karta))
            {
                continue;
            }
            else
            {
                PrivremenaRangLista.Add(kartaAi.RangZvanja);
                foreach (GameObject karta2 in SortiranoPoBojama)
                {
                    Ai karta2Ai = karta2.GetComponent<Ai>();
                    if (kartaAi.BojaKarte == karta2Ai.BojaKarte && karta != karta2)
                    {
                        PrivremenaRangLista.Add(karta2Ai.RangZvanja);
                    }
                }
            }
         
            PrivremenaRangLista.Sort();
            PrivremenaRangLista.Reverse();

            for (int a = 0; a < PrivremenaRangLista.Count; a++)
            {
                int Rang = PrivremenaRangLista[a];
                foreach (GameObject karta3 in SortiranoPoBojama)
                {
                    Ai karta3Ai = karta3.GetComponent<Ai>();
                    if (kartaAi.BojaKarte == karta3Ai.BojaKarte && karta3Ai.RangZvanja == Rang)
                    {
                        SortiranoPoBojamaIRangu.Add(karta3);
                    }
                }

            }
            
        }
      
        for(int a = 0; a < SortiranoPoBojamaIRangu.Count; a++)
        {
            GameObject Karta = SortiranoPoBojamaIRangu[a];
            Karta.transform.position = PozicijeKarataOrginalno[a];
        }
        
    }
    /*Provjerava dali Ai igrač ima belu u ruci i zove belu, dodaje bodove i prikazuje iskočni prozor.*/
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
                                   if(IgracNaTojPoziciji == Igrac2 || IgracNaTojPoziciji==Igrac4)
                                    {
                                        ZvanjaVi = ZvanjaVi + 20;
                                    }
                                    else if (IgracNaTojPoziciji == Igrac3)
                                    {
                                        ZvanjaMi = ZvanjaMi + 20;
                                    }

                                    IgracZvaoJeBelu.SetActive(true);
                                    PokaziImeIgracaKojiJeZvaoBelu(IgracNaTojPoziciji);
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
    /*Vadi iz Spremanje string igraca koji je zvao belu*/
    private void PokaziImeIgracaKojiJeZvaoBelu(GameObject ImeIgracaKojJeZvao)
    {
        if (ImeIgracaKojJeZvao.name == Igrac1.name)
        {
            if (Spremanje.Igrac1 != null)
            {
                IgracKojiJeZvaoBelu.SetText(Spremanje.Igrac1);
            }
            else
            {
                IgracKojiJeZvaoBelu.SetText(ImeIgracaKojJeZvao.name);
            }
        }
        else if (ImeIgracaKojJeZvao.name == Igrac2.name)
        {
            if (Spremanje.Igrac2 != null)
            {
                IgracKojiJeZvaoBelu.SetText(Spremanje.Igrac2);
            }
            else
            {
                IgracKojiJeZvaoBelu.SetText(ImeIgracaKojJeZvao.name);
            }
        }
        else if (ImeIgracaKojJeZvao.name == Igrac3.name)
        {
            if (Spremanje.Igrac3 != null)
            {
                IgracKojiJeZvaoBelu.SetText(Spremanje.Igrac3);
            }
            else
            {
                IgracKojiJeZvaoBelu.SetText(ImeIgracaKojJeZvao.name);
            }
        }
        else if (ImeIgracaKojJeZvao.name == Igrac4.name)
        {
            if (Spremanje.Igrac4 != null)
            {
                IgracKojiJeZvaoBelu.SetText(Spremanje.Igrac4);
            }
            else
            {
                IgracKojiJeZvaoBelu.SetText(ImeIgracaKojJeZvao.name);
            }
        }

    }
    /*Provjerava dali Igrac koji igra igru ima belu u ruci i daje mu opciju preko iskočnog prozora da ju zove ili da ne zove*/
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
    /*Ako igrac koji igra zove belu dodaju se bodovi njemu.*/
    public void Igrac1ZoveBelu()
    {
        ZvanjaMi = ZvanjaMi + 20;
    }
    /*Funkcija koja onemogučava bespotrebno vrtenje koda u Update metodi te se u 
    svakom slučaju isti kod izvrti samo jednom ili max 2 puta.*/
    public void OdbaciKartuAkoNaRedu()
    {
                     
        if (Spremanje.PozicijaKojaMoraBitiPopunjenaPrvo != null)
        {
            
            if (Spremanje.PozicijaKojaMoraBitiPopunjenaPrvo == OdigranoPozicija4)
            {
                if (OdigranoPozicija4.transform.childCount != 0 && OdigranoPozicija1.transform.childCount == 0 )
                {
                    if (Spremanje.PrvaOdigranaKarta == null && Spremanje.NajacaKartaDoSad == null)
                    {
                        Spremanje.PrvaOdigranaKarta = OdigranoPozicija4.transform.GetChild(0).gameObject;
                        Spremanje.NajacaKartaDoSad = OdigranoPozicija4.transform.GetChild(0).gameObject;
                    }
                }
            }

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
    /*Tijekom svakog novog kruga partije igrac koji je prvi na redu odigra kartu.*/
    public IEnumerator PrviNaReduODigraKartuPrviPut()
    {
        yield return new WaitForSecondsRealtime(3);
        if (Igrac1.transform.childCount == 8 && Igrac2.transform.childCount == 8 && Igrac3.transform.childCount == 8 && Igrac4.transform.childCount == 8)
        {
            OdluciKojiIgracJeNaReduITajIgracIgra(IgracKojiPrviZove);
            OdbaciKartuAudio.Play();
        }
    }
    /*Igrac koji je u ovom krugu prvi na redu za zvanje zove.*/
    public void OdlučiKojiIgracSadaPrviZove()
    {
        if(Spremanje.IgracKojiSadaZove == 1)
        {
            IgracKojiPrviZove = Igrac1;
            IgracNaMusu = Igrac4;
            Spremanje.IgracKojiJeSadNaRedu = Igrac1;


        }
        else if (Spremanje.IgracKojiSadaZove == 2)
        {
            IgracKojiPrviZove = Igrac2;
            IgracNaMusu = Igrac1;
            Spremanje.IgracKojiJeSadNaRedu = Igrac2;
            
        }
        else if (Spremanje.IgracKojiSadaZove == 3)
        {
            IgracKojiPrviZove = Igrac3;
            IgracNaMusu = Igrac2;
            Spremanje.IgracKojiJeSadNaRedu = Igrac3;
          
        }
        else if (Spremanje.IgracKojiSadaZove == 4)
        {
            IgracKojiPrviZove = Igrac4;
            IgracNaMusu = Igrac3;
            Spremanje.IgracKojiJeSadNaRedu = Igrac4;
            
        }
        
    }
    /*Usporava izvršavanje koda tako da izgleda koda igrač malo pričeka i "razmisli" pa zove adut ili dalje.*/
    public IEnumerator AiZoviAdutSaZakasnjenjem()
    {
            
        if (Spremanje.ZoviJednom == false)
        {            
            Spremanje.ZoviJednom = true;
            yield return new WaitForSecondsRealtime(2);
            StartCoroutine(AiZovi(IgracKojiPrviZove));
           
        }
        
    }

    /*Funkcija koja je zadužena da provjeri sva moguča zvanja kod svakog igrača i zove ovdje igrač koji igra igru nema
     mogučnost da ne zove nego ako ima zvanja i to je zvanje veče od ostali zvanja mu se automatski pišu.*/
    public IEnumerator AutomatskiProvjeriZvanjaIZovi()
    {       
        int NajveceZvanjeIgrac1 = VratiZvanjeAkoPostojiKodPojedinogIgraca(Igrac1).NajveceZvanje;
        int NajveceZvanjeIgrac2 = VratiZvanjeAkoPostojiKodPojedinogIgraca(Igrac2).NajveceZvanje;
        int NajveceZvanjeIgrac3 = VratiZvanjeAkoPostojiKodPojedinogIgraca(Igrac3).NajveceZvanje;
        int NajveceZvanjeIgrac4 = VratiZvanjeAkoPostojiKodPojedinogIgraca(Igrac4).NajveceZvanje;
        int[] SvaZvanja = { NajveceZvanjeIgrac1, NajveceZvanjeIgrac2, NajveceZvanjeIgrac3, NajveceZvanjeIgrac4 };
        int NajaceZvanje = 0;
        int BrojacJednakihZvanja = 0;        
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
        List<GameObject> KarteKojeSeZovu = VratiZvanjeAkoPostojiKodPojedinogIgraca(NajaciIgrac).KarteKojeSuUZvanju;
        List<GameObject> KarteKojeSeZovu2 = VratiZvanjeAkoPostojiKodPojedinogIgraca(SuigracNajacegaIgraca).KarteKojeSuUZvanju;
        KarteKojeSeZovu.AddRange(KarteKojeSeZovu2);        
        for(int i = 0; i < KarteKojeSeZovu.Count; i++)
        {
            GameObject Karta = KarteKojeSeZovu[i];
            Selectable SelectebleKarta = Karta.GetComponent<Selectable>();
            SelectebleKarta.KartaOkrenutaPremaGore = true;
        }
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
        yield return new WaitForSecondsRealtime(3);

        List<GameObject> PrivremenaLista = VratiZvanjeAkoPostojiKodPojedinogIgraca(Igrac1).KarteKojeSuUZvanju;
        for (int i = 0; i < KarteKojeSeZovu.Count; i++)
        {
            GameObject Karta = KarteKojeSeZovu[i];
            if (PrivremenaLista.Count != 0)
            {
                if (PrivremenaLista.Contains(Karta))
                {
                    continue;
                }
            }
            else
            {
                Selectable SelectebleKarta = Karta.GetComponent<Selectable>();
                SelectebleKarta.KartaOkrenutaPremaGore = false;
            }
            
            
        }      
    }
    /*Vrača sva zvanja koja postoje kod zadanog igrača i vrijednosti koje su bitne za usporedbu u metodi 
     AutomatskiProvjeriZvanjaIZovi() Metoda traži unos igrač kojemu želimo provjeriti zvanja*/
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
    /*Provjerava zvanja u istoj boji i dodaje vrača vrijednost koje su potrebne z usporedbu u metodi AutomatskiProvjeriZvanjaIZovi();*/
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
    /*Ako igrač ima zvanje koje se sastoji od 4 karte iste vrijednosti različitih boja, vrač sve vrijednosti 
     potrebne za usporedbu u metodi AutomatskiProvjeriZvanjaIZovi();*/
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
    /*Zbroji zbroj svih karta  koje su osvojene od strane para igrača.*/
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
    /*Promjeni bitne vrijednosti te restarta igru za sljedeci krug partije.*/
    public IEnumerator RestartajIgruZasljedeciKrugPartije()
    {
        yield return new WaitForSecondsRealtime(2);
        
        if (Igrac1.transform.childCount == 0 && Igrac2.transform.childCount == 0 && Igrac3.transform.childCount == 0 && Igrac4.transform.childCount == 0)
        {
           
            Spremanje.ZoviJednom = false;
            int CistoMi = VratiVrijednostKarataIgraci(IgraciJedanITri);
            int CistoVi = VratiVrijednostKarataIgraci(IgraciDvaiCetri);                      
            GameObject KojiIgracJeZvao = Spremanje.KojiIgracJeZvao;
            print(Spremanje.KojiIgracJeZvao.name);
            if (KojiIgracJeZvao == Igrac1 && CistoMi + ZvanjaMi < CistoVi + ZvanjaVi && CistoMi !=0 || KojiIgracJeZvao == Igrac3 && CistoMi + ZvanjaMi < CistoVi + ZvanjaVi && CistoMi != 0)
            {

                Rezultati.PostaviNoviRezultatMi(0);
                Rezultati.PostaviNoviRezultatVi(CistoMi + ZvanjaMi + CistoVi + ZvanjaVi);
            }
            else if (KojiIgracJeZvao == Igrac2 && CistoMi + ZvanjaMi > CistoVi + ZvanjaVi && CistoVi !=0 || KojiIgracJeZvao == Igrac4 && CistoMi + ZvanjaMi > CistoVi + ZvanjaVi && CistoVi != 0)
            {

                Rezultati.PostaviNoviRezultatMi(CistoMi + ZvanjaMi + CistoVi + ZvanjaVi);
                Rezultati.PostaviNoviRezultatVi(0);
            }
            else if (CistoVi == 0)
            {
                Rezultati.PostaviNoviRezultatMi(CistoMi + ZvanjaMi + CistoVi + ZvanjaVi+90);
                Rezultati.PostaviNoviRezultatVi(0);
            }
            else if (CistoMi == 0)
            {
                Rezultati.PostaviNoviRezultatVi(CistoMi + ZvanjaMi + CistoVi + ZvanjaVi + 90);
                Rezultati.PostaviNoviRezultatMi(0);
            }
            else
            {
                Rezultati.PostaviNoviRezultatMi(CistoMi + ZvanjaMi);
                Rezultati.PostaviNoviRezultatVi(CistoVi + ZvanjaVi);
            }

            
            if(Spremanje.IgracKojiSadaZove != 4)
            {
                Spremanje.IgracKojiSadaZove = Spremanje.IgracKojiSadaZove + 1;
            }
            else if (Spremanje.IgracKojiSadaZove == 4)
            {
                Spremanje.IgracKojiSadaZove = 1;
            }
            Spremanje.PosloziKarte = false;
            Spremanje.PoravnajKarte = false;
            
            SceneManager.LoadScene(SljedecaScena);
           
        }
         
       
    }
    /*Vrača ime igrača koji je zvao adut i pokazuje ga na ekranu.*/
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
    /*Adut postavljen na srce.*/
    public void PozvanoSrce(GameObject IgracKojiJeZvaoSrce)
    {
        if (BojaAduta == "Src") {
            PokaziImeIgracaKojiJeZvao(IgracKojiJeZvaoSrce);
            SlikaAduta.sprite = SlikeAduta[0];
            TkoJeZvao.SetActive(true);
            TextDalje.SetText("");
            KojiIgracJeZvao = IgracKojiJeZvaoSrce;
            Spremanje.KojiIgracJeZvao = IgracKojiJeZvaoSrce;
            
        }
    }
    /*Adut postavljen na Tikvu.*/
    public void PozvanoTikva(GameObject IgracKojiJeZvaoTikvu)
    {
        if (BojaAduta == "Tik")
        {
            PokaziImeIgracaKojiJeZvao(IgracKojiJeZvaoTikvu);
            SlikaAduta.sprite = SlikeAduta[1];
            TkoJeZvao.SetActive(true);
            TextDalje.SetText("");
            KojiIgracJeZvao = IgracKojiJeZvaoTikvu;
            Spremanje.KojiIgracJeZvao = IgracKojiJeZvaoTikvu;
        }
    }
    /*Adut postavljen na Zelje.*/
    public void PozvanoZelje(GameObject IgracKojiJeZvaoZelje)
    {
        if (BojaAduta == "Zel")
        {
            PokaziImeIgracaKojiJeZvao(IgracKojiJeZvaoZelje);
            SlikaAduta.sprite = SlikeAduta[2];
            TkoJeZvao.SetActive(true);
            TextDalje.SetText("");
            KojiIgracJeZvao = IgracKojiJeZvaoZelje;
            Spremanje.KojiIgracJeZvao = IgracKojiJeZvaoZelje;
        }
    }
    /*Adut postavljen na Zir.*/
    public void PozvanoZir(GameObject IgracKojiJeZvaoZir)
    {
        if (BojaAduta == "Zir")
        {
            PokaziImeIgracaKojiJeZvao(IgracKojiJeZvaoZir);
            SlikaAduta.sprite = SlikeAduta[3];
            TkoJeZvao.SetActive(true);
            TextDalje.SetText("");
            KojiIgracJeZvao = IgracKojiJeZvaoZir;
            Spremanje.KojiIgracJeZvao = IgracKojiJeZvaoZir;
        }
    }
    /*Igrac zove dalje.*/
    public void PozvanoDalje(GameObject IgracKojiJeZvaoDalje)
    {
        if (BojaAduta == null)
        {
            PokaziImeIgracaKojiJeZvao(IgracKojiJeZvaoDalje);
            TkoJeZvao.SetActive(true);
            TextDalje.SetText("DALJE");
        }
    }
    /*Pocisti ploču nakon što su odigrane 4 karte.*/
    public IEnumerator PocistiPlocuNakonIgreDelay()
    {       
            GameObject Igrac = PocistiPlocuIDajKartePobjedniku();
            OdbaciKartuAudio.Play();
            IgracKojiPrviBacaKartu = Igrac;
            Spremanje.IgracKojiJeSadNaRedu = Igrac;                        
           
        if (Igrac1.transform.childCount == 0 && Igrac2.transform.childCount == 0 && Igrac3.transform.childCount == 0 && Igrac4.transform.childCount == 0)
        {
            if (OdigranoPozicija1.transform.childCount == 0 && OdigranoPozicija2.transform.childCount == 0 && OdigranoPozicija3.transform.childCount == 0 && OdigranoPozicija4.transform.childCount == 0)
            {
                if (Igrac == Igrac1 || Igrac == Igrac3)
                {
                    ZvanjaMi = ZvanjaMi + 10;

                }
                else if (Igrac == Igrac2 || Igrac == Igrac4)
                {
                    ZvanjaVi = ZvanjaVi + 10;
                }
                StartCoroutine(RestartajIgruZasljedeciKrugPartije());

            }
        }
        else
        {
            yield return new WaitForSecondsRealtime(1);
            OdluciKojiIgracJeNaReduITajIgracIgra(Igrac);
            OdbaciKartuAudio.Play();
        }
    }
    /*Ako igrač zove dalje sljedeci igrač zove adut.*/
    public void IgracZoveDalje(GameObject ImeSljedecegIgraca)
    {
        StartCoroutine(AiZovi(ImeSljedecegIgraca));
    }
    /*Ako igrač jedan na redu za zvanje daje mu iskočni prozor pomoču kojega igrač zove 
     u slučaju da su Ai igrači onu zovu i taj prozor se zatvara.*/
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
    /*Pomočna funkcija pomoču koje se prati koji igrač je sljedeči na redu za zvanje.*/
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
    /*Provjeri sve karte kod igrača i po tim kartama odluči što zvati.*/
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

        if (VrijednostSrc > 34 || BrojKartaSrc > 4)
        {
            PostaviVrijednostiAduta("Src");
            PozvanoSrce(IgracKojiTrenutnoZove);
            
        }
        else if (VrijednostTik > 34 || BrojKartaTik > 4)
        {
            PostaviVrijednostiAduta("Tik");
            PozvanoTikva(IgracKojiTrenutnoZove);
           
        }
        else if (VrijednostZel > 34 || BrojKartaZel > 4)
        {
            PostaviVrijednostiAduta("Zel");
            PozvanoZelje(IgracKojiTrenutnoZove);
           
        }
        else if (VrijednostZir > 34 || BrojKartaZir >4)
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
    /*Ako je Ai igrač prvi na redu igra sam prvu kartu partije.*/
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
    /*Odlučuje koji igrač treba igrati sljedeči i taj igrač igra.*/
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
    /*Omogučuje igraču da počisti karte sa stola koje su odigrane na klik.*/
    private void OnMouseDown() 
    {
        if (OdigranoPozicija1.transform.childCount == 1 && OdigranoPozicija2.transform.childCount == 1 && OdigranoPozicija3.transform.childCount == 1 && OdigranoPozicija4.transform.childCount == 1)
        {
            Spremanje.PozicijaKojaMoraBitiPopunjenaPrvo = null;
            StartCoroutine(PocistiPlocuNakonIgreDelay());
        }
    }
    /*Premiješta karte sa stola na jednu lokaciju, to su karte koje su osvojene u tom krugu partije.*/
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
  /*Pomočnu prijašne funkcije premijesti sve karte na stolu na lokaciju igrača koji su ih osvojili.*/
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
                    GameObject TrenutnaPozicija = SveOdigraneKarte[i];
                    Ai TrenutnaPozicijaAi = TrenutnaPozicija.GetComponent<Ai>();
                    if (PrvaOdigrana == null)
                    {
                        PrvaOdigrana = SveOdigraneKarte[i];
                        NajacaPozicija = SveOdigraneKarte[i];
                            print("Izvrseno");
                    }
                    else if (PrvaOdigrana != null)
                    {
                            print("Izvrseno1");
                        Ai PrvaOdigranaAi = PrvaOdigrana.GetComponent<Ai>();
                        Ai NajacaPozicijaAi = NajacaPozicija.GetComponent<Ai>();
                        if (NajacaPozicijaAi.Adut == false && TrenutnaPozicijaAi.Adut == false && NajacaPozicijaAi.RangKarte < TrenutnaPozicijaAi.RangKarte && PrvaOdigranaAi.BojaKarte == TrenutnaPozicijaAi.BojaKarte)
                        {
                            print("Izvrseno2");
                            NajacaPozicija = TrenutnaPozicija;
                        }
                        else if (NajacaPozicijaAi.Adut == false && TrenutnaPozicijaAi.Adut == true)
                        {
                            print("Izvrseno3");
                            NajacaPozicija = TrenutnaPozicija;
                        }
                        else if (NajacaPozicijaAi.Adut == true && TrenutnaPozicijaAi.Adut == true && NajacaPozicijaAi.RangKarte < TrenutnaPozicijaAi.RangKarte)
                        {
                            print("Izvrseno4");
                            NajacaPozicija = TrenutnaPozicija;
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
    /*Vrača redosljed kojim igrači igraju taj krug partije.*/
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
    /*Uspoređuje sve karte na stolu i po pravilima bele Ai igrači odbacuju karte.*/
    public void OdluciKojaKartaJeDoSadNajvecaIStoTrebaOdbaciti() {

        GameObject[] SveOdigranePozicije = PozicijePoreduOdPRvogaIgraca();
        GameObject PrvaOdigrana= null;
        GameObject NajcaKarta=null;
        GameObject SljedeciIgrac=null;
        if (SveOdigranePozicije != null)
        {
            for (int i = 0; i < SveOdigranePozicije.Length - 1; i++)
            {
                GameObject Pozicija = SveOdigranePozicije[i];
                GameObject SljedecaPozicija = SveOdigranePozicije[i + 1];


                if (Pozicija.transform.GetChildCount() == 1)
                {
                    
                    if (PrvaOdigrana == null)
                    {
                        PrvaOdigrana = Pozicija.transform.GetChild(0).gameObject;
                        NajcaKarta = PrvaOdigrana;
                        PrvaOdigranaKartaURundi = PrvaOdigrana;                      
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
                                   
                                    AiOdluciKojuKartuBacaIzRuke(SljedeciIgrac, SljedecaPozicija, NajcaKarta, PrvaOdigrana);
                                }
                                else if (PrvaOdigranaAi.RangKarte > TrenutnaKartaAi.RangKarte && PrvaOdigranaAi.BojaKarte == TrenutnaKartaAi.BojaKarte)
                                {
                                    AiOdluciKojuKartuBacaIzRuke(SljedeciIgrac, SljedecaPozicija, PrvaOdigrana, PrvaOdigrana);
                                }
                                else if (PrvaOdigranaAi.BojaKarte != TrenutnaKartaAi.BojaKarte && TrenutnaKartaAi.Adut == true)
                                {
                                    NajcaKarta = TrenutnaKarta;
                                   
                                    AiOdluciKojuKartuBacaIzRuke(SljedeciIgrac, SljedecaPozicija, NajcaKarta, PrvaOdigrana);
                                }
                                else if (NajacaKartaAi.RangKarte < TrenutnaKartaAi.RangKarte && PrvaOdigranaAi.BojaKarte == TrenutnaKartaAi.BojaKarte)
                                {
                                    NajcaKarta = TrenutnaKarta;
                                  
                                    AiOdluciKojuKartuBacaIzRuke(SljedeciIgrac, SljedecaPozicija, NajcaKarta, PrvaOdigrana);
                                }
                                else if (NajacaKartaAi.RangKarte > TrenutnaKartaAi.RangKarte && PrvaOdigranaAi.BojaKarte == TrenutnaKartaAi.BojaKarte)
                                {
                                    AiOdluciKojuKartuBacaIzRuke(SljedeciIgrac, SljedecaPozicija, NajcaKarta, PrvaOdigrana);
                                }
                                else if (PrvaOdigranaAi.Adut == false && NajacaKartaAi.Adut == false && TrenutnaKartaAi.Adut == true)
                                {
                                    NajcaKarta = TrenutnaKarta;
                                   
                                    AiOdluciKojuKartuBacaIzRuke(SljedeciIgrac, SljedecaPozicija, NajcaKarta, PrvaOdigrana);
                                }
                                else if (NajacaKartaAi.Adut == true && NajacaKartaAi.RangKarte < TrenutnaKartaAi.RangKarte && TrenutnaKartaAi.Adut == true)
                                {
                                    NajcaKarta = TrenutnaKarta;
                                    
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
                        else
                        {
                            PomocnaFunkcijaKojaOdluciKojaJeKartaZaSadNajvecaISpremiJu();
                            
                        }
                    }
                   
                }
            }
        }       
    }
    /*Mijenja vrijednosti karata koje su u boji aduta.*/
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
    /*Postavlja vrijednosti karta kao što su boja rang karte rang zvanja itd.*/
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
    /*Odlučuje koju kartu prema kartima koje ima u ruci može smije ili mora odbaciti prema pravilima bele.*/
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



    



    

