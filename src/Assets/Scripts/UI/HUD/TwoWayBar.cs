using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class TwoWayBar : MonoBehaviour
{
    [SerializeField] private Slider _positive;
    [SerializeField] private Slider _negative;
    [SerializeField] private Image _positiveImage;
    [SerializeField] private Image _negativeImage;
    [SerializeField] private UIDataListener _dataListener;
    [SerializeField] private float _sensitivity = 0.05f;

    private void Start()
    {
        StartCoroutine(UpdateBar());
    }

    private IEnumerator UpdateBar()
    {
        while (true)
        {
            _positive.value = Mathf.Clamp(_dataListener.Data, _positive.minValue, _positive.maxValue);
            _negative.value = Math.Abs(Mathf.Clamp(_dataListener.Data, -_negative.maxValue, _negative.minValue));
            _positiveImage.color = _dataListener.GetBarColor();
            _negativeImage.color = _dataListener.GetBarColor();
            yield return new WaitForSeconds(_sensitivity);
        }
    }
}
