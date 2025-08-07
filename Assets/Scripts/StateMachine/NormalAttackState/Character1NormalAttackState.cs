using PlayerControl;
using UnityEngine;
using StateMachine;
using UnityEngine.InputSystem;

public class Character1NormalAttackState : EntityNormalAttackState
{
    public Character1NormalAttackState(EntityStateMachine entityStateMachine) : base(entityStateMachine)
    {
        NormalAttackStateMachine = new Character1NormalAttackSM(stateMachine.EntityController, stateMachine);
    }
}
