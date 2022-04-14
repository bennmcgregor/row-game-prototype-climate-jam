using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class DialogueView : MonoBehaviour
{
    // a component that accepts input from and displays output to the player
    [SerializeField] private DialogueManager _dialogueManager;
    [SerializeField] private TMP_Text _speechBox;
    [SerializeField] private List<DialogueOption> _options;

    private void Awake()
    {
        foreach (var option in _options)
        {
            option.OnOptionSelected += SelectOption;
        }

        // TODO: make this work for player speaking first too
        _dialogueManager.OnUpdateDialogue += NPCSpeakFirst;
    }

    private void Start()
    {
        // ASSUMING NICO SPEAKS FIRST
        NPCSpeakFirst();
    }

    private void NPCSpeakFirst()
    {
        // ASSUMING NICO SPEAKS FIRST
        // get the dialogue text
        GetSpeech();
        GetPlayerOptions();
    }

    private void GetSpeech()
    {
        _speechBox.text = _dialogueManager.CalculateSpeech();
    }

    private void GetPlayerOptions()
    {
        foreach (var option in _options)
        {
            option.Wipe();
        }
        // populate _options with the response options
        var dictOptions = _dialogueManager.GetResponseOptions;
        for (int i = 0; i < dictOptions.Keys.Count; i++)
        {
            int edgeId = dictOptions.Keys.ToList()[i];
            string responseText = dictOptions[edgeId];
            _options[i].Initialize(responseText, edgeId);
        }
    }

    private void SelectOption(int edgeId)
    {
        UnityEngine.Debug.Log($"Select Option: {edgeId}");
        _dialogueManager.SelectResponse(edgeId);
    }
}
