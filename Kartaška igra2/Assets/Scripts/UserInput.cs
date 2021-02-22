using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserInput : MonoBehaviour
{
    public Transform OdigranoPozicija1;
    
    private int brojacKlikova = 1;
    private GameObject Odabrano1;
    private GameObject Odabrano2;
    public GameObject Igrac1;
    public GameObject OdigranoPozicija1GameObject;
    public GameObject OdigranoPozicija1GameObjectIgrac4;

   

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Spremanje.IgracKojiJeSadNaRedu == Igrac1 || OdigranoPozicija1GameObjectIgrac4.transform.childCount == 1 && OdigranoPozicija1GameObject.transform.childCount <2 )
        {
            KartaOdabrana();         
        }
    }
/*Ako karta na koju kliknemo ima naziv "Karta" te se nalazi na poziciji "Igrac1".
  Izvrši se povecanje kliknute karte         
                 */
    public void KartaOdabrana()
    {
        
       // Vector3 pozicijaMisa = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -10));
        RaycastHit2D odabrano = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        List<GameObject> KarteKojeSeSmijukliknuti = ListaKarataKojeIgracSmijeKliknuti();
             
       
        if (Input.GetMouseButtonDown(0))
        {
            if (KarteKojeSeSmijukliknuti != null)
            {
                
                
               
                if (odabrano.collider.CompareTag("Karta") && odabrano.transform.IsChildOf(Igrac1.transform) == true && OdigranoPozicija1GameObject.transform.GetChildCount() < 1 && KarteKojeSeSmijukliknuti.Contains(odabrano.collider.gameObject))
                {
                   

                    if (brojacKlikova == 1)
                    {

                        Odabrano1 = odabrano.collider.gameObject;
                        Odabrano1.transform.position = new Vector3(Odabrano1.transform.position.x, Odabrano1.transform.position.y, Odabrano1.transform.position.z - 1);
                        // print("povecaj kartu");
                        brojacKlikova++;
                        return;
                    }
                    else if (brojacKlikova == 2)
                    {
                        Odabrano2 = odabrano.collider.gameObject;

                        if (Odabrano1 != Odabrano2)
                        {
                            Odabrano1.transform.position = new Vector3(Odabrano1.transform.position.x, Odabrano1.transform.position.y, Odabrano1.transform.position.z + 1);
                            Odabrano2.transform.position = new Vector3(Odabrano2.transform.position.x, Odabrano2.transform.position.y, Odabrano2.transform.position.z - 1);
                            brojacKlikova = 2;
                            // print("Smanji kartu i povečaj drugu");
                            Odabrano1 = Odabrano2;
                            return;
                        }
                        else if ((Odabrano1 == Odabrano2))
                        {
                            // print("Premjesti kartu gore");
                            odabrano.transform.SetParent(OdigranoPozicija1);
                            odabrano.transform.position = OdigranoPozicija1.position;
                            odabrano.transform.localScale = new Vector3(2, 2, 1);
                            brojacKlikova = 1;

                            return;
                        }
                    }
                    return;
                }
            }
            return;
        }
        
    }


    public List<GameObject> ListaKarataKojeIgracSmijeKliknuti()
    {
            List<GameObject> KarteKojeSeMoguIgratiIstaBoja = new List<GameObject>();
            List<GameObject> KarteKojeSeMoguIgratiAdut = new List<GameObject>();
            List<GameObject> KarteKojeSeMoguIgratiSveOstalo = new List<GameObject>();
       


        GameObject PrvaOdigrana = Spremanje.PrvaOdigranaKarta;
        GameObject NajacaOdigrana = Spremanje.NajacaKartaDoSad;
        
         if (PrvaOdigrana != null && NajacaOdigrana != null)
        {
            Ai PrvaOdigranaAi = PrvaOdigrana.GetComponent<Ai>();
            Ai NajacaOdigranaAi = NajacaOdigrana.GetComponent<Ai>();


            for (int i = 0; i < Igrac1.transform.childCount; i++)
            {
                GameObject TrenutnaKarta = Igrac1.transform.GetChild(i).gameObject;
                Ai TrenutnaKartaAi = TrenutnaKarta.GetComponent<Ai>();
                if (TrenutnaKartaAi.BojaKarte == PrvaOdigranaAi.BojaKarte && PrvaOdigranaAi.Adut == false)
                {
                    KarteKojeSeMoguIgratiIstaBoja.Add(TrenutnaKarta);
                }
                else if (TrenutnaKartaAi.Adut == true)
                {
                    KarteKojeSeMoguIgratiAdut.Add(TrenutnaKarta);
                }
                else if (PrvaOdigranaAi.BojaKarte != TrenutnaKartaAi.BojaKarte && TrenutnaKartaAi.Adut == false)
                {
                    KarteKojeSeMoguIgratiSveOstalo.Add(TrenutnaKarta);
                }
            }

            if (PrvaOdigranaAi.BojaKarte == NajacaOdigranaAi.BojaKarte &&PrvaOdigranaAi.Adut == false && KarteKojeSeMoguIgratiIstaBoja.Count > 0)
            {             
                List<GameObject> PrivremenaListaVece = new List<GameObject>();
                List<GameObject> PrivremenaListaManje = new List<GameObject>();               
                for (int a = 0; a < KarteKojeSeMoguIgratiIstaBoja.Count; a++)     
                {
                    GameObject TrenutnaKarta = KarteKojeSeMoguIgratiIstaBoja[a].gameObject;
                    Ai TrenutnaKartaAi = TrenutnaKarta.GetComponent<Ai>();

                    if (TrenutnaKartaAi.RangKarte > NajacaOdigranaAi.RangKarte)
                    {
                        PrivremenaListaVece.Add(TrenutnaKarta);
                    }
                    else if (TrenutnaKartaAi.RangKarte < NajacaOdigranaAi.RangKarte)
                    {
                        PrivremenaListaManje.Add(TrenutnaKarta);
                    }
                }            
                if (PrivremenaListaVece.Count > 0 )
                {
                    return PrivremenaListaVece;
                }
                else if (PrivremenaListaManje.Count > 0)
                {           
                    return PrivremenaListaManje;
                }
            }
            else if (PrvaOdigranaAi.BojaKarte != NajacaOdigranaAi.BojaKarte &&NajacaOdigranaAi.Adut==true && KarteKojeSeMoguIgratiIstaBoja.Count > 0)
            {
                List<GameObject> PrivremenaLista = new List<GameObject>();
                
                for (int a = 0; a < KarteKojeSeMoguIgratiIstaBoja.Count; a++)
                {
                    GameObject TrenutnaKarta = KarteKojeSeMoguIgratiIstaBoja[a].gameObject;
                    PrivremenaLista.Add(TrenutnaKarta);                                 
                }
                return PrivremenaLista;
            }
            else if (NajacaOdigranaAi.Adut == true && PrvaOdigranaAi.Adut==true && KarteKojeSeMoguIgratiAdut.Count > 0)
            {
                List<GameObject> PrivremenaListaVece = new List<GameObject>();
                List<GameObject> PrivremenaListaManje = new List<GameObject>();
                for (int a = 0; a < KarteKojeSeMoguIgratiAdut.Count; a++)
                {
                    GameObject TrenutnaKarta = KarteKojeSeMoguIgratiAdut[a].gameObject;
                    Ai TrenutnaKartaAi = TrenutnaKarta.GetComponent<Ai>();

                    if (TrenutnaKartaAi.RangKarte > NajacaOdigranaAi.RangKarte)
                    {
                        PrivremenaListaVece.Add(TrenutnaKarta);
                    }
                    else if (TrenutnaKartaAi.RangKarte < NajacaOdigranaAi.RangKarte)
                    {
                        PrivremenaListaManje.Add(TrenutnaKarta);
                    }
                }
                if (PrivremenaListaVece.Count > 0)
                {
                    return PrivremenaListaVece;
                }
                else if (PrivremenaListaManje.Count > 0)
                {
                    return PrivremenaListaManje;
                }
            }else if(KarteKojeSeMoguIgratiIstaBoja.Count == 0 && NajacaOdigranaAi.Adut == false && PrvaOdigranaAi.Adut==false && KarteKojeSeMoguIgratiAdut.Count > 0)
            {
                return KarteKojeSeMoguIgratiAdut;
            }
            else if (KarteKojeSeMoguIgratiIstaBoja.Count == 0 && KarteKojeSeMoguIgratiAdut.Count == 0)
            {
                return KarteKojeSeMoguIgratiSveOstalo;
            }
            else
            {
                List<GameObject> PrivremenaLista = new List<GameObject>();
                for (int i = 0; i < Igrac1.transform.childCount; i++)
                {
                    GameObject TrenutnaKarta = Igrac1.transform.GetChild(i).gameObject;
                    PrivremenaLista.Add(TrenutnaKarta);
                }
                return PrivremenaLista;
            }
        }
        else
        {
            List<GameObject> PrivremenaLista = new List<GameObject>();
            for (int i = 0; i < Igrac1.transform.childCount; i++)
            {
                GameObject TrenutnaKarta = Igrac1.transform.GetChild(i).gameObject;
                PrivremenaLista.Add(TrenutnaKarta);
            }
            return PrivremenaLista;
        }
        return null;
    }
}



