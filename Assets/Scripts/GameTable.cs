using System;
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
    [SerializeField] private Transform pDiscard;
    [SerializeField] private Transform eDiscard;
    private List<Card> playerCards = new();
    private List<Card> enemyCards = new();

    public static Action<Card, Card, Transform, Transform> OnBattleStart;
    public static Action<Card> OnCardReady;
    public static Action OnBattleRequest;
    public static Action<PlayerHand> OnBattleEnd;

    private void OnEnable()
    {
        Card.OnCardSelect += PlaceCard;
        Card.OnCardDefeat += RemoveCard;
        TurnManager.OnBattleCall += StartBattle;
    }

    private void OnDisable()
    {
        Card.OnCardSelect -= PlaceCard;
        Card.OnCardDefeat -= RemoveCard;
        TurnManager.OnBattleCall -= StartBattle;
    }

    private void PlaceCard(Card card, PlayerHand hand)
    {
        if (hand == eHand)
        {
            enemyCards.Add(card);
            card.transform.SetParent(canvas, false);
            card.transform.localPosition = eCastle.localPosition - new Vector3((enemyCards.Count - 1) * 80f, 0f);
            FlipCard(card);
        }
        else if (hand == pHand)
        {
            playerCards.Add(card);
            card.transform.SetParent(canvas, false);
            card.transform.localPosition = pCastle.localPosition + new Vector3((playerCards.Count - 1) * 80f, 0f);
            FlipCard(card);
        }
    }

    private void StartBattle()
    {
        if (playerCards.Count > 0 && enemyCards.Count > 0)
            OnBattleStart(playerCards[0], enemyCards[0], pDiscard, eDiscard);
    }

    public void FlipCard(Card card)
    {
        if (enemyCards.Count > 0 && card == enemyCards[0])
            OnCardReady(card);
        
        else if (playerCards.Count > 0 && card == playerCards[0])
            OnCardReady(card);
    }

    private void RemoveCard(Card card)
    {
        if (playerCards.Contains(card))
        { 
            playerCards.Remove(card);
            if (playerCards.Count != 0)
                UpdateCardPositions(playerCards, pCastle);
        }
        else if (enemyCards.Contains(card))
        {
            enemyCards.Remove(card);
            if (enemyCards.Count != 0)
                UpdateCardPositions(enemyCards, eCastle);
        }

        if (playerCards.Count != 0 && enemyCards.Count != 0)
            OnBattleRequest();
        else if (playerCards.Count > enemyCards.Count)
            OnBattleEnd(pHand);
        else
            OnBattleEnd(eHand);
    }

    private void UpdateCardPositions(List<Card> listToUpdate, Transform castle)
    {
        int mult = 1;
        int count = 0;

        if (castle == eCastle)
            mult = -1;

        foreach (Card card in listToUpdate)
        {
            card.transform.localPosition = castle.localPosition + (new Vector3(count * 80f, 0f) * mult);
            count++;
        }

        FlipCard(listToUpdate[0]);
    }

    public bool IsTableFull()
    {
        if (playerCards.Count >= 3 && enemyCards.Count >= 3)
            return true;
        else 
            return false;
    }
}
