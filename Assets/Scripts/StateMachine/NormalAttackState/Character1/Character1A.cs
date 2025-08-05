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
        
        CanAttack = false;
        
        AttackTimer = 1f;
        MotionTimer = 2f;
        
        EntityController.AddActionTrigger(ActionTrigger.Hit, OnHit);
        EntityController.AddActionTrigger(ActionTrigger.AirHit, OnAirHit);
        
        EntityController.AddActionTrigger(ActionTrigger.LightAttack, OnLightAttack);
        EntityController.AddActionTrigger(ActionTrigger.HeavyAttack, OnHeavyAttack);
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
        
        EntityController.RemoveActionTrigger(ActionTrigger.Hit, OnHit);
        EntityController.RemoveActionTrigger(ActionTrigger.AirHit, OnAirHit);
        
        EntityController.RemoveActionTrigger(ActionTrigger.LightAttack, OnLightAttack);
        EntityController.RemoveActionTrigger(ActionTrigger.HeavyAttack, OnHeavyAttack);
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
