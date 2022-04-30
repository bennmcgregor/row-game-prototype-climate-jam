using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InspectAction : Interactable
{
    public DialogueTrigger dialogueTrigger;

    protected override void Action ()
    {
        Debug.Log("Inspecting");
        dialogueTrigger.TriggerDialogue();
    }
}
