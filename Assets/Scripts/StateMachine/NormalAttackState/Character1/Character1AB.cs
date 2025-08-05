using PlayerControl;
using StateMachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class Character1AB : NormalAttackState
{
    public Character1AB (
        EntityController entityController, 
        NormalAttackStateMachine normalAttackStateMachine, 
        EntityStateMachine parentStateMachine) 
        : base(entityController, normalAttackStateMachine, parentStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        
        ParentStateMachine.PlayAnimation("AB");
        
        MotionTimer = 1f;
        
        EntityController.AddActionTrigger(ActionTriggerType.Hit, OnHit);
        EntityController.AddActionTrigger(ActionTriggerType.AirHit, OnAirHit);
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
        
        EntityController.RemoveActionTrigger(ActionTriggerType.Hit, OnHit);
        EntityController.RemoveActionTrigger(ActionTriggerType.AirHit, OnAirHit);
    }
}
