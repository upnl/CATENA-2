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
        base.Enter();
        
        PlayAnimation("Stun");
        
        _context = stateMachine.EntityController.AttackContext;
        
        if (_stunTimer < _context.StunTime) _stunTimer = _context.StunTime; 
        
        stateMachine.EntityController.AddActionTrigger(ActionTriggerType.Hit, OnHit);
        stateMachine.EntityController.AddActionTrigger(ActionTriggerType.AirHit, OnAirHit);
    }

    public override void Update()
    {
        base.Update();
        
        _stunTimer -= Time.deltaTime;

        if (_stunTimer <= 0)
        {
            stateMachine.ChangeState(stateMachine.EntityIdleState);
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