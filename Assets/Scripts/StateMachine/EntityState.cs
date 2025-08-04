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
        throw new System.NotImplementedException();
    }

    public virtual void Update()
    {
        throw new System.NotImplementedException();
    }

    public virtual void PhysicsUpdate()
    {
        throw new System.NotImplementedException();
    }

    public virtual void Exit()
    {
        throw new System.NotImplementedException();
    }
}