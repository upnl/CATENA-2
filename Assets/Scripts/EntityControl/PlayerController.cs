using System;
using PlayerControl;
using StateMachine;
using Unity.Cinemachine;
using UnityEngine;

public class PlayerController : EntityController
{
    public Transform cameraTransform;
    public PlayerStateMachine PlayerStateMachine => StateMachine as PlayerStateMachine;

    public bool isControllable;

    public float disappearTimer;
    
    
    protected override void Awake()
    {
        base.Awake();

        StateMachine = new Character1StateMachine(this);
    }

    private void OnEnable()
    {
        PlayerStateMachine.GoToEntryState();
        
        disappearTimer = 2f;
        isControllable = true;
    }

    protected override void Update()
    {
        base.Update();
        
        if (!isControllable)
        {
            if (!IsAttacking())
            {
                disappearTimer -= Time.deltaTime;

                if (disappearTimer <= 0)
                {
                    gameObject.SetActive(false);
                }
            }
        }
        else
        {
            LookDirection = cameraTransform.forward.ProjectOntoPlane(Vector3.up);
            LookDirection.Normalize();
        }
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

    public bool IsAttacking()
    {
        return StateMachine.CurrentState is EntityNormalAttackState;
    }
}
