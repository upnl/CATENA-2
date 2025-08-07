using StateMachine;

public class Character2Controller : PlayerController
{
    protected override void Awake()
    {
        base.Awake();
        
        StateMachine = new Character2StateMachine(this);
    }
}
