using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RowingInputListenerTwoFingersScreenSplit : RowingInputListener
{
    // parent class that provides a uniform interface of the ScreenSplit RowingInputListener
    // to the classes dependent on RowingInputListener

    public RowingInputListenerTwoFingersSingleHalf PortHalf;
    public RowingInputListenerTwoFingersSingleHalf StarboardHalf;
    
    public override OarState PortOarState => PortHalf.OarState;
    public override OarState StarboardOarState => StarboardHalf.OarState;
    public override bool IsRowingPort => PortHalf.Stick.y != 0;
    public override bool IsRowingStarboard => StarboardHalf.Stick.y != 0;

    public void Awake()
    {
        PortHalf.OnPortStickChange += value => OnPortStickChange?.Invoke(value);
        PortHalf.OnPortOarStateChange += value => OnPortOarStateChange?.Invoke(value);

        StarboardHalf.OnStarboardStickChange += value => OnStarboardStickChange?.Invoke(value);
        StarboardHalf.OnStarboardOarStateChange += value => OnStarboardOarStateChange?.Invoke(value);
    }
}