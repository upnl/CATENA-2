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

        _context = stateMachine.EntityController.AttackContext;

        // knockback 적용
        _rigidbody.AddForce(_context.KnockBack, ForceMode.Impulse);
        
        stateMachine.EntityController.AddActionTrigger(ActionTrigger.Hit, OnAirHit);
        stateMachine.EntityController.AddActionTrigger(ActionTrigger.AirHit, OnAirHit);
    }


    public override void Update()
    {
        base.Update();
        
        if (stateMachine.EntityController.LandingDetect())
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
        
        stateMachine.EntityController.RemoveActionTrigger(ActionTrigger.Hit, OnAirHit);
        stateMachine.EntityController.RemoveActionTrigger(ActionTrigger.AirHit, OnAirHit);
    }
}