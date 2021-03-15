using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Postavke : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject PanelPostavke;
    public string Igrac1;
    public string Igrac2;
    public string Igrac3;
    public string Igrac4;
    public Sprite OdabranaPloca;
    public Sprite Ploca1 ;
    public Sprite Ploca2 ;
    public Sprite Ploca3 ;
    public Sprite Ploca4 ;
    public TMP_InputField Igracc1;
    public TMP_InputField Igracc2;
    public TMP_InputField Igracc3;
    public TMP_InputField Igracc4;
    public TextMeshProUGUI OptionsText;


    private static readonly string MelodijaVolumen = "VolumenMelodije";
    private static readonly string ZvukVolumen = "VolumenZvuka";
    public Slider VolumenMelodije, VolumenZvuka;
    public AudioSource Melodija;
    public AudioSource[] Zvuk;

    /*Posprema postavke zvuka preko Prefaba*/
  public void SpremiPostavkeZvuka()
    {
        PlayerPrefs.SetFloat(MelodijaVolumen, VolumenMelodije.value);
        PlayerPrefs.SetFloat(ZvukVolumen, VolumenZvuka.value);
                    
    }
    /*Ako se na slajderu melodija u postavcima promjeni vrijednost jačina zvuka melodije se automatski podesi*/
    public void PromjeniJacinuZvukaSliderMelodija()
    {
        Melodija.volume = VolumenMelodije.value;    
    }
    /*Ako se na slajderu jacina zvuka u postavcima promjeni vrijednost jačina zvuka se automatski podesi*/
    public void PromjeniJacinuZvukaSliderZvuk()
    {       
        for (int i = 0; i < Zvuk.Length; i++)
        {
            Zvuk[i].volume = VolumenZvuka.value;
        }
    }

    void Start()
    {
        float MelodijaVolumenFloat = PlayerPrefs.GetFloat(MelodijaVolumen);
        VolumenMelodije.value = MelodijaVolumenFloat;
        float ZvukVolumenFloat = PlayerPrefs.GetFloat(ZvukVolumen);
        VolumenZvuka.value = ZvukVolumenFloat;


        LoadPostavke();
 
        Igracc1.text=Igrac1;
        Igracc2.text=Igrac2;
        Igracc3.text=Igrac3;
        Igracc4.text=Igrac4;

        Spremanje.Igrac1=Igrac1;
        Spremanje.Igrac2=Igrac2;
        Spremanje.Igrac3=Igrac3;
        Spremanje.Igrac4=Igrac4;

        Spremanje.IgracaPloca = OdabranaPloca;
        
        
        
       
        

    }
    /*Vrača prije zadane postavke koje je igrač postavio*/
    public void LoadPostavke()
    {
        string igrac1 =  PlayerPrefs.GetString("Igrac1 ime");
        Igrac1 = igrac1;
        string igrac2 =PlayerPrefs.GetString("Igrac2 ime");
        Igrac2 = igrac2;
        string igrac3 = PlayerPrefs.GetString("Igrac3 ime");
        Igrac3 = igrac3;
        string igrac4 = PlayerPrefs.GetString("Igrac4 ime");
        Igrac4 = igrac4;
        string ImePloce = PlayerPrefs.GetString("Odabrana ploca ime", null);
        if(ImePloce == null)
        {
            OdabranaPloca = Ploca1;
        }
        else if( ImePloce == Ploca1.name)
        {
            OdabranaPloca = Ploca1;          
        }
        else if (ImePloce == Ploca2.name)
        {
            OdabranaPloca = Ploca2;
        }
        else if (ImePloce == Ploca3.name)
        {
            OdabranaPloca = Ploca3;
        }
        else if (ImePloce == Ploca4.name)
        {
            OdabranaPloca = Ploca4;
        }

    }
   /*Spremanje postavka*/
    public void SpremiVrijednostiStalno()
    {
        PlayerPrefs.SetString("Igrac1 ime", Igracc1.text);
        PlayerPrefs.SetString("Igrac2 ime", Igracc2.text);
        PlayerPrefs.SetString("Igrac3 ime", Igracc3.text);
        PlayerPrefs.SetString("Igrac4 ime", Igracc4.text);
        PlayerPrefs.SetString("Odabrana ploca ime", OdabranaPloca.name);
        PlayerPrefs.Save();
    }
    /*Korištenje skripte spremanje radi da spremi imena igrača 
     radi lakšeg pristupa varijablima.*/
    public void SpremiPostavkeIgre()
    {
        if(Igrac1 != Igracc1.text)
        {
            Igrac1 = Igracc1.text;
            Spremanje.Igrac1 = Igrac1;
        }
        if (Igrac2 != Igracc2.text)
        {
            Igrac2 = Igracc2.text;
            Spremanje.Igrac2 = Igrac2;
        }
        if (Igrac3 != Igracc3.text)
        {
            Igrac3 = Igracc3.text;
            Spremanje.Igrac3 = Igrac3;
        }
        if (Igrac4 != Igracc4.text)
        {
            Igrac4 = Igracc4.text;
            Spremanje.Igrac4 = Igrac4;
        }
        if( Spremanje.IgracaPloca != OdabranaPloca)
        {
            Spremanje.IgracaPloca = OdabranaPloca;
        }
        SpremiPostavkeZvuka();
    }
    /*U postavcima igrač odabite pozadinu ili "igraču ploču" te se spremi.*/
    public void OdaberiPlocu(Sprite SpriteOdabranePloce)
    {
        OdabranaPloca = SpriteOdabranePloce;
    }
    /*Metoda koja pokazuje igraču da su izmjenjene postavke spremljene*/
    public void PrikaziSpremljeno()
    {
        StartCoroutine(PrikaziDaSuPromjeneSpremljene());
    }
    /*Nakratko prikazuje tekst "Postavke su spremljene".*/
    public IEnumerator PrikaziDaSuPromjeneSpremljene()
    {
        yield return new WaitForSecondsRealtime(1);
        OptionsText.text = "Promjene su spremljene!";
        yield return new WaitForSecondsRealtime(2);
        OptionsText.text = "POSTAVKE";
    }
    /*Ploča u postavcima koja je odabrana mijenja boju.*/
    public void PlocaMjenjaBoju(Button GumbUKojemJeTrenutnoPostavljenaPloca)
    {
        OdabranaPloca = GumbUKojemJeTrenutnoPostavljenaPloca.GetComponent<Image>().sprite;
       
    }

   
  

}
