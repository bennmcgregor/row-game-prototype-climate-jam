using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;
using System;

public class TrustMeter : MonoBehaviour
{
    public Action OnTrustValueUpdated;

    private int _trustValue = 50;

    public int TrustValue => _trustValue;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    [YarnCommand("add_trust_value")]
    public void AddToTrustValue(int amount)
    {
        _trustValue += amount;
        OnTrustValueUpdated?.Invoke();
        UnityEngine.Debug.Log($"Added {amount} trust to the meter, new total trust: {_trustValue}");
        // TODO: update rowboat parameters based on the _trustValue
    }
}
