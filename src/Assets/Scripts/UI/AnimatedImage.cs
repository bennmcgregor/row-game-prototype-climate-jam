using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimatedImage : MonoBehaviour
{
    [SerializeField] private float _frameRate;
    [SerializeField] private Sprite[] _sprites;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    private int _index = 0;
    private float _timer = 0;

    private void Update()
    {
        if ((_timer += Time.deltaTime) >= (1f / _frameRate))
        {
            _timer = 0;
            _spriteRenderer.sprite = _sprites[_index];
            _index = (_index + 1) % _sprites.Length;
        }
    }
}
