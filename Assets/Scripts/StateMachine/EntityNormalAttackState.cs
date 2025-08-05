using PlayerControl;
using UnityEngine;
using StateMachine;
using UnityEngine.InputSystem;

public class EntityNormalAttackState : EntityState
{
    private NormalAttackStateMachine _normalAttackStateMachine;
    
    public EntityNormalAttackState(EntityStateMachine entityStateMachine) : base(entityStateMachine)
    {
        _normalAttackStateMachine = new NormalAttackStateMachine(stateMachine.EntityController, stateMachine);
    }
    
    public override void Enter()
    {
        Debug.Log("EntityNormalAttackState: Enter");
        
        _normalAttackStateMachine.EnterNormalAttackState();
    }

    public override void Update()
    {
        Debug.Log("EntityNormalAttackState: Update");
        
        _normalAttackStateMachine.Update();
    }

    public override void PhysicsUpdate()
    {
        Debug.Log("EntityNormalAttackState: PhysicsUpdate");
        
        _normalAttackStateMachine.PhysicsUpdate();
    }

    public override void Exit()
    {
        Debug.Log("EntityNormalAttackState: Exit");
    }
}
