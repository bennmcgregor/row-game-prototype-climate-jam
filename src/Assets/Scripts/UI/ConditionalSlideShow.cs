using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class ConditionalSlideShow : SlideShowBase
{
    [SerializeField] private GameObject[] _children;
    [SerializeField] private float _totalTime;

    private float _nextActionTime = 0;
    private bool _hasStarted = false;

    public override void StartSlideshow(int startingSlideIdx = 0)
    {
        _nextActionTime = Time.time + _totalTime;
        _children[startingSlideIdx].gameObject.SetActive(true);
        _hasStarted = true;
    }

    private void Update()
    {
        if (_hasStarted)
        {
            if (Time.time > _nextActionTime)
            {
                OnSlideshowFinished?.Invoke();
            }
        }
    }
}
