using System;
using System.Collections;
using UnityEngine;

public enum GameStage { CastleDraw, HandDraw, CardSelectP1, CardSelectP2, Battle, Victory, Defeat }

public class TurnManager : MonoBehaviour
{
    [SerializeField] private Canvas canvas;
    [SerializeField] private GameTable gameTable;
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
    public static Action OnEnemyTurn;

    private void Start()
    {
        playerOne = pHand;
        StartCoroutine(CastleDraw());
    }

    private void OnEnable()
    {
        PlayerHand.OnEndTurn += DelaySwitchTurns;
        Card.OnCardCheck += SetFirstPlayer;
        GameTable.OnBattleRequest += ReceiveBattleRequest;
        GameTable.OnBattleEnd += AnnounceWinner;
    }

    private void OnDisable()
    {
        PlayerHand.OnEndTurn -= DelaySwitchTurns;
        Card.OnCardCheck -= SetFirstPlayer;
        GameTable.OnBattleRequest -= ReceiveBattleRequest;
        GameTable.OnBattleEnd -= AnnounceWinner;
    }

    private void SetFirstPlayer(PlayerHand losingHand)
    {
        startingPlayer = losingHand;
    }

    private void DelaySwitchTurns() => Invoke(nameof(SwitchTurns), 0.3f);

    private void SwitchTurns()
    {
        if (currentStage != GameStage.CardSelectP1 && currentStage != GameStage.CardSelectP2) 
            return;

        if (gameTable.IsTableFull())
        {
            ReceiveBattleRequest();
            return;
        }

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

        Invoke(nameof(InstantiateOverlayScreen), 0.2f);
    }

    private void CardSelectP2()
    {
        currentStage = GameStage.CardSelectP2;

        Invoke(nameof(InstantiateOverlayScreen), 0.2f);

        OnEnemyTurn();
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
            Invoke(nameof(InstantiateOverlayScreen), 0.2f);
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

        Invoke(nameof(InstantiateOverlayScreen), 0.2f);
    }

    private void InstantiateOverlayScreen()
    {
        Instantiate(overlayScreen, canvas.transform);
    }
}
