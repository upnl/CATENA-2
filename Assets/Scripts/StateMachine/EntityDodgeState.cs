using PlayerControl;
using UnityEngine;
using StateMachine;
using UnityEngine.InputSystem;

public class EntityDodgeState : EntityState
{
    private bool _move;

    private Rigidbody _rigidbody;
    
    private EntityController _entityController;
    public EntityDodgeState(EntityStateMachine entityStateMachine) : base(entityStateMachine)
    {
        _entityController = stateMachine.EntityController;
        
        _rigidbody = _entityController.GetComponent<Rigidbody>();
    }
    
    public override void Enter()
    {
        base.Enter();
        
        PlayAnimation("Dodge");
        
        var lookDir = stateMachine.EntityController.LookDirection;
        var lookRightDir = Vector3.Cross(Vector3.up, lookDir);
        var inputVec = stateMachine.EntityController.movementInput;
        var moveVec = inputVec.y * lookDir + inputVec.x * lookRightDir;
        
        var entityTransform = stateMachine.EntityController.transform;
        entityTransform.LookAt(entityTransform.position + moveVec);
        
        stateMachine.EntityController.AddActionTrigger(ActionTriggerType.MotionDone, OnDodgeDone);
        
        stateMachine.EntityController.AddActionTrigger(ActionTriggerType.MotionEvent, OnMotionEvent);
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
            _rigidbody.MovePosition(_entityController.transform.position + 
                                    _entityController.transform.forward * (_entityController.dodgeDash * Time.fixedDeltaTime));
        }
    }

    public override void Exit()
    {
        base.Exit();
        
        stateMachine.EntityController.RemoveActionTrigger(ActionTriggerType.MotionDone, OnDodgeDone);
        
        stateMachine.EntityController.RemoveActionTrigger(ActionTriggerType.MotionEvent, OnMotionEvent);
    }
    
    protected void OnDodgeDone(ActionTriggerContext ctx)
    {
        stateMachine.ChangeState(stateMachine.EntityIdleState);
    }
    
    private void OnMotionEvent(ActionTriggerContext ctx)
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
