using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using TTTSC.Player.Character;
using TTTSC.Player.Character.Controller;

namespace TTTSC.Player.NetworkCharacter
{
    public class NetworkCharacterMover : NetworkBehaviour
    {
        [SerializeField]
        private NetworkPlayerGhostReffrenceHub _playerGhostReffrenceHub;
        
        [SerializeField]
        private NetworkAliveReffrenceHub _aliveReffrenceHub;
        private PlayerStateMachine _playerStateMachine;
        private NetworkPlayerStateEnforcer _playerStateEnforcer;
        private NetworkCharacterStateMachine characterStateMachine;
        private CharacterMovementConfig _characterMovementConfig;
        private NetworkCharacterHover _characterHover;
        private PlayerInputReceiver _playerInputReceiver;
        private Rigidbody _characterRigidbody;


        [SerializeField]
        private bool _autoB_Hop;

        [SerializeField]
        private ForceModes _hoverForceMode, _moveForceMode;

        [SerializeField]
        private float _characterDrag;

        private float _jumpStageValue;
        private bool _jumpInputHeld;
        private bool _performingMoveInput;
        private Vector3 _moveDirection;

        private void Start()
        {
            _playerGhostReffrenceHub = GetComponentInParent<NetworkPlayerGhostReffrenceHub>();
            _characterRigidbody = _playerGhostReffrenceHub.characterRigidbody;
            _playerStateMachine = _playerGhostReffrenceHub.playerStateMachine;
            _playerStateEnforcer = _playerGhostReffrenceHub.playerStateEnforcrer;
            characterStateMachine = _aliveReffrenceHub.characterStateMachine;
            _characterMovementConfig = _aliveReffrenceHub.characterMovementConfig;
            _characterHover = _aliveReffrenceHub.characterHover;
            _playerInputReceiver = _playerGhostReffrenceHub.playerInputReceiver;


            _playerInputReceiver.MoveInputEvent += MoveInput;
            _playerInputReceiver.JumpInputEvent += JumpInput;

            _characterRigidbody = _playerGhostReffrenceHub.characterRigidbody;
        }

        private void OnDisable()
        {
            if (_playerInputReceiver != null)
            {
                _playerInputReceiver.MoveInputEvent -= MoveInput;
                _playerInputReceiver.JumpInputEvent -= JumpInput;
            }
        }

        #region Input event listeners

        private void MoveInput(Vector2 moveDirection, bool performing)
        {
            _performingMoveInput = performing;
            _moveDirection = new Vector2(moveDirection.x, moveDirection.y);
        }

        //Remember to change jumpPower from 0 to some other number that preferably is in the positives and not negatives
        private void JumpInput(bool performed)
        {
            _jumpInputHeld = performed;
        }

        #endregion

        enum ForceModes
        {
            force,
            acceleration,
            impulse,
            velocityChange

        }

        private void FixedUpdate()
        {

            Vector3 downVector = transform.TransformDirection(Vector3.down);

            Drag();

            switch (characterStateMachine.characterState)
            {
                case NetworkCharacterStateMachine.CharacterStates.Grounded:
                    if (characterStateMachine.movementState == NetworkCharacterStateMachine.MovementStates.Idle)
                    {
                        _characterDrag = _characterMovementConfig.idleDrag;
                    }
                    else
                    {
                        _characterDrag = _characterMovementConfig.moveDrag;
                    }

                    Move();

                    Jump();

                    break;
                case NetworkCharacterStateMachine.CharacterStates.InAir:
                    _characterDrag = _characterMovementConfig.inAirDrag;
                    InAirMove();

                    break;
            }


            _characterRigidbody.AddForce(_characterHover.hoverForce * downVector, ForceMode.VelocityChange);


        }

        private void Drag()
        {
            float multiplier = 1.0f - _characterDrag * Time.fixedDeltaTime;
            if (multiplier < 0.0f) multiplier = 0.0f;

            Vector3 newVelocity = new(_characterRigidbody.velocity.x * multiplier, _characterRigidbody.velocity.y, multiplier * _characterRigidbody.velocity.z);

            _characterRigidbody.velocity = newVelocity;
        }

