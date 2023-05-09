using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHand : PlayerHand
{
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
}
