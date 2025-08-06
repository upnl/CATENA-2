using PlayerControl;
using UnityEngine;
using StateMachine;
using UnityEngine.InputSystem;

public class EntitySkillState : EntityState
{
    protected AttackBoxDetector BoxDetector;
    protected AttackContext AttackContext;

    protected bool CanAttack = false;
    
    public EntitySkillState(EntityStateMachine entityStateMachine) : base(entityStateMachine)
    {
        BoxDetector = entityStateMachine.EntityController.GetComponent<AttackBoxDetector>();
    }
    
    public override void Enter()
    {
        base.Enter();
        
        stateMachine.EntityController.AddActionTrigger(ActionTriggerType.MotionDone, OnMotionDone);
        stateMachine.EntityController.AddActionTrigger(ActionTriggerType.ApplyAttack, OnApplyAttack);
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
        
        stateMachine.EntityController.RemoveActionTrigger(ActionTriggerType.MotionDone, OnMotionDone);
        stateMachine.EntityController.RemoveActionTrigger(ActionTriggerType.ApplyAttack, OnApplyAttack);
    }
    
    protected void OnHit(ActionTriggerContext context)
    {
        stateMachine.ChangeState(stateMachine.EntityHitState);
    }
    
    protected void OnAirHit(ActionTriggerContext context)
    {
        stateMachine.ChangeState(stateMachine.EntityAirHitState);
    }
    
    protected void OnCanAttack(ActionTriggerContext ctx)
    {
        CanAttack = true;
    }
    
    protected void OnMotionDone(ActionTriggerContext ctx)
    {
        stateMachine.ChangeState(stateMachine.EntityIdleState);
    }
    
    protected void OnApplyAttack(ActionTriggerContext ctx)
    {
        BoxDetector.Attack(AttackContext);
    }
}
