using PlayerControl;
using UnityEngine;
using StateMachine;
using UnityEngine.InputSystem;

public class EntityIdleState : EntityState
{
    public EntityIdleState(EntityStateMachine entityStateMachine) : base(entityStateMachine)
    {
    }
    
    public override void Enter()
    {
        base.Enter();

        PlayAnimation("Idle");
             
        stateMachine.EntityController.AddActionTrigger(ActionTrigger.MovementAction, OnMovement);
        
        stateMachine.EntityController.AddActionTrigger(ActionTrigger.Dodge, OnDodge);
        
        stateMachine.EntityController.AddActionTrigger(ActionTrigger.Hit, OnHit);
        stateMachine.EntityController.AddActionTrigger(ActionTrigger.AirHit, OnAirHit);
        
        stateMachine.EntityController.AddActionTrigger(ActionTrigger.LightAttack, OnLightAttack);
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
        
        stateMachine.EntityController.RemoveActionTrigger(ActionTrigger.MovementAction, OnMovement);
        
        stateMachine.EntityController.RemoveActionTrigger(ActionTrigger.Dodge, OnDodge);
        
        stateMachine.EntityController.RemoveActionTrigger(ActionTrigger.Hit, OnHit);
        stateMachine.EntityController.RemoveActionTrigger(ActionTrigger.AirHit, OnAirHit);
        
        stateMachine.EntityController.RemoveActionTrigger(ActionTrigger.LightAttack, OnLightAttack);
    }

    private void OnMovement(ActionTriggerContext context)
    {
        if (context.InputActionPhase == InputActionPhase.Performed)
        {
            stateMachine.ChangeState(stateMachine.EntityMoveState);
        }
    }
    
    private void OnDodge(ActionTriggerContext context)
    {
        if (context.InputActionPhase == InputActionPhase.Performed)
        {
            stateMachine.ChangeState(stateMachine.EntityDodgeState);
        }
    }
    
    private void OnLightAttack(ActionTriggerContext context)
    {
        if (context.InputActionPhase == InputActionPhase.Performed)
        {
            stateMachine.ChangeState(stateMachine.EntityNormalAttackState);
        }
    }
}
