using PlayerControl;
using UnityEngine;
using StateMachine;
using UnityEngine.InputSystem;

public class EntityAirHitState : EntityState
{
    private Rigidbody _rigidbody;
    private Transform _feetTransform;
    private AttackContext _context;

    private float _stunTimer;
    public EntityAirHitState(EntityStateMachine entityStateMachine) : base(entityStateMachine)
    {
        _rigidbody = stateMachine.EntityController.GetComponent<Rigidbody>();
        _feetTransform = stateMachine.EntityController.feetPosition;
    }

    public override void Enter()
    {
        base.Enter();

        PlayAnimation("Airhit");
        
        _context = stateMachine.EntityController.AttackContext;

        _stunTimer = 0.2f;

        // knockback 적용
        _rigidbody.linearVelocity = Vector3.zero;
        _rigidbody.AddForce(_context.knockBack, ForceMode.Impulse);
        
        stateMachine.EntityController.AddActionTrigger(ActionTriggerType.Hit, OnAirHit);
        stateMachine.EntityController.AddActionTrigger(ActionTriggerType.AirHit, OnAirHit);
    }


    public override void Update()
    {
        base.Update();
        
        _stunTimer -= Time.deltaTime;

        if (_stunTimer > 0) return;
        if (stateMachine.EntityController.LandingDetect())
        {
            if (_context.stunTime > 0) stateMachine.ChangeState(stateMachine.EntityStunState);
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
        
        stateMachine.EntityController.RemoveActionTrigger(ActionTriggerType.Hit, OnAirHit);
        stateMachine.EntityController.RemoveActionTrigger(ActionTriggerType.AirHit, OnAirHit);
    }
}