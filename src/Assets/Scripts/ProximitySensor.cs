using System;
using System.Collections;
using UnityEngine;

public class ProximitySensor : MonoBehaviour
{
    public Action<bool> OnProximityChange;

    public Transform PlayerTransform;
    public float Distance = 50f;

    private bool _withinDistance = false;
    private bool _prevWithinDistance = false;

    private void Start()
    {
        OnProximityChange?.Invoke(_withinDistance);
    }

    private void Update()
    {
        _prevWithinDistance = _withinDistance;
        _withinDistance = Vector3.Distance(transform.position, PlayerTransform.position) < Distance;
        if (_prevWithinDistance != _withinDistance)
        {
            OnProximityChange?.Invoke(_withinDistance);
        }
    }

    private void OnDisable()
    {
        OnProximityChange?.Invoke(false); // notify listeners to deactivate
    }

    private void OnEnable()
    {
        OnProximityChange?.Invoke(true); // notify listeners to activate
    }
}
