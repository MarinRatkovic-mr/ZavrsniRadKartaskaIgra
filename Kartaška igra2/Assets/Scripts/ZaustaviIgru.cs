using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZaustaviIgru : MonoBehaviour
{
    public static bool IgraJePauzirana = false;
    public GameObject IskocniProzorIzadi;
    public GameObject[] PanelCrni;
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (IgraJePauzirana)
            {
                NastaviIgru();
            }
            else
            {
                ZaustaviiIgru();
            }
        }

        if (IgraJePauzirana == true)
        {
            for(int i = 0; i < PanelCrni.Length; i++)
            {
               if(PanelCrni[i].active == true)
                {
                    IskocniProzorIzadi.SetActive(false);
                    IskocniProzorIzadi.SetActive(true);
                }            
            }
        }

    }

    public void NastaviIgru()
    {
        IskocniProzorIzadi.SetActive(false);
        Time.timeScale = 1f;
        IgraJePauzirana = false;  
        
    
        
    }

    public void ZaustaviiIgru()
    {
        IskocniProzorIzadi.SetActive(true);
        Time.timeScale = 0f;
        IgraJePauzirana = true;
    }
}
