using UnityEngine;

namespace StateMachine
{
    public class EntityStateMachine
    {
        public IState CurrentState { get; private set; }
        public EntityController EntityController { get; private set; }
        
        public EntityIdleState EntityIdleState { get; private set; }
        public EntityMoveState EntityMoveState { get; private set; }
        public EntityHitState EntityHitState { get; private set; }
        public EntityAirHitState EntityAirHitState { get; private set; }
        public EntityStunState EntityStunState { get; private set; }
        public EntityDodgeState EntityDodgeState { get; private set; }
        

        public EntityStateMachine(EntityController entityController)
        {
            EntityController = entityController;

            EntityIdleState = new EntityIdleState(this);
            EntityMoveState = new EntityMoveState(this);
            EntityHitState = new EntityHitState(this);
            EntityAirHitState = new EntityAirHitState(this);
            EntityStunState = new EntityStunState(this);
            EntityDodgeState = new EntityDodgeState(this);
            
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
