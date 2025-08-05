using StateMachine;
using UnityEngine;

public class Character1NormalAttackSM : NormalAttackStateMachine
{

    public Character1NormalAttackSM(EntityController entityController, EntityStateMachine entityStateMachine)
        : base(entityController, entityStateMachine)
    {
        EntryState = new Character1A(entityController, this, entityStateMachine);
    }
    

}
