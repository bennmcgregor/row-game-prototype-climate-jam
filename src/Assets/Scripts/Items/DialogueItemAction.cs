using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class DialogueItemAction : MonoBehaviour
{
    public string itemName;
    public int itemId;

    [YarnCommand("pickup_item")]
    public void PickupItem()
    {
        GameObject inv = GameObject.Find("Inventory");
        Inv i = inv.GetComponent<Inv>();

        Sprite s = GetComponent<SpriteRenderer>().sprite;

        i.addItem(new Item(itemId, itemName, s));

        Destroy(gameObject);
    }
}
