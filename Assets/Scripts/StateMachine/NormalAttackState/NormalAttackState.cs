using PlayerControl;
using StateMachine;
using UnityEngine;

public class NormalAttackState : IState
{
    protected EntityController EntityController;
    protected NormalAttackStateMachine NormalAttackStateMachine;
    protected EntityStateMachine ParentStateMachine;

    // 실제로는 Animation 의 현재 상황에 따라서 공격 가능 판단해야함
    protected float AttackTimer;
    protected float MotionTimer;

    protected bool CanAttack;
    public NormalAttackState(EntityController entityController, NormalAttackStateMachine normalAttackStateMachine, EntityStateMachine parentStateMachine)
    {
        EntityController = entityController;
        NormalAttackStateMachine = normalAttackStateMachine;
        ParentStateMachine = parentStateMachine;
    }
    
    public virtual void Enter()
    {
        Debug.Log($"[{GetType().Name}] Enter");
    }

    public virtual void Update()
    {
        Debug.Log($"[{GetType().Name}] Update");
        AttackTimer -= Time.deltaTime;
        MotionTimer -= Time.deltaTime;

        if (AttackTimer <= 0) CanAttack = true;
        if (MotionTimer <= 0) ParentStateMachine.ChangeState(ParentStateMachine.EntityIdleState);
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
        ParentStateMachine.ChangeState(ParentStateMachine.EntityHitState);
    }
    
    protected void OnAirHit(ActionTriggerContext context)
    {
        ParentStateMachine.ChangeState(ParentStateMachine.EntityAirHitState);
    }
}
