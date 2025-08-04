using System;
using PlayerControl;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputProcessor : MonoBehaviour
{
    private EntityController _entityController;

    public bool isControllable = true;
    
    private InputAction _movementAction;
    private InputAction _lightAttackAction;
    private InputAction _heavyAttackAction;
    private InputAction _dodgeAction;
    private InputAction[] _skillActions;
    private void Awake()
    {
        _entityController = GetComponent<EntityController>();
        
        _movementAction = InputSystem.actions.FindAction("Movement");
        _lightAttackAction = InputSystem.actions.FindAction("LightAttack");
        _heavyAttackAction = InputSystem.actions.FindAction("HeavyAttack");
        _dodgeAction = InputSystem.actions.FindAction("DodgeAttack");
        _skillActions = new InputAction[3];
        for (int i = 0; i < _skillActions.Length; i++)
        {
            _skillActions[i] = InputSystem.actions.FindAction("Skill" + (i+1));  
        }

        // Input Action 에 등록해주는 작업
        _movementAction.SubscribeAllPhases(PublishMovementTrigger);
        _lightAttackAction.SubscribeAllPhases(PublishLightAttackTrigger);
        _heavyAttackAction.SubscribeAllPhases(PublishHeavyAttackTrigger);
        _dodgeAction.SubscribeAllPhases(PublishDodgeTrigger);
        
        for (int i = 0; i < _skillActions.Length; i++)
        {
            _skillActions[i].SubscribeAllPhases(PublishSkillTrigger(i));
        }
    }

    private void PublishMovementTrigger(InputAction.CallbackContext ctx)
    {
        _entityController.PublishActionTrigger(
            ActionTrigger.MovementAction,
            new ActionTriggerContext
            {
                InputActionPhase = ctx.phase,
                MovementInput = ctx.ReadValue<Vector2>()
            });
    }
    
    private void PublishLightAttackTrigger(InputAction.CallbackContext ctx)
    {
        _entityController.PublishActionTrigger(
            ActionTrigger.LightAttack,
            new ActionTriggerContext
            {
                InputActionPhase = ctx.phase
            });
    }
    
    private void PublishHeavyAttackTrigger(InputAction.CallbackContext ctx)
    {
        _entityController.PublishActionTrigger(
            ActionTrigger.HeavyAttack,
            new ActionTriggerContext
            {
                InputActionPhase = ctx.phase
            });
    }
    
    private void PublishDodgeTrigger(InputAction.CallbackContext ctx)
    {
        _entityController.PublishActionTrigger(
            ActionTrigger.Dodge,
            new ActionTriggerContext
            {
                InputActionPhase = ctx.phase
            });
    }

    private Action<InputAction.CallbackContext> PublishSkillTrigger(int skillIndex)
    {
        return ctx => _entityController.PublishActionTrigger(
            ActionTrigger.Skill1 + skillIndex,
            new ActionTriggerContext
            {
                InputActionPhase = ctx.phase
            });
    }
    
    [ContextMenu("Publish Movement Action")]
    public void UnsubscribeAllPhases()
    {
        // Input Action 에서 해제해주는 작업
        _movementAction.UnsubscribeAllPhases(PublishMovementTrigger);
        _lightAttackAction.UnsubscribeAllPhases(PublishLightAttackTrigger);
        _heavyAttackAction.UnsubscribeAllPhases(PublishHeavyAttackTrigger);
        _dodgeAction.UnsubscribeAllPhases(PublishDodgeTrigger);
        
        for (int i = 0; i < _skillActions.Length; i++)
        {
            _skillActions[i].UnsubscribeAllPhases(PublishSkillTrigger(i));
        }

        isControllable = false;
    }
}
