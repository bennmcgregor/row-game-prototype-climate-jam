using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class SceneLoader : MonoBehaviour
{
    public Action OnSceneExitStarted;

    [SerializeField] public string[] _sceneOrder;
    [SerializeField] private Animator _transition;

    private int _nextSceneIdx = 1;
    private bool _shouldActivateNextScene = false;

    private bool _isCRRunning = false;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public void LoadNextScene()
    {
        if (!_isCRRunning)
        {
            _isCRRunning = true;
            StartCoroutine(LoadScene());
        }
    }

    public void LoadMenuScene()
    {
        StartCoroutine(MenuLoadScene());
    }

    // always call this before LoadNextScene()
    public void AddToNextSceneIndex(int summand)
    {
        _nextSceneIdx += summand;
        if (_nextSceneIdx >= _sceneOrder.Length)
        {
            _nextSceneIdx = _nextSceneIdx % _sceneOrder.Length;
        }
    }

    public void ActivateNextScene()
    {
        _shouldActivateNextScene = true;
    }

    private IEnumerator LoadScene()
    {
        yield return null;
        UnityEngine.Debug.Log($"_nextSceneIndex: {_nextSceneIdx}");

        // Begin to load the Scene you specify
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(_sceneOrder[_nextSceneIdx]);
        // Don't let the Scene activate until you allow it to
        asyncOperation.allowSceneActivation = false;
        // When the load is still in progress, output the Text and progress bar
        while (!asyncOperation.isDone)
        {
            // Check if the load has finished
            if (asyncOperation.progress >= 0.9f)
            {
                if (_shouldActivateNextScene)
                {
                    // tell listeners to start their exit procedures (fade audio out)
                    OnSceneExitStarted?.Invoke();
                    yield return new WaitForSeconds(1);

                    // start crossfade
                    _transition.SetTrigger("Start");
                    yield return new WaitForSeconds(1);

                    // Activate the Scene
                    asyncOperation.allowSceneActivation = true;
                    _shouldActivateNextScene = false;
                    AddToNextSceneIndex(1);
                }
            }
            _isCRRunning = false;

            yield return null;
        }
    }

    private IEnumerator MenuLoadScene()
    {
        yield return null;

        // Begin to load the Scene you specify
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(_sceneOrder[0]);
        _nextSceneIdx = 1;
        // Don't let the Scene activate until you allow it to
        asyncOperation.allowSceneActivation = false;
        // When the load is still in progress, output the Text and progress bar
        while (!asyncOperation.isDone)
        {
            // Check if the load has finished
            if (asyncOperation.progress >= 0.9f)
            {
                if (_shouldActivateNextScene)
                {
                    // tell listeners to start their exit procedures (fade audio out)
                    OnSceneExitStarted?.Invoke();
                    yield return new WaitForSeconds(1);

                    // start crossfade
                    _transition.SetTrigger("Start");
                    yield return new WaitForSeconds(1);

                    // Activate the Scene
                    asyncOperation.allowSceneActivation = true;
                    _shouldActivateNextScene = false;
                    AddToNextSceneIndex(1);
                }
            }

            yield return null;
        }
    }
}
