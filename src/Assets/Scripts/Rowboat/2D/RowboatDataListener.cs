using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// Facade class that exposes data for the rowboat HUD

public class RowboatDataListener : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private RowboatParamsProvider _rowboatParamsProvider;
    [SerializeField] private RowBoat2D _rowboat;

    private float _prevVelocity = 0f;
    private float _acceleration = 0f;
    private float _togethernessScore = 100f;
    private float _strokeLength = 0f;
    private IEnumerator _currentCountNPCDelay;
    private float _playerCatchPosition;

    public float Velocity => _rigidbody2D.velocity.x * _rowboatParamsProvider.RowboatParams.DisplayedSpeedMultiplier;
    public float Acceleration => _acceleration * _rowboatParamsProvider.RowboatParams.DisplayedAccelerationMultiplier;
    public bool SlowingDown => Math.Sign(_acceleration) != Math.Sign(Velocity);
    public float Togetherness => _togethernessScore;
    public float StrokeLength => _strokeLength;

    private void Awake()
    {
        _rowboat.OnPlayerCatch += StartTogethernessCount;
        // _rowboat.OnPlayerFinish += StartTogethernessCount;
        _rowboat.OnNPCCatch += StopTogethernessCount;
        // _rowboat.OnNPCFinish += StopTogethernessCount;

        _rowboat.OnPlayerCatch += StartLengthCount;
        _rowboat.OnPlayerFinish += EndLengthCount;
    }

    private void FixedUpdate()
    {
        _acceleration = _rigidbody2D.velocity.x - _prevVelocity;
        _prevVelocity = _rigidbody2D.velocity.x;
    }

    // calculate togetherness:
    // when the player catches/finishes, take the scalar distance the NPC is from them,
    // and then add on the amount of time it takes for the NPC to catch/finish.
    // Reset the score each time the player catches or finishes.

    private float _prevTogetherness;

    private void StartTogethernessCount(float playerSlider, float npcSlider)
    {
        if (_currentCountNPCDelay != null)
        {
            StopCoroutine(_currentCountNPCDelay);
        }
        _togethernessScore = 100 - Math.Abs(playerSlider - npcSlider) * 10;
        _prevTogetherness = _togethernessScore;
        UnityEngine.Debug.Log($"playerSlider - npcSlider: {playerSlider - npcSlider} playerSlider - npcSlider * 10: {_togethernessScore}");
        _currentCountNPCDelay = CountNPCDelay();
        StartCoroutine(_currentCountNPCDelay);
    }

    private void StopTogethernessCount()
    {
        StopCoroutine(_currentCountNPCDelay);
        UnityEngine.Debug.Log($"_togethernessScore: {_togethernessScore}, diff: {_togethernessScore - _prevTogetherness}");
    }

    private IEnumerator CountNPCDelay()
    {
        while (true)
        {
            _togethernessScore -= Time.deltaTime * 100; // need to multiply this by a regularization factor
            
            yield return null;
        }
    }

    private void StartLengthCount(float playerSlider, float npcSlider)
    {
        _playerCatchPosition = playerSlider;
        UnityEngine.Debug.Log($"catch pos: {_playerCatchPosition}");
    }

    private void EndLengthCount(float playerSlider, float npcSlider)
    {
        _strokeLength = Math.Abs(_playerCatchPosition - playerSlider);
        UnityEngine.Debug.Log($"stroke len: {_strokeLength}");
    }
}
