using UnityEngine;

namespace StateMachine
{
    public class Character2StateMachine : PlayerStateMachine
    {
        public Character2StateMachine(EntityController entityController) : base(entityController)
        {
            EntityNormalAttackState = new Character2NormalAttackState(this);
            
            EntitySkill1State = new Character2Skill1State(this);
        }
    }
}
