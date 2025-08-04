using System;
using StateMachine;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    private PlayerController _playerController;
    private PlayerStateMachine _playerStateMachine;
    private void Awake()
    {
        _playerController = GetComponent<PlayerController>();

        _playerStateMachine = new PlayerStateMachine(_playerController);
    }

    private void Update()
    {
        _playerStateMachine.Update();
    }

    private void FixedUpdate()
    {
        _playerStateMachine.PhysicsUpdate();
    }
}
