using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RowBoat2D : MonoBehaviour
{
    // apply force and drag to the boat depending on the input and state of the oars
    // move the camera to follow the boat at all times
    // control the entry/exit of the rower in the boat

    //visible Properties
    [SerializeField] private RowboatParams2D _rowboatParams2D;
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private NPCController _npcController;
    [SerializeField] private Rigidbody2D _rigidbody2D;

    private bool _hasPlayerCatched = false;
    private bool _hasDriveEnded = false;
    private RudderStateMachine _rudderStateMachine;
    private float _rudderTimer = 0f;
    private bool _hasMovedVerticallyOnThisStroke = false;
    private bool _hasStoppedBoatOnThisStroke = false;

    // multiplies the force from the player based on how in time the two rowers are.
    private float _timingMultiplier;
    private DirectionStateMachine _directionStateMachine;

    public DirectionState DirectionState => _directionStateMachine.State;

    private void Awake()
    {
        _timingMultiplier = _rowboatParams2D.NoBowEffectTimingMultiplier; 
        _rudderStateMachine = new RudderStateMachine(RudderState.NONE);
        _directionStateMachine = new DirectionStateMachine(DirectionState.FORWARD);
    }

    public void OnRudderUp()
    {
        _rudderStateMachine.OnRudderUp();
    }

    public void OnRudderDown()
    {
        _rudderStateMachine.OnRudderDown();
    }

    public void OnReverse()
    {
        _directionStateMachine.StateTransition();
    }

    private void FixedUpdate()
    {
        float boatForceMultiplierStroke = 0;
        
        if (_playerController.CurrentState == RowingState.DRIVE)
        {
            _rudderTimer += Time.deltaTime;
            if (!_hasPlayerCatched && _hasDriveEnded)
            {
                _hasPlayerCatched = true;
                _hasDriveEnded = false;
            }

            boatForceMultiplierStroke = _playerController.CurrentForce * _rowboatParams2D.MaxPullForce;
            if (DirectionState == DirectionState.REVERSE)
            {
                boatForceMultiplierStroke = _playerController.CurrentForce * _rowboatParams2D.MaxPushForce;
            }

            // UnityEngine.Debug.Log($"acceleration: {_playerController.CurrentForce}, mult: {boatForceMultiplierStroke}");
            // move the boat vertically, if necessary
            if ((_rudderTimer > _rowboatParams2D.RudderTimerThreshhold ||
                Math.Abs(_playerController.CurrentForce) > _rowboatParams2D.RudderAccelerationThreshhold) && 
                Math.Abs(_rigidbody2D.velocity.x) > _rowboatParams2D.RudderMotionThreshhold &&
                !_hasMovedVerticallyOnThisStroke)
            {
                switch (_rudderStateMachine.State)
                {
                    case RudderState.UP:
                        MoveVertically(false);
                        _hasMovedVerticallyOnThisStroke = true;
                        break;
                    case RudderState.DOWN:
                        MoveVertically(true);
                        _hasMovedVerticallyOnThisStroke = true;
                        break;
                    default:
                        break;
                }
            }

            // apply some extra force in the opposite direction to stop the boat
            if ((DirectionState == DirectionState.FORWARD && 
                 _rigidbody2D.velocity.x < 0 - _rowboatParams2D.StoppingSpeedThreshhold ||
                 DirectionState == DirectionState.REVERSE &&
                 _rigidbody2D.velocity.x > 0 + _rowboatParams2D.StoppingSpeedThreshhold) &&
                !_hasStoppedBoatOnThisStroke)
            {
                _rigidbody2D.AddForce(-_rigidbody2D.velocity * _rigidbody2D.mass, ForceMode2D.Impulse);
                _hasStoppedBoatOnThisStroke = true;
            }
        }
        else if (_playerController.CurrentState == RowingState.RECOVERY)
        {
            if (!_hasDriveEnded)
            {
                _hasDriveEnded = true;
                _timingMultiplier = _rowboatParams2D.NoBowEffectTimingMultiplier; 
                _rudderTimer = 0;
                _hasMovedVerticallyOnThisStroke = false;
                _hasStoppedBoatOnThisStroke = false;
            }
        }

        if (_npcController.CurrentState == RowingState.DRIVE)
        {
            if (_hasPlayerCatched)
            {
                float distanceDelta = Math.Abs(_npcController.Slider.Value - _playerController.Slider.Value) / 100f;
                if (distanceDelta < _rowboatParams2D.InPerfectTimeThreshhold)
                {
                    _timingMultiplier = _rowboatParams2D.PerfectTimingMultiplier;
                }
                else if (distanceDelta < _rowboatParams2D.InTimeThreshhold)
                {
                    _timingMultiplier = _rowboatParams2D.InTimeTimingMultiplier;
                }
                else
                {
                    _timingMultiplier = 1f - (float) Math.Sqrt(distanceDelta);
                }
                // UnityEngine.Debug.Log($"dd: {distanceDelta}, _timingMultiplier: {_timingMultiplier}");

                _hasPlayerCatched = false;
            }
        }

        float boatForceMultiplier = boatForceMultiplierStroke * _timingMultiplier;
        if (DirectionState == DirectionState.REVERSE)
        {
            boatForceMultiplier *= -1;
        }

        if (!_hasStoppedBoatOnThisStroke)
        {
            // oars left in the water at the finish for too long
            if (_playerController.GetSliderPosition() == 0 && 
                _playerController.CurrentState == RowingState.DRIVE &&
                Math.Abs(boatForceMultiplier) > 0)
            {
                UnityEngine.Debug.Log("ApplyOarDrag");
                _rigidbody2D.AddForce(
                    -_rigidbody2D.velocity*_rigidbody2D.mass*_rowboatParams2D.OarDragScalingFactor,
                    ForceMode2D.Impulse
                );
            }
            else
            {
                _rigidbody2D.AddForce(Vector2.right * boatForceMultiplier);
            }
        }
    }

    private void MoveVertically(bool down)
    {
        Vector3 directionMultiplier = Vector2.up;
        if (down)
        {
            directionMultiplier = Vector2.down;
        }

        _rigidbody2D.MovePosition(directionMultiplier + transform.position);
    }
}