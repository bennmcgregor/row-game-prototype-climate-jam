using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputListener : MonoBehaviour
{
    // received input from ... to control the player

    // base class

    public CharacterController Controller;
    public PlayerInput PlayerInput;
    public float PlayerSpeed = 2.0f;
    public float JumpHeight = 1.0f;
    public float GravityValue = -9.81f;
    public GameObject MotionJoystick;
    public InputListener InputListener;

    private Vector3 _playerVelocity;
    private bool _groundedPlayer;

    public Vector2 CameraMotion => PlayerInput.actions["CameraMotion"].ReadValue<Vector2>();

    private void Update()
    {
        _groundedPlayer = Controller.isGrounded;
        if (_groundedPlayer && _playerVelocity.y < 0)
        {
            _playerVelocity.y = 0f;
        }

        Vector3 rotation = new Vector3(0f, CameraMotion.x * 100f * Time.deltaTime, 0f);
        transform.Rotate(rotation);

        Vector2 input = PlayerInput.actions["PlayerMotion"].ReadValue<Vector2>();
        Vector3 move = transform.forward * input.y + transform.right * input.x;
        move.y = 0f;
        Controller.Move(move * Time.deltaTime * PlayerSpeed);

        _playerVelocity.y += GravityValue * Time.deltaTime;
        Controller.Move(_playerVelocity * Time.deltaTime);
    }

    private void OnEnable()
    {
        MotionJoystick.SetActive(true);
    }

    private void OnDisable()
    {
        MotionJoystick.SetActive(false);
    }
}