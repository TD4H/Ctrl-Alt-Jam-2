using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerHand : MonoBehaviour
{
    [SerializeField] private Card card;
    private List<Card> cards;

    private float xPos = 40;

    public static Action<Card, int, bool, PlayerHand> OnCardInstantiate;

    private void Awake()
    {
        cards = new List<Card>();
    }

    private void OnEnable()
    {
        CardDeck.OnCardDrawn += ReceiveCard;
        Card.OnCardSelect += SummonCard;
    }

    private void OnDisable()
    {
        CardDeck.OnCardDrawn -= ReceiveCard;
        Card.OnCardSelect -= SummonCard;
    }

    private void ReceiveCard(int index, bool isPlayer, PlayerHand hand)
    {
        if (hand == this)
        {
            UpdateReceivedCards();
            Card instance = Instantiate(card, new Vector3((xPos * cards.Count), transform.localPosition.y, 0f), Quaternion.identity);
            instance.transform.SetParent(GameObject.Find("Canvas").transform, false);
            OnCardInstantiate(instance, index, isPlayer, this);
            cards.Add(instance);
        }
    }

    private void UpdateReceivedCards()
    {
        foreach (Card card in cards)
        {
            card.transform.position -= new Vector3(40, 0);
        }
    }

    private void UpdateRemovedCards(int index)
    {
        for (int i = 0; i < cards.Count; i++)
        {
            if(i < index)
            {
                cards[i].transform.position += new Vector3(40, 0);
            }
            else
            {
                cards[i].transform.position -= new Vector3(40, 0);
            }
        }
    }

    private void SummonCard(Card card, PlayerHand hand)
    {
        if (hand == this)
        {
            int index = cards.IndexOf(card);
            cards.Remove(card);
            UpdateRemovedCards(index);
        }
    }
}
