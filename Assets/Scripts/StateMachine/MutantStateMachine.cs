using UnityEngine;

namespace StateMachine
{
    public class MutantStateMachine : EntityStateMachine
    {
        public MutantStateMachine(EntityController entityController) : base(entityController)
        {
            EntityNormalAttackState = new MutantNormalAttackState(this);
            
            EntitySkill1State = new MutantSkill1State(this);
        }
    }
}
