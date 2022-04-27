using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class RowingInputListenerStick : MonoBehaviour
{
    // exposes the input from the joysticks (ranging from -1 to 1) with optional sigmoidal scaling
    // manages the oar state based on the input from the controller

    // subscribable actions
    // oar state
    public Action<OarState> OnStarboardOarStateChange;
    public Action<OarState> OnPortOarStateChange;
    // joystick input
    public Action<Vector2> OnPortStickChange;
    public Action<Vector2> OnStarboardStickChange;

    // configurable parameters
    public bool SigmoidalInputScaling = true;

    // TEMP
    public OarlockListener OarlockListener;

    // getter
    public OarState PortOarState => _portOarState;
    public OarState StarboardOarState => _starboardOarState;
    public bool IsRowingPort => _portStick.y != 0;
    public bool IsRowingStarboard => _starboardStick.y != 0;

    //internal Properties
    private PlayerControls _controls;
    
    // TEMP CHANGES TO TEST NEW INPUT SYSTEM
    private OarState _starboardOarState = OarState.OarOutOfWater; // OarState.NotHoldingOars;
    private OarState _portOarState = OarState.OarOutOfWater; // OarState.NotHoldingOars;

    private Vector2 _starboardStick;
    private Vector2 _portStick;

    public void Awake()
    {
        _controls = new PlayerControls();

        _controls.Gameplay.StarboardOar.performed += StarboardStroke;
        _controls.Gameplay.PortOar.performed += PortStroke;
        _controls.Gameplay.StarboardOar.canceled += StarboardStroke;
        _controls.Gameplay.PortOar.canceled += PortStroke;

        // TEMP comment out: disable all input except joystick
        // _controls.Gameplay.LiftStarboardOar.performed += StarboardOarLifted;
        // _controls.Gameplay.LiftPortOar.performed += PortOarLifted;

        // _controls.Gameplay.GrabOars.performed += OarsGrabbed;

        OarlockListener.OnPortPullStateChanged += LiftOrPlacePortOar;
        OarlockListener.OnStarboardPullStateChanged += LiftOrPlaceStarboardOar;
    }

    private void StarboardStroke(InputAction.CallbackContext ctx)
    {
        if (_starboardOarState != OarState.NotHoldingOars)
        {
            _starboardStick = ScaleInput(ctx.ReadValue<Vector2>());
            OnStarboardStickChange?.Invoke(_starboardStick);
        }
    }

    private void PortStroke(InputAction.CallbackContext ctx)
    {
        if (_portOarState != OarState.NotHoldingOars)
        {
            _portStick = ScaleInput(ctx.ReadValue<Vector2>());
            OnPortStickChange?.Invoke(_portStick);
        }
    }

    private Vector2 ScaleInput(Vector2 inputVector)
    {
        if (SigmoidalInputScaling)
        {
            inputVector.y = Sigmoid(inputVector.y);
        }
        
        return inputVector;
    }

    // returns the value scaled into a sigmoid function ranging from y = -1 to 1
    private float Sigmoid(float value)
    {
        return 2.0f / (1.0f + (float) Math.Exp(-5.5f * value)) - 1.0f;
    }

    private void LiftOrPlacePortOar(PullState state) {
        if (state == PullState.CannotPull) {
            if (_portOarState == OarState.OarInWater)
            {
                _portOarState = OarState.OarOutOfWater;
            }
            else
            {
                _portOarState = OarState.OarInWater;
            }
            OnPortOarStateChange?.Invoke(_portOarState);
        }
    }

    private void LiftOrPlaceStarboardOar(PullState state) {
        if (state == PullState.CannotPull) {
            if (_starboardOarState == OarState.OarInWater)
            {
                _starboardOarState = OarState.OarOutOfWater;
            }
            else
            {
                _starboardOarState = OarState.OarInWater;
            }
            OnStarboardOarStateChange?.Invoke(_starboardOarState);
        }
    }

    private void StarboardOarLifted(InputAction.CallbackContext ctx)
    {
        if (_starboardOarState != OarState.NotHoldingOars)
        {
            if (_starboardOarState == OarState.OarInWater)
            {
                _starboardOarState = OarState.OarOutOfWater;
            }
            else
            {
                _starboardOarState = OarState.OarInWater;
            }
            OnStarboardOarStateChange?.Invoke(_starboardOarState);
        }
    }

    private void PortOarLifted(InputAction.CallbackContext ctx)
    {
        if (_portOarState != OarState.NotHoldingOars)
        {
            if (_portOarState == OarState.OarInWater)
            {
                _portOarState = OarState.OarOutOfWater;
            }
            else
            {
                _portOarState = OarState.OarInWater;
            }
            OnPortOarStateChange?.Invoke(_portOarState);
        }
    }

    private void OarsGrabbed(InputAction.CallbackContext ctx)
    {
        if (_portOarState != OarState.NotHoldingOars && _starboardOarState != OarState.NotHoldingOars)
        {
            _portOarState = OarState.NotHoldingOars;
            _starboardOarState = OarState.NotHoldingOars;
        }
        else
        {
            _portOarState = OarState.OarInWater;
            _starboardOarState = OarState.OarInWater;
        }
        OnStarboardOarStateChange?.Invoke(_starboardOarState);
        OnPortOarStateChange?.Invoke(_portOarState);
    }

    public void OnEnable()
    {
        _controls.Gameplay.Enable();
    }

    public void OnDisable()
    {
        _controls.Gameplay.Disable();
    }
}