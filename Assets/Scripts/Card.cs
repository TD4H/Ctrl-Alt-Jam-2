using System;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    [SerializeField] private Image imgRenderer;
    [SerializeField] private Sprite back;
    [SerializeField] private CardConfig[] cards;
    private Sprite front;
    private DamageTypes[] strongVs;
    private DamageTypes[] weakVs;
    private bool isUp = false;
    private PlayerHand hand;

    public enum DamageTypes { Light, Frost, Water, Earth, Dark, Fire, Thunder, Wind }
    
    public static Action<Card, PlayerHand> OnCardSelect;

    private void OnEnable()
    {
        PlayerHand.OnCardInstantiate += PopulateSelectedCard;
    }

    private void OnDisable()
    {
        PlayerHand.OnCardInstantiate -= PopulateSelectedCard;
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
            strongVs = cards[cardIndex].strongVs;
            weakVs = cards[cardIndex].weakVs;
            this.hand = hand;
        }
    }

    public void SelectCard()
    {
        OnCardSelect(this, hand);
    }
}
