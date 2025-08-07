using System;
using StateMachine;
using UnityEngine;

public class Character3Controller : PlayerController
{
    public Action onCollisionEvent;
    protected override void Awake()
    {
        base.Awake();
        
        StateMachine = new Character3StateMachine(this);
    }

    private void OnCollisionEnter(Collision other)
    {
        //TODO : Made HitBox
        onCollisionEvent.Invoke();
        
    }
}
