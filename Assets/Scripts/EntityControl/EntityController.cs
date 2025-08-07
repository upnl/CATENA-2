using System;
using System.Collections.Generic;
using PlayerControl;
using StateMachine;
using UnityEngine;


/// <summary>
/// 상태 기계 기반 Entity 의 행동을 제어하기 위한 Controller Class 입니다
/// </summary>
public class EntityController : MonoBehaviour
{
    protected EntityStateMachine StateMachine;
    
    public Dictionary<ActionTriggerType, Action<ActionTriggerContext>> actionTriggers;

    public Transform feetPosition;
    public float landingDetectDistance;
    public LayerMask groundLayerMask;

    private Rigidbody _rigidbody;
    
    public Vector2 movementInput = Vector2.zero;

    public float hp;
    public float mp;
    
    public float maxHp, maxMp;

    public Animator Animator { get; private set; }

    /// <summary>
    /// 카메라가 현재 바라보는 방향. 이동과 공격, 스킬 방향에 영향을 미칩니다.
    /// </summary>
    public Vector3 LookDirection { get; protected set; }
    public AttackContext AttackContext { get; private set; }

    public float movementSpeed;
    public float bodyRotateSpeed;
    
    public float dodgeDash;

    protected bool isPlayer;

    public AttackContextsSO attackContextSO;

    protected virtual void Awake()
    {
        actionTriggers = new Dictionary<ActionTriggerType, Action<ActionTriggerContext>>();
        
        _rigidbody = GetComponent<Rigidbody>();
        Animator = GetComponentInChildren<Animator>();
    }

    public void AddActionTrigger(ActionTriggerType triggerType, Action<ActionTriggerContext> callback)
    {
        if (actionTriggers.ContainsKey(triggerType))
        {
            actionTriggers[triggerType] += callback;
        }
        else
        {
            actionTriggers[triggerType] = callback;
        }
    }

    public void RemoveActionTrigger(ActionTriggerType triggerType, Action<ActionTriggerContext> callback)
    {
        if (actionTriggers.ContainsKey(triggerType))
        {
            actionTriggers[triggerType] -= callback;
            
            if (actionTriggers[triggerType] == null)
                actionTriggers.Remove(triggerType);
        }
    }

    public void PublishActionTrigger(ActionTriggerType triggerType, ActionTriggerContext context)
    {
        if (actionTriggers.TryGetValue(triggerType, out var action))
        {
            action.Invoke(context);
        }
        else
        {
#if DEBUG
            Debug.LogWarning("Trigger " + triggerType + " has no action trigger");
#endif
        }
    }
    
    protected virtual void Update()
    {
        StateMachine.Update();
    }

    protected virtual void FixedUpdate()
    {
        StateMachine.PhysicsUpdate();
    }
    
    public virtual bool Hit(AttackContext ctx)
    {
        AttackContext = ctx;

        if (StateMachine.CurrentState is EntityDodgeState)
        {
            if (!ctx.isIgnoringDodge) return false;
        }
        
        if (ctx.knockBack.y == 0) PublishActionTrigger(ActionTriggerType.Hit, new ActionTriggerContext{ AttackContext = ctx });
        else PublishActionTrigger(ActionTriggerType.AirHit, new ActionTriggerContext{ AttackContext = ctx });

        
        var actualDamage = CalculateDamage(ctx.damage);
        hp -= actualDamage;

        if (ctx.stunTime == 0) SoundManager.instance.PlaySound(SoundType.Hit, 4);
        else SoundManager.instance.PlaySound(SoundType.Stun);
        
        DamageObjectSpawner.Instance.SpawnDamageObject((int)Mathf.Ceil(actualDamage), transform.position + Vector3.up, isPlayer);

        return true;
    }

    public virtual float CalculateDamage(float damage)
    {
        return damage;
    }

    public bool LandingDetect()
    {
        return Physics.Raycast(
            feetPosition.position, 
            Vector3.down, 
            landingDetectDistance,
            groundLayerMask) && _rigidbody.linearVelocity.y <= 0;
    }
}
