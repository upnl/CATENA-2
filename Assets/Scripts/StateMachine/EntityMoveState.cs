using PlayerControl;
using UnityEngine;
using StateMachine;
using UnityEngine.InputSystem;

public class EntityMoveState : EntityState
{
    private Rigidbody _rigidbody;
    public EntityMoveState(EntityStateMachine entityStateMachine) : base(entityStateMachine)
    {
        _rigidbody = entityStateMachine.EntityController.GetComponent<Rigidbody>();
    }
    
    public override void Enter()
    {
        base.Enter();
        
        PlayAnimation("Move");
        
        stateMachine.EntityController.AddActionTrigger(ActionTriggerType.MovementAction, OnMovement);
        
        stateMachine.EntityController.AddActionTrigger(ActionTriggerType.Dodge, OnDodge);
        
        stateMachine.EntityController.AddActionTrigger(ActionTriggerType.Hit, OnHit);
        stateMachine.EntityController.AddActionTrigger(ActionTriggerType.AirHit, OnAirHit);
    }

    public override void Update()
    {
        base.Update();
        
        var entityTransform = stateMachine.EntityController.transform;
        entityTransform.LookAt(entityTransform.position + 
                               Vector3.Lerp(entityTransform.forward,
                                   stateMachine.EntityController.LookDirection, 
                                   stateMachine.EntityController.bodyRotateSpeed * Time.deltaTime));
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        
        _rigidbody.MovePosition(stateMachine.EntityController.transform.position + 
                                stateMachine.EntityController.LookDirection * (stateMachine.EntityController.movementSpeed * Time.fixedDeltaTime));
    }

    public override void Exit()
    {
        base.Exit();
        
        stateMachine.EntityController.RemoveActionTrigger(ActionTriggerType.MovementAction, OnMovement);
        
        stateMachine.EntityController.RemoveActionTrigger(ActionTriggerType.Dodge, OnDodge);
        
        stateMachine.EntityController.RemoveActionTrigger(ActionTriggerType.Hit, OnHit);
        stateMachine.EntityController.RemoveActionTrigger(ActionTriggerType.AirHit, OnAirHit);
    }

    private void OnMovement(ActionTriggerContext context)
    {
        if (context.InputActionPhase == InputActionPhase.Canceled)
        {
            stateMachine.ChangeState(stateMachine.EntityIdleState);
        }
    }
    
    private void OnDodge(ActionTriggerContext context)
    {
        if (context.InputActionPhase == InputActionPhase.Performed)
        {
            stateMachine.ChangeState(stateMachine.EntityDodgeState);
        }
    }
}
