using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateSprite : MonoBehaviour
{
    public Sprite KarteIzgled;
    public Sprite KartePrefab;

    private SpriteRenderer spriteRenderer;
    private Selectable selectable;
    private Bela bela;


    
    void Start()
    {
        List<string> deck = Bela.KreirajSpil();
        bela = FindObjectOfType<Bela>();

        int i = 0;
        foreach(string card in deck)
        {
            if (this.name ==card)
            {
                KarteIzgled = bela.KarteIzgled[i];
                break;
            }
            i++;
        }
        spriteRenderer = GetComponent<SpriteRenderer>();
        selectable = GetComponent<Selectable>();
    }

   
    void Update()
    {
        if (selectable.KartaOkrenutaPremaGore == true)
        {
            spriteRenderer.sprite = KarteIzgled;
        }
        else
        {
            spriteRenderer.sprite = KartePrefab;
        }
    }
}
