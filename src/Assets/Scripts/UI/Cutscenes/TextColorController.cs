using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class TextColorController : MonoBehaviour
{
    [SerializeField] private SlideShowDialogueRunner _slideshowDialogueRunner;
    [SerializeField] private Color[] _textColors;
    [SerializeField] private TMP_Text _characterName;
    [SerializeField] private TMP_Text _body;

    private int _currentIndex = 0;

    private void Awake()
    {
        _slideshowDialogueRunner.NextSlideCalled += OnNextSlide;
        OnNextSlide();
    }

    private void OnNextSlide()
    {
        ChangeTextColor();
        _currentIndex++;
    }

    private void ChangeTextColor()
    {
        Color color = _textColors[_currentIndex];
        _characterName.color = color;
        _body.color = color;
    }
}