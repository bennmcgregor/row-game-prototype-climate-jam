using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTransitioner : Interactable
{
    [SerializeField] private GameplaySceneController _sceneController;

    protected override void Action()
    {
        _sceneController.GoToNextScene();
    }
}
