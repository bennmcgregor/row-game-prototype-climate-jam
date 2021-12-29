using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class RowingInputListenerOneFinger : RowingInputListener
{
    // exposes the input from the touchscreen (ranging from -1 to 1) with optional sigmoidal scaling
    // manages the oar state based on the input from the controller

    // input scheme where 1 finger: oar in water, 0 fingers: no oar in water

    private bool _isSwiping;

    public void Awake()
    {
        _controls = new PlayerControls(); 
        // _controls.Gameplay.StarboardOar.performed += StarboardStroke;
        // _controls.Gameplay.PortOar.performed += PortStroke;
        // _controls.Gameplay.StarboardOar.canceled += StarboardStroke;
        // _controls.Gameplay.PortOar.canceled += PortStroke;

        // _controls.Gameplay.PrimaryContact.performed += PlaceOars;
        // _controls.Gameplay.PrimaryContact.started += PlaceOars;
        // _controls.Gameplay.PrimaryContact.canceled += LiftOars;

        _controls.Gameplay.PrimaryPosition.performed += OnSwipePerformed;
        _controls.Gameplay.PrimaryPosition.canceled += OnSwipeCanceled;
        _isSwiping = false;

        // OarlockListener.OnPortPullStateChanged += LiftOrPlacePortOar;
        // OarlockListener.OnStarboardPullStateChanged += LiftOrPlaceStarboardOar;
    }

    // private void StarboardStroke(InputAction.CallbackContext ctx)
    // {
    //     if (_starboardOarState != OarState.NotHoldingOars)
    //     {
    //         _starboardStick = ScaleInput(ctx.ReadValue<Vector2>());
    //         OnStarboardStickChange?.Invoke(_starboardStick);
    //     }
    // }

    // private void PortStroke(InputAction.CallbackContext ctx)
    // {
    //     if (_portOarState != OarState.NotHoldingOars)
    //     {
    //         _portStick = ScaleInput(ctx.ReadValue<Vector2>());
    //         OnPortStickChange?.Invoke(_portStick);
    //     }
    // }

    // private Vector2 ScaleInput(Vector2 inputVector)
    // {
    //     if (SigmoidalInputScaling)
    //     {
    //         inputVector.y = Sigmoid(inputVector.y);
    //     }
        
    //     return inputVector;
    // }

    // returns the value scaled into a sigmoid function ranging from y = -1 to 1
    private float Sigmoid(float value)
    {
        // return 2.0f / (1.0f + (float) Math.Exp(-5.5f * value)) - 1.0f;
        return 9.0f * (2.0f / (1.0f + (float) Math.Exp(-0.1f * value)) - 1.0f);
    }

    private Vector2 ConvertToForce(Vector2 inputVector)
    {
        Vector2 vel = GetComponent<Camera>().ScreenToViewportPoint(inputVector) / Time.deltaTime; // normalize the screen coords
        vel.x = Mathf.Clamp(vel.x, -20, 20); // simple clamping for x values
        if (SigmoidalInputScaling) // the sigmoid currently clamps output to (-9, 9)
        {
            vel.y = Sigmoid(vel.y);
        }
        // UnityEngine.Debug.Log(vel);
        return vel;
    }

    private void OnSwipePerformed(InputAction.CallbackContext ctx)
    {
        _starboardStick = ConvertToForce(ctx.ReadValue<Vector2>());
        _portStick = ConvertToForce(ctx.ReadValue<Vector2>());

        if ( !_isSwiping )
        {
            _isSwiping = true;
            PlaceOars();
        }

        // apply force in opposite direction of boat if at end of stroke
        if ( OarlockListener.PortPullState == PullState.CannotPull )
        {
            _portStick *= -1; 
        }
        if ( OarlockListener.StarboardPullState == PullState.CannotPull )
        {
            _starboardStick *= -1;
        }

        OnStarboardStickChange?.Invoke(_starboardStick);
        OnPortStickChange?.Invoke(_portStick);
    }

    private void OnSwipeCanceled(InputAction.CallbackContext ctx)
    {
        _starboardStick = Vector2.zero;
        _portStick = Vector2.zero;

        if ( _isSwiping )
        {
            _isSwiping = false;
            LiftOars();
        }
        
        OnStarboardStickChange?.Invoke(_starboardStick);
        OnPortStickChange?.Invoke(_portStick);
    }

    private void PlaceOars()
    {
        // UnityEngine.Debug.Log("place oars");

        _portOarState = OarState.OarInWater;
        _starboardOarState = OarState.OarInWater;

        OnPortOarStateChange?.Invoke(_portOarState);
        OnStarboardOarStateChange?.Invoke(_starboardOarState);
    }

    private void LiftOars()
    {
        // UnityEngine.Debug.Log("lift oars");

        _portOarState = OarState.OarOutOfWater;
        _starboardOarState = OarState.OarOutOfWater;

        OnPortOarStateChange?.Invoke(_portOarState); 
        OnStarboardOarStateChange?.Invoke(_starboardOarState);
    }

    // private void LiftOrPlacePortOar(PullState state) {
    //     if (state == PullState.CannotPull) {
    //         if (_portOarState == OarState.OarInWater)
    //         {
    //             _portOarState = OarState.OarOutOfWater;
    //         }
    //         else
    //         {
    //             _portOarState = OarState.OarInWater;
    //         }
    //         OnPortOarStateChange?.Invoke(_portOarState);
    //     }
    // }

    // private void LiftOrPlaceStarboardOar(PullState state) {
    //     if (state == PullState.CannotPull) {
    //         if (_starboardOarState == OarState.OarInWater)
    //         {
    //             _starboardOarState = OarState.OarOutOfWater;
    //         }
    //         else
    //         {
    //             _starboardOarState = OarState.OarInWater;
    //         }
    //         OnStarboardOarStateChange?.Invoke(_starboardOarState);
    //     }
    // }

    // private void StarboardOarLifted(InputAction.CallbackContext ctx)
    // {
    //     if (_starboardOarState != OarState.NotHoldingOars)
    //     {
    //         if (_starboardOarState == OarState.OarInWater)
    //         {
    //             _starboardOarState = OarState.OarOutOfWater;
    //         }
    //         else
    //         {
    //             _starboardOarState = OarState.OarInWater;
    //         }
    //         OnStarboardOarStateChange?.Invoke(_starboardOarState);
    //     }
    // }

    // private void PortOarLifted(InputAction.CallbackContext ctx)
    // {
    //     if (_portOarState != OarState.NotHoldingOars)
    //     {
    //         if (_portOarState == OarState.OarInWater)
    //         {
    //             _portOarState = OarState.OarOutOfWater;
    //         }
    //         else
    //         {
    //             _portOarState = OarState.OarInWater;
    //         }
    //         OnPortOarStateChange?.Invoke(_portOarState);
    //     }
    // }

    // private void OarsGrabbed(InputAction.CallbackContext ctx)
    // {
    //     if (_portOarState != OarState.NotHoldingOars && _starboardOarState != OarState.NotHoldingOars)
    //     {
    //         _portOarState = OarState.NotHoldingOars;
    //         _starboardOarState = OarState.NotHoldingOars;
    //     }
    //     else
    //     {
    //         _portOarState = OarState.OarInWater;
    //         _starboardOarState = OarState.OarInWater;
    //     }
    //     OnStarboardOarStateChange?.Invoke(_starboardOarState);
    //     OnPortOarStateChange?.Invoke(_portOarState);
    // }

    public void OnEnable()
    {
        _controls.Gameplay.Enable();
    }

    public void OnDisable()
    {
        _controls.Gameplay.Disable();
    }
}