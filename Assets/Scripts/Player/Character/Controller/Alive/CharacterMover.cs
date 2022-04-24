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




            //if (_characterRigidbody.velocity.x != 0f || _characterRigidbody.velocity.z != 0f)
            //Debug.Log("character speed x: " + _characterRigidbody.velocity.x + "character speed z:" + _characterRigidbody.velocity.z);

            Vector3 downVector = transform.TransformDirection(Vector3.down);

            _characterRigidbody.drag = _characterDrag;

            switch (_characterStateMachine.characterState)
            {
                case CharacterStateMachine.CharacterStates.Grounded:
                    _characterRigidbody.drag = _characterDrag;
                    break;
                default:
                    _characterRigidbody.drag = 0;
                    break;
            }
            

            _characterRigidbody.AddForce(_characterHover.hoverForces * downVector, ForceMode.VelocityChange);

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

            Vector3 movement = _characterMovementConfig.moveSpeed * _moveDirection.x * Time.deltaTime * transform.right + _characterRigidbody.velocity.y * transform.up + _characterMovementConfig.moveSpeed * _moveDirection.z * Time.deltaTime * transform.forward;
            _characterRigidbody.velocity = movement;
        }

        private void AddForceMover()
        {
            Vector3 movement = _characterMovementConfig.moveSpeed * _moveDirection.x * Time.deltaTime * transform.right + _characterMovementConfig.moveSpeed * _moveDirection.z * Time.deltaTime * transform.forward;



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
                        _characterRigidbody.AddForce(movement, ForceMode.VelocityChange);
                        break;
                }
        }

        private void Walk()
        {

        }
    }
}
