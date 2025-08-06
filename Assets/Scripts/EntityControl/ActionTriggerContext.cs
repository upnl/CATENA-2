using UnityEngine;
using UnityEngine.InputSystem;

namespace PlayerControl
{
    public struct ActionTriggerContext
    {
        public InputActionPhase InputActionPhase;
        public Vector2 MovementInput;
        public AttackContext AttackContext;

        public int AttackActionCtxNum;

        public int SkillNum;
    }
}