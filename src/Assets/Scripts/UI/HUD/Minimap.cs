using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minimap : MonoBehaviour
{
    [SerializeField] private Transform _playerTransform;

    private void LateUpdate()
    {
        Vector3 newPosition = _playerTransform.position;
        newPosition.y = transform.position.y;
        transform.position = newPosition;
    }
}
