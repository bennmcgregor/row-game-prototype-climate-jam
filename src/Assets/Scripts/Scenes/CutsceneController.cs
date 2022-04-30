using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneController : MonoBehaviour
{
    [SerializeField] private SlideShowBase _slideShow;
    [SerializeField] private AudioSource _audioSource;
    private SceneLoader _sceneLoader;

    private void Awake()
    {
        _slideShow.OnSlideshowFinished += () => _sceneLoader.ActivateNextScene();
        var foundSceneLoaders = FindObjectsOfType<SceneLoader>();
        // _sceneLoader = foundSceneLoaders[0];
    }

    private void Start()
    {
        // play audio
        _audioSource.Play();
        // start slideshow
        _slideShow.StartSlideshow();
        
        // start asynchronously loading the next scene
        // _sceneLoader.LoadNextScene();
    }
}
