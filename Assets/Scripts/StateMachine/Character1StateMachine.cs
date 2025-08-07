using UnityEngine;

namespace StateMachine
{
    public class Character1StateMachine : PlayerStateMachine
    {
        public Character1StateMachine(EntityController entityController) : base(entityController)
        {
            EntityNormalAttackState = new Character1NormalAttackState(this);
            
            EntitySkill1State = new Character1Skill1State(this);
        }
    }
}
