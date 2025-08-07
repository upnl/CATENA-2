using System;
using System.Collections;
using Unity.Cinemachine;
using UnityEditor.AssetImporters;
using UnityEngine;
using UnityEngine.InputSystem;

public class PartyController : MonoBehaviour
{
    public PlayerInputProcessor[] playerInputProcessors;
    
    public CinemachineCamera cinemachineCamera;

    private InputAction _changeAction;
    private int _currentCharacterIndex;

    public Action onCharacterChange;

    public float[] changeCooldowns;

    private void Start()
    {
        _changeAction = InputSystem.actions.FindAction("ChangeCharacter");
        
        _changeAction.SubscribeAllPhases(OnChange);

        changeCooldowns = new float[playerInputProcessors.Length];

        _currentCharacterIndex = 0;
    }

    private void Update()
    {
        for (int i = 0; i<changeCooldowns.Length; i++)
        {
            if (changeCooldowns[i] > 0) changeCooldowns[i] -= Time.deltaTime;
            else changeCooldowns[i] = Mathf.Clamp(changeCooldowns[i], 0, 100);
        }
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

            if (changeCooldowns[num - 1] > 0) return;

            if (_currentCharacterIndex == num - 1) return;

            changeCooldowns[_currentCharacterIndex] = 5f;
            
            var currentPos =  playerInputProcessors[_currentCharacterIndex].transform.position;
            
            playerInputProcessors[_currentCharacterIndex].UnsubscribeAllPhases();
            _currentCharacterIndex = num - 1;
            playerInputProcessors[_currentCharacterIndex].gameObject.SetActive(true);
            playerInputProcessors[_currentCharacterIndex].transform.position = currentPos;

            cinemachineCamera.Target.TrackingTarget = playerInputProcessors[_currentCharacterIndex].transform;
            cinemachineCamera.Target.LookAtTarget = playerInputProcessors[_currentCharacterIndex].transform;

            StartCoroutine(ChangeCharacterEffect());
            
            onCharacterChange.Invoke();
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
