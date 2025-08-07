using PlayerControl;
using UnityEngine;
using StateMachine;
using UnityEngine.InputSystem;

public class MutantNormalAttackState : EntityNormalAttackState
{
    public MutantNormalAttackState(EntityStateMachine entityStateMachine) : base(entityStateMachine)
    {
        NormalAttackStateMachine = new MutantNormalAttackSM(stateMachine.EntityController, stateMachine);
    }
}
