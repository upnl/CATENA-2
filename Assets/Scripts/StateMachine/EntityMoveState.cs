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
        Debug.Log("EntityMoveState: Enter");
        
        stateMachine.EntityController.AddActionTrigger(ActionTrigger.MovementAction, OnMovement);
    }

    public override void Update()
    {
        Debug.Log("EntityMoveState: Update, We Moving toward " + stateMachine.LookDirection);
    }

    public override void PhysicsUpdate()
    {
        Debug.Log("EntityMoveState: PhysicsUpdate");
    }

    public override void Exit()
    {
        Debug.Log("EntityMoveState: Exit");
        
        stateMachine.EntityController.AddActionTrigger(ActionTrigger.MovementAction, OnMovement);
    }

    private void OnMovement(ActionTriggerContext context)
    {
        if (context.InputActionPhase == InputActionPhase.Canceled)
        {
            stateMachine.ChangeState(stateMachine.EntityIdleState);
        }
    }
}
