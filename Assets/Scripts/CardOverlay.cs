using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardOverlay : MonoBehaviour
{
    [SerializeField] private Image overlayFace;

    private void OnEnable()
    {
        Card.OnCardHover += FillOverlay;
        Card.OnHoverExit += DestroySelf;
    }

    private void OnDisable()
    {
        Card.OnCardHover -= FillOverlay;
        Card.OnHoverExit -= DestroySelf;
    }

    private void FillOverlay(Sprite cardFace)
    {
        overlayFace.sprite = cardFace;
    }

    private void DestroySelf()
    {
        Destroy(gameObject);
    }
}
