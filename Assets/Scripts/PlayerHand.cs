using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHand : MonoBehaviour
{
    [SerializeField] private Card card;
    private List<Card> cards;

    private float xPos = -300f;

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
        Card instance = Instantiate(card, new Vector3(xPos, 0f, 0f), Quaternion.identity);
        instance.transform.SetParent(GameObject.Find("Canvas").transform, false);
        instance.PopulateSelectedCard(index);
        cards.Add(instance);
        xPos += 80f;
    }
}
