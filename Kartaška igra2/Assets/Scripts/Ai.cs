using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
    
    public string BojaKarte;
    public string NazivKarte;
    public int RangKarte;
    public int VrijednostKarte;
    public string BojaAduta;
    public bool Adut = false;
    public bool NaRedu = true;
    public int BrojacRunda;
    public GameObject IgracKojiJeZvaoURundiPrije = null;
    public GameObject IgracKojiPrviZove;
    public GameObject IgracKojiTrenutnoZove = null;
    public GameObject IgracKojiZoveSljedeci = null;

    
    // Start is called before the first frame update
    void Start()
    {
        
        PostaviVrijednostiKarataPriPokretanju();
        OnMouseDown();
        
        
    }

    // Update is called once per frame
    void Update()
    {

        OdluciKojaKartaJeDoSadNajvecaIStoTrebaOdbaciti();
        if ( OdigranoPozicija1.transform.childCount == 1 && OdigranoPozicija2.transform.childCount == 1 && OdigranoPozicija3.transform.childCount == 1 && OdigranoPozicija4.transform.childCount == 1)
        {
          StartCoroutine(PocistiPlocuNakonIgreDelay());
        }
        
        

        
    }
   
    public IEnumerator PocistiPlocuNakonIgreDelay()
    {
        yield return new WaitForSecondsRealtime(3);
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
            if (VrijednostSrc > 40 || BrojKartaSrc > 5)
            {
                PostaviVrijednostiAduta("Src");

            }
            else if (VrijednostTik > 40 || BrojKartaTik > 5)
            {
                PostaviVrijednostiAduta("Tik");

            }
            else if (VrijednostZel > 40 || BrojKartaZel > 5)
            {
                PostaviVrijednostiAduta("Zel");

            }
            else if (VrijednostZir > 40 || BrojKartaZir > 5)
            {
                PostaviVrijednostiAduta("Zir");
                
            }

            else
            {
                ZoviDalje(IgracNaReduZaZvanje);
                if (IgracKojiZoveSljedeci == IgracKojiPrviZove)
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
                    }
                    else if (IndexNajveceVrijednosti == 2)
                    {
                        PostaviVrijednostiAduta("Tik");
                    }
                    else if (IndexNajveceVrijednosti == 3)
                    {
                        PostaviVrijednostiAduta("Zel");
                    }
                    else if (IndexNajveceVrijednosti == 4)
                    {
                        PostaviVrijednostiAduta("Zir");
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
                RangKarte = 1;
                VrijednostKarte = 0;
            }
            if (NazivKarte == "8")
            {
                RangKarte = 2;
                VrijednostKarte = 0;
            }
            if (NazivKarte == "9")
            {
                RangKarte = 3;
                VrijednostKarte = 0;
            }
            if (NazivKarte == "10")
            {
                RangKarte = 7;
                VrijednostKarte = 10;
            }
            if (NazivKarte == "Decko")
            {
                RangKarte = 4;
                VrijednostKarte = 2;
            }
            if (NazivKarte == "Baba")
            {
                RangKarte = 5;
                VrijednostKarte = 3;
            }
            if (NazivKarte == "Kralj")
            {
                RangKarte = 6;
                VrijednostKarte = 4;
            }
            if (NazivKarte == "As")
            {
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



    



    

