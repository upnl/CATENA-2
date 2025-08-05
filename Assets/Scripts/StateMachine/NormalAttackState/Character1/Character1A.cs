using PlayerControl;
using StateMachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class Character1A : NormalAttackState
{
    private Character1AA _aaState;
    private Character1AB _abState;
    
    public Character1A(
        EntityController entityController, 
        NormalAttackStateMachine normalAttackStateMachine,
        EntityStateMachine parentStateMachine)
        : base(entityController, normalAttackStateMachine, parentStateMachine)
    {
        _aaState = new Character1AA(entityController, normalAttackStateMachine, parentStateMachine);
        _abState = new Character1AB(entityController, normalAttackStateMachine, parentStateMachine);
    }

    public override void Enter()
    {
        base.Enter();
        
        ParentStateMachine.PlayAnimation("A");
        
        CanAttack = false;
        
        AttackTimer = 1f;
        MotionTimer = 2f;
        
        EntityController.AddActionTrigger(ActionTriggerType.Hit, OnHit);
        EntityController.AddActionTrigger(ActionTriggerType.AirHit, OnAirHit);
        
        EntityController.AddActionTrigger(ActionTriggerType.LightAttack, OnLightAttack);
        EntityController.AddActionTrigger(ActionTriggerType.HeavyAttack, OnHeavyAttack);
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
        
        EntityController.RemoveActionTrigger(ActionTriggerType.LightAttack, OnLightAttack);
        EntityController.RemoveActionTrigger(ActionTriggerType.HeavyAttack, OnHeavyAttack);
    }

    private void OnLightAttack(ActionTriggerContext ctx)
    {
        if (!CanAttack) return;
        
        if (ctx.InputActionPhase == InputActionPhase.Started)
        {
            NormalAttackStateMachine.ChangeState(_aaState);
        }
    }

    private void OnHeavyAttack(ActionTriggerContext ctx)
    {
        if (!CanAttack) return;
        
        if (ctx.InputActionPhase == InputActionPhase.Started)
        {
            NormalAttackStateMachine.ChangeState(_abState);
        }
    }
}
