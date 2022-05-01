using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class RainPlayer : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSourceRain;
    [SerializeField] private AudioSource _music;

    [YarnCommand("play_rain")]
    public void PlayRain()
    {
        UnityEngine.Debug.Log("play rain");
        AudioHelper.FadeIn(_audioSourceRain, 5);
        AudioHelper.FadeOut(_music, 2);
    }
}
