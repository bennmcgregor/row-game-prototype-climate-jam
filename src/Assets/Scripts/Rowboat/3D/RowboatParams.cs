using UnityEngine;

[CreateAssetMenu(fileName = "New RowboatParams", menuName = "Rowboat Params")]
public class RowboatParams : ScriptableObject
{
    public float RiggerLength = 0.8f;
    public float InboardLength = 0.88f; // spread / 2 + 8cm
    public float OutboardLength = 2f; // 200cm
    public float MaxPullForce = 450f; // 700 N
    public float MaxPushForce = 200f;
    public float TranslationalXDragFactor = 12f;
    public float TranslationalYDragFactor = 0.1f;
    public float TranslationalZDragFactor = 0.5f;
    public float AngularDragFactor = 15f;
    public float OarTranslationalZDragFactor = 3f;
    public float OarAngularDragFactor = 0.5f;
    public float RushingForceMultiplier = 0.3f;
}