using PlayerControl;
using UnityEngine;
using StateMachine;
using UnityEngine.InputSystem;

public class Character2Skill1State : EntitySkillState
{
    private Rigidbody _rigidbody;
    private PlayerController _playerController;
    
    private Transform _playerTransform;

    private bool _isMoving;
    private float _chargingTime;
    private Vector3 _dashDir;
    
    public Character2Skill1State(EntityStateMachine entityStateMachine) : base(entityStateMachine)
    {
        _rigidbody = stateMachine.EntityController.GetComponent<Rigidbody>();
        _playerController = stateMachine.EntityController as PlayerController;

        _playerTransform = _playerController.transform;

        AttackContext = _playerController.attackContextSO.contexts[5];
    }
    
    public override void Enter()
    {
        base.Enter();
        
        ((Character2Controller) stateMachine.EntityController).SetDamageReductionRate(0.8f);
        
        stateMachine.PlayAnimation("Skill1");
        _chargingTime = 0f;
        AttackContext = _playerController.attackContextSO.contexts[5];
        
        _playerController.AddActionTrigger(ActionTriggerType.Skill, OnCharging);
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
        
        ((Character2Controller) stateMachine.EntityController).SetDamageReductionRate(0f);
        
        _playerController.RemoveActionTrigger(ActionTriggerType.Skill, OnCharging);
    }
    
    /*private void OnMotionEvent(ActionTriggerContext ctx)
    {
        switch (ctx.AttackActionCtxNum)
        {
            case 0:
                if (ctx.InputActionPhase == InputActionPhase.Canceled)
                {
                    stateMachine.PlayAnimation("Skill1-1");
                    Debug.Log("Skill1-1 performed");
                }
                break;
            case 1:
                if (ctx.InputActionPhase == InputActionPhase.Canceled)
                {
                    stateMachine.PlayAnimation("Skill1-2");
                    Debug.Log("Skill1-2 performed");
                }
                break;
            case 2:
                if (ctx.InputActionPhase == InputActionPhase.Canceled)
                {
                    stateMachine.PlayAnimation("Skill1-3");
                    Debug.Log("Skill1-3 performed");
                }
                break;
        }
    }*/
    private void OnCharging(ActionTriggerContext ctx)
    {
        switch (ctx.InputActionPhase)
        {
            case InputActionPhase.Started:
                _chargingTime = 0f;
                break;
            case InputActionPhase.Canceled:
                switch (_chargingTime)
                {
                    //TODO : Change this Fucking hardcoded values to something more dynamic
                    case < 1.5f:
                        stateMachine.PlayAnimation("Skill1-1");
                        _rigidbody.AddForce(_playerController.LookDirection * _playerController.normalAttackDashes[0], ForceMode.Impulse);
                        // Debug.Log("Skill1-1 performed");
                        break;
                    case < 3.0f :
                        stateMachine.PlayAnimation("Skill1-2");
                        _rigidbody.AddForce(_playerController.LookDirection * _playerController.normalAttackDashes[0] * 2f, ForceMode.Impulse);
                        // Debug.Log("Skill1-2 performed");
                        break;
                    case > 3.0f :
                        stateMachine.PlayAnimation("Skill1-3");
                        _rigidbody.AddForce(_playerController.LookDirection * _playerController.normalAttackDashes[0] * 3.5f, ForceMode.Impulse);
                        // Debug.Log("Skill1-3 performed");
                        break;
                    default:
                        stateMachine.ChangeState(stateMachine.EntityIdleState);
                        break; 
                }
                _playerController.RemoveActionTrigger(ActionTriggerType.Skill, OnCharging);
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
