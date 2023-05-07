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
    private bool isUp = true;

    public enum DamageTypes { Light, Frost, Water, Earth, Dark, Fire, Thunder, Wind }

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

    public void PopulateSelectedCard(int cardIndex)
    {
        front = cards[cardIndex].front;
        strongVs = cards[cardIndex].strongVs;
        weakVs = cards[cardIndex].weakVs;
    }
}
