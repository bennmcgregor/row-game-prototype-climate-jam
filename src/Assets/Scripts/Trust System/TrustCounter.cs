using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using Yarn.Unity;

public class TrustCounter : MonoBehaviour
{
    private Action OnTrustUpdate;

    // ranges from 0 to 1
    [SerializeField] private float _kInitialValue = 0f;
    [SerializeField] private Slider _slider;
    [SerializeField] private TrustVariableStorage _trustVariableStorage;

    public float TrustCount() {
        _trustVariableStorage.TryGetValue("$trustCount", out float value);
        return value;
    }

    private void Awake()
    {
        OnTrustUpdate += () => _slider.value = TrustCount();
        _trustVariableStorage.OnTrustCountUpdated += OnTrustUpdate;
        UpdateTrust(_kInitialValue);
    }

    [YarnCommand("add_trust")]
    public void AddTrust(float amount)
    {
        UpdateTrust(TrustCount() + amount);
    }

    private void UpdateTrust(float newTrust)
    {
        _trustVariableStorage.SetValue("$trustCount", newTrust);
    }
}
