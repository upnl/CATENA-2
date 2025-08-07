using PlayerControl;
using UnityEngine;
using StateMachine;
using UnityEngine.InputSystem;

public class Character3Skill1State : EntitySkillState
{
    private Rigidbody _rigidbody;
    private PlayerController _playerController;
    private Character3Controller _controller;
    
    private Transform _playerTransform;

    private bool _isMoving;
    private float _chargingTime;
    private Vector3 _dashDir;
    
    public Character3Skill1State(EntityStateMachine entityStateMachine) : base(entityStateMachine)
    {
        _rigidbody = stateMachine.EntityController.GetComponent<Rigidbody>();
        _playerController = stateMachine.EntityController as PlayerController;
        _controller = _playerController as Character3Controller;
        
        _playerTransform = _playerController.transform;

        AttackContext = _playerController.attackContextSO.contexts[5];
    }
    
    public override void Enter()
    {
        base.Enter();
        
        stateMachine.PlayAnimation("Skill1");
        _chargingTime = 0f;
        AttackContext = _playerController.attackContextSO.contexts[5];

        _controller.onCollisionEvent += OnCollision;
        
        _playerController.AddActionTrigger(ActionTriggerType.MotionEvent, OnMotionEvent);
    }

    private void OnCollision()
    {
        BoxDetector.Attack(AttackContext);
    }

    public override void Update()
    {
        base.Update();
        _chargingTime += Time.deltaTime;
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        if (_isMoving)
        {
            _rigidbody.MovePosition(stateMachine.EntityController.transform.position +
                                    _dashDir * (AttackContext.floatVariables[0] * Time.fixedDeltaTime));
        }
    }

    public override void Exit()
    {
        base.Exit();
        
        _controller.onCollisionEvent -= OnCollision;
        _playerController.RemoveActionTrigger(ActionTriggerType.MotionEvent, OnMotionEvent);
    }
    
    private void OnMotionEvent(ActionTriggerContext ctx)
    {
        switch (ctx.AttackActionCtxNum)
        {
            case 0:
                _dashDir = stateMachine.EntityController.transform.forward;
                _isMoving = true;
                break;
            case 1:
                _rigidbody.AddForce(_dashDir * (AttackContext.floatVariables[1])* (AttackContext.floatVariables[2]), ForceMode.Impulse);
                break;
            case 2:
                _rigidbody.linearVelocity = Vector3.zero;
                break;
        }
    }
    
    private void OnDodge(ActionTriggerContext context)
    {
        if (context.InputActionPhase == InputActionPhase.Performed)
        {
            stateMachine.ChangeState(stateMachine.EntityDodgeState);
        }
    }
}
