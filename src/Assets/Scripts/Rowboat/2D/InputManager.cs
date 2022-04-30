using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Yarn.Unity;

public class InputManager : MonoBehaviour
{
    [SerializeField] private List<PlayerInput> _playerInputs;
    [SerializeField] private RowBoat2D _rowboat;

    [YarnCommand("deactivate_input")]
    public void DeactivateInput()
    {
        _rowboat.DeactivateInput();
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
