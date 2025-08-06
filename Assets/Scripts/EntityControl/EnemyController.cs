using System;
using PlayerControl;
using StateMachine;
using Unity.Cinemachine;
using UnityEngine;

public class EnemyController : EntityController
{
    public PlayerStateMachine PlayerStateMachine => StateMachine as PlayerStateMachine;
    
    public float[] normalAttackDashes;
    
    protected override void Awake()
    {
        base.Awake();

        StateMachine = new PlayerStateMachine(this);
    }
    

    protected override void Update()
    {
        base.Update();
    }

    
}
