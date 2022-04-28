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

    private void Awake()
    {
        _value = _startValue;
        _spriteRenderer.sprite = _recoverySequence[0];
    }

    // animates the rowboat
    public void AddValue(float value)
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

        UpdateSprite(value);
    }

    private void UpdateSprite(float value)
    {
        Sprite sprite;
        if (value >= 0) // recovery
        {
            // UnityEngine.Debug.Log($"recovery: {GetSpriteIndex(_recoverySequence.Length)}");
            sprite = _recoverySequence[GetSpriteIndex(_recoverySequence.Length)];
        }
        else // drive
        {
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
        if (_rowboat.IsReverse)
        {
            return newScale - 1 - idx;
        }
        return idx;
    }
}
