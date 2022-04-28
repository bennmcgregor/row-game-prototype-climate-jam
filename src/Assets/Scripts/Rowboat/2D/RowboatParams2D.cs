using UnityEngine;

[CreateAssetMenu(fileName = "New RowboatParams2D", menuName = "Rowboat Params 2D")]
public class RowboatParams2D : ScriptableObject
{
    public float MaxPullForce = 450f; // 700 N
    public float MaxPushForce = 200f;
    public float TranslationalDragFactor = 0.5f;
    public float OarDragForce = 50f;
}