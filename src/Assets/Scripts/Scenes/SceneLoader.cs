using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] public string[] _sceneOrder;

    private int _nextSceneIdx = 1;
    private bool _shouldActivateNextScene = false;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public void LoadNextScene()
    {
        if (_nextSceneIdx < _sceneOrder.Length)
        {
            StartCoroutine(LoadScene());
        }
    }

    public void ActivateNextScene()
    {
        _shouldActivateNextScene = true;
    }

    private IEnumerator LoadScene()
    {
        yield return null;

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
                    // Activate the Scene
                    asyncOperation.allowSceneActivation = true;
                    _shouldActivateNextScene = false;
                    _nextSceneIdx += 1;
                }
            }

            yield return null;
        }
    }
}
