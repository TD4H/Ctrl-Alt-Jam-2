using System;
using System.Collections.Generic;
using UnityEngine;
 

public class CardDeck : MonoBehaviour
{
    private const int CARDS = 8;
    private List<int> cards = new List<int>(8);

    public static Action<int> drawCard;

    private void Start()
    {
        PopulateDeck();
    }

    private void Update()
    {
        PrintDeck();
    }

    public void DrawCard()
    {
        //drawCard(cards[^1]);
        cards.RemoveAt(cards.Count - 1);
    }

    private void PopulateDeck()
    {
        for(int i = 0; i< CARDS; i++)
        {
            cards.Add(i);
        }

        ShuffleList();
    }

    private void PrintDeck()
    {
        foreach (int i in cards)
        {
            Debug.Log(i.ToString());
        }
    }

    private void ShuffleList()
    {
        for (int i = 0; i < cards.Count; i++)
        {
            int temp = cards[i];
            int randomIndex = UnityEngine.Random.Range(i, cards.Count);
            cards[i] = cards[randomIndex];
            cards[randomIndex] = temp;
        }
    }

}