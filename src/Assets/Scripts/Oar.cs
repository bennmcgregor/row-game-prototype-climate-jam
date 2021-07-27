using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Oar : MonoBehaviour
{
    //visible Properties
    public float RiggerLength = 0.8f; // spread = 160cm
    public float InboardLength = 0.88f; // spread / 2 + 8cm
    public float OutboardLength = 2f; // 200cm
    public float MaxPullForce = 450f; // 700 N
    public float MaxPushForce = 200f;
    public float DragFactor = 1f;
    public bool IsPort = false;
    public InputListener InputListener;
    public Rigidbody OarRigidbody;

    //internal Properties
    private Vector2 _stick;
    private OarState _state;
    private float _angleMultiplier;

    public void Start()
    {
        if (IsPort)
        {
            InputListener.OnPortOarStateChange += state => _state = state;
            InputListener.OnPortStickChange += stick => _stick = stick;
            _angleMultiplier = -1;
        }
        else
        {
            InputListener.OnStarboardOarStateChange += state => _state = state;
            InputListener.OnStarboardStickChange += stick => _stick = stick;
            _angleMultiplier = 1;
        }
    }

    public void FixedUpdate()
    {
        /* Angular drag based on boat translational (?) velocity
        if (_state != OarState.OarOutOfWater)
        {

        }*/

        // TODO: Add angular drag based on medium that the oar is in (air or water)

        // Torque_net = 2 * Frower * inboard length
        if (_state != OarState.NotHoldingOars)
        {
            // linear scaling of force input
            float forceMultiplier = _stick.y > 0 ? MaxPushForce : MaxPullForce;
            forceMultiplier = forceMultiplier * 2 * InboardLength; // torque on oar = 2 * Frower * inboard length
            OarRigidbody.AddRelativeTorque(transform.up * _angleMultiplier * -_stick.y * forceMultiplier);
        }
    }

}