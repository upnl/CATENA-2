using PlayerControl;
using UnityEngine;
using StateMachine;
using UnityEngine.InputSystem;

public class EntityState : IState
{
    protected EntityStateMachine stateMachine;

    public EntityState(EntityStateMachine entityStateMachine)
    {
        stateMachine = entityStateMachine;
    }  
    
    public virtual void Enter()
    {
        Debug.Log($"[{GetType().Name}] Enter");
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
    }
    
    protected void OnHit(ActionTriggerContext context)
    {
        stateMachine.ChangeState(stateMachine.EntityHitState);
    }
    
    protected void OnAirHit(ActionTriggerContext context)
    {
        stateMachine.ChangeState(stateMachine.EntityAirHitState);
    }

    public void PlayAnimation(string animationStateName)
    {
        stateMachine.PlayAnimation(animationStateName);
    }

    protected void OnSkill(ActionTriggerContext context)
    {
        if (context.SkillNum == 1 && context.InputActionPhase != InputActionPhase.Canceled)
        {
            if (stateMachine.EntityController.mp >= context.AttackContext.mp) 
                stateMachine.ChangeState(stateMachine.EntitySkill1State);

        }
    }
}