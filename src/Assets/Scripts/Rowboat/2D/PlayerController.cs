using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class PlayerController : MonoBehaviour
{
    public Action<float> OnCatchPositionUpdated;
    public Action<float> OnFinishPositionUpdated;

    [SerializeField] private RowboatSlider _slider;
    [SerializeField] private PlayerRowboatParams _playerRowboatParams;
    [SerializeField] private float _velocity = 1f;
    [SerializeField] private RowBoat2D _rowboat;

    private RowingStateMachine _stateMachine;
    private float _catchPosition;
    private float _finishPosition;
    private ForceCurve _currentForceCurve;
    private float _timeDelta;
    private float _force;
    private float _spamTimer;

    public float CatchPosition => _catchPosition;
    public float FinishPosition => _finishPosition;
    public float CurrentSpeed => Math.Abs(_velocity);
    public RowingState CurrentState => _stateMachine.State;
    public float CurrentForce => _force;
    public RowboatSlider Slider => _slider;
    
    private void Awake()
    {
        _stateMachine = new RowingStateMachine(RowingState.RECOVERY);
        UpdateCatchPosition(100);
        UpdateFinishPosition(0);
        _timeDelta = 0;
        _spamTimer = 0;
    }

    public float GetSliderPosition()
    {
        return _slider.Value;
    }

    public void OnCatch()
    {
        switch (_stateMachine.State)
        {
            case RowingState.DRIVE:
                UpdateFinishPosition(GetSliderPosition());
                _velocity = _playerRowboatParams.RecoverySpeed;
                _force = 0;
                _stateMachine.StateTransition();
                break;
            case RowingState.RECOVERY:
                if (_spamTimer > _playerRowboatParams.SpamTimeThreshold && 
                    _rowboat.DirectionState != DirectionState.STOPPING) // don't accept new spacebar presses when stopping
                {
                    UpdateCatchPosition(GetSliderPosition());
                    float predictedDriveLength = _catchPosition - _finishPosition;
                    _currentForceCurve = new ForceCurve(
                        predictedDriveLength,
                        _playerRowboatParams.RecoverySpeed,
                        _playerRowboatParams.MaxAcceleration,
                        predictedDriveLength / _playerRowboatParams.DriveSpeed);
                    UpdateVelocityForDrive(0);
                    _spamTimer = 0;
                    _stateMachine.StateTransition();
                }
                break;
            default:
                break;
        }
    }

    public void OnReverse()
    {
        _velocity = 0;
        StartCoroutine(StopBoat());
    }

    private IEnumerator StopBoat()
    {
        while (Math.Round(_slider.Value) != 50)
        {
            if (_slider.Value >= 55)
            {
                _slider.AddValue(-5, RowingState.DRIVE);
            }
            else if (_slider.Value > 50 && _slider.Value < 55)
            {
                _slider.AddValue(-1, RowingState.DRIVE);
            }
            else if (_slider.Value < 50 && _slider.Value > 45)
            {
                _slider.AddValue(1, RowingState.DRIVE);
            }
            else if (_slider.Value <= 45)
            {
                _slider.AddValue(5, RowingState.DRIVE);
            }
            yield return new WaitForFixedUpdate();
        }
        UnityEngine.Debug.Log("run stopboat player");
        if (_stateMachine.State == RowingState.DRIVE)
        {
            _stateMachine.StateTransition();
        }
        // _velocity = 1f;
        UpdateCatchPosition(100);
        UpdateFinishPosition(0);
    }

    private void UpdateCatchPosition(float pos)
    {
        _catchPosition = pos;
        // UnityEngine.Debug.Log($"new _catchPosition = {_catchPosition}");
        OnCatchPositionUpdated?.Invoke(_catchPosition);
    }

    private void UpdateFinishPosition(float pos)
    {
        _finishPosition = pos;
        // UnityEngine.Debug.Log($"new _finishPosition = {_finishPosition}");
        OnFinishPositionUpdated?.Invoke(_finishPosition);
    }

    private void UpdateVelocityForDrive(float newTime)
    {
        _timeDelta = newTime;
        _velocity = _currentForceCurve.GetVelocityAtTime(_timeDelta);
        _velocity *= -1; // because speed from UpdateVelocityForDrive is positive
        _force = _currentForceCurve.GetForceAtTime(_timeDelta);
    }

    private void FixedUpdate()
    {
        _slider.AddValue(_velocity, _stateMachine.State);

        if (_stateMachine.State == RowingState.DRIVE && _rowboat.DirectionState != DirectionState.STOPPING)
        {
            UpdateVelocityForDrive(_timeDelta + 1);
        }
    }

    private void Update()
    {
        _spamTimer += Time.deltaTime;
    }
}
