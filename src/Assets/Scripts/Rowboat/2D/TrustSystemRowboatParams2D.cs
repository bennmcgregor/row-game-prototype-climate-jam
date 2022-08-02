using UnityEngine;

[CreateAssetMenu(fileName = "New TrustSystemRowboatParams2D", menuName = "Trust System Rowboat Params 2D")]
public class TrustSystemRowboatParams2D : ScriptableObject
{
    public float InPerfectTimeThreshhold = 0.05f;
    public float InTimeThreshhold = 0.2f;
    public float NoBowEffectTimingMultiplier = 1f;
    public float PerfectTimingMultiplier = 2f;
    public float InTimeTimingMultiplier = 1.5f;

    public float NPCDelayTime = 0.2f;
}