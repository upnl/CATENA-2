using PlayerControl;
using UnityEngine;
using StateMachine;
using UnityEngine.InputSystem;

public class Character2Skill2State : EntitySkillState
{
    private Rigidbody _rigidbody;
    private PlayerController _playerController;
    
    private Transform _playerTransform;

    private bool _isMoving;
    private float _chargingTime;
    private Vector3 _dashDir;
    
    public Character2Skill2State(EntityStateMachine entityStateMachine) : base(entityStateMachine)
    {
        _rigidbody = stateMachine.EntityController.GetComponent<Rigidbody>();
        _playerController = stateMachine.EntityController as PlayerController;

        _playerTransform = _playerController.transform;

        AttackContext = _playerController.attackContextSO.contexts[5];
    }
    
    public override void Enter()
    {
        //TODO : skill2
        base.Enter();
        
        
        
        stateMachine.PlayAnimation("Skill2");
        _chargingTime = 0f;
        AttackContext = _playerController.attackContextSO.contexts[5];
        
    }

    public override void Update()
    {
        
        base.Update();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        
        ((Character2Controller) stateMachine.EntityController).SetDamageReductionRate(0f);

        if (_isMoving)
        {
            _rigidbody.MovePosition(stateMachine.EntityController.transform.position +
                                    _dashDir * (AttackContext.floatVariables[0] * Time.fixedDeltaTime));
        }
    }

    public override void Exit()
    {
        base.Exit();
        
        // _playerController.RemoveActionTrigger(ActionTriggerType.Skill, );
    }
    
    private void OnDodge(ActionTriggerContext context)
    {
        if (context.InputActionPhase == InputActionPhase.Performed)
        {
            stateMachine.ChangeState(stateMachine.EntityDodgeState);
        }
    }
}
