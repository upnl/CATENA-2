using PlayerControl;
using UnityEngine;
using StateMachine;
using UnityEngine.InputSystem;

public class Character3NormalAttackState : EntityNormalAttackState
{
    public Character3NormalAttackState(EntityStateMachine entityStateMachine) : base(entityStateMachine)
    {
        NormalAttackStateMachine = new Character3NormalAttackSM(stateMachine.EntityController, stateMachine);
    }
}
