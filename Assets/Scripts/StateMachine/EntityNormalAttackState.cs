using PlayerControl;
using UnityEngine;
using StateMachine;
using UnityEngine.InputSystem;

public class EntityNormalAttackState : EntityState
{
    protected NormalAttackStateMachine NormalAttackStateMachine;
    
    public EntityNormalAttackState(EntityStateMachine entityStateMachine) : base(entityStateMachine)
    {
        NormalAttackStateMachine = new NormalAttackStateMachine(stateMachine.EntityController, stateMachine);
    }
    
    public override void Enter()
    {
        base.Enter();
        
        NormalAttackStateMachine.EnterNormalAttackState();
    }

    public override void Update()
    {
        base.Update();
        
        NormalAttackStateMachine.Update();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        
        NormalAttackStateMachine.PhysicsUpdate();
    }

    public override void Exit()
    {
        base.Exit();
        
        NormalAttackStateMachine.Exit();
    }
}
