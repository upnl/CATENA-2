using System;
using UnityEngine;

[Serializable]
public class EnemyDetectBoxContext
{
    public Vector3 boxSize;
    public Vector3 offset;
}

[CreateAssetMenu(fileName = "EnemyBoxDetecorSO", menuName = "PlayerData/EnemyBoxDetecorSO")]
public class EnemyBoxDetecorSO : ScriptableObject
{
    public EnemyDetectBoxContext[] contexts;
}
