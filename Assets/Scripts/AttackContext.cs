using System;
using UnityEngine;
using UnityEngine.Serialization;


[Serializable]
public struct AttackContext
{
    public Vector3 knockBack;
    public bool isIgnoringDodge;
    
    public float damage;
    public float stunTime;

    public Vector3 boxOffset;
    public Vector3 boxSize;

    public float[] floatVariables;

    public LayerMask groundMask;

    public GameObject hitEffect;
    
    public float mp;
}
