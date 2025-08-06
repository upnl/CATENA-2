using StateMachine;

public class Character1Controller : PlayerController
{
    protected override void Awake()
    {
        base.Awake();
        
        StateMachine = new Character1StateMachine(this);
    }
}
