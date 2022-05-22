using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

namespace TTTSC.Player.Character.Controller
{
    [RequireComponent(typeof(CharacterStateMachine))]
    public class CharacterStateChanger : NetworkBehaviour
    {
        private PlayerGhostReffrenceHub _playerGhostReffrenceHub;
        private PlayerInputReceiver _playerInputReceiver;
        private CharacterStateMachine _characterStateMachine;

        private bool _walkIsPerforming, _crouchIsHeld, _sprintIsPerforming;

        private void Start()
        {
            _playerGhostReffrenceHub = GetComponentInParent<PlayerGhostReffrenceHub>();
            _playerInputReceiver = _playerGhostReffrenceHub.playerInputReceiver;
            _characterStateMachine = GetComponent<CharacterStateMachine>();
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
            if (isLocalPlayer)
            {
                Debug.Log("I am the local player");
                
                if (_crouchIsHeld && !_sprintIsPerforming)
                {
                    CmdCrouch();
                }

                if (_walkIsPerforming && !_crouchIsHeld && !_sprintIsPerforming)
                    _characterStateMachine.movementStates = CharacterStateMachine.MovementStates.Walking;

                if (_sprintIsPerforming && !_crouchIsHeld && _walkIsPerforming)
                    _characterStateMachine.movementStates = CharacterStateMachine.MovementStates.Sprinting;


                if (!_walkIsPerforming && !_crouchIsHeld && !_sprintIsPerforming)
                {
                    _characterStateMachine.movementStates = CharacterStateMachine.MovementStates.Idle;
                }
            }

        }

        [Command]
        void CmdCrouch()
        {
            RpcCrouch();
        }
        
        [ClientRpc]
        void RpcCrouch()
        {
            _characterStateMachine.movementStates = CharacterStateMachine.MovementStates.Crouching;
        }
    }
}

