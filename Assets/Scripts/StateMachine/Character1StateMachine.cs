using UnityEngine;

namespace StateMachine
{
    public class Character1StateMachine : PlayerStateMachine
    {
        public Character1StateMachine(EntityController entityController) : base(entityController)
        {
            EntityNormalAttackState = new Character1NormalAttackState(this);
        }
    }
}
