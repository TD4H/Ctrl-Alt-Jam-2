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
    private int index = 0;

    public enum DamageTypes { Light, Frost, Water, Earth, Dark, Fire, Thunder, Wind }

    void Update()
    {
        if (isUp)
            imgRenderer.sprite = front;
        else imgRenderer.sprite = back;

        if (Input.GetKeyUp(KeyCode.Tab))
        {
            index++;
            if (index > 7)
            {
                index = 0;
            }
        }

        if (Input.GetButtonDown("Jump"))
        {
            if (isUp)
                isUp = false;
            else
                isUp = true;
        }

        PopulateSelectedCard(index);
    }

    private void PopulateSelectedCard(int cardIndex)
    {
        front = cards[cardIndex].front;
        strongVs = cards[cardIndex].strongVs;
        weakVs = cards[cardIndex].weakVs;
    }
}
