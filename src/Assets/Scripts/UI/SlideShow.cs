using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class SlideShow : MonoBehaviour
{
    public Action OnSlideshowFinished;

    [SerializeField] public float[] _times;

    private List<Transform> _children = new List<Transform>();
    private float _nextActionTime = 0;
    private int _currentIndex = 0;
    private bool _hasStarted = false;

    public void StartSlideshow()
    {
        _nextActionTime = Time.time + _times[0];
        foreach (Transform child in transform)
        {
            _children.Add(child);
            child.gameObject.SetActive(false);
        }
        _children[0].gameObject.SetActive(true);
        _hasStarted = true;
    }

    private void Update()
    {
        if (_hasStarted)
        {
            if (Time.time > _nextActionTime)
            {
                NextSlide();
            }
        }
    }

    private void NextSlide()
    {
        if (_currentIndex + 1 < _children.Count)
        {
            _children[_currentIndex].gameObject.SetActive(false);
            _children[++_currentIndex].gameObject.SetActive(true);
            _nextActionTime += _times[_currentIndex];
        }
        else
        {
            OnSlideshowFinished?.Invoke();
        }
    }
}
