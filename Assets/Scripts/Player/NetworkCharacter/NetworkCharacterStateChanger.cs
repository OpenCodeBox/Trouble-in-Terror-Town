using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TTTSC.Player.Character.Controller;
using TTTSC.Player.Character;
using Mirror;

namespace TTTSC.Player.NetworkedCharacter
{
    public class NetworkCharacterStateChanger : MonoBehaviour
    {
        private PlayerGhostReffrenceHub _playerGhostReffrenceHub;
        private PlayerInputReceiver _playerInputReceiver;
        private NetworkCharacterStateMachine _networkCharacterStateMachine;

        private bool _walkIsPerforming, _crouchIsHeld, _sprintIsPerforming;

        private void Start()
        {
            _playerGhostReffrenceHub = GetComponentInParent<PlayerGhostReffrenceHub>();
            _playerInputReceiver = _playerGhostReffrenceHub.playerInputReceiver;
            _networkCharacterStateMachine = GetComponent<NetworkCharacterStateMachine>();
            _playerInputReceiver.MoveInputEvent += Walk;
            _playerInputReceiver.CrouchInputEvent += Crouch;
            _playerInputReceiver.SprintInputEvent += Sprint;
        }

        private void Walk(Vector2 direction, bool performing)
        {
            _walkIsPerforming = performing;
        }
        
        private void Crouch(bool performing)
        {
            _crouchIsHeld = performing;
        }

        private void Sprint(bool held)
        {
            _sprintIsPerforming = held;
        }

        private void Update()
        {
            
            if (_crouchIsHeld && !_sprintIsPerforming)
                _networkCharacterStateMachine.movementState = NetworkCharacterStateMachine.MovementStates.Crouching;

            if (_walkIsPerforming && !_crouchIsHeld && !_sprintIsPerforming)
                _networkCharacterStateMachine.movementState = NetworkCharacterStateMachine.MovementStates.Walking;

            if (_sprintIsPerforming && !_crouchIsHeld && _walkIsPerforming)
                _networkCharacterStateMachine.movementState = NetworkCharacterStateMachine.MovementStates.Sprinting;


            if (!_walkIsPerforming && !_crouchIsHeld && !_sprintIsPerforming)
            {
                _networkCharacterStateMachine.movementState = NetworkCharacterStateMachine.MovementStates.Idle;
            }
        }
    }
}

