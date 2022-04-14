using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DialogueNode
{
    // map of possible speeches, key is floor(trust_threshold * 100) and value is text
    private Dictionary<int, string> _speechMap;

    // map of possible responses, key is edge_id or floor(trust_threshold * 100) and value is text
    private Dictionary<int, string> _responseMap;

    // map of out-neighbours, key is edge_id, value is DialogueNode*
    private Dictionary<int,DialogueNode> _outNeighbours;
    // TODO: make this an Event object (mapping from event to dialogue)
    private bool _shouldTriggerEvent;
    
    public bool IsLastNode => _outNeighbours.Keys.Count == 0;
    public bool ShouldTriggerEvent => _shouldTriggerEvent;
    public Dictionary<int, string> SpeechMap => _speechMap;
    public Dictionary<int, string> ResponseMap => _responseMap;

    // TODO: initialization function
    public DialogueNode(Dictionary<int, string> speechMap, Dictionary<int, string> responseMap)
    {
        _speechMap = speechMap;
        _responseMap = responseMap;
    }

    public string SelectSpeech(float trustValue)
    {
        int key = (int) Math.Floor(trustValue * 100);
        if (!_speechMap.ContainsKey(key))
        {
            throw new System.Exception($"DialogueNode.SelectSpeech: _speechMap does not contain key: {key}");
        }
        return _speechMap[key];
    }

    public string SelectResponseFromTrustValue(float trustValue)
    {
        int key = (int) Math.Floor(trustValue * 100);
        if (!_responseMap.ContainsKey(key))
        {
            throw new System.Exception($"DialogueNode.SelectResponseFromTrustValue: _responseMap does not contain key: {key}");
        }
        return _responseMap[key];
    }

    public DialogueNode NextNode(int edgeId)
    {
        if (!_outNeighbours.ContainsKey(edgeId))
        {
            throw new System.Exception($"DialogueNode.NextNode: _outNeighbours does not contain key: {edgeId}");
        }
        return _outNeighbours[edgeId];
    }
}
