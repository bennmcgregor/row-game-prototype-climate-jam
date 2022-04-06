using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class DialogueManager : MonoBehaviour
{
    public Action OnUpdateDialogue;

    [SerializeField] private TrustCounter _trustCounter;
    private DialogueGraph _graph;
    // TEMP
    private DialogueNode _node;

    public Dictionary<int, string> GetResponseOptions => _node.ResponseMap;

    private void Awake()
    {
        // TODO: initialize graph (use a module that reads from file, builds the graph, then passes it to this component)
        _node = new DialogueNode(
            new Dictionary<int, string>{
                {0, "Zero trust threshold text, this is a speech."},
                {25, "25% trust threshold text, this is also a speech."},
                {60, "60% trust threshold text, this yet again a speech."},
            },
            new Dictionary<int, string>{
                {0, "option 1 with id 0"},
                {1, "option 2 with id 1"},
                {2, "option 3 with id 2"},
                {3, "option 4 with id 3"}
            });
    }

    // ASSUMING NICO SPEAKS FIRST
    public string CalculateSpeech()
    {
        return NPCDialogueEngine.SelectResponse(_trustCounter.TrustCount(), _node);
    }

    public void SelectResponse(int edgeId)
    {
        _graph.NextNode(edgeId);
        OnUpdateDialogue?.Invoke();
    }
}
