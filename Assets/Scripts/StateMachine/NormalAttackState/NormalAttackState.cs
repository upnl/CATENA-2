using PlayerControl;
using StateMachine;
using UnityEngine;

public class NormalAttackState : IState
{
    protected EntityController EntityController;
    protected NormalAttackStateMachine NormalAttackStateMachine;
    protected EntityStateMachine ParentStateMachine;
    
    protected AttackBoxDetector BoxDetector;

    protected bool CanAttack;
    public NormalAttackState(EntityController entityController, NormalAttackStateMachine normalAttackStateMachine, EntityStateMachine parentStateMachine)
    {
        EntityController = entityController;
        NormalAttackStateMachine = normalAttackStateMachine;
        ParentStateMachine = parentStateMachine;

        BoxDetector = entityController.GetComponent<AttackBoxDetector>();
    }
    
    public virtual void Enter()
    {
        Debug.Log($"[{GetType().Name}] Enter");
        
        EntityController.AddActionTrigger(ActionTriggerType.CanAttack, OnCanAttack);
        EntityController.AddActionTrigger(ActionTriggerType.MotionDone, OnAttackDone);
        
        EntityController.AddActionTrigger(ActionTriggerType.ApplyAttack, OnApplyAttack);
    }

    public virtual void Update()
    {
        Debug.Log($"[{GetType().Name}] Update");
    }

    public virtual void PhysicsUpdate()
    {
        Debug.Log($"[{GetType().Name}] PhysicsUpdate");
    }

    public virtual void Exit()
    {
        Debug.Log($"[{GetType().Name}] Exit");
        
        EntityController.RemoveActionTrigger(ActionTriggerType.CanAttack, OnCanAttack);
        EntityController.RemoveActionTrigger(ActionTriggerType.MotionDone, OnAttackDone);
        
        EntityController.RemoveActionTrigger(ActionTriggerType.ApplyAttack, OnApplyAttack);
    }
    
    protected void OnHit(ActionTriggerContext context)
    {
        ParentStateMachine.ChangeState(ParentStateMachine.EntityHitState);
    }
    
    protected void OnAirHit(ActionTriggerContext context)
    {
        ParentStateMachine.ChangeState(ParentStateMachine.EntityAirHitState);
    }
    
    protected void OnCanAttack(ActionTriggerContext ctx)
    {
        CanAttack = true;
    }
    
    protected void OnAttackDone(ActionTriggerContext ctx)
    {
        ParentStateMachine.ChangeState(ParentStateMachine.EntityIdleState);
    }

    protected void OnApplyAttack(ActionTriggerContext ctx)
    {
        BoxDetector.Attack();
    }
}
