using StateMachine;
using UnityEngine;

public class Character2NormalAttackSM : NormalAttackStateMachine
{

    public Character2NormalAttackSM(EntityController entityController, EntityStateMachine entityStateMachine)
        : base(entityController, entityStateMachine)
    {
        EntryState = new Character2A(entityController, this, entityStateMachine);
    }
    

}
