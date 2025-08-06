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
             
        stateMachine.EntityController.AddActionTrigger(ActionTriggerType.MovementAction, OnMovement);
        
        stateMachine.EntityController.AddActionTrigger(ActionTriggerType.Dodge, OnDodge);
        
        stateMachine.EntityController.AddActionTrigger(ActionTriggerType.Hit, OnHit);
        stateMachine.EntityController.AddActionTrigger(ActionTriggerType.AirHit, OnAirHit);
        
        stateMachine.EntityController.AddActionTrigger(ActionTriggerType.LightAttack, OnLightAttack);
        
        stateMachine.EntityController.AddActionTrigger(ActionTriggerType.Skill, OnSkill);
    }

    public override void Update()
    {
        base.Update();

        if (stateMachine.EntityController.movementInput != Vector2.zero)
        {
            stateMachine.ChangeState(stateMachine.EntityMoveState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void Exit()
    {
        base.Exit();
        
        stateMachine.EntityController.RemoveActionTrigger(ActionTriggerType.MovementAction, OnMovement);
        
        stateMachine.EntityController.RemoveActionTrigger(ActionTriggerType.Dodge, OnDodge);
        
        stateMachine.EntityController.RemoveActionTrigger(ActionTriggerType.Hit, OnHit);
        stateMachine.EntityController.RemoveActionTrigger(ActionTriggerType.AirHit, OnAirHit);
        
        stateMachine.EntityController.RemoveActionTrigger(ActionTriggerType.LightAttack, OnLightAttack);
        
        stateMachine.EntityController.RemoveActionTrigger(ActionTriggerType.Skill, OnSkill);
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
