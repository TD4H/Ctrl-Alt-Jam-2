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
    private PlayerHand hand;

    public enum DamageTypes { Light, Frost, Water, Earth, Dark, Fire, Thunder, Wind }
    
    public static Action<Card, PlayerHand> OnCardSelect;
    public static Action<Card> OnCardDefeat;

    private void OnEnable()
    {
        PlayerHand.OnCardInstantiate += PopulateSelectedCard;
        GameTable.OnBattleStart += VerifyWeakness;
        GameTable.OnCardReady += FlipCard;
    }

    private void OnDisable()
    {
        PlayerHand.OnCardInstantiate -= PopulateSelectedCard;
        GameTable.OnBattleStart -= VerifyWeakness;
        GameTable.OnCardReady -= FlipCard;
    }
    void Update()
    {
        if (isUp)
            imgRenderer.sprite = front;
        else imgRenderer.sprite = back;

        if (Input.GetButtonDown("Jump"))
        {
            if (isUp)
                isUp = false;
            else
                isUp = true;
        }
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

    public void SelectCard()
    {
        isUp = false;
        OnCardSelect(this, hand);
        Destroy(buttonComp);
    }

    private void VerifyWeakness(Card pCard, Card eCard, Transform pDiscard, Transform eDiscard)
    {
        if (this == pCard && (weakVs.Contains(eCard.cardType) || !(weakVs.Contains(eCard.cardType) || strongVs.Contains(eCard.cardType))))
        {
            transform.localPosition = pDiscard.localPosition;
            OnCardDefeat(this);
        }
        else if (this == eCard && (weakVs.Contains(pCard.cardType) || !(weakVs.Contains(pCard.cardType) || strongVs.Contains(pCard.cardType)))) 
        {
            transform.localPosition = eDiscard.localPosition;
            OnCardDefeat(this);
        }
    }

    private void FlipCard(Card card)
    {
        if (card == this)
            isUp = true;
    }
}
