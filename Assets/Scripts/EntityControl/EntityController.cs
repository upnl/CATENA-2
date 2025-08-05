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
    
    public Dictionary<ActionTrigger, Action<ActionTriggerContext>> actionTriggers;

    public Transform feetPosition;
    public float landingDetectDistance;
    public LayerMask groundLayerMask;

    private Rigidbody _rigidbody;
    
    /// <summary>
    /// 카메라가 현재 바라보는 방향. 이동과 공격, 스킬 방향에 영향을 미칩니다.
    /// </summary>
    public Vector3 LookDirection { get; protected set; }
    public AttackContext AttackContext { get; private set; }

    protected virtual void Awake()
    {
        actionTriggers = new Dictionary<ActionTrigger, Action<ActionTriggerContext>>();
        
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void AddActionTrigger(ActionTrigger trigger, Action<ActionTriggerContext> callback)
    {
        if (actionTriggers.ContainsKey(trigger))
        {
            actionTriggers[trigger] += callback;
        }
        else
        {
            actionTriggers[trigger] = callback;
        }
    }

    public void RemoveActionTrigger(ActionTrigger trigger, Action<ActionTriggerContext> callback)
    {
        if (actionTriggers.ContainsKey(trigger))
        {
            actionTriggers[trigger] -= callback;
            
            if (actionTriggers[trigger] == null)
                actionTriggers.Remove(trigger);
        }
    }

    public void PublishActionTrigger(ActionTrigger trigger, ActionTriggerContext context)
    {
        if (actionTriggers.TryGetValue(trigger, out var action))
        {
            action.Invoke(context);
        }
        else
        {
#if DEBUG
            Debug.LogWarning("Trigger " + trigger + " has no action trigger");
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
    
    public void Hit(AttackContext ctx)
    {
        AttackContext = ctx;
        
        if (ctx.KnockBack.y == 0) PublishActionTrigger(ActionTrigger.Hit, new ActionTriggerContext{ AttackContext = ctx });
        else PublishActionTrigger(ActionTrigger.AirHit, new ActionTriggerContext{ AttackContext = ctx });
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
