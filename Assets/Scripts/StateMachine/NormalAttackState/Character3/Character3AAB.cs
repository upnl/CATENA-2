using PlayerControl;
using StateMachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class Character3AAB : NormalAttackState
{
    // 실제로는 Animation 의 현재 상황에 따라서 공격 가능 판단해야함
    private float _attackTimer;
    private float _motionTimer;

    private bool _move;

    private Rigidbody _rigidbody;
    
    private PlayerController _playerController;
    public Character3AAB(
        EntityController entityController, 
        NormalAttackStateMachine normalAttackStateMachine, 
        EntityStateMachine parentStateMachine) 
        : base(entityController, normalAttackStateMachine, parentStateMachine)
    {
        _rigidbody = EntityController.GetComponent<Rigidbody>();
        
        _playerController = EntityController as PlayerController;
        
        AttackContext = _playerController.attackContextSO.contexts[3];
    }

    public override void Enter()
    {
        base.Enter();
        
        ParentStateMachine.PlayAnimation("AAB");
        
        CanAttack = false;
        
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

        if (_move)
        {
            _rigidbody.MovePosition(EntityController.transform.position + 
                                    EntityController.transform.forward * (_playerController.normalAttackDashes[1] * Time.fixedDeltaTime));
        }
    }

    public override void Exit()
    {
        base.Exit();
        
        EntityController.RemoveActionTrigger(ActionTriggerType.Hit, OnHit);
        EntityController.RemoveActionTrigger(ActionTriggerType.AirHit, OnAirHit);
        
        EntityController.RemoveActionTrigger(ActionTriggerType.MotionEvent, OnAttackAction);
    }

    private void OnLightAttack(ActionTriggerContext ctx)
    {
        if (ctx.InputActionPhase == InputActionPhase.Started)
        {
            
        }
    }
    
    private void OnAttackAction(ActionTriggerContext ctx)
    {
        if (ctx.AttackActionCtxNum == 0)
        {
            _move = true;
        }
        else
        {
            _move = false;
        }
    }
}
