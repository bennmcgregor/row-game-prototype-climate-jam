using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class RotatingSign : MonoBehaviour
{
    public Action OnUsed;

    public ProximitySensor Sensor;
    public Transform Target;
    public Button Button;
    public bool ShouldDisappearOnUse = true;
    public Vector3 HeightDelta;

    private bool _active = false;

    private void Awake()
    {
        Sensor.OnProximityChange += active => {
            _active = active;
            gameObject.SetActive(active);
        };

        Button.onClick.AddListener(OnButtonClick);
    }

    private void Update()
    {
        if (_active)
        {
            transform.LookAt(Target);
            
            // update position to always hover above the object
            transform.position = transform.parent.position + HeightDelta;
        }
    }

    private void OnButtonClick()
    {
        if (ShouldDisappearOnUse)
        {
            Sensor.enabled = false;
        }
        OnUsed?.Invoke();
    }
}
