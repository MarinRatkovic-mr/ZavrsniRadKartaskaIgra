using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Bela : MonoBehaviour
{
    public Sprite[] cardFaces;
    public GameObject cardPrefab;

    //Pravananje po igračima
    public GameObject[] PlayersHandPos;
    public GameObject[] PlayersPlayedPos;

    public static string[] suits = new string[] {"Zelje","Srce","Tikva","Zir" };
    public static string[] values = new string[] { "7", "8", "9", "10","Decko","Baba","Kralj","As" };

    public List<string>[] playersHand;
    public List<string>[] playersPlayed;

    private List<string> playersHand1 = new List<string>();
    private List<string> playersHand2 = new List<string>();
    private List<string> playersHand3 = new List<string>();
    private List<string> playersHand4 = new List<string>();

    public List<string> deck;
    // Start is called before the first frame update
    void Start()
    {
        playersHand = new List<string>[] { playersHand1, playersHand2, playersHand3, playersHand4 };
        PlayCards();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayCards()
    {

        deck = GenerateDeck();
        Shuffle(deck);
        //test the cards in the deck
        foreach(string card in deck)
        {
            print(card);
        }
        BelaSort();
        StartCoroutine(BelaDeal());
    }

    public static List<string> GenerateDeck()
    {
        List<string> newDeck = new List<string>();
        foreach(string s in suits)
        {
            foreach ( string v in values)
            {
                newDeck.Add(s + v);
            }
        }

        return newDeck;
    }

    void Shuffle<T>(List<T> list)
    {
        System.Random random = new System.Random();
        int n = list.Count;
        while (n > 1)
        {
            int k = random.Next(n);
            n--;
            T temp = list[k];
            list[k] = list[n];
            list[n] = temp;
        }

    }
    IEnumerator BelaDeal()
    {
       for(int i=0;i<4;i++) 
       { 

        
        float yOffset = 0;
        float zOffset = 0;
        float xOffset = 0;
        foreach(string card in playersHand[i])
        {
                
                yield return new WaitForSeconds(0.05f);
            GameObject newCard = Instantiate(cardPrefab,new Vector3(PlayersHandPos[i].
                transform.position.x -xOffset, PlayersHandPos[i].transform.position.y - yOffset, 
                PlayersHandPos[i].transform.position.z - zOffset), Quaternion.identity, 
                PlayersHandPos[i].transform);
            newCard.name = card;
               for(int a = 0; a < 6; a++) { 
                  if(card == playersHand1[a]) 
                  { 
                    
                     newCard.GetComponent<Selectable>().faceUp = true;
                  }
                }
               for(int b = 0; b < 8; b++) { 
                   if(card == playersHand1[b])
                   {
                        zOffset = zOffset + 0.03f;
                        xOffset = xOffset - 2f;
                    }
                else if(card == playersHand2[b])
                    {
                        zOffset = zOffset + 0.03f;
                        yOffset = yOffset + 1.5f;
                    }
                    else if (card == playersHand3[b])
                    {
                        zOffset = zOffset + 0.03f;
                        xOffset = xOffset + 2f;
                    }
                    else if(card == playersHand4[b])
                    {
                        zOffset = zOffset + 0.03f;
                        yOffset = yOffset - 1.5f;
                    }
                }
                
                /*
                zOffset = zOffset + 0.03f;
                xOffset = xOffset - 2f;
                */
            }
       }
    }

    void BelaSort()
    {
        /*
        for (int i=0;i<4;i++)
        {
            for (int j= i; j < 4; j++)
            {
                playersHand[j].Add(deck.Last<string>());
                deck.RemoveAt(deck.Count - 1);
            }
        }
        */
        int a = 0;

        while (a < 32) { 
            
            for (int i = 0; i < 4; i++)
            {
                a++;
                playersHand[i].Add(deck.Last<string>());
                deck.RemoveAt(deck.Count -1);
                
            }
       }
    }
}
