using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TTTSC.Player.Character.Controller;
using TTTSC.Player.Character;
using Mirror;

namespace TTTSC.Player.NetworkCharacter
{
    public class NetworkCharacterStateChanger : NetworkBehaviour
    {
        private NetworkIdentity _parentNetworkIdentity;
        private NetworkIdentity _networkIdentity;
        private NetworkPlayerGhostReffrenceHub _playerGhostReffrenceHub;
        private PlayerInputReceiver _playerInputReceiver;
        private NetworkCharacterStateMachine _networkCharacterStateMachine;
        private TTTSC_NetworkManager _networkManager;
        
        private bool _walkIsPerforming, _crouchIsHeld, _sprintIsPerforming;

        private void Start()
        {
            
            // Get the player network manager, if unable to find network manager then print error to console 
            try
            {
                _networkManager = FindObjectOfType<TTTSC_NetworkManager>();
            }
            catch (System.Exception e)
            {
                Debug.LogError($"Error, can't find network manager: {e}");
            }

            // Get the player ghost reffrence hub, if unable to find ghost reffrence hub then print error to console
            try
            {
                _playerGhostReffrenceHub = GetComponentInParent<NetworkPlayerGhostReffrenceHub>();
            }
            catch (System.Exception e)
            {
                Debug.LogError($"Error, can't find player ghost reffrence hub: {e}");
            }

            // Get the player input receiver, if unable to find input receiver then print error to console
            try
            {
                _playerInputReceiver = _playerGhostReffrenceHub.playerInputReceiver;
            }
            catch (System.Exception e)
            {
                Debug.LogError($"Error, can't find player input receiver: {e}");
            }

            // try to get the network character state machine, if unable to find network character state machine print error to console
            try
            {
                _networkCharacterStateMachine = GetComponent<NetworkCharacterStateMachine>();
            }
            catch (System.Exception e)
            {
                Debug.LogError($"Error, can't find network character state machine: {e}");
            }

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
            if (hasAuthority)
            {
                if (_crouchIsHeld && !_sprintIsPerforming)
                {
                    Debug.Log("he is attempting to crouch");
                    CmdCrouch();
                }

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


        [Command]
        void CmdCrouch()
        {
            Debug.Log("CrouchCmd");
            RpcCrouch();
        }

        [ClientRpc]
        void RpcCrouch()
        {
            Debug.Log("CrouchRpc");
            _networkCharacterStateMachine.movementState = NetworkCharacterStateMachine.MovementStates.Crouching;
        }
    }
}

