using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 using UnityEngine.UI;

public class AnimacijeGlavnaKameraMeni : MonoBehaviour
{
    public Animator GlavnaKamera;
    public Button Postavke;
    public Button Natrag;
    public Button IgrajIgru;
    void Start()
    {
        GlavnaKamera = GetComponent<Animator>();
        
    }
    /*-----------------*/
    /*Metode koje kad se pozovu pokrenu animacije na glavnoj kameri u glavnom izborniku. */
    public void PomakniKameruPremaPostvkama()
    {
        GlavnaKamera.Play("RotirajNaPostavke");       
    }
    public void PomakniKameruPremaNatrag()
    {
        GlavnaKamera.Play("VratiNaGlavniMeni");
    }
    public void PomakniKameruPremaLogu()
    {
        GlavnaKamera.Play("PribliziLogo");
    }
    /*-----------------*/
}
