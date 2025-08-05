using StateMachine;
using UnityEngine;

public class NormalAttackState : IState
{
    private EntityController _entityController;
    private EntityStateMachine _parentStateMachine;

    public NormalAttackState(EntityController entityController, EntityStateMachine parentStateMachine)
    {
        _entityController = entityController;
        _parentStateMachine = parentStateMachine;
    }
    
    public void Enter()
    {
        throw new System.NotImplementedException();
    }

    public void Update()
    {
        throw new System.NotImplementedException();
    }

    public void PhysicsUpdate()
    {
        throw new System.NotImplementedException();
    }

    public void Exit()
    {
        throw new System.NotImplementedException();
    }
}
