using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class Destroyable : MonoBehaviour
{
    [YarnCommand("destroy")]
    public void DestroySelf()
    {
        Destroy(this.gameObject);
    }
}
