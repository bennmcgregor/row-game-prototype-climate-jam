using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oarlock : MonoBehaviour
{
    // pivots the oarlock (and attached oar) when the oar state has changed
    // to place the oar in the water or release it from the water
    public float DroppedZAxisRotation = -7.5f;
    public float LiftedZAxisRotation = 0f; 
    public bool IsPort = false;
    public RowingInputListener InputListener;
    public Transform Connector;

    private OarState _state;
    private Vector3 _prevConnectorRotation;

    private void Start()
    {
        if (IsPort)
        {
            InputListener.OnPortOarStateChange += OnStateChange;
            OnStateChange(InputListener.PortOarState);
        }
        else
        {
            InputListener.OnStarboardOarStateChange += OnStateChange;
            OnStateChange(InputListener.StarboardOarState);
        }

        _prevConnectorRotation = Connector.eulerAngles;
    }

    private void OnStateChange(OarState state)
    {
        _state = state;
        UpdateZAxisRotation();
    }

    private void UpdateZAxisRotation()
    {
        Vector3 rotationDelta = Vector3.forward;
        if (_state == OarState.OarOutOfWater)
        {
            rotationDelta.z *= (LiftedZAxisRotation - transform.localEulerAngles.z);
        }
        else
        {
            rotationDelta.z *= (DroppedZAxisRotation - transform.localEulerAngles.z);
        }
        transform.Rotate(rotationDelta);
    }
}
