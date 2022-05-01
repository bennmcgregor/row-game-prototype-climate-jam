using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Yarn.Unity;

public class SlideShowDialogueRunner : MonoBehaviour
{
    public Action NextSlideCalled;

    [SerializeField] private SlideShow _slideShow;
    [SerializeField] private DialogueRunner _dialogueRunner;
    [SerializeField] private string[] _dialogueNodeNames;
    
    private int _currentIndex = 1;

    private void Awake()
    {
        _slideShow.OnNextSlide += OnNextSlide;
    }

    private void OnNextSlide()
    {
        _dialogueRunner.Stop();
        _dialogueRunner.StartDialogue(_dialogueNodeNames[_currentIndex]);
        _currentIndex++;
        NextSlideCalled?.Invoke();
    }

    [YarnCommand("kill_dialogue")]
    public void KillDialogue()
    {
        _dialogueRunner.gameObject.SetActive(false);
        _slideShow.OnNextSlide -= OnNextSlide;
    }
}
