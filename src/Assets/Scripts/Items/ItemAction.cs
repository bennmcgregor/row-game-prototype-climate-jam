using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAction : Interactable
{
    public string itemName;
    public int itemId;

    protected override void Action ()
    {
        GameObject inv = GameObject.Find("Inventory");
        Inv i = inv.GetComponent<Inv>();

        Sprite s = GetComponent<SpriteRenderer>().sprite;

        i.addItem(new Item(itemId, itemName, s));

        Destroy(gameObject);
    }
}
