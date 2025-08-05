using System;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class PartyController : MonoBehaviour
{
    public PlayerInputProcessor[] playerInputProcessors;
    
    public CinemachineCamera cinemachineCamera;

    private InputAction _changeAction;
    private int _currentCharacterIndex;

    private void Start()
    {
        _changeAction = InputSystem.actions.FindAction("ChangeCharacter");
        
        _changeAction.SubscribeAllPhases(OnChange);

        _currentCharacterIndex = 0;
    }

    private void OnChange(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            int num = (int) context.ReadValue<float>();

            if (_currentCharacterIndex == num - 1) return;
            
            var currentPos =  playerInputProcessors[_currentCharacterIndex].transform.position;
            
            playerInputProcessors[_currentCharacterIndex].UnsubscribeAllPhases();
            _currentCharacterIndex = num - 1;
            playerInputProcessors[_currentCharacterIndex].gameObject.SetActive(true);
            playerInputProcessors[_currentCharacterIndex].transform.position = currentPos;
            
            cinemachineCamera.Follow = playerInputProcessors[_currentCharacterIndex].transform;
        }
    }
}
