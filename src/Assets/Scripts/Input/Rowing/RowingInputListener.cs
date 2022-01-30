using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class RowingInputListener : MonoBehaviour
{
    // exposes the input from the touchscreen (ranging from -1 to 1) with optional sigmoidal scaling
    // manages the oar state based on the input from the controller

    // base class

    // subscribable actions
    // oar state
    public Action<OarState> OnStarboardOarStateChange;
    public Action<OarState> OnPortOarStateChange;

    // swipe input
    public Action<Vector2> OnPortStickChange;
    public Action<Vector2> OnStarboardStickChange;

    // configurable parameters
    public bool SigmoidalInputScaling = true;
    public OarlockListener OarlockListener;
    public InputListener InputListener;

    // getter
    public virtual OarState PortOarState => _portOarState;
    public virtual OarState StarboardOarState => _starboardOarState;
    public virtual bool IsRowingPort => _portStick.y != 0;
    public virtual bool IsRowingStarboard => _starboardStick.y != 0;

    //internal Properties
    protected PlayerControls _controls;
    
    protected OarState _starboardOarState = OarState.OarOutOfWater;
    protected OarState _portOarState = OarState.OarOutOfWater;

    protected Vector2 _starboardStick;
    protected Vector2 _portStick;
}