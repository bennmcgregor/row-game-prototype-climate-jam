using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueGraph
{
    // a connected DAG of DialogueNodes.

    private DialogueNode _currentNode;
    public DialogueNode CurrentNode => _currentNode;

    // TODO: initialization function

    public void NextNode(int edgeId)
    {
        if (_currentNode.IsLastNode)
        {
            if (_currentNode.ShouldTriggerEvent)
            {
                // TODO: create event
                // invoke action that triggers the 
            }
        }
        else
        {
            _currentNode = _currentNode.NextNode(edgeId);
            // invoke action that updates the node
        }
    }
}
