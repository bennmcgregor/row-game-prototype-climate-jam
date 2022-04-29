using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RowboatSlider : MonoBehaviour
{
    [SerializeField] private Sprite[] _driveSequence = default;
    [SerializeField] private Sprite[] _recoverySequence = default;
    [SerializeField] private float _startValue = 0f;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private RowBoat2D _rowboat = default;

    public float Value => _value;
    public bool ShouldApplyDrag => _shouldApplyDrag;

    // value on a scale from 0 to 100
    private float _value;
    private bool _shouldApplyDrag = false;
    // private bool _isTransitionConcluded = false;
    // private int _currentIndex;
    // private bool _currentlyDrive = false;

    private void Awake()
    {
        _value = _startValue;
        _spriteRenderer.sprite = _recoverySequence[0];

        // _rowboat.OnReverseStarted += () => StartCoroutine(TransitionSprite());
        // _rowboat.OnReverseEnded += () => StartCoroutine(TransitionSprite());
    }

    // animates the rowboat
    public void AddValue(float value, RowingState rowingState)
    {
        // UnityEngine.Debug.Log(value);

        _value += value;
        if (_value < 0)
        {
            _value = 0;
        }
        else if (_value > 100)
        {
            _value = 100;
        }

        UpdateSprite(value, rowingState);
    }

    private void UpdateSprite(float value, RowingState rowingState)
    {
        Sprite sprite;
        if (rowingState == RowingState.RECOVERY)
        {
            // _currentlyDrive = false;
            // UnityEngine.Debug.Log($"recovery: {GetSpriteIndex(_recoverySequence.Length)}");
            sprite = _recoverySequence[GetSpriteIndex(_recoverySequence.Length)];
        }
        else
        {
            // _currentlyDrive = true;
            // UnityEngine.Debug.Log($"drive: {GetSpriteIndex(_driveSequence.Length)}");
            int idx = GetSpriteIndex(_driveSequence.Length);
            sprite = _driveSequence[idx];

            if (idx == _driveSequence.Length && value != 0) // TODO: add reverse condition (idx == 0)
            {
                _shouldApplyDrag = true;
            }
            else
            {
                _shouldApplyDrag = false;
            }
        }

        _spriteRenderer.sprite = sprite;
    }

    private int GetSpriteIndex(int newScale)
    {
        float scale = (float) newScale - 0.01f;
        float unrounded = (scale / 100f) * (Math.Abs(_value) - 100f) + scale;
        // UnityEngine.Debug.Log($"unrounded: {unrounded}");
        int idx = (int) Math.Floor(unrounded); // floor so that the sprites are evenly distributed
        if (_rowboat.DirectionState == DirectionState.REVERSE/* && _isTransitionConcluded || // reverse
            !_rowboat.IsReverse && !_isTransitionConcluded*/) // when transitioning from reverse
        {
            // _currentIndex = newScale - 1 - idx;
            return newScale - 1 - idx;
        }
        // _currentIndex = idx;
        return idx;
    }

    // private IEnumerator TransitionSprite()
    // {
    //     _isTransitionConcluded = false;
    //     int newScale = _recoverySequence.Length;
    //     if (_currentlyDrive)
    //     {
    //         newScale = _driveSequence.Length;
    //     }

    //     while (_currentIndex != GetSpriteIndex(newScale))
    //     {
    //         if (_currentIndex > GetSpriteIndex(newScale))
    //         {
    //             _currentIndex -= 1;
    //         }
    //         else
    //         {
    //             _currentIndex += 1;
    //         }

    //         if (_currentlyDrive)
    //         {
    //             _spriteRenderer.sprite = _driveSequence[_currentIndex];
    //         }
    //         else
    //         {
    //             _spriteRenderer.sprite = _recoverySequence[_currentIndex];
    //         }

    //         yield return new WaitForSeconds(.05f);
    //     }
    //     _isTransitionConcluded = true;
    // }
}
