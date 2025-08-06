using PlayerControl;
using StateMachine;
using UnityEditor.Searcher;
using UnityEngine;

public class PlayerAnimationEventReceiver : MonoBehaviour
{
    public PlayerController playerController;

    
    //TODO: 고쳐야 함
    public void CanAttack()
    {
        playerController.PublishActionTrigger(ActionTriggerType.CanAttack, new ActionTriggerContext());
    }

    public void EndOfMotion()
    {
        playerController.PublishActionTrigger(ActionTriggerType.MotionDone, new ActionTriggerContext());
    }

    public void ApplyAttack()
    {
        playerController.PublishActionTrigger(ActionTriggerType.ApplyAttack, new ActionTriggerContext());
    }

    public void MotionEvent(int n)
    {
        playerController.PublishActionTrigger(
            ActionTriggerType.MotionEvent, 
            new ActionTriggerContext { AttackActionCtxNum = n});
    }
}
