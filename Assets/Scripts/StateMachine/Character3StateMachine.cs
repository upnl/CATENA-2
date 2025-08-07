using UnityEngine;

namespace StateMachine
{
    public class Character3StateMachine : PlayerStateMachine
    {
        public Character3StateMachine(EntityController entityController) : base(entityController)
        {
            EntityNormalAttackState = new Character3NormalAttackState(this);
            
            EntitySkill1State = new Character3Skill1State(this);
        }
    }
}
