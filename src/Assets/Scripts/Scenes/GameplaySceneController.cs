using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplaySceneController : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private bool _hasBranchingInNextScene = false;
    private SceneLoader _sceneLoader;

    private void Awake()
    {
        _sceneLoader = FindObjectOfType<SceneLoader>();
    }

    private void Start()
    {
        // play audio
        _audioSource.Play();
        
        // start asynchronously loading the next scene
        if (_sceneLoader != null && !_hasBranchingInNextScene)
        {
            _sceneLoader.LoadNextScene();
        }
    }

    public void LoadBranchingScene(int numberOfScenesToSkip)
    {
        _sceneLoader.AddToNextSceneIndex(numberOfScenesToSkip);
        _sceneLoader.LoadNextScene();
    }

    public void GoToNextScene()
    {
        _sceneLoader.ActivateNextScene();
    }
}
