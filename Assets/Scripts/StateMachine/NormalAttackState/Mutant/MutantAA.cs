using PlayerControl;
using StateMachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class MutantAA : NormalAttackState
{
    private MutantAAA _aaaState;
    
    private Rigidbody _rigidbody;
    private EnemyController _enemyController;
    public MutantAA(
        EntityController entityController, 
        NormalAttackStateMachine normalAttackStateMachine, 
        EntityStateMachine parentStateMachine) 
        : base(entityController, normalAttackStateMachine, parentStateMachine)
    {
        _aaaState = new MutantAAA(entityController, normalAttackStateMachine, parentStateMachine);
        
        _rigidbody = EntityController.GetComponent<Rigidbody>();
        _enemyController = EntityController as EnemyController;
        
        AttackContext = _enemyController.attackContextSO.contexts[1];
    }

    public override void Enter()
    {
        base.Enter();
        
        ParentStateMachine.PlayAnimation("AA");
        
        _rigidbody.AddForce(EntityController.LookDirection * AttackContext.floatVariables[0], ForceMode.Impulse);
        
        CanAttack = false;
        
        var entityTransform = EntityController.transform;
        var playerPos = _enemyController.playerTransform.position;
        playerPos.y = EntityController.transform.position.y;
        entityTransform.LookAt(playerPos);
        
        EntityController.AddActionTrigger(ActionTriggerType.Hit, OnHit);
        EntityController.AddActionTrigger(ActionTriggerType.AirHit, OnAirHit);
        
        EntityController.AddActionTrigger(ActionTriggerType.LightAttack, OnLightAttack);
        
        EntityController.AddActionTrigger(ActionTriggerType.MotionEvent, OnAttackAction);
    }

    public override void Update()
    {
        base.Update();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void Exit()
    {
        base.Exit();
        
        EntityController.RemoveActionTrigger(ActionTriggerType.Hit, OnHit);
        EntityController.RemoveActionTrigger(ActionTriggerType.AirHit, OnAirHit);
        
        EntityController.RemoveActionTrigger(ActionTriggerType.LightAttack, OnLightAttack);
       
        
        EntityController.RemoveActionTrigger(ActionTriggerType.MotionEvent, OnAttackAction);
    }

    private void OnLightAttack(ActionTriggerContext ctx)
    {
        if (!CanAttack) return;
        
        if (ctx.InputActionPhase == InputActionPhase.Started || ctx.InputActionPhase == InputActionPhase.Performed)
        {
            NormalAttackStateMachine.ChangeState(_aaaState);
        }
    }
    private void OnAttackAction(ActionTriggerContext ctx)
    {
        _rigidbody.AddForce(EntityController.LookDirection * _enemyController.normalAttackDashes[0], ForceMode.Impulse);
    }
}
