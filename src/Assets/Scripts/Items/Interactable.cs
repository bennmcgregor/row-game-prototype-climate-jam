using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class Interactable : MonoBehaviour
{
    public float minDistance = 3.5f;
    public GameObject boat;
    public GameObject canvas;
    public bool isRequired = false;
    private bool isComplete = false;

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

    public void Start ()
    {
        ChildStart();
        isComplete = !isRequired;
    }

    public void OnInteract()
    {
        float distance = Vector3.Distance(transform.position, boat.transform.position);
        if (Mathf.Abs(distance) < Mathf.Abs(minDistance))
        {
            isComplete = true;
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

    protected virtual void ChildStart ()
    {
        return;
    }

    public bool GetIsComplete()
    {
        return isComplete;
    }

    [YarnCommand("set_interactable_complete")]
    public void SetInteractableComplete(bool complete)
    {
        isComplete = complete;
    }
}
