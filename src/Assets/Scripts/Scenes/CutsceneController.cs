using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneController : MonoBehaviour
{
    [SerializeField] private SlideShowBase _slideShow;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private int _numberOfScenesToSkip = 0;
    private SceneLoader _sceneLoader;

    private void Awake()
    {
        _slideShow.OnSlideshowFinished += () => _sceneLoader.ActivateNextScene();
        
    }

    private void Start()
    {
        // play audio
        _audioSource.Play();
        // start slideshow
        _slideShow.StartSlideshow();

        _sceneLoader = FindObjectOfType<SceneLoader>();
        
        // start asynchronously loading the next scene
        if (_sceneLoader != null)
        {
            if (_numberOfScenesToSkip > 0)
            {
                _sceneLoader.AddToNextSceneIndex(_numberOfScenesToSkip);
            }
            _sceneLoader.LoadNextScene();
        }
        else 
        {
            Debug.Log("SceneLoader not found");
        }
    }
}
