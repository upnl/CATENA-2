using UnityEngine;

namespace StateMachine
{
    public class PlayerStateMachine : EntityStateMachine
    {
        public PlayerStateMachine(EntityController entityController) : base(entityController)
        {
            EntityIdleState = new PlayerIdleState(this);
            
            
            ChangeState(EntityIdleState);
        }

        public void GoToEntryState()
        {
            ChangeState(EntityIdleState);
        }
    }
}
