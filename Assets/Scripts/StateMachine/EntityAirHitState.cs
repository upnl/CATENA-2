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

        _context = stateMachine.EntityController.AttackContext;

        // knockback 적용
        _rigidbody.AddForce(_context.KnockBack, ForceMode.Impulse);
        Debug.Log("EntityAirHitState: Enter and get damage of " + _context.Damage);
        
        stateMachine.EntityController.AddActionTrigger(ActionTrigger.Hit, OnAirHit);
        stateMachine.EntityController.AddActionTrigger(ActionTrigger.AirHit, OnAirHit);
    }


    public override void Update()
    {
        Debug.Log("EntityAirHitState: Update");
        
        if (stateMachine.EntityController.LandingDetect())
        {
            if (_context.StunTime > 0) stateMachine.ChangeState(stateMachine.EntityStunState);
            else stateMachine.ChangeState(stateMachine.EntityIdleState);
        }
    }

    public override void PhysicsUpdate()
    {
        Debug.Log("EntityAirHitState: PhysicsUpdate");
    }

    public override void Exit()
    {
        Debug.Log("EntityAirHitState: Exit");
        
        stateMachine.EntityController.RemoveActionTrigger(ActionTrigger.Hit, OnAirHit);
        stateMachine.EntityController.RemoveActionTrigger(ActionTrigger.AirHit, OnAirHit);
    }
}