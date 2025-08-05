using PlayerControl;
using UnityEngine;
using StateMachine;
using UnityEngine.InputSystem;

public class PlayerIdleState : EntityIdleState
{
    public PlayerIdleState(EntityStateMachine entityStateMachine) : base(entityStateMachine)
    {
    }
    
    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        base.Update();
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
