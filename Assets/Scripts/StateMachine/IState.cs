namespace StateMachine
{
    public interface IState
    {
        public void Enter();
        public void Update();
        public void PhysicsUpdate();
        public void Exit();
    }
}