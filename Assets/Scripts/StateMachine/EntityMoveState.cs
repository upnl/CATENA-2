using PlayerControl;
using UnityEngine;
using StateMachine;
using UnityEngine.InputSystem;

public class EntityMoveState : EntityState
{
    public EntityMoveState(EntityStateMachine entityStateMachine) : base(entityStateMachine)
    {
    }
    
    public override void Enter()
    {
        base.Enter();
        
        stateMachine.EntityController.AddActionTrigger(ActionTrigger.MovementAction, OnMovement);
        stateMachine.EntityController.AddActionTrigger(ActionTrigger.Hit, OnHit);
        stateMachine.EntityController.AddActionTrigger(ActionTrigger.AirHit, OnAirHit);
    }

    public override void Update()
    {
        base.Update();
        
        var entityTransform = stateMachine.EntityController.transform;
        entityTransform.LookAt(entityTransform.position + stateMachine.EntityController.LookDirection);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void Exit()
    {
        base.Exit();
        
        stateMachine.EntityController.RemoveActionTrigger(ActionTrigger.MovementAction, OnMovement);
        stateMachine.EntityController.RemoveActionTrigger(ActionTrigger.Hit, OnHit);
        stateMachine.EntityController.RemoveActionTrigger(ActionTrigger.AirHit, OnAirHit);
    }

    private void OnMovement(ActionTriggerContext context)
    {
        if (context.InputActionPhase == InputActionPhase.Canceled)
        {
            stateMachine.ChangeState(stateMachine.EntityIdleState);
        }
    }
}
