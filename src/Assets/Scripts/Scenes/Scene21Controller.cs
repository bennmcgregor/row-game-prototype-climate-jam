using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;


public class Scene21Controller : MonoBehaviour
{
    [SerializeField] private GameplaySceneController _gameplaySceneController;
    [SerializeField] private TrustSystemParams _trustSystemParams;
    
    private TrustMeter _trustMeter;

    private void Start()
    {
        _trustMeter = FindObjectOfType<TrustMeter>();
    }

    [YarnCommand("choose_scene_22")]
    public void ChooseScene22()
    {
        if (_trustMeter == null)
        {
            _trustMeter = FindObjectOfType<TrustMeter>();
        }

        if (_trustMeter.TrustValue > _trustSystemParams.WinThreshhold)
        {
            _gameplaySceneController.LoadBranchingScene(1);
            _gameplaySceneController.GoToNextScene();
        }
        else
        {
            _gameplaySceneController.LoadBranchingScene(0);
            _gameplaySceneController.GoToNextScene();
        }
    }
}
