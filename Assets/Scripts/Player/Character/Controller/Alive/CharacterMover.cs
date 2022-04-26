using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TTTSC.Player.Character.Controller.Alive
{
    public class CharacterMover : MonoBehaviour
    {
        [SerializeField]
        private CharacterReffrenceHub _ReffrenceHub;
        private PlayerStateMachine _playerStateMachine;
        private PlayerStateEnforcer _playerStateEnforcer;
        private CharacterStateMachine _characterStateMachine;
        private CharacterMovementConfig _characterMovementConfig;
        private CharacterHover _characterHover;
        private PlayerInputReceiver _playerInputReceiver;
        private Rigidbody _characterRigidbody;
        [SerializeField]
        private ForceModes _hoverForceMode, _moveForceMode;

        [SerializeField]
        private float _characterDrag;

        private bool _performingMoveInput;
        private Vector3 _moveDirection;

        private void Awake()
        {
            _playerStateMachine = _ReffrenceHub.playerStateMachine;
            _playerStateEnforcer = _ReffrenceHub.playerStateEnforcrer;
            _characterStateMachine = _ReffrenceHub.characterStateMachine;
            _characterMovementConfig = _ReffrenceHub.characterMovementConfig;
            _characterHover = _ReffrenceHub.characterHover;
            _playerInputReceiver = _ReffrenceHub.playerInputReceiver;
            _playerInputReceiver.MoveInputEvent += MoveInput;
        }

        void Start()
        {
            _characterRigidbody = _characterMovementConfig.characterRigidbody;
        }

        #region Input event listeners

        private void MoveInput(Vector2 moveDirection, bool performing)
        {
            _performingMoveInput = performing;
            _moveDirection = new Vector2(moveDirection.x, moveDirection.y);
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
            
            switch (_characterStateMachine.characterState)
            {
                case CharacterStateMachine.CharacterStates.Grounded:
                    _characterDrag = 10;

                    Move();

                    break;
                case CharacterStateMachine.CharacterStates.InAir:
                    _characterDrag = 0;
                    InAirMove();

                    break;
            }
            

            _characterRigidbody.AddForce(_characterHover.hoverForces * downVector, ForceMode.VelocityChange);


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

            switch (_characterStateMachine.movementStates)
            {
                case CharacterStateMachine.MovementStates.Walking:
                    Walking();
                    break;

                case CharacterStateMachine.MovementStates.Crouching:
                    Crouching();
                    break;

                case CharacterStateMachine.MovementStates.Sprinting:
                    Sprinting();
                    break;
            }


        }

        private void InAirMove()
        {
            Vector3 movement = _moveDirection.x * Time.deltaTime * transform.right + _moveDirection.y * Time.deltaTime * transform.forward;

            Vector3 normalizedMovement = movement.normalized * _characterMovementConfig.airControlStrength;



            if (_characterStateMachine.movementStates == CharacterStateMachine.MovementStates.Walking)
            {
                _characterRigidbody.AddForce(normalizedMovement, ForceMode.Impulse);
            }

        }

        private void Jump()
        {

        }

        private void Walking()
        {
            Vector3 movement = _moveDirection.x * Time.deltaTime * transform.right + _moveDirection.y * Time.deltaTime * transform.forward;

            Vector3 normalizedMovement = movement.normalized * _characterMovementConfig.moveSpeed;

            _characterRigidbody.AddForce(normalizedMovement, ForceMode.Impulse);
        }

        private void Crouching()
        {
            Vector3 movement = _moveDirection.x * Time.deltaTime * transform.right + _moveDirection.y * Time.deltaTime * transform.forward;

            Vector3 normalizedMovement = movement.normalized * _characterMovementConfig.moveSpeed / _characterMovementConfig.crouchSpeedDecrease;

            _characterRigidbody.AddForce(normalizedMovement, ForceMode.Impulse);
        }

        private void Sprinting()
        {
            Vector3 movement = _moveDirection.x * Time.deltaTime * transform.right + _moveDirection.y * Time.deltaTime * transform.forward;

            Vector3 normalizedMovement = movement.normalized * _characterMovementConfig.moveSpeed * _characterMovementConfig.sprintSpeedIncrease;

            _characterRigidbody.AddForce(normalizedMovement, ForceMode.Impulse);
        }

    }
}
