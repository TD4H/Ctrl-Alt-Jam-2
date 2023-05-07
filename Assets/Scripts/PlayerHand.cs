using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHand : MonoBehaviour
{
    [SerializeField] private Card card;
    private List<Card> cards;

    private float xPos = 40;

    private void Awake()
    {
        cards = new List<Card>();
    }

    private void OnEnable()
    {
        CardDeck.OnCardDrawn += ReceiveCard;
    }

    private void OnDisable()
    {
        CardDeck.OnCardDrawn -= ReceiveCard;
    }

    private void ReceiveCard(int index)
    {
        UpdateCards();
        Card instance = Instantiate(card, new Vector3((xPos*cards.Count), transform.localPosition.y, 0f), Quaternion.identity);
        instance.transform.SetParent(GameObject.Find("Canvas").transform, false);
        instance.PopulateSelectedCard(index);
        cards.Add(instance);
    }

    private void UpdateCards()
    {
        foreach (Card card in cards)
        {
            card.transform.position -= new Vector3(40, 0);
        }
    }
}
