using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplaySceneController : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
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
        if (_sceneLoader != null)
        {
            _sceneLoader.LoadNextScene();
        }
    }

    public void GoToNextScene()
    {
        _sceneLoader.ActivateNextScene();
    }
}
