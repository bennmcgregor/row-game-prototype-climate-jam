using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTransitionTrigger : MonoBehaviour
{
    [SerializeField] private GameplaySceneController _sceneController;

    private void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("NextScene");
        _sceneController.GoToNextScene();
    }
}