        private void Move()
        {
            switch (characterStateMachine.movementState)
            {
                case NetworkCharacterStateMachine.MovementStates.Walking:
                    Walking();
                    break;

                case NetworkCharacterStateMachine.MovementStates.Crouching:
                    Crouching();
                    break;

                case NetworkCharacterStateMachine.MovementStates.Sprinting:
                    Sprinting();
                    break;
            }
        }

        private void InAirMove()
        {
            Vector3 movement = _moveDirection.x * Time.deltaTime * transform.right + _moveDirection.y * Time.deltaTime * transform.forward;

            Vector3 normalizedMovement = movement.normalized * _characterMovementConfig.airControlForce;

            Vector3 counterForce = -movement.normalized * _characterMovementConfig.airControlCounterForce;




            if (characterStateMachine.movementState == NetworkCharacterStateMachine.MovementStates.Walking)
            {
                _characterRigidbody.AddForce(normalizedMovement, ForceMode.VelocityChange);
                _characterRigidbody.AddForce(counterForce, ForceMode.VelocityChange);
            }

        }

        private void Jump()
        {

            switch (_autoB_Hop)
            {
                case true:
                    if (_jumpInputHeld && characterStateMachine.movementState != NetworkCharacterStateMachine.MovementStates.Crouching)
                    {
                        _characterRigidbody.velocity = new Vector3(_characterRigidbody.velocity.x, 0f, _characterRigidbody.velocity.z);
                        _characterRigidbody.AddForce(_characterMovementConfig.jumpForce * transform.up, ForceMode.VelocityChange);
                    }
                    break;
                case false:
                    if (_jumpStageValue == 1 && characterStateMachine.movementState != NetworkCharacterStateMachine.MovementStates.Crouching)
                    {
                        _characterRigidbody.velocity = new Vector3(_characterRigidbody.velocity.x, 0f, _characterRigidbody.velocity.z);
                        _characterRigidbody.AddForce(_characterMovementConfig.jumpForce * transform.up, ForceMode.VelocityChange);
                    }
                    break;

            }


        }

        private void Walking()
        {
            Vector3 movement = _moveDirection.x * Time.deltaTime * transform.right + _moveDirection.y * Time.deltaTime * transform.forward;

            Vector3 normalizedMovement = movement.normalized * _characterMovementConfig.moveForce;

            //Vector3 counterForce = -movement.normalized * _characterMovementConfig.moveCounterForce;

            _characterRigidbody.AddForce(normalizedMovement, ForceMode.VelocityChange);
            //_characterRigidbody.AddForce(counterForce, ForceMode.VelocityChange);
        }

        private void Crouching()
        {
            //bool switched;

            Vector3 movement = _moveDirection.x * Time.deltaTime * transform.right + _moveDirection.y * Time.deltaTime * transform.forward;

            Vector3 normalizedMovement = movement.normalized * _characterMovementConfig.crouchMoveForce;

            //Vector3 counterForce = -movement.normalized * _characterMovementConfig.crouchMoveCounterForce;

            /*(switch (switched)
            {
                case false:
                    _characterRigidbody.AddForce(_characterRigidbody.velocity, ForceMode.VelocityChange);
                    switched = true;
                    break;
            }*/

            _characterRigidbody.AddForce(normalizedMovement, ForceMode.VelocityChange);
            //_characterRigidbody.AddForce(counterForce, ForceMode.VelocityChange);
        }

        private void Sprinting()
        {

            Vector3 movement = _moveDirection.x * Time.deltaTime * transform.right + _moveDirection.y * Time.deltaTime * transform.forward;

            Vector3 normalizedMovement = movement.normalized * _characterMovementConfig.sprintMoveForce;

            //Vector3 counterForce = -movement.normalized *_characterMovementConfig.sprintMoveCounterForce;

            _characterRigidbody.AddForce(normalizedMovement, ForceMode.VelocityChange);
            //_characterRigidbody.AddForce(counterForce, ForceMode.VelocityChange);
        }

    }
}
