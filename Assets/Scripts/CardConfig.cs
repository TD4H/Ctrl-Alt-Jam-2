using UnityEngine;
using static Card;

[CreateAssetMenu(fileName = "CardConfig", menuName = "CardConfig")]
public class CardConfig : ScriptableObject
{
    public Card cardPrefab;
    public Sprite front;
    public DamageTypes cardType;
    public DamageTypes[] strongVs;
    public DamageTypes[] weakVs;
}