using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Oar : MonoBehaviour
{
    // listens to the oar state and updates the force/rotation on the oar rigidbody

    //visible Properties
    public bool IsPort = false;
    public RowboatParams RowboatParams;
    public RowingInputListener InputListener;
    public Rigidbody OarRigidbody;

    // TEMP: for oar moving during the recovery
    public Rigidbody BoatRigidbody;
    public float RecoveryVelMultiplier = 0.8f;

    public bool ShouldApplyRecoveryForce => _shouldApplyRecoveryForce;

    //internal Properties
    private Vector2 _stick;
    private OarState _state;
    private float _angleMultiplier;
    private bool _shouldApplyRecoveryForce;

    public void Start()
    {
        if (IsPort)
        {
            InputListener.OnPortOarStateChange += state => _state = state;
            InputListener.OnPortStickChange += stick => _stick = stick;
            _angleMultiplier = -1;
            _state = InputListener.PortOarState;
        }
        else
        {
            InputListener.OnStarboardOarStateChange += state => _state = state;
            InputListener.OnStarboardStickChange += stick => _stick = stick;
            _angleMultiplier = 1;
            _state = InputListener.StarboardOarState;
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
        // linear scaling of force input
        float forceMultiplier = _stick.y > 0 ? RowboatParams.MaxPushForce : RowboatParams.MaxPullForce;
        forceMultiplier = forceMultiplier * 2 * RowboatParams.InboardLength; // torque on oar = 2 * Frower * inboard length
        OarRigidbody.AddRelativeTorque(transform.up * _angleMultiplier * -_stick.y * forceMultiplier);

        // TEMP: for oar moving during the recovery
        if (_state == OarState.OarOutOfWater)// && ( IsPort ? OarlockListener.PortPullState : OarlockListener.StarboardPullState ) == PullState.CanPull)
        {
            Vector3 newAngularVel = new Vector3(OarRigidbody.angularVelocity.x, (BoatRigidbody.velocity.z * RecoveryVelMultiplier) / RowboatParams.InboardLength, OarRigidbody.angularVelocity.z); // angular velocity = - boat translational velocity / radius
            
            // tell the rowboat to add lurch force from rushing the slide
            if ( _stick.y != 0 && Math.Abs( OarRigidbody.angularVelocity.y ) > Math.Abs ( newAngularVel.y ) )
            {
                _shouldApplyRecoveryForce = true;
            } else {
                _shouldApplyRecoveryForce = false;
            }

            // update oar speed if user not rowing the oar
            if ( !( InputListener.IsRowingPort || InputListener.IsRowingStarboard ) )
            {
                if ( !IsPort )
                {
                    newAngularVel.y *= -1;
                }
                OarRigidbody.angularVelocity = newAngularVel;
            }

            // if ( IsPort )
            // {
            //     UnityEngine.Debug.Log( $"Oar Port: {newAngularVel}" );
            // }
            // else
            // {
            //     UnityEngine.Debug.Log( $"Oar Starboard: {newAngularVel}" );
            // }
            // UnityEngine.Debug.Log($"Boat: {BoatRigidbody.velocity}");
        }
    }

}