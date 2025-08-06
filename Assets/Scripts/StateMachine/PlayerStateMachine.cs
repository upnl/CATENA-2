using UnityEngine;

namespace StateMachine
{
    public class PlayerStateMachine : EntityStateMachine
    {
        public PlayerStateMachine(EntityController entityController) : base(entityController)
        {
            EntityIdleState = new PlayerIdleState(this);
            EntitySkill1State = new CharacterSkill1State(this);
            
            ChangeState(EntityIdleState);
        }

        public void GoToEntryState()
        {
            ChangeState(EntityIdleState);
        }
    }
}
