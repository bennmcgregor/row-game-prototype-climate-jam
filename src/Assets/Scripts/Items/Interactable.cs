using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public float minDistance = 3.5f;
    public GameObject boat;
    public GameObject canvas;

    // Update is called once per frame
    public void Update()
    {
        ChildUpdate();
        float distance = Vector3.Distance(transform.position, boat.transform.position);
        if (Mathf.Abs(distance) < Mathf.Abs(minDistance))
        {
            canvas.SetActive(true);
        }
        else
        {
            canvas.SetActive(false);
        }
    }

    public void OnInteract()
    {
        float distance = Vector3.Distance(transform.position, boat.transform.position);
        if (Mathf.Abs(distance) < Mathf.Abs(minDistance))
        {
            Action();
        }
    }

    protected virtual void Action ()
    {
        return;
    }

    protected virtual void ChildUpdate ()
    {
        return;
    }
}
