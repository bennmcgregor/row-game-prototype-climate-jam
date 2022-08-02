using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class TrustMeterUIDataListener : UIDataListener
{
    [SerializeField] private int[] _thresholds;
    [SerializeField] private Color[] _colors;

    private TrustMeter _trustMeter;

    public override float Data => GetData(); 

    private float GetData()
    {
        if (_trustMeter == null)
        {
            _trustMeter = FindObjectOfType<TrustMeter>();
        }
        return _trustMeter.TrustValue;
    }

    public override Color GetBarColor()
    {
        for (int i = 1; i < _thresholds.Length; i++)
        {
            if (Data < _thresholds[i])
            {
                return _colors[i-1];
            }
        }
        return _colors[_thresholds.Length - 1];
    }
}
