using PlayerControl;
using UnityEngine;
using StateMachine;
using UnityEngine.InputSystem;

public class MutantSkill1State : EntitySkillState
{
    private Rigidbody _rigidbody;
    private EnemyController _enemyController;
    
    private Transform _playerTransform;

    private bool _isMoving;
    private Vector3 _dashDir;
    
    public MutantSkill1State(EntityStateMachine entityStateMachine) : base(entityStateMachine)
    {
        _rigidbody = stateMachine.EntityController.GetComponent<Rigidbody>();
        _enemyController = stateMachine.EntityController as EnemyController;

        _playerTransform = _enemyController.transform;

        AttackContext = _enemyController.attackContextSO.contexts[5];
    }
    
    public override void Enter()
    {
        base.Enter();
        
        LookPlayer();
        
        stateMachine.PlayAnimation("Skill1");
        
        AttackContext = _enemyController.attackContextSO.contexts[5];
        
        _enemyController.AddActionTrigger(ActionTriggerType.MotionEvent, OnMotionEvent);
        _enemyController.mp -= 20f;
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
        
        _enemyController.RemoveActionTrigger(ActionTriggerType.MotionEvent, OnMotionEvent);
        _enemyController.RemoveActionTrigger(ActionTriggerType.Dodge, OnDodge);
    }

    private void LookPlayer()
    {
        var entityTransform = stateMachine.EntityController.transform;
        var playerPos = _enemyController.playerTransform.position;
        playerPos.y = entityTransform.position.y;
        entityTransform.LookAt(playerPos);
    }

    private void OnMotionEvent(ActionTriggerContext ctx)
    {
        switch (ctx.AttackActionCtxNum)
        {
            case 0:
                LookPlayer();
                _dashDir = stateMachine.EntityController.transform.forward;
                _isMoving = true;
                break;
            case 1:
                _isMoving = false;
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
