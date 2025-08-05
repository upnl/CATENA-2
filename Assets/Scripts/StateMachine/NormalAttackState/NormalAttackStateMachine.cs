using StateMachine;
using UnityEngine;

public class NormalAttackStateMachine
{
    private IState _currentState;

    protected EntityController EntityController;
    protected EntityStateMachine ParentStateMachine;
    
    protected IState EntryState;
    
    public NormalAttackStateMachine(EntityController entityController, EntityStateMachine entityStateMachine)
    {
        EntityController = entityController;
        ParentStateMachine = entityStateMachine;

        EnterNormalAttackState();
    }
    
    public void ChangeState(IState newState)
    {
        if (_currentState != null) _currentState.Exit();
        
        _currentState = newState;
        
        if (_currentState != null) _currentState.Enter();
    }

    public void EnterNormalAttackState()
    {
        _currentState = EntryState;
        if (_currentState != null) _currentState.Enter();
    }

    public void Update()
    {
        if (_currentState != null) _currentState.Update();
    }

    public void PhysicsUpdate()
    {
        if (_currentState != null) _currentState.PhysicsUpdate();
    }

    public void Exit()
    {
        _currentState.Exit();
    }
}
