using Ditzelgames;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(WaterFloat))]
public class OarOld : MonoBehaviour
{
    //visible Properties
    public float RiggerLength = 10f;
    public float Power = 5f;
    public float Drag = 0.1f;
    public Boolean IsPort = false;
    public float CatchAngle = 75f; // should be absolute value
    public float FinishAngle = 60f; // should be absolute value
    public float DroppedAngle = 7.5f; // should be absolute value
    public float LiftingAngle = 0f;

    //used Components
    protected Rigidbody Rigidbody;

    //internal Properties
    private PlayerControls controls;

    private Vector3 forward;

    private Vector2 stick;
    private Boolean isHoldingOars = false;
    private Boolean isLiftingOars = false;
    private int angleMultiplier;
    private Vector3 baseRotation;

    public void Awake()
    {
        Rigidbody = GetComponent<Rigidbody>();
        InitializeControls();

        baseRotation = transform.localEulerAngles;
        angleMultiplier = IsPort ? 1 : -1;
    }

    private void InitializeControls()
    {
        controls = new PlayerControls();
        if (!IsPort)
        {
            controls.Gameplay.StarboardOar.performed += StrokePerformed;
            controls.Gameplay.StarboardOar.canceled += StrokeCanceled;
            controls.Gameplay.LiftStarboardOar.performed += OarLifted;
        } 
        else
        {
            controls.Gameplay.PortOar.performed += StrokePerformed;
            controls.Gameplay.PortOar.canceled += StrokeCanceled;
            controls.Gameplay.LiftPortOar.performed += OarLifted;
        }
        controls.Gameplay.GrabOars.performed += OarHeld;
    }

    private void StrokePerformed(InputAction.CallbackContext ctx)
    {
        stick = ctx.ReadValue<Vector2>();
    }

    private void StrokeCanceled(InputAction.CallbackContext ctx)
    {
        stick = ctx.ReadValue<Vector2>();
    }

    private void OarLifted(InputAction.CallbackContext ctx)
    {
        if (isHoldingOars)
        {
            isLiftingOars = !isLiftingOars;
        }
    }

    private void OarHeld(InputAction.CallbackContext ctx)
    {
        if (isHoldingOars)
        {
            isLiftingOars = false;
        }
        isHoldingOars = !isHoldingOars;
    }

    public void OnEnable()
    {
        controls.Gameplay.Enable();
    }

    public void OnDisable()
    {
        controls.Gameplay.Disable();
    }

    public void FixedUpdate()
    {
        // TODO: change controls to force application + automatic rowing animation
        // TODO: determine oar rotational velocity based on net torque on oars (Torque_net = Torque_oars - Torque_drag)
        // TODO: determine force on oarlock based on parallel component of net torque on oars + pass it to RowBoat
        float xRotationDelta;
        float yRotationDelta;
        Vector3 rotationDelta = Vector3.zero; 

        if (isHoldingOars)
        {
            // range: 15deg (catch) to 150deg (finish) (starboard) (-75 to +60)
            // range: 345deg (catch) to 210deg (finish) (port) (+75 to -60)
            if (stick.y > 0) // closer to the catch
            {
                yRotationDelta = baseRotation.y + stick.y * CatchAngle * angleMultiplier - transform.localEulerAngles.y;
            }
            else if (stick.y < 0) // closer to finish
            {
                yRotationDelta = baseRotation.y + stick.y * FinishAngle * angleMultiplier - transform.localEulerAngles.y;
            }
            else
            {
                yRotationDelta = baseRotation.y - transform.localEulerAngles.y;
            }
            rotationDelta.y += yRotationDelta;

            if (isLiftingOars)
            {
                xRotationDelta = LiftingAngle - transform.localEulerAngles.x;
            }
            else
            {
                xRotationDelta = DroppedAngle - transform.localEulerAngles.x;
            }
            rotationDelta.x += xRotationDelta;
        }
        else
        {
            xRotationDelta = DroppedAngle - transform.localEulerAngles.x;
            rotationDelta.x += xRotationDelta;
        }

        transform.Rotate(rotationDelta);
    }

}