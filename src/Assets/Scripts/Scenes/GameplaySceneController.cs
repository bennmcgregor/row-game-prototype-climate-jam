using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;


public class GameplaySceneController : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private bool _hasBranchingInNextScene = false;
    private SceneLoader _sceneLoader;

    private void Start()
    {
        Debug.Log("Here");
        // play audio
        _audioSource.Play();

        _sceneLoader = FindObjectOfType<SceneLoader>();
        
        // start asynchronously loading the next scene
        if (_sceneLoader != null && !_hasBranchingInNextScene)
        {
            _sceneLoader.LoadNextScene();
        }
        else 
        {
            Debug.Log("SceneLoader not found");
        }
    }

    public void LoadBranchingScene(int numberOfScenesToSkip)
    {
        _sceneLoader.AddToNextSceneIndex(numberOfScenesToSkip);
        _sceneLoader.LoadNextScene();
    }
    [YarnCommand("go_to_next_scene")]
    public void GoToNextScene()
    {
        _sceneLoader = FindObjectOfType<SceneLoader>();
        _sceneLoader.ActivateNextScene();
    }
}
