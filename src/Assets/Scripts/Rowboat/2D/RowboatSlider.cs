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
    public void AddValue(float value, RowingState rowingState)
    {
        // UnityEngine.Debug.Log(value);

        _value += value;
        _shouldApplyDrag = false;

        if (_value < 0)
        {
            _value = 0;
            if (rowingState == RowingState.DRIVE)
            {
                _shouldApplyDrag = true;
            }
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
            sprite = _recoverySequence[GetSpriteIndex(_recoverySequence.Length)];
        }
        else
        {
            int idx = GetSpriteIndex(_driveSequence.Length);
            sprite = _driveSequence[idx];
        }

        _spriteRenderer.sprite = sprite;
    }

    private int GetSpriteIndex(int newScale)
    {
        float scale = (float) newScale - 0.01f;
        float unrounded = (scale / 100f) * (Math.Abs(_value) - 100f) + scale;
        int idx = (int) Math.Floor(unrounded); // floor so that the sprites are evenly distributed
        if (idx > newScale - 1)
        {
            idx = newScale - 1;
        }
        else if (idx < 0)
        {
            idx = 0;
        }
        
        if (_rowboat.DirectionState == DirectionState.REVERSE)
        {
            return newScale - 1 - idx;
        }
        return idx;
    }
}
