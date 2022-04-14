using System.Collections;
using System.Collections.Generic;
using System;

static class NPCDialogueEngine
{
    // pick the response with the maximum trust threshold
    public static string SelectResponse(float currentTrust, DialogueNode node)
    {
        int trustVal = (int) Math.Floor(currentTrust * 100);
        int candidate = 0;
        foreach (int key in node.SpeechMap.Keys)
        {
            if (key < trustVal && key > candidate)
            {
                candidate = key;
            }
        }
        return node.SpeechMap[trustVal];
    }
}
