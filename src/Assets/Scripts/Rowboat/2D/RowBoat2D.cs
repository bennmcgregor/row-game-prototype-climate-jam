using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RowBoat2D : MonoBehaviour
{
    // apply force and drag to the boat depending on the input and state of the oars
    // move the camera to follow the boat at all times
    // control the entry/exit of the rower in the boat

    //visible Properties
    [SerializeField] private RowboatParams2D _rowboatParams2D;
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private NPCController _npcController;
    [SerializeField] private Rigidbody2D _rigidbody2D;

    private void FixedUpdate()
    {
        float boatForceMultiplierStroke = 0;
        float boatForceMultiplierBow = 0;
        if (_playerController.CurrentState == RowingState.DRIVE)
        {
            if (_playerController.Slider.ShouldApplyDrag) // when the oars are stuck in the water at the finish
            {
                boatForceMultiplierStroke = -1 * _rowboatParams2D.OarDragForce;
            }
            else
            {
                boatForceMultiplierStroke = _playerController.CurrentForce * _rowboatParams2D.MaxPullForce;
            }
        }
        if (_npcController.CurrentState == RowingState.DRIVE)
        {
            if (_npcController.Slider.ShouldApplyDrag)
            {
                boatForceMultiplierBow = -1 * _rowboatParams2D.OarDragForce;
            }
            else
            {
                // manually calculate the acceleration
                // UnityEngine.Debug.Log($"current: {_npcController.CurrentSpeed}, prev: {_prevBowSpeed}");
                // float accel = _npcController.CurrentSpeed - _prevBowSpeed;
                // _prevBowSpeed = _npcController.CurrentSpeed;
                // UnityEngine.Debug.Log($"accel: {_npcController.CurrentForce}");
                // boatForceMultiplierBow = _npcController.CurrentForce * _rowboatParams2D.MaxPullForce;
            }
        }

        // UnityEngine.Debug.Log($"stroke mult: {boatForceMultiplierStroke}, bow mult: {boatForceMultiplierBow}");

        float boatForceMultiplier = boatForceMultiplierStroke + boatForceMultiplierBow;
        _rigidbody2D.AddForce(Vector2.right * boatForceMultiplier);
    }
}