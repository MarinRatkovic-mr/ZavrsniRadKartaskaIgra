using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Rezultati : MonoBehaviour
{
    public GameObject IgraciDvaiCetri;
    public TextMeshProUGUI Rezultat2i4;
    public GameObject IgraciJedaniTri;
    public TextMeshProUGUI Rezultat1i3;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        BrojiRezultatStalno(IgraciDvaiCetri, Rezultat2i4);
        BrojiRezultatStalno(IgraciJedaniTri, Rezultat1i3);
    }

    public void BrojiRezultatStalno(GameObject Igraci, TextMeshProUGUI Rezultat)
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
            Rezultat.SetText(rezultat.ToString());
        }
    }
}
