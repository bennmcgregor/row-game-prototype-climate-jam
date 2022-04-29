using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private SceneLoader _sceneLoader;
    [SerializeField] private string _creditsSceneName;

    private void Awake()
    {
        _sceneLoader.LoadNextScene();
    }

    public void PlayGame()
    {
        _sceneLoader.ActivateNextScene();
    }

    public void OpenCredits()
    {
        SceneManager.LoadScene(_creditsSceneName);
    }
}
