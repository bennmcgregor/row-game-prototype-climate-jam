using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LengthBarUIDataListener : UIDataListener
{
    [SerializeField] private RowboatDataListener _dataListener;
    [SerializeField] private int[] _thresholds;
    [SerializeField] private Color[] _colors;

    public override float Data => _dataListener.StrokeLength;
    
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
