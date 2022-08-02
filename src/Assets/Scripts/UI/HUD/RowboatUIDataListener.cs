using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RowboatUIDataListener : UIDataListener
{
    [SerializeField] private RowboatDataListener _dataListener;
    [SerializeField] private bool _isVelocity;

    public override float Data => _isVelocity ? _dataListener.Velocity : _dataListener.Acceleration;
    
    public override Color GetBarColor()
    {
        if (_isVelocity)
        {
            return Color.white;
        }

        return _dataListener.SlowingDown ? Color.red : Color.green;
    }
}
