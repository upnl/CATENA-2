using PlayerControl;
using StateMachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class Character3AB : NormalAttackState
{
    private Rigidbody _rigidbody;
    private PlayerController _playerController;
    public Character3AB (
        EntityController entityController, 
        NormalAttackStateMachine normalAttackStateMachine, 
        EntityStateMachine parentStateMachine) 
        : base(entityController, normalAttackStateMachine, parentStateMachine)
    {
        _rigidbody = EntityController.GetComponent<Rigidbody>();
        _playerController = EntityController as PlayerController;
        
        AttackContext = _playerController.attackContextSO.contexts[4];
    }

    public override void Enter()
    {
        base.Enter();
        
        ParentStateMachine.PlayAnimation("AB");
        
        var entityTransform = EntityController.transform;
        entityTransform.LookAt(entityTransform.position + EntityController.LookDirection);
        
        EntityController.AddActionTrigger(ActionTriggerType.Hit, OnHit);
        EntityController.AddActionTrigger(ActionTriggerType.AirHit, OnAirHit);
        
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
        
        EntityController.RemoveActionTrigger(ActionTriggerType.MotionEvent, OnAttackAction);
    }

    private void OnAttackAction(ActionTriggerContext ctx)
    {
        _rigidbody.AddForce(EntityController.LookDirection * _playerController.normalAttackDashes[0], ForceMode.Impulse);
    }
}
