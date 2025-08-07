using StateMachine;
using UnityEngine;

public class MutantNormalAttackSM : NormalAttackStateMachine
{

    public MutantNormalAttackSM(EntityController entityController, EntityStateMachine entityStateMachine)
        : base(entityController, entityStateMachine)
    {
        EntryState = new MutantA(entityController, this, entityStateMachine);
    }
    

}
