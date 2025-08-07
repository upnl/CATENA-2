using System;
using PlayerControl;
using StateMachine;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class MutantController : EnemyController
{
    private ActionTriggerContext _actionTriggerContext;

    private PartyController _partyController;

    public AboveEnemyUI hpBarUi;
    protected override void Awake()
    {
        base.Awake();

        hp = maxHp;
        StateMachine = new MutantStateMachine(this);

        _partyController = GameObject.FindObjectOfType<PartyController>();
        _partyController.onCharacterChange += OnCharacterChange;
        
        _actionTriggerContext = new ActionTriggerContext {SkillNum = 1, InputActionPhase = InputActionPhase.Performed};
    }

    private void OnDestroy()
    {
        _partyController.onCharacterChange -= OnCharacterChange;
    }


    protected override void Update()
    {
        base.Update();
        
        mp += Time.deltaTime;

        hp = Mathf.Clamp(hp, 0, maxHp);
        mp = Mathf.Clamp(mp,0, maxMp);
        
        hpBarUi.SetHpSlider(hp/maxHp);

        if (playerTransform == null) return;
        
        // death mechanism
        if (hp <= 0) Destroy(gameObject);

        movementInput = Vector2.up;
        var dir = (playerTransform.position - transform.position);
        dir.y = 0;
        
        LookDirection = dir.normalized; 
        
        if (Vector3.Distance(playerTransform.position, transform.position) < detectDistance)
        {
            if (mp >= 20) PublishActionTrigger(ActionTriggerType.Skill, _actionTriggerContext);
            else PublishActionTrigger(ActionTriggerType.LightAttack, _actionTriggerContext);
        }
    }

    public void OnCharacterChange()
    {
        playerTransform = _partyController.GetCurrentCharacter();
    }
}
