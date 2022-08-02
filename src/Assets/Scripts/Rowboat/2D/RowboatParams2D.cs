using UnityEngine;

[CreateAssetMenu(fileName = "New RowboatParams2D", menuName = "Rowboat Params 2D")]
public class RowboatParams2D : ScriptableObject
{
    public float MaxPullForce = 450f; // 700 N
    public float MaxPushForce = 200f;
    public float OarDragScalingFactor = 0.25f;
    public float StoppingForce = 700f;

    public float RudderMotionThreshhold = 1f; // the minimum boat velocity required for it to move vertically
    public float RudderTimerThreshhold = 0.3f; // the minimum duration into the stroke before the boat moves
    public float RudderAccelerationThreshhold = 1f; // the minimum acceleration of the player before the boat moves

    public float StoppingSpeedThreshhold = 0.5f;

    public float VerticalMoveSpeed = 5f;
    public float MaxVerticalMoveTime = 0.5f;

    public float DisplayedSpeedMultiplier = 8f; // multiplies the rigidbody velocity to give the speed displayed on the HUD
    public float DisplayedAccelerationMultiplier = 8f;

    public float TotalInitialHealth = 100f;
}