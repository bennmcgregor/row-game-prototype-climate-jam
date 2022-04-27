using Ditzelgames;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(WaterFloat))]
public class RowBoat : MonoBehaviour
{
    // apply force and drag to the boat depending on the input and state of the oars
    // move the camera to follow the boat at all times
    // control the entry/exit of the rower in the boat

    //visible Properties
    public RowboatParams RowboatParams;
    public RowingInputListener InputListener;
    public OarlockListener OarlockListener;
    public Oar StarboardOar;
    public Oar PortOar;
    public RotatingSign RotatingSign;
    public Rower Rower;
    public Transform RowerPosition;
    public ProximitySensor ProximitySensor;

    //used Components
    protected Rigidbody Rigidbody;
    protected Camera Camera;

    //internal Properties
    protected Vector3 CamVel;

    private OarState _starboardState;
    private OarState _portState;
    private Vector2 _starboardStick; 
    private Vector2 _portStick;

    public void Awake()
    {
        Rigidbody = GetComponent<Rigidbody>();
        Camera = Camera.main;

        InputListener.OnPortOarStateChange += state => _portState = state;
        InputListener.OnPortStickChange += stick => _portStick = stick;
        InputListener.OnStarboardOarStateChange += state => _starboardState = state;
        InputListener.OnStarboardStickChange += stick => _starboardStick = stick;

        _portState = InputListener.PortOarState;
        _starboardState = InputListener.StarboardOarState;

        RotatingSign.OnUsed += OnGetIntoBoat;
    }

    public void FixedUpdate()
    {
        float translationalZDragFactor = RowboatParams.TranslationalZDragFactor;
        float angularDragFactor = RowboatParams.AngularDragFactor;

        // calculate drag on boat due to drag on oars when not pulling or when end of stroke reached
        if (_portState != OarState.OarOutOfWater && (!InputListener.IsRowingPort || OarlockListener.PortPullState == PullState.CannotPull))
        {
            translationalZDragFactor += RowboatParams.OarTranslationalZDragFactor * OarlockListener.PortEffortScalingFactor;
            angularDragFactor += RowboatParams.OarAngularDragFactor * OarlockListener.PortEffortScalingFactor;
        }

        if (_starboardState != OarState.OarOutOfWater && (!InputListener.IsRowingStarboard || OarlockListener.StarboardPullState == PullState.CannotPull))
        {
            translationalZDragFactor += RowboatParams.OarTranslationalZDragFactor * OarlockListener.StarboardEffortScalingFactor;
            angularDragFactor += RowboatParams.OarAngularDragFactor * OarlockListener.StarboardEffortScalingFactor;
        }
        
        // force and torque applied to boat when pulling on the oars
        if (_portState == OarState.OarInWater && OarlockListener.PortPullState == PullState.CanPull)
        {
            // linear scaling of force input
            float inputForceMultiplier = _portStick.y > 0 ? RowboatParams.MaxPushForce : RowboatParams.MaxPullForce;
            float boatForceMultiplier = inputForceMultiplier * -OarlockListener.PortEffortScalingFactor * (RowboatParams.InboardLength / RowboatParams.OutboardLength); // force on boat = Frower.z * (inboard/outboard)
            
            Vector3 appliedForceOnBoat = transform.forward * _portStick.y * boatForceMultiplier;
            Vector3 appliedTorqueOnBoat = Vector3.up * _portStick.y * RowboatParams.RiggerLength * boatForceMultiplier; // torque on boat = force on boat x Lrigger = force on boat * Lrigger
            
            UnityEngine.Debug.Log("PORT Force: " + appliedForceOnBoat + " Torque: " + appliedTorqueOnBoat);

            Rigidbody.AddForce(appliedForceOnBoat);
            Rigidbody.AddRelativeTorque(appliedTorqueOnBoat);
        }

        if (_starboardState == OarState.OarInWater && OarlockListener.StarboardPullState == PullState.CanPull)
        {
            // linear scaling of force input
            float inputForceMultiplier = _starboardStick.y > 0 ? RowboatParams.MaxPushForce : RowboatParams.MaxPullForce;
            float boatForceMultiplier = inputForceMultiplier * -OarlockListener.StarboardEffortScalingFactor * (RowboatParams.InboardLength / RowboatParams.OutboardLength); // force on boat = Frower.z * (inboard/outboard)

            Vector3 appliedForceOnBoat = transform.forward * _starboardStick.y * boatForceMultiplier;
            Vector3 appliedTorqueOnBoat = Vector3.up * _starboardStick.y * -1 * RowboatParams.RiggerLength * boatForceMultiplier; // torque on boat = force on boat x Lrigger = force on boat * Lrigger

            UnityEngine.Debug.Log("STARBOARD Force: " + appliedForceOnBoat + " Torque: " + appliedTorqueOnBoat);

            Rigidbody.AddForce(appliedForceOnBoat);
            Rigidbody.AddRelativeTorque(appliedTorqueOnBoat);
        }
        
        // force from rushing the slide
        if ( PortOar.ShouldApplyRecoveryForce )
        {
            Vector3 appliedForceOnBoat = transform.forward * _portStick.y * RowboatParams.RushingForceMultiplier;

            // only add force, not torque, since it's caused by the rower rushing the slide
            Rigidbody.AddForce(appliedForceOnBoat);
        }

        if ( StarboardOar.ShouldApplyRecoveryForce )
        {
            Vector3 appliedForceOnBoat = transform.forward * _starboardStick.y * RowboatParams.RushingForceMultiplier;
            Rigidbody.AddForce(appliedForceOnBoat);
        }

        // TODO: update drag factors based on speed of boat relative to the water ie vel_net = vel_water - vel_boat

        // UnityEngine.Debug.Log("trans: " + translationalZDragFactor);
        // UnityEngine.Debug.Log("ang: " + angularDragFactor);

        // apply the drag to the boat
        Rigidbody.velocity = Vector3.Scale(
            Rigidbody.velocity,
            new Vector3(
                (1 - Time.deltaTime * RowboatParams.TranslationalXDragFactor),
                (1 - Time.deltaTime * RowboatParams.TranslationalYDragFactor),
                (1 - Time.deltaTime * translationalZDragFactor)
            )
        );

        Rigidbody.angularVelocity *= (1 - Time.deltaTime * angularDragFactor);

        //camera position
        // Camera.transform.LookAt(transform.position - transform.forward * 6f + transform.up * 2f);
        // Camera.transform.position = Vector3.SmoothDamp(Camera.transform.position, transform.position - transform.forward * -8f + transform.up * 2f, ref CamVel, 0.05f);
    }

    private void OnGetIntoBoat()
    {
        Rower.GetIntoBoat(RowerPosition);
        InputListener.InputListener.UpdateCurrentControl(ControlScheme.Rowing);
    }
}