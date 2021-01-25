using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 using UnityEngine.SceneManagement;
public class PromjeniScenu : MonoBehaviour
{
   

    public void PocniScenuBela(bool PocniPrijelaz = false)
    {
        if( PocniPrijelaz == true)
        {
            StartCoroutine( UdiUigru());
           
        }
    }

    public void PocniScenuMenu(bool PocniPrijelaz = false)
    {
        if (PocniPrijelaz == true)
        {
            StartCoroutine(VratiSeIzIgre());

        }
    }
    public IEnumerator UdiUigru()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("BelaScen1");
    }
    public IEnumerator VratiSeIzIgre()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("GlavniMeni");
    }
}
