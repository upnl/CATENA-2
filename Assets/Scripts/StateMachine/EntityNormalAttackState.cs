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
        
        stateMachine.EntityController.AddActionTrigger(ActionTriggerType.Dodge, OnDodge);
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
        
        stateMachine.EntityController.RemoveActionTrigger(ActionTriggerType.Dodge, OnDodge);
    }
    
    private void OnDodge(ActionTriggerContext context)
    {
        if (context.InputActionPhase == InputActionPhase.Performed)
        {
            stateMachine.ChangeState(stateMachine.EntityDodgeState);
        }
    }
}
