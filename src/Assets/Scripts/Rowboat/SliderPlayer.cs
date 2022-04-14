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
    [SerializeField] private float _velocity = 1f;

    private RowingStateMachine _stateMachine;
    private float _catchPosition;
    private float _finishPosition;

    public float CatchPosition => _catchPosition;
    public float FinishPosition => _finishPosition;
    public float CurrentSpeed => Math.Abs(_velocity);
    public RowingState CurrentState => _stateMachine.State;
    
    private void Awake()
    {
        _stateMachine = new RowingStateMachine(RowingState.RECOVERY);
        UpdateCatchPosition(100);
        UpdateFinishPosition(0);
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
                break;
            case RowingState.RECOVERY:
                UpdateCatchPosition(GetSliderPosition());
                break;
            default:
                break;
        }
        _velocity *= -1;
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

    private void FixedUpdate()
    {
        if (GetSliderPosition() <= 100 && GetSliderPosition() >= 0)
        {
            _slider.value += _velocity;
        }
    }
    
    // measure the time of a cycle input by the player
    // a cycle is catch-drive-recovery-catch.
    // On the next stroke, the stroke slider should set its cycle time to that value 
    // and adjust its velocity to hit the optimal catch (_slider.value >= 100) within that time

    // measure the position of the spacebar press at the end of a cycle
    // a cycle is catch-drive-recovery-catch.
    // On the next stroke, the stroke slider should set its spacebar position to that value
    // and perform the catch at that point.

    // use the previous catch position (most recent catch position after) AND the current velocity of the rower

    // status quo:
        // AI is moving to get to the previous catch position of the player at the same time as the player,
        // assuming they keep their current speed.
            // ie given the player's previous catch position, their current distance from that position, and their current speed,
            // what speed do I need to travel at to reach the same position at the same time as the player?
            // need to predict the time it will take the player to reach that position using their distance and current speed.
            // Then using the time, predict the speed the AI needs to reach that position given their current distance from it.
        // Player changes catch position:
            // 
        // Player changes speed (player can't affect speed, but speed may change spontaneously):
}
