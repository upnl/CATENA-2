using PlayerControl;
using UnityEngine;
using StateMachine;
using UnityEngine.InputSystem;

public class EntityHitState : EntityState
{
    private Rigidbody _rigidbody;
    private AttackContext _context;

    private float _hitMotionTimer;
    
    public EntityHitState(EntityStateMachine entityStateMachine) : base(entityStateMachine)
    {
        _rigidbody = stateMachine.EntityController.GetComponent<Rigidbody>();
    }
    
    public override void Enter()
    {
        base.Enter();
        
        PlayAnimation("Hit");
        
        _context = stateMachine.EntityController.AttackContext;
        
        // knockback 적용
        _rigidbody.linearVelocity = Vector3.zero;
        _rigidbody.AddForce(_context.KnockBack, ForceMode.Impulse);


        _hitMotionTimer = 1f;
        
        stateMachine.EntityController.AddActionTrigger(ActionTriggerType.Hit, OnHit);
        stateMachine.EntityController.AddActionTrigger(ActionTriggerType.AirHit, OnAirHit);
    }

    public override void Update()
    {
        base.Update();
        
        if (_hitMotionTimer > 0) _hitMotionTimer -= Time.deltaTime;
        if (_hitMotionTimer <= 0)
        {
            if (_context.StunTime > 0) stateMachine.ChangeState(stateMachine.EntityStunState);
            else stateMachine.ChangeState(stateMachine.EntityIdleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void Exit()
    {
        base.Exit();
        
        stateMachine.EntityController.RemoveActionTrigger(ActionTriggerType.Hit, OnHit);
        stateMachine.EntityController.RemoveActionTrigger(ActionTriggerType.AirHit, OnAirHit);
    }
}
