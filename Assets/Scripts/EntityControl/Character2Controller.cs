using StateMachine;

public class Character2Controller : PlayerController
{
    public float damageReductionRate = 0f;
    protected override void Awake()
    {
        base.Awake();
        
        StateMachine = new Character2StateMachine(this);
    }

    public void SetDamageReductionRate(float rate)
    {
        damageReductionRate = rate;
    }

    public override float CalculateDamage(float damage)
    {
        return damage * (1 - damageReductionRate);
    }
}
