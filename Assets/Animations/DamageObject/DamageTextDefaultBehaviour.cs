using UnityEngine;

public class DamageTextDefaultBehaviour : StateMachineBehaviour
{
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // When the state exits, destroy the game object
        if (animator.gameObject != null)
        {
            Destroy(animator.gameObject);
        }
    }
}
