using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RowingInputListenerTwoFingersSingleHalf : RowingInputListener
{
    // exposes the input from the touchscreen (ranging from -1 to 1) with optional sigmoidal scaling
    // manages the oar state based on the input from the controller

    // input scheme where 1 finger: row in water, 2 fingers: move oar out of water, 0: oars move naturally
    // each oar moves independently of the other based on the position of the swipe on the screen

    private int _prevNumTouches;
    public ScreenSplitTouchProcessor ScreenSplitTouchProcessor;
    public Camera Camera;
    public bool IsPort = false;

    private List<Touch> _inputList => IsPort ? ScreenSplitTouchProcessor.RightTouches : ScreenSplitTouchProcessor.LeftTouches;
    public Vector2 Stick {
        get => IsPort ? _portStick : _starboardStick;
        set
        {
            if (IsPort)
            {
                _portStick = value;
            }
            else
            {
                _starboardStick = value;
            }
        }
    }
    public OarState OarState
    {
        get => IsPort ? _portOarState : _starboardOarState;
        set
        {
            if (IsPort)
            {
                _portOarState = value;
            }
            else
            {
                _starboardOarState = value;
            }
        }
    }
    private PullState _pullState => IsPort ? OarlockListener.PortPullState : OarlockListener.StarboardPullState;
    public Action<Vector2> OnStickChange => IsPort ? OnPortStickChange : OnStarboardStickChange;
    public Action<OarState> OnOarStateChange => IsPort ? OnPortOarStateChange : OnStarboardOarStateChange;

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
        if ( _prevNumTouches != _inputList.Count )
        {
            if ( _inputList.Count == 0 )
            {
                LiftOars();
                OnSwipeCanceled();
            } else if ( _inputList.Count == 1 )
            {
                PlaceOars();
            } else if ( _inputList.Count == 2 )
            {
                LiftOars();
            }
            _prevNumTouches = _inputList.Count;
        }
        
        if ( _inputList.Count > 0 )
        {
            string logString = "inputList elements: ";
            foreach (var elem in _inputList)
            {
                logString += elem.deltaPosition + " ";
            }
            UnityEngine.Debug.Log(logString + " IsPort: " + IsPort);

            Vector2 stickVal = _inputList[0].deltaPosition;
            if ( _inputList.Count == 2 )
            {
                // average the position of the two fingers
                stickVal = (_inputList[0].deltaPosition + _inputList[1].deltaPosition) / 2;
            }
            OnSwipePerformed( stickVal );
        }
    }

    private void OnSwipePerformed(Vector2 stickVal)
    {
        Stick = ConvertToForce(stickVal);
        
        // if oars in water
        // apply force in opposite direction of boat if at end of stroke
        if ( _pullState == PullState.CannotPull )
        {
            Stick = Stick * -1; 
        }

        OnStickChange?.Invoke(Stick);
    }

    private void OnSwipeCanceled()
    {   
        Stick = Vector2.zero;
        
        OnStickChange?.Invoke(Stick);
    }

    private void PlaceOars()
    {
        // UnityEngine.Debug.Log("place oars");

        OarState = OarState.OarInWater;

        OnOarStateChange?.Invoke(OarState);
    }

    private void LiftOars()
    {
        // UnityEngine.Debug.Log("lift oars");

        OarState = OarState.OarOutOfWater;

        OnOarStateChange?.Invoke(OarState);
    }
}