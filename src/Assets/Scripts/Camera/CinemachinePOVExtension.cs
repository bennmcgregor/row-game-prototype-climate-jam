using UnityEngine;
using Cinemachine;

public class CinemachinePOVExtension : CinemachineExtension
{
    public PlayerInputListener PlayerInputListener;
    public float ClampAngle = 80f;
    public float HorizontalSpeed = 100f;
    public float VerticalSpeed = 100f;
    private Vector3 _startingRotation;

    protected override void PostPipelineStageCallback(CinemachineVirtualCameraBase vcam, CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
    {
        if (vcam.Follow)
        {
            if (stage == CinemachineCore.Stage.Aim)
            {
                if (_startingRotation == null)
                {
                    _startingRotation = transform.localRotation.eulerAngles;
                }
                Vector2 deltaInput = PlayerInputListener.CameraMotion;
                _startingRotation.x += deltaInput.x * HorizontalSpeed * Time.deltaTime;
                _startingRotation.y += deltaInput.y * VerticalSpeed * Time.deltaTime;
                _startingRotation.y = Mathf.Clamp(_startingRotation.y, -ClampAngle, ClampAngle);
                state.RawOrientation = Quaternion.Euler(-_startingRotation.y, _startingRotation.x, 0f);
            }
        }
    }
}
