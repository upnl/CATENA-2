using PlayerControl;
using StateMachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class Character2AA : NormalAttackState
{
    // private Character1AAA _aaaState;
    private Character2AAB _aabState;
    
    private Rigidbody _rigidbody;
    private PlayerController _playerController;
    public Character2AA(
        EntityController entityController, 
        NormalAttackStateMachine normalAttackStateMachine, 
        EntityStateMachine parentStateMachine) 
        : base(entityController, normalAttackStateMachine, parentStateMachine)
    {
        // _aaaState = new Character1AAA(entityController, normalAttackStateMachine, parentStateMachine);
        _aabState = new Character2AAB(entityController, normalAttackStateMachine, parentStateMachine);
        
        _rigidbody = EntityController.GetComponent<Rigidbody>();
        _playerController = EntityController as PlayerController;
        
        AttackContext = _playerController.attackContextSO.contexts[1];
    }

    public override void Enter()
    {
        base.Enter();
        
        ParentStateMachine.PlayAnimation("AA");
        
        CanAttack = false;
        
        var entityTransform = EntityController.transform;
        entityTransform.LookAt(entityTransform.position + EntityController.LookDirection);
        
        EntityController.AddActionTrigger(ActionTriggerType.Hit, OnHit);
        EntityController.AddActionTrigger(ActionTriggerType.AirHit, OnAirHit);
        
        EntityController.AddActionTrigger(ActionTriggerType.LightAttack, OnLightAttack);
        EntityController.AddActionTrigger(ActionTriggerType.HeavyAttack, OnHeavyAttack);
        
        EntityController.AddActionTrigger(ActionTriggerType.MotionEvent, OnAttackAction);
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
        
        EntityController.RemoveActionTrigger(ActionTriggerType.MotionEvent, OnAttackAction);
    }

    private void OnLightAttack(ActionTriggerContext ctx)
    {
        if (!CanAttack) return;
        
        if (ctx.InputActionPhase == InputActionPhase.Started)
        {
            // NormalAttackStateMachine.ChangeState(_aaaState);
        }
    }

    private void OnHeavyAttack(ActionTriggerContext ctx)
    {
        if (!CanAttack) return;
        
        if (ctx.InputActionPhase == InputActionPhase.Started)
        {
            NormalAttackStateMachine.ChangeState(_aabState);
        }
    }
    
    private void OnAttackAction(ActionTriggerContext ctx)
    {
        _rigidbody.AddForce(EntityController.LookDirection * _playerController.normalAttackDashes[0], ForceMode.Impulse);
    }
}
