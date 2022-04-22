using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TTTSC.Player.Character.Controller.Alive
{
    public class CharacterMover : MonoBehaviour
    {
        [SerializeField]
        private PlayerStateMachine _playerStateMachine;
        [SerializeField]
        private PlayerStateEnforcer _playerStateEnforcer;
        [SerializeField]
        private CharacterStateMachine _characterStateMachine;
        [SerializeField]
        private CharacterMovementConfig _characterConfig;
        [SerializeField]
        private CharacterHover _characterHover;
        [SerializeField]
        private PlayerInputReceiver _playerInputReceiver;
        private Rigidbody _characterRigidbody;
        [SerializeField, Tooltip("use Velocity mode for testing interactable objects + other things like pickackable items")]
        private MoveTypes moveType;
        [SerializeField]
        private ForceModes _hoverForceMode, _moveForceMode;

        private bool _performingMoveInput;
        private Vector3 _moveDirection;

        private void Awake()
        {
            _playerInputReceiver.MoveInputEvent += MoveInput;
        }

        void Start()
        {
            _characterRigidbody = _characterConfig.characterRigidbody;
        }

        #region Input event listeners

        private void MoveInput(Vector2 moveDirection, bool performing)
        {
            _performingMoveInput = performing;
            _moveDirection = new Vector3(moveDirection.x, 0, moveDirection.y);
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
            Debug.Log("player speed: " + _characterRigidbody.velocity.magnitude);

            Vector3 downVector = transform.TransformDirection(Vector3.down);


            switch (_hoverForceMode)
            {
                case ForceModes.force:
                    _characterRigidbody.AddForce(_characterHover.hoverForce * downVector, ForceMode.Force);
                    break;
                case ForceModes.acceleration:
                    _characterRigidbody.AddForce(_characterHover.hoverForce * downVector, ForceMode.Acceleration);
                    break;
                case ForceModes.impulse:
                    _characterRigidbody.AddForce(_characterHover.hoverForce * downVector, ForceMode.Impulse);
                    break;
                case ForceModes.velocityChange:
                    _characterRigidbody.AddForce(_characterHover.hoverForce * downVector, ForceMode.VelocityChange);
                    break;
            }

            switch (moveType)
            {
                case MoveTypes.Velocity:
                    VelocityChangeMover();
                    break;
                case MoveTypes.AddForce:
                    AddForceMover();
                    break;
            }

        }


        private void VelocityChangeMover()
        {
            
            Vector3 move = _characterConfig.moveSpeed * _moveDirection.x * Time.deltaTime * transform.right + _characterRigidbody.velocity.y * transform.up + _characterConfig.moveSpeed * _moveDirection.z * Time.deltaTime * transform.forward;
            if(move != Vector3.zero)
                Debug.Log(move);
            _characterRigidbody.velocity = move;
        }

        private void AddForceMover()
        {
            float desieredSpeed;

            /*
            switch (_moveForceMode)
            {
                case ForceModes.force:
                    _characterRigidbody.AddForce(movement, ForceMode.Force);
                    break;
                case ForceModes.acceleration:
                    _characterRigidbody.AddForce(movement, ForceMode.Acceleration);
                    break;
                case ForceModes.impulse:
                    _characterRigidbody.AddForce(movement, ForceMode.Impulse);
                    break;
                case ForceModes.velocityChange:
                    _characterRigidbody.AddForce(movement, ForceMode.VelocityChange);
                    break;
            }*/
        }
    }
}
