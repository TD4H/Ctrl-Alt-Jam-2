using System;
using System.Collections;
using UnityEngine;

public enum GameStage { CastleDraw, HandDraw, CardSelectP1, CardSelectP2, Battle, Victory, Defeat }

public class TurnManager : MonoBehaviour
{
    [SerializeField] private Canvas canvas;
    [SerializeField] private PlayerHand pHand;
    [SerializeField] private EnemyHand eHand;
    [SerializeField] private OverlayScreen overlayScreen;
    private bool startClash = true;
    public static PlayerHand startingPlayer;
    public static PlayerHand playerOne;
    public static GameStage currentStage;

    public static Action<PlayerHand> OnDrawNeeded;
    public static Action OnCastleDraw;
    public static Action OnBattleCall;
    public static Action OnVictory;
    public static Action OnDefeat;

    private void Start()
    {
        playerOne = pHand;
        StartCoroutine(CastleDraw());
    }

    private void OnEnable()
    {
        PlayerHand.OnEndTurn += SwitchTurns;
        Card.OnCardCheck += SetFirstPlayer;
        GameTable.OnPlacementDone += ReceiveBattleRequest;
        GameTable.OnBattleEnd += AnnounceWinner;
    }

    private void OnDisable()
    {
        PlayerHand.OnEndTurn -= SwitchTurns;
        Card.OnCardCheck -= SetFirstPlayer;
        GameTable.OnPlacementDone -= ReceiveBattleRequest;
        GameTable.OnBattleEnd -= AnnounceWinner;
    }

    private void SetFirstPlayer(PlayerHand losingHand)
    {
        startingPlayer = losingHand;
    }

    private void SwitchTurns()
    {
        if (currentStage != GameStage.CardSelectP1 && currentStage != GameStage.CardSelectP2) 
            return;

        if (currentStage == GameStage.CardSelectP1)
            CardSelectP2();
        else
            CardSelectP1();
    }

    private IEnumerator CastleDraw()
    {
        currentStage = GameStage.CastleDraw;

        yield return new WaitForSeconds(2f);

        OnDrawNeeded(pHand);
        OnDrawNeeded(eHand);
        OnCastleDraw();
        OnBattleCall();

        StartCoroutine(HandDraw());
    }

    private IEnumerator HandDraw()
    {
        currentStage = GameStage.HandDraw;

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

        Instantiate(overlayScreen, canvas.transform);
    }

    private void CardSelectP2()
    {
        currentStage = GameStage.CardSelectP2;

        Instantiate(overlayScreen, canvas.transform);
    }

    private void ReceiveBattleRequest()
    {
        StartCoroutine(StartBattle());
    }

    private IEnumerator StartBattle()
    {
        if (startClash)
        {
            startClash = false;
            currentStage = GameStage.Battle;
            Instantiate(overlayScreen, canvas.transform); 
            yield return new WaitForSeconds(4f);
        }
        else
            yield return new WaitForSeconds(2f);

        OnBattleCall();
    }

    private void AnnounceWinner(PlayerHand winner)
    {
        if (winner == pHand)
            currentStage = GameStage.Victory;
        else
            currentStage = GameStage.Defeat;

        Instantiate(overlayScreen, canvas.transform);
    }
}
