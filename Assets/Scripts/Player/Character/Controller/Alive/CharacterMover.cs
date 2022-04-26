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
        [SerializeField, Tooltip("use Velocity mode for testing interactable objects + other things like pickackable items")]
        private MoveTypes moveType;
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

        enum MoveTypes
        {
            Velocity,
            AddForce
        }

        enum ForceModes
        {
            force,
            acceleration,
            impulse,
            velocityChange

        }

        private void FixedUpdate()
        {

            //if (_characterRigidbody.velocity.x != 0f || _characterRigidbody.velocity.z != 0f)
            //Debug.Log("character speed x: " + _characterRigidbody.velocity.x + "character speed z:" + _characterRigidbody.velocity.z);

            Vector3 downVector = transform.TransformDirection(Vector3.down);

            Drag();
            
            switch (_characterStateMachine.characterState)
            {
                case CharacterStateMachine.CharacterStates.Grounded:
                    switch (moveType)
                    {
                        case MoveTypes.Velocity:
                            VelocityChangeMover();
                            break;
                        case MoveTypes.AddForce:
                            AddForceMover();
                            break;
                    }

                    break;
                case CharacterStateMachine.CharacterStates.InAir:
                    InAirMove();

                    break;
            }
            

            _characterRigidbody.AddForce(_characterHover.hoverForces * downVector, ForceMode.VelocityChange);




        }


        private void VelocityChangeMover()
        {

            Vector3 movement = _characterMovementConfig.moveSpeed * _moveDirection.x * Time.deltaTime * transform.right + _characterRigidbody.velocity.y * transform.up + _characterMovementConfig.moveSpeed * _moveDirection.y * Time.deltaTime * transform.forward;
            _characterRigidbody.velocity = movement;
        }

        private void AddForceMover()
        {


            switch (_characterStateMachine.movementStates)
            {
                case CharacterStateMachine.MovementStates.Walking:
                    Walk();
                    break;

                case CharacterStateMachine.MovementStates.Crouching:

                    break;

                case CharacterStateMachine.MovementStates.Sprinting:

                    break;
            }


        }

        private void Drag()
        {
            float multiplier = 1.0f - _characterDrag * Time.fixedDeltaTime;
            if (multiplier < 0.0f) multiplier = 0.0f;

            Vector3 newVelocity = new(_characterRigidbody.velocity.x * multiplier, _characterRigidbody.velocity.y, multiplier * _characterRigidbody.velocity.z);

            _characterRigidbody.velocity = newVelocity;
        }

        private void Walk()
        {
            Vector3 movement = _characterMovementConfig.moveSpeed * _moveDirection.x * Time.deltaTime * transform.right + _characterMovementConfig.moveSpeed * _moveDirection.y * Time.deltaTime * transform.forward;

            if (_characterStateMachine.characterState == CharacterStateMachine.CharacterStates.Grounded)
                switch (_moveForceMode)
                {
                    case ForceModes.force:
                        _characterRigidbody.AddForce(movement, ForceMode.Force);
                        break;
                    case ForceModes.acceleration:
                        _characterRigidbody.AddForce(movement, ForceMode.Acceleration);
                        break;
                    case ForceModes.impulse:
                        _characterRigidbody.AddForce(movement.normalized, ForceMode.Impulse);
                        break;
                    case ForceModes.velocityChange:
                        _characterRigidbody.AddForce(movement.normalized, ForceMode.VelocityChange);
                        break;
                }
        }

        private void InAirMove()
        {
            Vector3 movement = _characterMovementConfig.airControlStrength * _moveDirection.x * Time.deltaTime * transform.right + _characterMovementConfig.airControlStrength * _moveDirection.y * Time.deltaTime * transform.forward;

            if (_characterStateMachine.movementStates == CharacterStateMachine.MovementStates.Walking)
            {
                _characterRigidbody.AddForce(movement.normalized, ForceMode.Impulse);
            }

        }
    }
}
