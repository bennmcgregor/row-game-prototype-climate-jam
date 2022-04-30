using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class MapPanner : MonoBehaviour
{
    [SerializeField] private SlideShow _slideShow;
    [SerializeField] private int _timesIdx = 1;
    [SerializeField] private RectTransform _rectTransform;
    [SerializeField] private int _panDirection = 1; // positive

    private float _shotTime;
    private float _totalHeight;
    private float _velocity;

    private void Awake()
    {
        _shotTime = _slideShow.Times[_timesIdx];
        _totalHeight = 2*Math.Abs(_rectTransform.anchoredPosition.y);
        _velocity = (_panDirection * Time.fixedDeltaTime * _totalHeight) / _shotTime;
    }

    private void FixedUpdate()
    {
        Vector2 pos = new Vector2(_rectTransform.anchoredPosition.x, _rectTransform.anchoredPosition.y + _velocity);
        _rectTransform.anchoredPosition = pos;
    }
}