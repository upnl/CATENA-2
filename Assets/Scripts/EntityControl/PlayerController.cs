using PlayerControl;
using StateMachine;
using Unity.Cinemachine;
using UnityEngine;

public class PlayerController : EntityController
{
    public Transform cameraTransform;
    public PlayerStateMachine PlayerStateMachine => StateMachine as PlayerStateMachine;
    
    
    protected override void Awake()
    {
        base.Awake();

        StateMachine = new Character1StateMachine(this);
    }

    protected override void Update()
    {
        base.Update();
        LookDirection = cameraTransform.forward.ProjectOntoPlane(Vector3.up);
        LookDirection.Normalize();
    }

    [ContextMenu("Hit")]
    public void Hit()
    {
        Hit(new AttackContext { Damage = 10f, IsIgnoringDodge = true, KnockBack = new Vector3(0, 0, -5f), StunTime = 2f});
    }
    
    [ContextMenu("AirHit")]
    public void AirHit()
    {
        Hit(new AttackContext { 
            Damage = 10f, 
            IsIgnoringDodge = true, 
            KnockBack = new Vector3(0, 8f, 0.2f), 
            StunTime = 1f});
    }
}
