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
        OdluciKojiIgracJeNaReduITajIgracIgra(PocistiPlocuIDajKartePobjedniku());

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
            if (IgracKojiIgra != null)
            {
                PozicijaNaKojuIgracOdigra = GameObject.Find(ImePozicije);
                print("Ime pozicije koja se trenutno igra:" + ImePozicije);
                
            }
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
            else if (NajvecaKarta != null)
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
            else
            {
                Selectable PokaziKartu = NajmanjaKarta.GetComponent<Selectable>();
                PokaziKartu.KartaOkrenutaPremaGore = true;
                NajmanjaKarta.transform.SetParent(PozicijaNaKojuIgracOdigra.transform);
                NajmanjaKarta.transform.position = PozicijaNaKojuIgracOdigra.transform.position;
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
        GameObject OdigranaKartaPozicija2;
        GameObject OdigranaKartaPozicija3;
        GameObject OdigranaKartaPozicija4;
        GameObject NajaciIgrac;
        if (OdigranoPozicija1.transform.childCount == 1 && OdigranoPozicija2.transform.childCount == 1 && OdigranoPozicija3.transform.childCount == 1 && OdigranoPozicija4.transform.childCount == 1)
        {       
            
           OdigranaKartaPozicija2 = OdigranoPozicija2.transform.GetChild(0).gameObject;                      
           OdigranaKartaPozicija3 = OdigranoPozicija3.transform.GetChild(0).gameObject;                       
           OdigranaKartaPozicija4 = OdigranoPozicija4.transform.GetChild(0).gameObject;
           Ai PrvaOdigranaKartaAi = PrvaOdigranaKarta.transform.GetComponent<Ai>();
           Ai OdigranaKartaPozicija2Ai = OdigranaKartaPozicija2.transform.GetComponent<Ai>();
           Ai OdigranaKartaPozicija3Ai = OdigranaKartaPozicija3.transform.GetComponent<Ai>();
           Ai OdigranaKartaPozicija4Ai = OdigranaKartaPozicija4.transform.GetComponent<Ai>();
            if(PrvaOdigranaKartaAi.RangKarte > OdigranaKartaPozicija2Ai.RangKarte && OdigranaKartaPozicija2Ai.Adut ==false && PrvaOdigranaKartaAi.RangKarte > OdigranaKartaPozicija3Ai.RangKarte && OdigranaKartaPozicija3Ai.Adut == false && PrvaOdigranaKartaAi.RangKarte > OdigranaKartaPozicija4Ai.RangKarte && OdigranaKartaPozicija4Ai.Adut == false)
            {
                
                PremjestiViseKarataNaJeduLokaciju(PrvaOdigranaKarta, OdigranaKartaPozicija2, OdigranaKartaPozicija3, OdigranaKartaPozicija4, IgraciJedanITri);
                return NajaciIgrac = Igrac1;
            }
            else if (PrvaOdigranaKartaAi.RangKarte < OdigranaKartaPozicija2Ai.RangKarte && PrvaOdigranaKartaAi.BojaKarte == OdigranaKartaPozicija2Ai.BojaKarte && OdigranaKartaPozicija2Ai.RangKarte > OdigranaKartaPozicija3Ai.RangKarte && OdigranaKartaPozicija3Ai.Adut == false && OdigranaKartaPozicija2Ai.RangKarte > OdigranaKartaPozicija4Ai.RangKarte && OdigranaKartaPozicija4Ai.Adut == false)
            {              
                PremjestiViseKarataNaJeduLokaciju(PrvaOdigranaKarta, OdigranaKartaPozicija2, OdigranaKartaPozicija3, OdigranaKartaPozicija4, IgraciDvaiCetri);
                return NajaciIgrac = Igrac2;
            }
            else if (PrvaOdigranaKartaAi.RangKarte < OdigranaKartaPozicija3Ai.RangKarte && PrvaOdigranaKartaAi.BojaKarte == OdigranaKartaPozicija3Ai.BojaKarte && OdigranaKartaPozicija2Ai.RangKarte < OdigranaKartaPozicija3Ai.RangKarte && OdigranaKartaPozicija2Ai.Adut == false && OdigranaKartaPozicija3Ai.RangKarte > OdigranaKartaPozicija4Ai.RangKarte && OdigranaKartaPozicija4Ai.Adut == false)
            {
                
                PremjestiViseKarataNaJeduLokaciju(PrvaOdigranaKarta, OdigranaKartaPozicija2, OdigranaKartaPozicija3, OdigranaKartaPozicija4, IgraciJedanITri);
                return NajaciIgrac = Igrac3;
            }
            else if (PrvaOdigranaKartaAi.RangKarte < OdigranaKartaPozicija4Ai.RangKarte && PrvaOdigranaKartaAi.BojaKarte == OdigranaKartaPozicija4Ai.BojaKarte && OdigranaKartaPozicija4Ai.RangKarte > OdigranaKartaPozicija3Ai.RangKarte && OdigranaKartaPozicija3Ai.Adut == false && OdigranaKartaPozicija2Ai.RangKarte < OdigranaKartaPozicija4Ai.RangKarte && OdigranaKartaPozicija2Ai.Adut == false)
            {               
                PremjestiViseKarataNaJeduLokaciju(PrvaOdigranaKarta, OdigranaKartaPozicija2, OdigranaKartaPozicija3, OdigranaKartaPozicija4, IgraciDvaiCetri);
                return NajaciIgrac = Igrac4;
            }

            else if (PrvaOdigranaKartaAi.Adut== true && OdigranaKartaPozicija3Ai.Adut==false && OdigranaKartaPozicija2Ai.Adut == false && OdigranaKartaPozicija4Ai.Adut==false)
            {               
                PremjestiViseKarataNaJeduLokaciju(PrvaOdigranaKarta, OdigranaKartaPozicija2, OdigranaKartaPozicija3, OdigranaKartaPozicija4, IgraciJedanITri);
                return NajaciIgrac = Igrac1;
            }
            else if (PrvaOdigranaKartaAi.Adut == false && OdigranaKartaPozicija3Ai.Adut == true && OdigranaKartaPozicija2Ai.Adut == false && OdigranaKartaPozicija4Ai.Adut == false)
            {
             
                PremjestiViseKarataNaJeduLokaciju(PrvaOdigranaKarta, OdigranaKartaPozicija2, OdigranaKartaPozicija3, OdigranaKartaPozicija4, IgraciJedanITri);
                return NajaciIgrac = Igrac3;
            }
            else if (PrvaOdigranaKartaAi.Adut == false && OdigranaKartaPozicija3Ai.Adut==false && OdigranaKartaPozicija2Ai.Adut == true && OdigranaKartaPozicija4Ai.Adut == false)
            {                             
                PremjestiViseKarataNaJeduLokaciju(PrvaOdigranaKarta, OdigranaKartaPozicija2, OdigranaKartaPozicija3, OdigranaKartaPozicija4, IgraciDvaiCetri);
                return NajaciIgrac = Igrac2;
            }
            else if (PrvaOdigranaKartaAi.Adut == false && OdigranaKartaPozicija3Ai.Adut == false && OdigranaKartaPozicija2Ai.Adut == false && OdigranaKartaPozicija4Ai.Adut == true)
            {
                PremjestiViseKarataNaJeduLokaciju(PrvaOdigranaKarta, OdigranaKartaPozicija2, OdigranaKartaPozicija3, OdigranaKartaPozicija4, IgraciDvaiCetri);
                return NajaciIgrac = Igrac4;
            }

            else if (PrvaOdigranaKartaAi.Adut == true  | OdigranaKartaPozicija3Ai.Adut == true && OdigranaKartaPozicija2Ai.Adut == true | OdigranaKartaPozicija4Ai.Adut == true)
            {
                if(PrvaOdigranaKartaAi.Adut==true && OdigranaKartaPozicija2Ai.Adut == true && PrvaOdigranaKartaAi.RangKarte > OdigranaKartaPozicija2Ai.RangKarte && OdigranaKartaPozicija4Ai.Adut == false)
                {
                    PremjestiViseKarataNaJeduLokaciju(PrvaOdigranaKarta, OdigranaKartaPozicija2, OdigranaKartaPozicija3, OdigranaKartaPozicija4, IgraciJedanITri);
                    return NajaciIgrac = Igrac1;
                }
                else if (PrvaOdigranaKartaAi.Adut == true && OdigranaKartaPozicija4Ai.Adut == true && PrvaOdigranaKartaAi.RangKarte > OdigranaKartaPozicija4Ai.RangKarte && OdigranaKartaPozicija2Ai.Adut == false)
                {
                    PremjestiViseKarataNaJeduLokaciju(PrvaOdigranaKarta, OdigranaKartaPozicija2, OdigranaKartaPozicija3, OdigranaKartaPozicija4, IgraciJedanITri);
                    return NajaciIgrac = Igrac1;
                }
                else if (PrvaOdigranaKartaAi.Adut == true && OdigranaKartaPozicija4Ai.Adut == true && PrvaOdigranaKartaAi.RangKarte > OdigranaKartaPozicija4Ai.RangKarte && OdigranaKartaPozicija2Ai.Adut == true && PrvaOdigranaKartaAi.RangKarte> OdigranaKartaPozicija2Ai.RangKarte)
                {
                    PremjestiViseKarataNaJeduLokaciju(PrvaOdigranaKarta, OdigranaKartaPozicija2, OdigranaKartaPozicija3, OdigranaKartaPozicija4, IgraciJedanITri);
                    return NajaciIgrac = Igrac1;
                }
                else if (OdigranaKartaPozicija3Ai.Adut == true && OdigranaKartaPozicija2Ai.Adut == true && OdigranaKartaPozicija3Ai.RangKarte > OdigranaKartaPozicija2Ai.RangKarte && OdigranaKartaPozicija4Ai.Adut == false)
                {
                    PremjestiViseKarataNaJeduLokaciju(PrvaOdigranaKarta, OdigranaKartaPozicija2, OdigranaKartaPozicija3, OdigranaKartaPozicija4, IgraciJedanITri);
                    return NajaciIgrac = Igrac3;
                }
                else if (OdigranaKartaPozicija3Ai.Adut == true && OdigranaKartaPozicija4Ai.Adut == true && OdigranaKartaPozicija3Ai.RangKarte > OdigranaKartaPozicija4Ai.RangKarte && OdigranaKartaPozicija2Ai.Adut == false)
                {
                    PremjestiViseKarataNaJeduLokaciju(PrvaOdigranaKarta, OdigranaKartaPozicija2, OdigranaKartaPozicija3, OdigranaKartaPozicija4, IgraciJedanITri);
                    return NajaciIgrac = Igrac3;
                }
                else if (OdigranaKartaPozicija3Ai.Adut == true && OdigranaKartaPozicija4Ai.Adut == true && OdigranaKartaPozicija3Ai.RangKarte > OdigranaKartaPozicija4Ai.RangKarte && OdigranaKartaPozicija2Ai.Adut == true && OdigranaKartaPozicija3Ai.RangKarte > OdigranaKartaPozicija2Ai.RangKarte)
                {
                    PremjestiViseKarataNaJeduLokaciju(PrvaOdigranaKarta, OdigranaKartaPozicija2, OdigranaKartaPozicija3, OdigranaKartaPozicija4, IgraciJedanITri);
                    return NajaciIgrac = Igrac3;
                }
                else
                {
                    PremjestiViseKarataNaJeduLokaciju(PrvaOdigranaKarta, OdigranaKartaPozicija2, OdigranaKartaPozicija3, OdigranaKartaPozicija4, IgraciDvaiCetri);
                    return NajaciIgrac = Igrac4;
                }               
            }
        }
        return null;
    }
    public void OdluciKojaKartaJeDoSadNajvecaIStoTrebaOdbaciti()
    {        
          GameObject DrugaOdigranaKarta = null;
          GameObject TrecaOdigranaKarta = null;

        if (OdigranoPozicija1.transform.childCount == 1)
        {
            PrvaOdigranaKarta = OdigranoPozicija1.transform.GetChild(0).gameObject;
            if (OdigranoPozicija2.transform.childCount == 0)
            {
                AiOdluciKojuKartuBacaIzRuke(Igrac2, OdigranoPozicija2, PrvaOdigranaKarta, false);
            }
        }
        if (OdigranoPozicija2.transform.childCount == 1)
        {
            if (PrvaOdigranaKarta != null)
            {
                DrugaOdigranaKarta = OdigranoPozicija2.transform.GetChild(0).gameObject;
                Ai PrvaOdigranaKartaAi = PrvaOdigranaKarta.transform.GetComponent<Ai>();
                Ai DrugaOdigranaKartaAi = DrugaOdigranaKarta.transform.GetComponent<Ai>();
                if (DrugaOdigranaKartaAi.RangKarte > PrvaOdigranaKartaAi.RangKarte && DrugaOdigranaKartaAi.BojaKarte == PrvaOdigranaKartaAi.BojaKarte)
                {
                    if (OdigranoPozicija3.transform.childCount == 0)
                    {
                        AiOdluciKojuKartuBacaIzRuke(Igrac3, OdigranoPozicija3, DrugaOdigranaKarta,false);
                    }
                }
                else if (DrugaOdigranaKartaAi.RangKarte < PrvaOdigranaKartaAi.RangKarte && DrugaOdigranaKartaAi.BojaKarte == PrvaOdigranaKartaAi.BojaKarte)
                {
                    if (OdigranoPozicija3.transform.childCount == 0)
                    {
                        AiOdluciKojuKartuBacaIzRuke(Igrac3, OdigranoPozicija3, PrvaOdigranaKarta,false);
                    }
                }
                else if (DrugaOdigranaKartaAi.Adut == true)
                {                   
                    if (OdigranoPozicija3.transform.childCount == 0)
                    {
                        AiOdluciKojuKartuBacaIzRuke(Igrac3, OdigranoPozicija3, PrvaOdigranaKarta,true);
                    }
                }
                else if (DrugaOdigranaKartaAi.Adut == false && DrugaOdigranaKartaAi.BojaKarte != PrvaOdigranaKartaAi.BojaKarte)
                {
                    if (OdigranoPozicija3.transform.childCount == 0)
                    {
                        AiOdluciKojuKartuBacaIzRuke(Igrac3, OdigranoPozicija3, PrvaOdigranaKarta,false);
                    }
                }
            }
        }
        if (OdigranoPozicija3.transform.childCount == 1)
        {
            TrecaOdigranaKarta = OdigranoPozicija3.transform.GetChild(0).gameObject;
            Ai PrvaOdigranaKartaAi = PrvaOdigranaKarta.transform.GetComponent<Ai>();          
            Ai DrugaOdigranaKartaAi = DrugaOdigranaKarta.transform.GetComponent<Ai>();
            Ai TrecaOdigranaKartaAi = TrecaOdigranaKarta.transform.GetComponent<Ai>();
            if (TrecaOdigranaKartaAi.RangKarte > PrvaOdigranaKartaAi.RangKarte && TrecaOdigranaKartaAi.RangKarte > DrugaOdigranaKartaAi.RangKarte && TrecaOdigranaKartaAi.BojaKarte == PrvaOdigranaKartaAi.BojaKarte && DrugaOdigranaKartaAi.Adut == false && TrecaOdigranaKartaAi.Adut == false)
            {
                if (OdigranoPozicija4.transform.childCount == 0)
                {
                    AiOdluciKojuKartuBacaIzRuke(Igrac4, OdigranoPozicija4, TrecaOdigranaKarta,false);
                }
            }
            else if (PrvaOdigranaKartaAi.RangKarte > TrecaOdigranaKartaAi.RangKarte && DrugaOdigranaKartaAi.RangKarte < PrvaOdigranaKartaAi.RangKarte && DrugaOdigranaKartaAi.Adut == false && TrecaOdigranaKartaAi.Adut == false)
            {
                if (OdigranoPozicija4.transform.childCount == 0)
                {
                    AiOdluciKojuKartuBacaIzRuke(Igrac4, OdigranoPozicija4, PrvaOdigranaKarta,false);
                }
            }
            else if (DrugaOdigranaKartaAi.RangKarte > TrecaOdigranaKartaAi.RangKarte && DrugaOdigranaKartaAi.RangKarte > PrvaOdigranaKartaAi.RangKarte && DrugaOdigranaKartaAi.Adut == false && TrecaOdigranaKartaAi.Adut == false && DrugaOdigranaKartaAi.BojaKarte == PrvaOdigranaKartaAi.BojaKarte)
            {
                if (OdigranoPozicija4.transform.childCount == 0)
                {
                    AiOdluciKojuKartuBacaIzRuke(Igrac4, OdigranoPozicija4, DrugaOdigranaKarta,false);
                }
            }
            else if (DrugaOdigranaKartaAi.Adut == true | TrecaOdigranaKartaAi.Adut==true && PrvaOdigranaKartaAi.Adut == false )
            {
                
                if (OdigranoPozicija4.transform.childCount == 0)
                {
                    AiOdluciKojuKartuBacaIzRuke(Igrac4, OdigranoPozicija4, PrvaOdigranaKarta,true);
                }
            }
            else if (TrecaOdigranaKartaAi.Adut == false && TrecaOdigranaKartaAi.BojaKarte != PrvaOdigranaKartaAi.BojaKarte | DrugaOdigranaKartaAi.Adut == false && DrugaOdigranaKartaAi.BojaKarte != PrvaOdigranaKartaAi.BojaKarte)
            {
                if (OdigranoPozicija4.transform.childCount == 0)
                {
                    AiOdluciKojuKartuBacaIzRuke(Igrac4, OdigranoPozicija4, PrvaOdigranaKarta,false);
                }
            }
            else if(PrvaOdigranaKartaAi.Adut == true)
            {
                if (OdigranoPozicija4.transform.childCount == 0)
                {
                    AiOdluciKojuKartuBacaIzRuke(Igrac4, OdigranoPozicija4, PrvaOdigranaKarta, false);
                }
            }

        }
        else if( OdigranoPozicija4.transform.childCount == 1)
        {
            UserInput IgraIgrac = Igrac1.GetComponent<UserInput>();
            IgraIgrac.KartaOdabrana();
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



    



    

