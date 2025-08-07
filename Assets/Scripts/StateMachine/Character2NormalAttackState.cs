using PlayerControl;
using UnityEngine;
using StateMachine;
using UnityEngine.InputSystem;

public class Character2NormalAttackState : EntityNormalAttackState
{
    public Character2NormalAttackState(EntityStateMachine entityStateMachine) : base(entityStateMachine)
    {
        NormalAttackStateMachine = new Character2NormalAttackSM(stateMachine.EntityController, stateMachine);
    }
}
