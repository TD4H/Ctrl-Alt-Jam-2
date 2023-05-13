using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    [SerializeField] private Image imgRenderer;
    [SerializeField] private Button buttonComp;
    [SerializeField] private Sprite back;
    [SerializeField] private CardConfig[] cards;
    [HideInInspector] public DamageTypes cardType;
    private Sprite front;
    private DamageTypes[] strongVs;
    private DamageTypes[] weakVs;
    private bool isUp = false;
    private bool isFirstCheck = true;
    private PlayerHand hand;

    public enum DamageTypes { Light, Frost, Water, Earth, Dark, Fire, Thunder, Wind }
    
    public static Action<Card, PlayerHand> OnCardSelect;
    public static Action<Card> OnCardDefeat;
    public static Action<PlayerHand> OnCardCheck;

    private void OnEnable()
    {
        PlayerHand.OnCardInstantiate += PopulateSelectedCard;
        EnemyHand.OnCardChosen += EnemySelectCard;
        GameTable.OnBattleStart += VerifyWeakness;
        GameTable.OnCardReady += FlipCard;
        TurnManager.OnCastleDraw += SelectCard;
    }

    private void OnDisable()
    {
        PlayerHand.OnCardInstantiate -= PopulateSelectedCard;
        EnemyHand.OnCardChosen -= EnemySelectCard;
        GameTable.OnBattleStart -= VerifyWeakness;
        GameTable.OnCardReady -= FlipCard;
        TurnManager.OnCastleDraw -= SelectCard;
    }

    void Update()
    {
        if (isUp)
            imgRenderer.sprite = front;
        else imgRenderer.sprite = back;

        if (TurnManager.currentStage != GameStage.CastleDraw)
            isFirstCheck = false;
    }

    public void PopulateSelectedCard(Card card, int cardIndex, bool isPlayer, PlayerHand hand)
    {
        if (card == this)
        {
            isUp = isPlayer;
            front = cards[cardIndex].front;
            cardType = cards[cardIndex].cardType;
            strongVs = cards[cardIndex].strongVs;
            weakVs = cards[cardIndex].weakVs;
            this.hand = hand;
        }
    }

    public void CardOnClick()
    {
        if (hand == TurnManager.playerOne && TurnManager.currentStage != GameStage.CardSelectP1)
            return;
        else if (hand != TurnManager.playerOne && TurnManager.currentStage != GameStage.CardSelectP2)
            return;

        SelectCard();
    }

    private void EnemySelectCard(Card selectedCard)
    {
        if (selectedCard == this)
            SelectCard();
    }

    private void SelectCard()
    {
        isUp = false;
        OnCardSelect(this, hand);
        Destroy(buttonComp);
    }

    private void VerifyWeakness(Card pCard, Card eCard, Transform pDiscard, Transform eDiscard)
    {
        if (this == pCard && (weakVs.Contains(eCard.cardType) || !(weakVs.Contains(eCard.cardType) || strongVs.Contains(eCard.cardType))))
        {
            if (!isFirstCheck)
            {
                transform.localPosition = pDiscard.localPosition;
                OnCardDefeat(this);
            }
            else
            {
                isFirstCheck = false;
                OnCardCheck(hand);
            }
        }
        else if (this == eCard && (weakVs.Contains(pCard.cardType) || !(weakVs.Contains(pCard.cardType) || strongVs.Contains(pCard.cardType))))
        {
            if (!isFirstCheck) 
            {
                transform.localPosition = eDiscard.localPosition;
                OnCardDefeat(this);
            }
            else
            {
                isFirstCheck = false;
                OnCardCheck(hand);
            }
        }
    }

    private void FlipCard(Card card)
    {
        if (card == this)
            isUp = true;
    }
}
