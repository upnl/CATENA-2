using System;
using PlayerControl;
using StateMachine;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class MutantController : EnemyController
{
    private ActionTriggerContext _actionTriggerContext;
    
    protected override void Awake()
    {
        base.Awake();
        
        StateMachine = new MutantStateMachine(this);
        
        _actionTriggerContext = new ActionTriggerContext {SkillNum = 1, InputActionPhase = InputActionPhase.Performed};
    }


    protected override void Update()
    {
        base.Update();
        
        mp += Time.deltaTime;

        if (playerTransform == null) return;

        movementInput = Vector2.up;
        var dir = (playerTransform.position - transform.position);
        dir.y = 0;
        
        LookDirection = dir.normalized; 
        
        if (Vector3.Distance(playerTransform.position, transform.position) < detectDistance)
        {
            PublishActionTrigger(ActionTriggerType.Skill, _actionTriggerContext);
        }
    }
}
