using UnityEngine;

[CreateAssetMenu(fileName = "New PlayerRowboatParams", menuName = "Player Rowboat Params")]
public class PlayerRowboatParams : ScriptableObject
{
    // we want drive to take about 1s, recovery about 2s.
    // should cover a distance of 100 units in 1s on drive, 100 units in 2s on recovery
    // what is the distance per fixedupdate call?
    // (Time.fixedDeltaTime * 100 / 2)
    // assume Time.fixedDeltaTime = 0.02. Then RecoverySpeed = 1f.
    public float RecoverySpeed = 1f;
    public float DriveRecoveryRatio = 2f;
    public float MaxAcceleration = 5f; // random number, needs adjusting
    public float MaxA = 3f;
    public float SpamTimeThreshold = 0.5f; // minimum duration between spacebar presses

    public float DriveSpeed => RecoverySpeed * DriveRecoveryRatio;
}