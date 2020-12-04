using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ai : MonoBehaviour
{
    public GameObject OdigranoPozicija1;
    private GameObject OdigranaKartaIgrac1;
    public string BojaKarte;
    public string NazivKarte;
    public int RangKarte;
    public int VrijednostKarte;
    public bool Adut;
    public bool PrviNaRedu = true;
    

    // Start is called before the first frame update
    void Start()
    {
        PostaviVrijednostiKarataPriPokretanju();
    }

    // Update is called once per frame
    void Update()
    {
        PostaviVrijednostiAdut();
        AiIgraci();
    }

    public void PostaviVrijednostiAdut()
    {
        if (Adut == true)
        {         
                if (NazivKarte == "9")
                {
                    RangKarte = 9;
                    VrijednostKarte = 14;
                }
                if (NazivKarte == "Decko")
                {
                    RangKarte = 10;
                    VrijednostKarte = 20;
                }
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

    public void AiIgraci()
    {
        for(int i = 0; i < 3; i++)
        {

            if (OdigranoPozicija1.transform.childCount != 0)
            {
                OdigranaKartaIgrac1 = OdigranoPozicija1.transform.GetChild(0).gameObject;


            }
            else
            {
                print("Niste Odigrali Kartu");
            }
        }



    }

    public void IzracunajVrijednostiKarteOdbaceneOdStraneIgraca()
    {
        /*
        string NazivKarte;
        OdigranaKartaIgrac1 = OdigranoPozicija1.transform.GetChild(0).gameObject;
        NazivKarte = OdigranaKartaIgrac1.name;*/

    }
}
