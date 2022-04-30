using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;
using System;

public class TrustVariableStorage : VariableStorageBehaviour
{
    public Action OnTrustCountUpdated;

    private string _kTrustCountStr = "$trustCount";
    private float _trustCount;

    public override bool TryGetValue<T>(string variableName, out T result)
    {
        if (variableName == _kTrustCountStr && typeof(T).IsAssignableFrom(_trustCount.GetType()))
        {
            result = (T)(object)_trustCount;
            return true;
        }

        result = default;
        return false;
    }

    public override void SetValue(string variableName, string stringValue) {}

    public override void SetValue(string variableName, float floatValue)
    {
        if (variableName == _kTrustCountStr)
        {
            _trustCount = floatValue;
            OnTrustCountUpdated?.Invoke();
        }
    }

    public override void SetValue(string variableName, bool boolValue) {}

    public override void Clear() {
        _trustCount = 0;
        OnTrustCountUpdated?.Invoke();
    }

    public override bool Contains(string variableName) {
        if (variableName == _kTrustCountStr)
        {
            return true;
        }
        return false;
    }

    private bool Exists(string variableName, System.Type type)
    {
        if (type == typeof(string))
        {
            return false;
        } else if (type == typeof(bool))
        {
            return false;
        } else if (type == typeof(float))
        {
            return true;
        }
        return false;
    }
}