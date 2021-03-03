using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PovecajPozadinuPoEkranu : MonoBehaviour
{
    public GameObject OdabranaPloca;
    public Sprite Ploca1;
    public Sprite Ploca2;
    public Sprite Ploca3;
    public Sprite Ploca4;
    
    /*Ova cijela skripta se koristi da se pozadina prilagodi igračoj ploči pokrene se kod ulaska u igru.
     sav kod nalazi se u metodi start.*/
    void Start()
    {
        if (Spremanje.IgracaPloca != null)
        {
            OdabranaPloca.GetComponent<SpriteRenderer>().sprite = Spremanje.IgracaPloca;
        }
        else
        {
            OdabranaPloca.GetComponent<SpriteRenderer>().sprite = Ploca4;
        }

        if (Spremanje.IgracaPloca == Ploca1)
        {
            OdabranaPloca.transform.position = new Vector3(-0.18f, 0.64f, 0.71f);
            OdabranaPloca.transform.localRotation = Quaternion.Euler(0f, 0f, 90f);
            OdabranaPloca.transform.localScale =new Vector3(0.7536899f, 0.7580379f, 1.615f);
        }
        else if (Spremanje.IgracaPloca == Ploca2)
        {
            OdabranaPloca.transform.position = new Vector3(-0.9f, 0.1f, 6.7f);
            OdabranaPloca.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
            OdabranaPloca.transform.localScale = new Vector3(6.39347f, 5.6277f, 1f);
        }
        else if (Spremanje.IgracaPloca == Ploca3)
        {
            OdabranaPloca.transform.position = new Vector3(0.17f, 0.1f, 6.7f);
            OdabranaPloca.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
            OdabranaPloca.transform.localScale = new Vector3(7.771509f, 7.632147f, 1f);
        }
        else if (Spremanje.IgracaPloca == Ploca4)
        {
            OdabranaPloca.transform.position = new Vector3(-0.1f, 0.1f, 6.7f);
            OdabranaPloca.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
            OdabranaPloca.transform.localScale = new Vector3(4.020016f, 4.157918f, 1f);
        }

    }

    
}
