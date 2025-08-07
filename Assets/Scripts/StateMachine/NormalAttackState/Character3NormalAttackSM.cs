using StateMachine;
using UnityEngine;

public class Character3NormalAttackSM : NormalAttackStateMachine
{

    public Character3NormalAttackSM(EntityController entityController, EntityStateMachine entityStateMachine)
        : base(entityController, entityStateMachine)
    {
        EntryState = new Character3A(entityController, this, entityStateMachine);
    }
    

}
