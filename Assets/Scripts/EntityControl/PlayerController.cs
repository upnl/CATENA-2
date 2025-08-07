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

    public bool canAttack;

    // 임시. 
    // AB AAB 
    public float[] normalAttackDashes;

    
    protected override void Awake()
    {
        base.Awake();

        StateMachine = new PlayerStateMachine(this);

        isPlayer = true;

        hp = maxHp;
        mp = maxMp;
        
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void OnEnable()
    {
        LookDirection = cameraTransform.forward.ProjectOntoPlane(Vector3.up);
        LookDirection.Normalize();
        
        PlayerStateMachine.GoToEntryState();
        
        disappearTimer = 2f;
        isControllable = true;
    }

    protected override void Update()
    {
        base.Update();

        if (hp < 0) Time.timeScale = 0;
        
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

    public override bool Hit(AttackContext ctx)
    {
        if (!isControllable) return false;

        return base.Hit(ctx);
    }
    
    [ContextMenu("Hit")]
    public void Hit()
    {
        Hit(new AttackContext { damage = 10f, isIgnoringDodge = true, knockBack = new Vector3(0, 0, -5f), stunTime = 2f});
    }
    
    [ContextMenu("AirHit")]
    public void AirHit()
    {
        Hit(new AttackContext { 
            damage = 10f, 
            isIgnoringDodge = true, 
            knockBack = new Vector3(0, 8f, 0.2f), 
            stunTime = 1f});
    }

    public bool IsAttacking()
    {
        return StateMachine.CurrentState is EntityNormalAttackState || StateMachine.CurrentState is EntitySkillState;
    }
}
