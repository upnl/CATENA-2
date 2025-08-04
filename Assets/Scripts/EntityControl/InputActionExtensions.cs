using System;
using UnityEngine.InputSystem;

public static class InputActionExtensions
{
    /// <summary>
    /// Subscribes the same callback to started, performed, and canceled.
    /// </summary>
    public static void SubscribeAllPhases(this InputAction action, Action<InputAction.CallbackContext> callback)
    {
        action.started += callback;
        action.performed += callback;
        action.canceled += callback;
    }

    /// <summary>
    /// Unsubscribes the callback from all phases.
    /// </summary>
    public static void UnsubscribeAllPhases(this InputAction action, Action<InputAction.CallbackContext> callback)
    {
        action.started -= callback;
        action.performed -= callback;
        action.canceled -= callback;
    }
}