using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SlideShowRunner : MonoBehaviour
{
    [SerializeField] private SlideShow _slideShow;

    private void OnEnable()
    {
        _slideShow.StartSlideshow();
    }
}
