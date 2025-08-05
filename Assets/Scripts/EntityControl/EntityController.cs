using System;
using System.Collections.Generic;
using PlayerControl;
using UnityEngine;


/// <summary>
/// 상태 기계 기반 Entity 의 행동을 제어하기 위한 Controller Class 입니다
/// </summary>
public class EntityController : MonoBehaviour
{
    public Dictionary<ActionTrigger, Action<ActionTriggerContext>> actionTriggers;
    /// <summary>
    /// 카메라가 현재 바라보는 방향. 이동과 공격, 스킬 방향에 영향을 미칩니다.
    /// </summary>
    public Vector3 LookDirection { get; protected set; }

    protected virtual void Awake()
    {
        actionTriggers = new Dictionary<ActionTrigger, Action<ActionTriggerContext>>();
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
}
