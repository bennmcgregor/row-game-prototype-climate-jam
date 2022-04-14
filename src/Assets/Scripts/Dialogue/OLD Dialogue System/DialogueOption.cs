using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class DialogueOption : MonoBehaviour
{
    // int type is the edge_id
    public Action<int> OnOptionSelected;

    // the view logic for the option buttons
    [SerializeField] private TMP_Text _text;
    [SerializeField] private Button _button;

    private int _edgeId;

    private void Awake()
    {
        _button.onClick.AddListener(() => OnOptionSelected(_edgeId));
    }

    public void Initialize(string text, int edgeId)
    {
        gameObject.SetActive(true);
        _text.text = text;
        _edgeId = edgeId;
    }

    // after wiping, the option can no longer be selected and stores no value
    public void Wipe()
    {
        _text.text = "";
        _edgeId = 0;
        gameObject.SetActive(false);

    }
}
