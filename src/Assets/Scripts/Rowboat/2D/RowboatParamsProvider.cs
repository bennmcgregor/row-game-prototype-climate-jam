using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RowboatParamsProvider : MonoBehaviour
{
    [SerializeField] private int[] _thresholds;
    [SerializeField] private TrustSystemRowboatParams2D[] _trustSystemRowboatParams;
    [SerializeField] private RowboatParams2D _rowboatParams;

    public RowboatParams2D RowboatParams => _rowboatParams;

    private TrustMeter _trustMeter;

    private void Start()
    {
        _trustMeter = FindObjectOfType<TrustMeter>();
        // _trustMeter.OnTrustValueUpdated += UpdateRowboatParams;
        // UpdateRowboatParams();
    }

    public TrustSystemRowboatParams2D GetTrustSystemParams()
    {
        if (_trustMeter == null)
        {
            _trustMeter = FindObjectOfType<TrustMeter>();
            if (_trustMeter == null)
            {
                return _trustSystemRowboatParams[_thresholds.Length / 2];
            }
        }

        for (int i = 1; i < _thresholds.Length; i++)
        {
            if (_trustMeter.TrustValue < _thresholds[i])
            {
                return _trustSystemRowboatParams[i-1];
            }
        }
        return _trustSystemRowboatParams[_thresholds.Length - 1];
    }

    // private void UpdateRowboatParams()
    // {
    //     for (int i = 1; i < _thresholds.Length; i++)
    //     {
    //         if (_trustMeter.TrustValue < _thresholds[i])
    //         {
    //             _rowboatParams.TrustSystemRowboatParams2D = _trustSystemRowboatParams[i-1];
    //             return;
    //         }
    //     }
    //     _rowboatParams.TrustSystemRowboatParams2D = _trustSystemRowboatParams[_thresholds.Length - 1];
    // }
}
