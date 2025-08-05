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
        Debug.Log("EntityDodgeState: Enter");
        
        _timerElapsed = 0f;
    }

    public override void Update()
    {
        Debug.Log("EntityDodgeState: Update");

        _timerElapsed += Time.deltaTime;

        if (_timerElapsed >= _dodgeTimer)
        {
            stateMachine.ChangeState(stateMachine.EntityIdleState);
        }
    }

    public override void PhysicsUpdate()
    {
        Debug.Log("EntityDodgeState: PhysicsUpdate");
    }

    public override void Exit()
    {
        Debug.Log("EntityDodgeState: Exit");
    }
}
