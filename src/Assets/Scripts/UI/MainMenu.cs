using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private SceneLoader _sceneLoader;
    [SerializeField] private AudioSource _audioSourceBgMusic;
    [SerializeField] private AudioSource _audioSourceButton;
    [SerializeField] private GameObject _credits;
    [SerializeField] private GameObject _menu;

    private void Awake()
    {
        _sceneLoader.LoadNextScene();
        StartCoroutine(AudioHelper.FadeIn(_audioSourceBgMusic, 5));
        _sceneLoader.OnSceneExitStarted += StartFadeOut;
    }

    private void OnDestroy()
    {
        _sceneLoader.OnSceneExitStarted -= StartFadeOut;
    }

    private void StartFadeOut()
    {
        StartCoroutine(AudioHelper.FadeOut(_audioSourceBgMusic, 2));
    }

    public void PlayGame()
    {
        _sceneLoader.ActivateNextScene();
        _audioSourceButton.Play();
    }

    public void OpenCredits()
    {
        _credits.SetActive(true);
        _menu.SetActive(false);
        _audioSourceButton.Play();
    }

    public void CloseCredits()
    {
        _menu.SetActive(true);
        _credits.SetActive(false);
        _audioSourceButton.Play();
    }
}
