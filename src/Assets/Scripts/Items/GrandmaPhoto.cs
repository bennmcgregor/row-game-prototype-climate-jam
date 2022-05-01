using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrandmaPhoto : Interactable
{
    public DialogueTrigger dialogueTrigger;
    public string itemName;
    public int itemId;
    private bool hasRun = false;

    protected override void Action ()
    {
        if (!hasRun)
        {
            GameObject inv = GameObject.Find("Inventory");
            Inv i = inv.GetComponent<Inv>();

            Sprite s = GetComponent<SpriteRenderer>().sprite;

            i.addItem(new Item(itemId, itemName, s));

            dialogueTrigger.TriggerDialogue();
            hasRun = true;
        }
    }
}
