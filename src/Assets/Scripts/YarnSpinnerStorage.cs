using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class YarnSpinnerStorage : MonoBehaviour
{
    private static bool _wasMotherSaved = false;

    public void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    [YarnFunction("getWasMotherSaved")]
    public static bool getWasMotherSaved ()
    {
        return _wasMotherSaved;
    }

    [YarnFunction("setWasMotherSaved")]
    public static void setWasMotherSaved (bool value)
    {
        _wasMotherSaved = value;
    }
}
