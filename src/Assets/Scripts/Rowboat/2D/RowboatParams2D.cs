using UnityEngine;

[CreateAssetMenu(fileName = "New RowboatParams2D", menuName = "Rowboat Params 2D")]
public class RowboatParams2D : ScriptableObject
{
    public float MaxPullForce = 450f; // 700 N
    public float MaxPushForce = 200f;
    public float OarDragForce = 50f;

    public float InPerfectTimeThreshhold = 0.05f;
    public float InTimeThreshhold = 0.2f;
    public float NoBowEffectTimingMultiplier = 1f;
    public float PerfectTimingMultiplier = 2f;
    public float InTimeTimingMultiplier = 1.5f;

    public float RudderMotionThreshhold = 1f; // the minimum boat velocity required for it to move vertically
    public float RudderTimerThreshhold = 0.3f; // the minimum duration into the stroke before the boat moves
    public float RudderAccelerationThreshhold = 1f; // the minimum acceleration of the player before the boat moves
}