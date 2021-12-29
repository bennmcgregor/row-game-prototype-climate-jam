using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputListener : MonoBehaviour
{
    // manages all input for the game
    // base class

    public Action<ControlScheme> OnControlSchemeUpdate;

    public ControlScheme CurrentControl = ControlScheme.Player;
    public RowingInputListener RowingInputListener;
    public PlayerInputListener PlayerInputListener;

    private void Awake() {
        UpdateListeners();
    }

    public void UpdateCurrentControl(ControlScheme newControl)
    {
        CurrentControl = newControl;
        OnControlSchemeUpdate?.Invoke(CurrentControl);
        UpdateListeners();
    }

    private void UpdateListeners()
    {
        RowingInputListener.enabled = false;
        PlayerInputListener.enabled = false;

        switch(CurrentControl) {
            case ControlScheme.Player:
                PlayerInputListener.enabled = true;
                break;
            case ControlScheme.Rowing:
                RowingInputListener.enabled = true;
                break;
            default:
                break;
        }
    }

}