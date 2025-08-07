using StateMachine;

public class Character3Controller : PlayerController
{
    protected override void Awake()
    {
        base.Awake();
        
        StateMachine = new Character3StateMachine(this);
    }
}
