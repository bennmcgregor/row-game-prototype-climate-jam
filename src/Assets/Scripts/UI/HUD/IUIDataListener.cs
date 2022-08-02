using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UIDataListener : MonoBehaviour
{
    public abstract float Data
    {
        get;
    }
    
    public abstract Color GetBarColor();
}
