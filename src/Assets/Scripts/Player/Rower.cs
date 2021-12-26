using System;
using System.Collections;
using UnityEngine;

public class Rower : MonoBehaviour
{
    public BoxCollider BoxCollider;
    public CharacterController CharacterController;
    public Inventory Inventory;

    public void GetIntoBoat(Transform ParentTransform)
    {
        CharacterController.enabled = false;
        BoxCollider.enabled = false;
        transform.SetParent(ParentTransform, false);
        transform.localPosition = Vector3.zero;
        CharacterController.enabled = true;

        // todo: update rotation (also need to update camera rotation)
    }
}
