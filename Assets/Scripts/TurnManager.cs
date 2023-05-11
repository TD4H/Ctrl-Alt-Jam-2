using System;
using System.Collections;
using UnityEngine;

public enum GameStage { CastleDraw, HandDraw, CardSelectP1, CardSelectP2, Battle, Victory, Defeat }

public class TurnManager : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI turnText;
    [SerializeField] private PlayerHand pHand;
    [SerializeField] private EnemyHand eHand;
    public static PlayerHand startingPlayer;
    public static PlayerHand playerOne;
    public static GameStage currentStage;

    public static Action<PlayerHand> OnDrawNeeded;
    public static Action OnCastleDraw;
    public static Action OnBattleCall;

    private void Start()
    {
        playerOne = pHand;
        StartCoroutine(CastleDraw());
    }

    private void OnEnable()
    {
        PlayerHand.OnEndTurn += SwitchTurns;
        Card.OnCardCheck += SetFirstPlayer;
    }

    private void OnDisable()
    {
        PlayerHand.OnEndTurn -= SwitchTurns;
        Card.OnCardCheck -= SetFirstPlayer;
    }

    private void SetFirstPlayer(PlayerHand losingHand)
    {
        startingPlayer = losingHand;

        //turnText.text = "Current turn: " + currentStage;
    }

    private void SwitchTurns()
    {
        if (currentStage == GameStage.CardSelectP1)
            CardSelectP2();
        else
            CardSelectP1();
    }

    private IEnumerator CastleDraw()
    {
        currentStage = GameStage.CastleDraw;
        turnText.text = "Current turn: " + currentStage;

        yield return new WaitForSeconds(3f);

        OnDrawNeeded(pHand);
        OnDrawNeeded(eHand);
        OnCastleDraw();
        OnBattleCall();

        StartCoroutine(HandDraw());
    }

    private IEnumerator HandDraw()
    {
        currentStage = GameStage.HandDraw;
        turnText.text = "Current turn: " + currentStage;

        yield return new WaitForSeconds(2f);

        for (int i = 0; i < 5; i++)
        {
            OnDrawNeeded(pHand);
            OnDrawNeeded(eHand);
        }

        if (startingPlayer == pHand)
            CardSelectP1();
        else
            CardSelectP2();
    }

    private void CardSelectP1()
    {
        currentStage = GameStage.CardSelectP1;

        turnText.text = "Current turn: " + currentStage;
    }

    private void CardSelectP2()
    {
        currentStage = GameStage.CardSelectP2;

        turnText.text = "Current turn: " + currentStage;
    }
}
