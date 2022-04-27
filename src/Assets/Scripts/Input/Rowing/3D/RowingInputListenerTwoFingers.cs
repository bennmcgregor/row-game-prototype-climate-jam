using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class RowingInputListenerTwoFingers : RowingInputListener
{
    // exposes the input from the touchscreen (ranging from -1 to 1) with optional sigmoidal scaling
    // manages the oar state based on the input from the controller

    // input scheme where 1 finger: row in water, 2 fingers: move oar out of water, 0: oars move naturally

    private int _prevNumTouches;
    public Camera Camera;

    public void Awake()
    {
        _prevNumTouches = 0;
    }

    // returns the value scaled into a sigmoid function ranging from y = -1 to 1
    private float Sigmoid(float value)
    {
        // return 2.0f / (1.0f + (float) Math.Exp(-5.5f * value)) - 1.0f;
        return 9.0f * (2.0f / (1.0f + (float) Math.Exp(-0.1f * value)) - 1.0f);
    }

    private Vector2 ConvertToForce(Vector2 inputVector)
    {
        Vector2 vel = Camera.ScreenToViewportPoint(inputVector) / Time.deltaTime; // normalize the screen coords
        vel.x = Mathf.Clamp(vel.x, -20, 20); // simple clamping for x values
        if (SigmoidalInputScaling) // the sigmoid currently clamps output to (-9, 9)
        {
            vel.y = Sigmoid(vel.y);
        }
        // UnityEngine.Debug.Log(vel);
        return vel;
    }

    private void Update()
    {
        if ( _prevNumTouches != Input.touchCount )
        {
            if ( Input.touchCount == 0 )
            {
                LiftOars();
                OnSwipeCanceled();
            } else if ( Input.touchCount == 1 )
            {
                PlaceOars();
            } else if ( Input.touchCount == 2 )
            {
                LiftOars();
            }
            _prevNumTouches = Input.touchCount;
        }
        
        if ( Input.touchCount > 0 )
        {
            Vector2 stickVal = Input.GetTouch(0).deltaPosition;
            if ( Input.touchCount == 2 )
            {
                // average the position of the two fingers
                stickVal = (Input.GetTouch(0).deltaPosition + Input.GetTouch(1).deltaPosition) / 2;
            }
            OnSwipePerformed( stickVal );
        }
    }

    private void OnSwipePerformed(Vector2 stickVal)
    {
        _starboardStick = ConvertToForce(stickVal);
        _portStick = ConvertToForce(stickVal);
        
        // if oars in water
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

    private void OnSwipeCanceled()
    {
        _starboardStick = Vector2.zero;
        _portStick = Vector2.zero;
        
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
}