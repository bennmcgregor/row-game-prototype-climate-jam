using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DamageSystem : MonoBehaviour
{
    [SerializeField] private RowboatParamsProvider _rowboatParamsProvider;

    private float _currentDamageLevel;

    private void Awake()
    {
        _currentDamageLevel = _rowboatParamsProvider.RowboatParams.TotalInitialHealth;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        UnityEngine.Debug.Log($"impulse: {collision.relativeVelocity}");
        UnityEngine.Debug.Log($"Layer: {collision.transform.gameObject.layer}");
    }
}
