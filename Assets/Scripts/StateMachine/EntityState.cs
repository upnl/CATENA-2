using PlayerControl;
using UnityEngine;
using StateMachine;

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
}