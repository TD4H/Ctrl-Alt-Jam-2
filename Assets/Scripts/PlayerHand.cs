using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHand : MonoBehaviour
{
    [SerializeField] protected Card card;
    protected List<Card> cards;

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

    protected virtual void ReceiveCard(int index, bool isPlayer, PlayerHand hand)
    {
        if (hand == this)
        {
            Card instance = Instantiate(card, new Vector3(0f, transform.localPosition.y, 0f), Quaternion.identity);
            instance.transform.SetParent(GameObject.Find("PlayerHand").transform, false);
            OnCardInstantiate(instance, index, isPlayer, this);
            cards.Add(instance);
        }
    }

    private void SummonCard(Card card, PlayerHand hand)
    {
        if (hand == this)
        {
            cards.Remove(card);
        }
    }
}
