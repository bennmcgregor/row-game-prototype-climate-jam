using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Yarn.Unity;

public class InputManager : MonoBehaviour
{
    [SerializeField] private List<PlayerInput> _playerInputs;

    // private void Start()
    // {
    //     var playerInputs = FindObjectsOfType<PlayerInput>();
    //     foreach (var playerInput in playerInputs)
    //     {
    //         _playerInputs.Add(playerInput);
    //     }
    // }

    [YarnCommand("deactivate_input")]
    public void DeactivateInput()
    {
        foreach (var playerInput in _playerInputs)
        {
            playerInput.DeactivateInput();
        }
    }

    [YarnCommand("activate_input")]
    public void ActivateInput()
    {
        foreach (var playerInput in _playerInputs)
        {
            playerInput.ActivateInput();
        }
    }
}
