using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ScreenSplitTouchProcessor : MonoBehaviour
{
    // manages the touches on the screen, categorizing them into the left and right 
    // halves of the screen. RowingInputListenerTwoFingersScreenSplit subscribes to 
    // this class to independently control the left and right oars in the boat.

    public Camera Camera;
    private List<Touch> _leftTouches;
    private List<Touch> _rightTouches;

    public List<Touch> LeftTouches => _leftTouches;
    public List<Touch> RightTouches => _rightTouches;

    public void Awake()
    {
        _leftTouches = new List<Touch>();
        _rightTouches = new List<Touch>();
    }

    private void Update()
    {
        // touches are structs (value types), so need to update list every frame (yikes!)
        _leftTouches = new List<Touch>();
        _rightTouches = new List<Touch>();
        for ( int i = 0; i < Input.touchCount; i++ )
        {
            TouchAdd(Input.GetTouch(i));
        }
    }

    private void TouchAdd(Touch touch)
    {
        bool isOnLeft = touch.position.x < (Screen.width / 2);
        if ( isOnLeft )
        {
            if ( _leftTouches.Count < 2 )
            {
                _leftTouches.Add(touch);
            }
        }
        else
        {
            if ( _rightTouches.Count < 2 )
            {
                _rightTouches.Add(touch);
            }
        }
    }
}