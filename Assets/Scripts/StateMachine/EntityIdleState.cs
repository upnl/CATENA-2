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
        Debug.Log("EntityIdleState: Enter");
        
        stateMachine.EntityController.AddActionTrigger(ActionTrigger.MovementAction, OnMovement);
    }

    public override void Update()
    {
        Debug.Log("EntityIdleState: Update");
    }

    public override void PhysicsUpdate()
    {
        Debug.Log("EntityIdleState: PhysicsUpdate");
    }

    public override void Exit()
    {
        Debug.Log("EntityIdleState: Exit");
        
        stateMachine.EntityController.RemoveActionTrigger(ActionTrigger.MovementAction, OnMovement);
    }

    private void OnMovement(ActionTriggerContext context)
    {
        if (context.InputActionPhase == InputActionPhase.Started)
        {
            stateMachine.ChangeState(stateMachine.EntityMoveState);
        }
    }
}
