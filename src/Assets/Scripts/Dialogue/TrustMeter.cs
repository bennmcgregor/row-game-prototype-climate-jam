using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class TrustMeter : MonoBehaviour
{
    private int _trustValue = 50;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    [YarnCommand("add_trust_value")]
    public void AddToTrustValue(int amount)
    {
        _trustValue += amount;
    }
}
