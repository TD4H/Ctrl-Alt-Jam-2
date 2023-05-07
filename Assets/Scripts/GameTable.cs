using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTable : MonoBehaviour
{
    [SerializeField] private PlayerHand pHand;
    [SerializeField] private EnemyHand eHand;
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

    void Update()
    {
        
    }

    private void PlaceCard(Card card, PlayerHand hand)
    {
        if (hand == pHand)
        {
            playerCards.Add(card);
            card.transform.localPosition = Vector3.zero;
        }else if (hand == eHand)
        {
            enemyCards.Add(card);
            card.transform.localPosition = Vector3.zero;
        }
    }
}
