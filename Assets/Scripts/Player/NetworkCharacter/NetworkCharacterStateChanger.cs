using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TTTSC.Player.Character.Controller;
using TTTSC.Player.Character;
using Mirror;

namespace TTTSC.Player.NetworkedCharacter
{
    public class NetworkCharacterStateChanger : NetworkBehaviour
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

            Debug.Log(isLocalPlayer);
        }

        #region input
        private void Walk(Vector2 direction, bool performing)
        {
            if (isLocalPlayer)
            {
                _walkIsPerforming = performing;
            }
        }

        private void Crouch(bool performing)
        {
            if (isLocalPlayer)
            {
                Debug.Log("Crouch event triggered with status " + performing);
                _crouchIsHeld = performing;
            }
        }

        private void Sprint(bool held)
        {
            if (isLocalPlayer)
            {
                Debug.Log("Sprint event triggered with status " + held);
                _sprintIsPerforming = held;
            }
        }
        #endregion

        private void Update()
        {
            //Debug.Log("Local player is updating");
            if (!isLocalPlayer)
            {
                Debug.LogError("This is not the local player");
                return;
            }

            if (_crouchIsHeld && !_sprintIsPerforming)
            {
                Debug.Log("asking server to crouch");
                CmdCrouch();
            }

            if (_walkIsPerforming && !_crouchIsHeld && !_sprintIsPerforming)
            {
                CmdWalk();
            }

            if (_sprintIsPerforming && !_crouchIsHeld && _walkIsPerforming)
            {
                CmdSprint();
            }

            if (!_walkIsPerforming && !_crouchIsHeld && !_sprintIsPerforming)
            {
                CmdIdle();
            }

        }

        #region client
        [Command]
        void CmdCrouch()
        {
            Debug.Log("asking clients to crouch");
            RpcCrouch();
        }

        [Command]
        void CmdWalk()
        {
            RpcWalk();
        }

        [Command]
        void CmdSprint()
        {
            RpcSprint();
        }

        [Command]
        void CmdIdle()
        {
            RpcIdle();
        }
        #endregion


        #region server
        [ClientRpc]
        void RpcCrouch()
        {
            Debug.Log("crouching");
            _networkCharacterStateMachine.movementState = NetworkCharacterStateMachine.MovementStates.Crouching;
        }

        [ClientRpc]
        void RpcWalk()
        {
            _networkCharacterStateMachine.movementState = NetworkCharacterStateMachine.MovementStates.Walking;
        }

        [ClientRpc]
        void RpcSprint()
        {
            _networkCharacterStateMachine.movementState = NetworkCharacterStateMachine.MovementStates.Sprinting;
        }

        [ClientRpc]
        void RpcIdle()
        {
            _networkCharacterStateMachine.movementState = NetworkCharacterStateMachine.MovementStates.Idle;
        }
        #endregion
    }
}

