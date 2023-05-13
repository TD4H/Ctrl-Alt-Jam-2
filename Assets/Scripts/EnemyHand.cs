using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHand : PlayerHand
{
    [SerializeField] private Animator animator;

    public static Action<Card> OnCardChosen;

    protected override void OnEnable()
    {
        base.OnEnable();
        TurnManager.OnEnemyTurn += BeginTurn;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        TurnManager.OnEnemyTurn -= BeginTurn;
    }

    protected override void ReceiveCard(int index, bool isPlayer, PlayerHand hand)
    {
        if (hand == this)
        {
            Card instance = Instantiate(card, new Vector3(0f, transform.localPosition.y, 0f), Quaternion.Euler(new Vector3(0f, 0f, 180f)));
            instance.transform.SetParent(GameObject.Find("EnemyHand").transform, false);
            OnCardInstantiate(instance, index, isPlayer, this);
            cards.Add(instance);
        }
    }

    private void BeginTurn()
    {
        if (TurnManager.currentStage == GameStage.CardSelectP2)
            animator.SetTrigger("BeginTurn");
    }

    private void ChooseCard()
    {
        OnCardChosen(cards[0]);
    }
}
