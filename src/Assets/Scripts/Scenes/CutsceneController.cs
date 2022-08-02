using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneController : MonoBehaviour
{
    [SerializeField] private SlideShowBase _slideShow;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private int _numberOfScenesToSkip = 0;
    [SerializeField] private bool _loadMainMenuNext = false;
    private SceneLoader _sceneLoader;

    private void Awake()
    {
        _slideShow.OnSlideshowFinished += ActivateNextScene;
    }

    public void ActivateNextScene()
    {
        _sceneLoader.ActivateNextScene();
    }

    private void Start()
    {
        // fade in audio
        StartCoroutine(AudioHelper.FadeIn(_audioSource, 5));
        // start slideshow
        _slideShow.StartSlideshow();

        _sceneLoader = FindObjectOfType<SceneLoader>();
        // _sceneLoader.OnSceneExitStarted += StartFadeOut;
        
        // start asynchronously loading the next scene
        if (_loadMainMenuNext)
        {
            _sceneLoader.LoadNextScene();
        }
        else if (_sceneLoader != null)
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

    // private void OnDestroy()
    // {
    //     _sceneLoader.OnSceneExitStarted -= StartFadeOut;
    // }

    // private void StartFadeOut()
    // {
    //     StartCoroutine(AudioHelper.FadeOut(_audioSource, 2));
    // }
    
}
