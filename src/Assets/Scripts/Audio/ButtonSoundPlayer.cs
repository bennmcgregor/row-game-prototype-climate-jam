using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSoundPlayer : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSourceButton;
    
    public void OnClick()
    {
        _audioSourceButton.Play();
    }
}
