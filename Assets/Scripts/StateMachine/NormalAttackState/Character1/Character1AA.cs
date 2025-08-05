using PlayerControl;
using StateMachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class Character1AA : NormalAttackState
{
    // 실제로는 Animation 의 현재 상황에 따라서 공격 가능 판단해야함
    private float _attackTimer;
    private float _motionTimer;
    public Character1AA(
        EntityController entityController, 
        NormalAttackStateMachine normalAttackStateMachine, 
        EntityStateMachine parentStateMachine) 
        : base(entityController, normalAttackStateMachine, parentStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        
        ParentStateMachine.PlayAnimation("AA");
        
        CanAttack = false;
            
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

    private void OnLightAttack(ActionTriggerContext ctx)
    {
        if (ctx.InputActionPhase == InputActionPhase.Started)
        {
            
        }
    }
}
