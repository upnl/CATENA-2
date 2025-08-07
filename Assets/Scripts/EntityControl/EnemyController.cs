using System;
using PlayerControl;
using StateMachine;
using Unity.Cinemachine;
using UnityEngine;

public class EnemyController : EntityController
{
    public Transform playerTransform;
    public float[] normalAttackDashes;
    
    public float detectDistance;
    
    public float mp;
    
    protected override void Awake()
    {
        base.Awake();

        StateMachine = new EntityStateMachine(this);

        mp = 0;
    }


    protected override void Update()
    {
        base.Update();
    }
}
