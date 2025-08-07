using PlayerControl;
using StateMachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class MutantA : NormalAttackState
{
    private MutantAA _aaState;
    
    private Rigidbody _rigidbody;
    private EnemyController _enemyController;
    
    public MutantA(
        EntityController entityController, 
        NormalAttackStateMachine normalAttackStateMachine,
        EntityStateMachine parentStateMachine)
        : base(entityController, normalAttackStateMachine, parentStateMachine)
    {
        _aaState = new MutantAA(entityController, normalAttackStateMachine, parentStateMachine);
        
        _rigidbody = EntityController.GetComponent<Rigidbody>();
        _enemyController = EntityController as EnemyController;

        AttackContext = _enemyController.attackContextSO.contexts[0];
    }

    public override void Enter()
    {
        base.Enter();
        
        ParentStateMachine.PlayAnimation("A");
        
        CanAttack = false;
        
        var entityTransform = EntityController.transform;
        var playerPos = _enemyController.playerTransform.position;
        playerPos.y = 0;
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
            NormalAttackStateMachine.ChangeState(_aaState);
        }
    }
    
    private void OnAttackAction(ActionTriggerContext ctx)
    {
        _rigidbody.AddForce(EntityController.LookDirection * _enemyController.normalAttackDashes[0], ForceMode.Impulse);
    }
}
