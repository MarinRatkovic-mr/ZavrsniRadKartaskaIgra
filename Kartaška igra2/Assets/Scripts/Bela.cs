using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;
public class Bela : MonoBehaviour
{
    
    public Sprite[] KarteIzgled;
    public GameObject KartePrefab;

    //Poravnanje karata prema pozicijama igrača tjekom djeljenja
    public GameObject[] PozicijaIgraca;
    public GameObject[] PozicijaIgracaOdigrano;

    public static string[] boje = new string[] {"Zel","Src","Tik","Zir" };
    public static string[] vrijednosti = new string[] { "7", "8", "9", "10","Decko","Baba","Kralj","As" };

    public List<string>[] Igraci;
    public List<string>[] IgraciOdigrano;

    private List<string> Igrac1 = new List<string>();
    private List<string> Igrac2 = new List<string>();
    private List<string> Igrac3 = new List<string>();
    private List<string> Igrac4 = new List<string>();

    public  List<string> spil;
    
    void Start()
    {
        
        OdigrajKarte();
    }

    
    void Update()
    {
        
    }

    
    // Metoda za prizvanje promješanog špila karata
    public void OdigrajKarte()
    {
        
        
        Igrac1.Clear();
        Igrac2.Clear();
        Igrac3.Clear();
        Igrac4.Clear();
        spil.Clear();      
        Igraci = new List<string>[] { Igrac1, Igrac2, Igrac3, Igrac4 };

        spil = KreirajSpil();
        PromjesajKarte(spil);        
        BelaSortiraj();       
        StartCoroutine(BelaDjeli());

        
       

    }

    //Kreiranje špila karata tako da uzmemo sve vrijednosti iz vektora "boje" i "vrijednosti"
    //Te ih spojimo zajedno da se stvori sve kombinacije iz ta dva vektora
    public static List<string> KreirajSpil()
    {
        List<string> NoviSpil = new List<string>();
        foreach(string s in boje)
        {
            foreach ( string v in vrijednosti)
            {
                NoviSpil.Add(s + v);
            }
        }

        return NoviSpil;
    }

    //Metoda koja će promiješati sve karte u špilu
    void PromjesajKarte<T>(List<T> listKarata)
    {
        System.Random random = new System.Random();
        int n = listKarata.Count;
        while (n > 1)
        {
            int k = random.Next(n);
            n--;
            T temp = listKarata[k];
            listKarata[k] = listKarata[n];
            listKarata[n] = temp;
        }

    }
    //Metoda koja dodjeljuje izmješane karte igračima gdje je igrač 1 koji je korisnik igre
    //Postavlja karte na stol tako da samo igrač 1 vidi prvih 6 karata a ostale ne
    //Usmjeruje karte pri podjeli u pravim pravcima 
    public IEnumerator BelaDjeli()
    {
        
        for (int i = 0; i < Igraci.Length; i++) {
            
            float zAngle = 0f;

            float yOffset = 0;
            float zOffset = 0;
            float xOffset = 0;
        foreach(string karta in Igraci[i].ToArray())
        {
                
                    yield return new WaitForSeconds(0.06f);
            GameObject novaKarta = Instantiate(KartePrefab,new Vector3(PozicijaIgraca[i].transform.position.x -xOffset, PozicijaIgraca[i].transform.position.y - yOffset,PozicijaIgraca[i].transform.position.z + zOffset), Quaternion.Euler(0f, 0f, PozicijaIgraca[i].transform.localRotation.z +zAngle),PozicijaIgraca[i].transform);
                
                novaKarta.name = karta;
                

                for (int a = 0; a < 6; a++) 
                { 
                  if(karta == Igrac1[a]) 
                  { 
                    
                     novaKarta.GetComponent<Selectable>().KartaOkrenutaPremaGore = true;
                  }
                }

               for(int b = 0; b < 8; b++) 
               {
                    
                    if (karta == Igrac1[b])
                   {
                        
                        zOffset = zOffset + 0.03f;
                        xOffset = xOffset - 2f;
                        zAngle = 0f;

                    }
                     if(karta == Igrac2[b])
                     {
                        
                        zAngle =  90f;
                        zOffset = zOffset + 0.03f;
                        yOffset = yOffset + 1.5f;                                                
                        
                     }
                    else if (karta == Igrac3[b])
                    {
                        zOffset = zOffset + 0.03f;
                        xOffset = xOffset + 2f;
                        zAngle = 0;
                    }
                    else if(karta == Igrac4[b])
                    {
                        zAngle = 90f;
                        zOffset = zOffset + 0.03f;
                        yOffset = yOffset - 1.5f;
                    }
               }
                
               
        }
      }
    }

    void BelaSortiraj()
    {
        int a = 0;

        while (a < 32) { 
            
            for (int i = 0; i < 4; i++)
            {
                a++;
                Igraci[i].Add(spil.Last<string>());
                spil.RemoveAt(spil.Count -1);
                
            }
       }
    }

    
}
