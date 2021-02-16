using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class PodesiZvukoveIMelodijePremaPostavkima : MonoBehaviour
{
    private static readonly string MelodijaVolumen = "VolumenMelodije";
    private static readonly string ZvukVolumen = "VolumenZvuka";
    public AudioSource Melodija;
    public AudioSource[] Zvuk;
    public Button ButtonMelodijeMute,ButtonZvukMute;
    public TextMeshProUGUI x7Melodija,x7Zvuk;

    public GameObject[] Kartae;
    public Vector3[] PozicijaKarte;
    private bool PokreniJednom;
    private void Awake()
    {
        PodesiZvukIMelodijeVolumen();     
        
    }
    private void Start()
    {       
       
    }
  
    
    private void PodesiZvukIMelodijeVolumen()
    {
        float MelodijaVolumenFloat = PlayerPrefs.GetFloat(MelodijaVolumen);       
        float ZvukVolumenFloat = PlayerPrefs.GetFloat(ZvukVolumen);
        if(MelodijaVolumenFloat != 0f)
        {
            x7Melodija.SetText("7");
            PostaviText(x7Melodija);
            ButtonMelodijeMute.GetComponent<Image>().color = Color.green;
        }
        else
        {
            x7Melodija.SetText("x");
            PostaviText(x7Melodija);
            ButtonMelodijeMute.GetComponent<Image>().color = Color.red;
        }
        if (ZvukVolumenFloat != 0f)
        {
            x7Zvuk.SetText("7");
            PostaviText(x7Zvuk);
        }
        else
        {
            x7Zvuk.SetText("x");
            PostaviText(x7Zvuk);          
        }

        Melodija.volume = MelodijaVolumenFloat;

        for(int i = 0; i < Zvuk.Length; i++)
        {
            Zvuk[i].volume = ZvukVolumenFloat;
        }


        
    }
    public void PostaviText(TextMeshProUGUI x7Text)
    {
        if (x7Text.text == "7" )
        {          
            x7Text.transform.localRotation = Quaternion.Euler(0, -171f, 237.38f);
        }
        else if( x7Text.text == "x")
        {      
            x7Text.transform.localRotation = Quaternion.Euler(0, 0, 0);
        }


    }
    

    
    public void AkoGumbPritisnutMelodija()
    {
        if(x7Melodija.text == "7")
        {
            MelodijaSmanjiDoKraja();
           
            ButtonMelodijeMute.GetComponent<Image>().color = Color.red;
            x7Melodija.SetText("x");
            PostaviText(x7Melodija);
            return;
        }
        else if( x7Melodija.text == "x")
        {
            MelodijaPojacaj();
            
            ButtonMelodijeMute.GetComponent<Image>().color = Color.green;
            x7Melodija.SetText("7");
            PostaviText(x7Melodija);
            return;
        }
    }

    public void AkoGumbPritisnutZvuk()
    {
        if (x7Zvuk.text == "7")
        {
            ZvukSmanjiDoKraja();

            ButtonZvukMute.GetComponent<Image>().color = Color.red;
            x7Zvuk.SetText("x");
            PostaviText(x7Zvuk);
            return;
        }
        else if (x7Zvuk.text == "x")
        {
            ZvukPojacaj();

            ButtonZvukMute.GetComponent<Image>().color = Color.green;
            x7Zvuk.SetText("7");
            PostaviText(x7Zvuk);
            return;
        }
    }


    public void MelodijaSmanjiDoKraja()
    {
        Melodija.volume = 0f;
        PlayerPrefs.SetFloat(MelodijaVolumen, Melodija.volume);
        
    }

    public void MelodijaPojacaj()
    {
        Melodija.volume = 0.5f;
        PlayerPrefs.SetFloat(MelodijaVolumen, Melodija.volume);
    }

    public void ZvukSmanjiDoKraja()
    {
        for (int i = 0; i < Zvuk.Length; i++)
        {
            Zvuk[i].volume = 0f;
            PlayerPrefs.SetFloat(ZvukVolumen, Zvuk[i].volume);
        }
    }

    public void ZvukPojacaj()
    {
        for (int i = 0; i < Zvuk.Length; i++)
        {
            Zvuk[i].volume = 0.5f;
            PlayerPrefs.SetFloat(ZvukVolumen, Zvuk[i].volume);
        }
    }
}
