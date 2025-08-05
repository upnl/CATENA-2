using PlayerControl;
using UnityEngine;
using StateMachine;
using UnityEngine.InputSystem;

public class EntityDodgeState : EntityState
{
    private float _dodgeTimer = 0.5f;
    private float _timerElapsed = 0f;
    public EntityDodgeState(EntityStateMachine entityStateMachine) : base(entityStateMachine)
    {
    }
    
    public override void Enter()
    {
        base.Enter();
        
        _timerElapsed = 0f;
    }

    public override void Update()
    {
        base.Update();
        
        _timerElapsed += Time.deltaTime;

        if (_timerElapsed >= _dodgeTimer)
        {
            stateMachine.ChangeState(stateMachine.EntityIdleState);
        }
    }

    public override void PhysicsUpdate()
    { 
        base.PhysicsUpdate();
    }

    public override void Exit()
    {
        base.Exit();
    }
}
