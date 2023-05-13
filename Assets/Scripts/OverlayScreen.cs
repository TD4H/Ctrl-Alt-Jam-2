using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OverlayScreen : MonoBehaviour
{
    [SerializeField] private Image overlay;
    [SerializeField] private Image border;
    [SerializeField] private TMPro.TextMeshProUGUI resultText;

    private void Start()
    {
        switch (TurnManager.currentStage)
        {
            case GameStage.CardSelectP1:
                TurnScreen(true);
                return;

            case GameStage.CardSelectP2:
                TurnScreen(false);
                return;

            case GameStage.Battle:
                CallClash();
                return;

            case GameStage.Victory:
                CallVictory();
                return;

            case GameStage.Defeat:
                CallDefeat();
                return;

            default: 
                return;
        }
    }

    private void TurnScreen(bool isPlayerTurn)
    {
        if (isPlayerTurn)
            resultText.text = "Player Turn";
        else
            resultText.text = "Enemy Turn";
        TurnOverlayOn();

        Destroy(gameObject, 2f);
    }

    private void CallClash()
    {
        resultText.text = "Clash!";
        TurnOverlayOn();

        Destroy(gameObject, 2f);
    }

    private void CallVictory()
    {
        resultText.text = "Victory!";
        TurnOverlayOn();

        Invoke(nameof(CallMenu), 2f);
    }

    private void CallDefeat()
    {
        resultText.text = "Defeat!";
        TurnOverlayOn();

        Invoke(nameof(CallMenu), 2f);
    }

    private void TurnOverlayOn()
    {
        overlay.enabled = true;
        border.enabled = true;
        resultText.enabled = true;
    }

    private void CallMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }
}
