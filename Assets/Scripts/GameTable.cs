using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTable : MonoBehaviour
{
    [SerializeField] private Transform canvas;
    [SerializeField] private PlayerHand pHand;
    [SerializeField] private EnemyHand eHand;
    [SerializeField] private Transform pCastle;
    [SerializeField] private Transform eCastle;
    private List<Card> playerCards = new List<Card>();
    private List<Card> enemyCards = new List<Card>();

    private void OnEnable()
    {
        Card.OnCardSelect += PlaceCard;
    }

    private void OnDisable()
    {
        Card.OnCardSelect -= PlaceCard;
    }

    private void PlaceCard(Card card, PlayerHand hand)
    {
        if (hand == pHand)
        {
            playerCards.Add(card);
            card.transform.SetParent(canvas, false);
            card.transform.localPosition = pCastle.localPosition + new Vector3((playerCards.Count - 1) * 80f, 0f);
        }
        else if (hand == eHand)
        {
            enemyCards.Add(card);
            card.transform.SetParent(canvas, false);
            card.transform.localPosition = eCastle.localPosition - new Vector3((enemyCards.Count - 1) * 80f, 0f);
        }
    }
}
