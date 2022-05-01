using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimatedImageWithImage : MonoBehaviour
{
    [SerializeField] private float _frameRate;
    [SerializeField] private Sprite[] _sprites;
    [SerializeField] private Image _image;
    private int _index = 0;
    private float _timer = 0;

    private void Update()
    {
        if ((_timer += Time.deltaTime) >= (1f / _frameRate))
        {
            _timer = 0;
            _image.sprite = _sprites[_index];
            _index = (_index + 1) % _sprites.Length;
        }
    }
}
