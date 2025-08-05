using PlayerControl;
using UnityEngine;
using StateMachine;
using UnityEngine.InputSystem;

public class EntityStunState : EntityState
{
    private Rigidbody _rigidbody;
    private AttackContext _context;

    private float _stunTimer;
    
    public EntityStunState(EntityStateMachine entityStateMachine) : base(entityStateMachine)
    {
        _rigidbody = stateMachine.EntityController.GetComponent<Rigidbody>();
    }
    
    public override void Enter()
    {
        
        _context = stateMachine.EntityController.AttackContext;
        
        // knockback 적용
        _rigidbody.AddForce(_context.KnockBack, ForceMode.Impulse);
        Debug.Log("EntityStunState: Enter and stun time of " + _context.StunTime);
        
        if (_stunTimer < _context.StunTime) _stunTimer = _context.StunTime; 
        
        stateMachine.EntityController.AddActionTrigger(ActionTrigger.Hit, OnHit);
        stateMachine.EntityController.AddActionTrigger(ActionTrigger.AirHit, OnAirHit);
    }

    public override void Update()
    {
        Debug.Log("EntityStunState: Update");
        
        _stunTimer -= Time.deltaTime;

        if (_stunTimer <= 0)
        {
            stateMachine.ChangeState(stateMachine.EntityIdleState);
        }
    }

    public override void PhysicsUpdate()
    {
        Debug.Log("EntityStunState: PhysicsUpdate");
    }

    public override void Exit()
    {
        Debug.Log("EntityStunState: Exit");
        
        stateMachine.EntityController.RemoveActionTrigger(ActionTrigger.Hit, OnHit);
        stateMachine.EntityController.RemoveActionTrigger(ActionTrigger.AirHit, OnAirHit);
    }
}