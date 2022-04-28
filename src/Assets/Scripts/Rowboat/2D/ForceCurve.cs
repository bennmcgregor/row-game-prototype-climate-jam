using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using System;

public class ForceCurve
{
    float _tFinal;
    float _tMax;
    float _fMax;
    float _v0;
    float _displacement;
    float _a;


    public ForceCurve(float displacement, float v0, float fMax, float tFinal)
    {
        _tMax = tFinal / 2.0f;
        _displacement = displacement;
        _v0 = v0;
        _fMax = fMax;
        _tFinal = tFinal;
        _a = CalculateA();
    }

    private float CalculateA()
    {
        float mult1 = 12.0f / (float)(Math.Pow((_tFinal-_tMax), 4) + 4*(float)Math.Pow(_tMax, 3)*_tFinal);
        float mult2 = 0.5f * _fMax * (float)Math.Pow(_tFinal, 2) + _v0 * _tFinal - _displacement;
        return mult1 * mult2;
    }

    public bool IsAPastLimit(float limit) 
    {
        return _a > limit;
    }

    public float GetDisplacementAtTime(float t)
    {
        if (t > _tFinal)
        {
            return GetDisplacementAtTime(_tFinal) + t*_v0;
        }

        float sum1 = 0.5f * _fMax * (float)Math.Pow(t, 2);
        float sum2 = (_a/12.0f) * (float)Math.Pow((t-_tMax), 4);
        float sum3 = t*(_v0-(_a/3.0f) * (float)Math.Pow(_tMax, 3));
        return sum1 - sum2 + sum3;
    }

    public float GetVelocityAtTime(float t)
    {
        if (t > _tFinal)
        {
            return _v0;
        }

        float sum1 = t*(_fMax - _a*_tMax);
        float sum2 = t*t*_a*_tMax;
        float sum3 = t*t*t*(_a / 3.0f);
        return sum1 + sum2 - sum3 + _v0;
    }

    public float GetForceAtTime(float t)
    {
        if (t > _tFinal)
        {
            return 0;
        }

        return _fMax - _a*(t-_tMax)*(t-_tMax);
    }
}
