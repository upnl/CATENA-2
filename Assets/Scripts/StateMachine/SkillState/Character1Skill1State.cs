using PlayerControl;
using UnityEngine;
using StateMachine;
using UnityEngine.InputSystem;

public class Character1Skill1State : EntitySkillState
{
    private Rigidbody _rigidbody;
    private PlayerController _playerController;
    
    private Transform _playerTransform;

    private bool _isMoving;
    private Vector3 _dashDir;
    
    public Character1Skill1State(EntityStateMachine entityStateMachine) : base(entityStateMachine)
    {
        _rigidbody = stateMachine.EntityController.GetComponent<Rigidbody>();
        _playerController = stateMachine.EntityController as PlayerController;

        _playerTransform = _playerController.transform;

        AttackContext = _playerController.attackContextSO.contexts[5];
    }
    
    public override void Enter()
    {
        base.Enter();
        
        stateMachine.PlayAnimation("Skill1");
        
        AttackContext = _playerController.attackContextSO.contexts[5];
        
        _playerController.AddActionTrigger(ActionTriggerType.MotionEvent, OnMotionEvent);
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
    }

    public override void Exit()
    {
        base.Exit();
        
        _playerController.RemoveActionTrigger(ActionTriggerType.MotionEvent, OnMotionEvent);
        _playerController.RemoveActionTrigger(ActionTriggerType.Dodge, OnDodge);
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
                _isMoving = false;
                break;
            case 2:
                _rigidbody.AddForce(_dashDir * (AttackContext.floatVariables[1]) + Vector3.up * (AttackContext.floatVariables[2]), ForceMode.Impulse);
                break;
            case 3:
                _rigidbody.linearVelocity = Vector3.zero;
                if (Physics.Raycast(_playerTransform.position, Vector3.down, out RaycastHit hit, Mathf.Infinity,
                        AttackContext.groundMask))
                {
                    _playerTransform.position = hit.point + Vector3.up * 0.2f;
                    AttackContext = _playerController.attackContextSO.contexts[6];
                    
                    _playerController.AddActionTrigger(ActionTriggerType.Dodge, OnDodge);
                }
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
