using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] private string _nodeName;
    [SerializeField] private DialogueRunner _dialogueRunner;
    [SerializeField] private InputManager _inputManager;

    // private void OnTriggerEnter2D(Collider2D col)
    // {
    //     TriggerDialogue();
    //     Destroy(gameObject);
    // }

    private void OnCollisionEnter2D(Collision2D col)
    {
        TriggerDialogue();
        Destroy(gameObject);
    }

    public void TriggerDialogue()
    {
        _inputManager.DeactivateInput();
        _dialogueRunner.StartDialogue(_nodeName);
    }
}
