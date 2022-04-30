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
    private bool _isPressingCatch = false;

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

    public void DeactivateInput()
    {
        OnCatchRelease();
        _isPressingCatch = false;
    }

    public void OnCatchPress()
    {
        if (!_isPressingCatch && _spamTimer > _playerRowboatParams.SpamTimeThreshold)
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

            _isPressingCatch = true;
        }
    }

    public void OnCatchRelease()
    {
        if (_isPressingCatch)
        {
            UpdateFinishPosition(GetSliderPosition());
            _velocity = _playerRowboatParams.RecoverySpeed;
            _force = 0;
            _stateMachine.StateTransition();
            _isPressingCatch = false;
        }
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

        if (_stateMachine.State == RowingState.DRIVE)
        {
            UpdateVelocityForDrive(_timeDelta + 1);
        }
    }

    private void Update()
    {
        _spamTimer += Time.deltaTime;
    }
}
