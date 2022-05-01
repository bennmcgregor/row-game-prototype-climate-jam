using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private SceneLoader _sceneLoader;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private GameObject _credits;
    [SerializeField] private GameObject _menu;

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
        _credits.SetActive(true);
        _menu.SetActive(false);
    }

    public void CloseCredits()
    {
        _menu.SetActive(true);
        _credits.SetActive(false);
    }
}
