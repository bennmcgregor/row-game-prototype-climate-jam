using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class SliderPlayer : MonoBehaviour
{
    public Action<float> OnCatchPositionUpdated;
    public Action<float> OnFinishPositionUpdated;

    [SerializeField] private Slider _slider;
    [SerializeField] private PlayerRowboatParams _playerRowboatParams;
    [SerializeField] private float _velocity = 1f;

    private RowingStateMachine _stateMachine;
    private float _catchPosition;
    private float _finishPosition;
    private ForceCurve _currentForceCurve;
    private float _timeDelta;

    public float CatchPosition => _catchPosition;
    public float FinishPosition => _finishPosition;
    public float CurrentSpeed => Math.Abs(_velocity);
    public float CurrentVelocity => _velocity;
    public RowingState CurrentState => _stateMachine.State;
    
    private void Awake()
    {
        _stateMachine = new RowingStateMachine(RowingState.RECOVERY);
        UpdateCatchPosition(100);
        UpdateFinishPosition(0);
        _timeDelta = 0;
    }

    public float GetSliderPosition()
    {
        if (_slider.value > 100)
        {
            return 100;
        }
        if (_slider.value < 0)
        {
            return 0;
        }
        return _slider.value;
    }

    public void OnCatch()
    {
        switch (_stateMachine.State)
        {
            case RowingState.DRIVE:
                UpdateFinishPosition(GetSliderPosition());
                _velocity = _playerRowboatParams.RecoverySpeed;
                break;
            case RowingState.RECOVERY:
                UpdateCatchPosition(GetSliderPosition());
                float predictedDriveLength = _catchPosition - _finishPosition;
                _currentForceCurve = new ForceCurve(
                    predictedDriveLength,
                    _playerRowboatParams.RecoverySpeed,
                    _playerRowboatParams.MaxAcceleration,
                    predictedDriveLength / _playerRowboatParams.DriveSpeed);
                UpdateVelocityForDrive(0);
                break;
            default:
                break;
        }
        _stateMachine.StateTransition();
    }

    private void UpdateCatchPosition(float pos)
    {
        _catchPosition = pos;
        UnityEngine.Debug.Log($"new _catchPosition = {_catchPosition}");
        OnCatchPositionUpdated?.Invoke(_catchPosition);
    }

    private void UpdateFinishPosition(float pos)
    {
        _finishPosition = pos;
        UnityEngine.Debug.Log($"new _finishPosition = {_finishPosition}");
        OnFinishPositionUpdated?.Invoke(_finishPosition);
    }

    private void UpdateVelocityForDrive(float newTime)
    {
        _timeDelta = newTime;
        _velocity = _currentForceCurve.GetVelocityAtTime(_timeDelta);
        _velocity *= -1; // because speed from UpdateVelocityForDrive is positive
    }

    private void FixedUpdate()
    {
        if (GetSliderPosition() <= 100 && GetSliderPosition() >= 0)
        {
            _slider.value += _velocity;
        }

        if (_stateMachine.State == RowingState.DRIVE)
        {
            UpdateVelocityForDrive(_timeDelta + 1);
        }
    }
}
