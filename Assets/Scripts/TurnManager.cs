using System;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    [SerializeField] private PlayerHand hand;
    private bool isPlayerTurn;

    public static Action<PlayerHand, bool> OnTurnSwitch;

    private void OnEnable()
    {
        PlayerHand.OnEndTurn += SwitchTurns;
    }

    private void OnDisable()
    {
        PlayerHand.OnEndTurn -= SwitchTurns;
    }

    private void SwitchTurns()
    {
        if (isPlayerTurn)
            isPlayerTurn = false;
        else
            isPlayerTurn = true;
    }
}
