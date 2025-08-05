using UnityEngine;

namespace StateMachine
{
    public class EntityStateMachine
    {
        public IState CurrentState { get; private set; }
        public EntityController EntityController { get; private set; }
        
        public EntityIdleState EntityIdleState { get; private set; }
        public EntityMoveState EntityMoveState { get; private set; }

        public EntityStateMachine(EntityController entityController)
        {
            EntityController = entityController;

            EntityIdleState = new EntityIdleState(this);
            EntityMoveState = new EntityMoveState(this);
            
            CurrentState = EntityIdleState;
            CurrentState.Enter();
        }

        public void ChangeState(IState newState)
        {
            if (CurrentState != null) CurrentState.Exit();
            
            CurrentState = newState;
            
            if (CurrentState != null) CurrentState.Enter();
        }

        public void Update()
        {
            CurrentState?.Update();
        }

        public void PhysicsUpdate()
        {
            CurrentState?.PhysicsUpdate();
        }
    }
}
