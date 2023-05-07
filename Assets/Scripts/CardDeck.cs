using System;
using System.Collections.Generic;
using UnityEngine;
 

public class CardDeck : MonoBehaviour
{
    private const int CARDS = 8;
    private List<int> cards;

    public static Action<int> OnCardDrawn;

    private void Start()
    {
        cards = new List<int>(8);
        PopulateDeck();
    }

    public void DrawCard()
    {
        OnCardDrawn(cards[^1]);
        cards.RemoveAt(cards.Count - 1);

        if (cards.Count == 0)
            Destroy(gameObject);
    }

    private void PopulateDeck()
    {
        for(int i = 0; i < CARDS; i++)
        {
            cards.Add(i);
        }

        ShuffleList();
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