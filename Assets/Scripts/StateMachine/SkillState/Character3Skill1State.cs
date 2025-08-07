using System;
using System.Collections;
using PlayerControl;
using UnityEngine;
using StateMachine;
using UnityEditor.Searcher;
using UnityEngine.InputSystem;

public class Character3Skill1State : EntitySkillState
{
    private Rigidbody _rigidbody;
    private PlayerController _playerController;
    private Character3Controller _controller;
    
    private Transform _playerTransform;

    private bool _isMoving;
    private bool _isApplyingForce;
    private float _forceApplyTimer = 0f;
    private float _forceApplyDuration = 2.5f;  //TODO : change this fucking hard value to dynamic value
    private Vector3 _forceDirToApply;
    private Action Dashing;
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
        AttackContext = _playerController.attackContextSO.contexts[5];

        _isMoving = true;
        _isApplyingForce = false;
        _forceApplyTimer = 0f;
        _controller.onCollisionEvent += OnCollision;
        
        _playerController.AddActionTrigger(ActionTriggerType.MotionEvent, OnMotionEvent);
        _playerController.AddActionTrigger(ActionTriggerType.Dodge, OnDodge);
    }

    private void OnCollision()
    {
        Debug.Log("Collision Detected!!!!!!!!!!!!!!!!");
        _isApplyingForce = false;
        _rigidbody.AddForce(-1*_forceDirToApply*_rigidbody.linearVelocity.magnitude*1.5f, ForceMode.Impulse);
        
        stateMachine.ChangeState(stateMachine.EntityIdleState);

        AttackContext.damage *= Mathf.Clamp(_rigidbody.linearVelocity.magnitude / 3f, 0.2f, 2f);
        AttackContext.knockBack *= Mathf.Clamp(_rigidbody.linearVelocity.magnitude, 0f, 1f);
        BoxDetector.Attack(AttackContext);
    }

    public override void Update()
    {
        base.Update();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        if (_isMoving)
        {
            _rigidbody.MovePosition(stateMachine.EntityController.transform.position +
                                    _dashDir * (AttackContext.floatVariables[0] * Time.fixedDeltaTime));
        }

        if (_isApplyingForce)
        {
            if (_forceApplyTimer < _forceApplyDuration)
            {
                _rigidbody.AddForce(_forceDirToApply*9.8f, ForceMode.Force);   //TODO : change this fucking hard code
                _forceApplyTimer += Time.fixedDeltaTime;
                
            }
            else
            {
                _isApplyingForce = false;
                _rigidbody.linearVelocity = Vector3.zero;
                stateMachine.ChangeState(stateMachine.EntityIdleState);
            }
        }
    }

    public override void Exit()
    {
        base.Exit();
        
        _controller.onCollisionEvent -= OnCollision;
        _playerController.RemoveActionTrigger(ActionTriggerType.MotionEvent, OnMotionEvent);
        _playerController.RemoveActionTrigger(ActionTriggerType.Dodge, OnDodge);
    }
    
    private void OnMotionEvent(ActionTriggerContext ctx)
    {
        switch (ctx.AttackActionCtxNum)
        {
            case 0:
                _forceDirToApply = stateMachine.EntityController.transform.forward;
                _isMoving = false;
                break;
            case 1:
                _isApplyingForce = true;
                break;
            default:
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
