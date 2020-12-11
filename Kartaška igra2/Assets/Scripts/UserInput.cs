using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserInput : MonoBehaviour
{
    public Transform OdigranoPozicija1;
    private int brojacKlikova = 1;
    private GameObject Odabrano1;
    private GameObject Odabrano2;
    

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       KartaOdabrana();
       
    }
/*Ako karta na koju kliknemo ima naziv "Karta" te se nalazi na poziciji "Igrac1".
                  Izvrši se povecanje kliknute karte         
                 */
    void KartaOdabrana()
    {
        
       // Vector3 pozicijaMisa = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -10));
        RaycastHit2D odabrano = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
      
        if (Input.GetMouseButtonDown(0))
        {   
            if (odabrano.collider.CompareTag("Karta"))
            {
                if (brojacKlikova == 1)
                {

                    Odabrano1 = odabrano.collider.gameObject;
                    Odabrano1.transform.position = new Vector3(Odabrano1.transform.position.x, Odabrano1.transform.position.y, Odabrano1.transform.position.z - 1);                 
                    print("povecaj kartu");
                    brojacKlikova++;                 
                    return;
                }
                else if (brojacKlikova == 2)
                {
                    Odabrano2 = odabrano.collider.gameObject;

                    if (Odabrano1 != Odabrano2)
                    {
                        Odabrano1.transform.position = new Vector3(Odabrano1.transform.position.x, Odabrano1.transform.position.y, Odabrano1.transform.position.z + 1);
                        Odabrano2.transform.position = new Vector3(Odabrano2.transform.position.x, Odabrano2.transform.position.y, Odabrano2.transform.position.z - 1);
                        brojacKlikova = 2;
                        print("Smanji kartu i povečaj drugu");
                        Odabrano1 = Odabrano2;                
                        return;
                    }
                    else if ((Odabrano1 == Odabrano2))
                    {
                        print("Premjesti kartu gore");
                        odabrano.transform.SetParent(OdigranoPozicija1);
                        odabrano.transform.position = OdigranoPozicija1.position;
                        odabrano.transform.localScale = new Vector3(2, 2, 1);
                        brojacKlikova = 1;
                        
                        return;
                    }
                }
                return;
            }
            return;
        }       
    }

  

    

}



