using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 using UnityEngine.SceneManagement;
public class PromjeniScenu : MonoBehaviour
{
   
    /*Mijenja trenutnu scenu na scenu gdje se nalazi igra.*/
    public void PocniScenuBela(bool PocniPrijelaz = false)
    {
        if( PocniPrijelaz == true)
        {
            StartCoroutine( UdiUigru());
           
        }
    }

    /*Mijenja trenutnu scenu na scenu gdje se nalazi glavni meni.*/
    public void PocniScenuMenu(bool PocniPrijelaz = false)
    {
        if (PocniPrijelaz == true)
        {
            StartCoroutine(VratiSeIzIgre());

        }
    }
    /*Usporava ulaz u igru radi animacija prilikom ulaza.*/
    public IEnumerator UdiUigru()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("BelaScen1");
    }
    /*Malo usporava izlaz iz igre.*/
    public IEnumerator VratiSeIzIgre()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("GlavniMeni");
    }
    /*Gasi cijelu aplikaciju*/
    public void IzadiIzCijeleIgre()
    {      
        Application.Quit();
    }
}
