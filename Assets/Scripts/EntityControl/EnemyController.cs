using System;
using PlayerControl;
using StateMachine;
using Unity.Cinemachine;
using UnityEngine;

public class EnemyController : EntityController
{
    public float[] normalAttackDashes;
    
    protected override void Awake()
    {
        base.Awake();

        StateMachine = new EntityStateMachine(this);
    }
    

    protected override void Update()
    {
        base.Update();
    }

    
}
