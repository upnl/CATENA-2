using System;
using System.Collections;
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

    public Transform GetCurrentCharacter()
    {
        return playerInputProcessors[_currentCharacterIndex].transform;
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

            cinemachineCamera.Target.TrackingTarget = playerInputProcessors[_currentCharacterIndex].transform;
            cinemachineCamera.Target.LookAtTarget = playerInputProcessors[_currentCharacterIndex].transform;

            StartCoroutine(ChangeCharacterEffect());
        }
    }

    private IEnumerator ChangeCharacterEffect()
    {
        Time.timeScale = 0.5f;
        Time.fixedDeltaTime *= 0.5f;

        yield return new WaitForSecondsRealtime(1f);
        
        Time.timeScale = 1f;
        Time.fixedDeltaTime *= 2f;
    }
}
