using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTransitioner : Interactable
{
    [SerializeField] private GameplaySceneController _sceneController;
    [SerializeField] private Interactable[] _interactables = new Interactable[0];

    [SerializeField] private DialogueTrigger _dialogueTrigger;

    private bool _hasNotRunYet = true;

    protected override void Action()
    {
        if (_hasNotRunYet)
        {
            _hasNotRunYet = false;
            bool canProgress = true;
            foreach (Interactable interactable in _interactables)
            {
                if (!interactable.GetIsComplete())
                {
                    canProgress = false;
                    break;
                }   
            }
            if (canProgress)
            {
                _sceneController.GoToNextScene();
            }
            else 
            {
                _dialogueTrigger.TriggerDialogue();
            }
        }
    }
}
