
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public abstract class SlideShowBase : MonoBehaviour
{
    public Action OnSlideshowFinished;
    public abstract void StartSlideshow(int startingSlideIdx = 0);
}