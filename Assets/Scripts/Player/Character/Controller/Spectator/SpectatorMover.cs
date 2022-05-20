using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TTTSC.Player.Character.Controller
{
    public class SpectatorMover : MonoBehaviour
    {

        private PlayerGhostReffrenceHub _playerGhostReffrenceHub;
        private Transform _cameraTransform;
        private Rigidbody _characterRigidbody;
        [SerializeField]
        private SpectatorMoveConfig _spectatorMoveConfig;
        [SerializeField]
        private CharacterStateMachine _characterStateMachine;
        private PlayerInputReceiver _playerInputReceiver;

        private bool _performingFlyUp;
        private bool _performingFlyDown;

        private bool _performingMoveInput;
        private Vector2 _moveDirection;

        private void Start()
        {
            _playerGhostReffrenceHub = GetComponentInParent<PlayerGhostReffrenceHub>();
            _cameraTransform = Camera.main.transform;
            _characterRigidbody = _playerGhostReffrenceHub.characterRigidbody;
            _playerInputReceiver = _playerGhostReffrenceHub.playerInputReceiver;
            _playerInputReceiver.MoveInputEvent += MoveInput;
            _playerInputReceiver.SpectatorFlyUpInputEvent += FlyUpInput;
            _playerInputReceiver.SpectatorFlyDownInputEvent += FlyDownInput;
        }

        private void OnDisable()
        {
            _playerInputReceiver.MoveInputEvent -= MoveInput;
            _playerInputReceiver.SpectatorFlyUpInputEvent -= FlyUpInput;
            _playerInputReceiver.SpectatorFlyDownInputEvent -= FlyDownInput;
        }

        private void OnDestroy()
        {
            _playerInputReceiver.MoveInputEvent -= MoveInput;
            _playerInputReceiver.SpectatorFlyUpInputEvent -= FlyUpInput;
            _playerInputReceiver.SpectatorFlyDownInputEvent -= FlyDownInput;
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            Drag();

            Move();

            if (_performingFlyUp)
            {
                FlyUp();
            }

            if (_performingFlyDown)
            {
                FlyDown();
            }
        }

        #region input
        private void MoveInput(Vector2 moveDirection, bool performing)
        {
            _performingMoveInput = performing;
            _moveDirection = new Vector2(moveDirection.x, moveDirection.y);
        }

        private void FlyUpInput(bool performing)
        {
            _performingFlyUp = performing;
        }

        private void FlyDownInput(bool performing)
        {
            _performingFlyDown = performing;
        }
        #endregion

        private void Drag()
        {
            float multiplier = 1.0f - _spectatorMoveConfig.spectatorDrag * Time.fixedDeltaTime;
            if (multiplier < 0.0f) multiplier = 0.0f;

            Vector3 newVelocity = new(_characterRigidbody.velocity.x * multiplier, _characterRigidbody.velocity.y * multiplier, multiplier * _characterRigidbody.velocity.z);

            _characterRigidbody.velocity = newVelocity;
        }

        private void Move()
        {
            switch (_characterStateMachine.movementStates)
            {
                case CharacterStateMachine.MovementStates.Walking:
                    FlyNormal();
                    break;

                case CharacterStateMachine.MovementStates.Crouching:
                    FlySlow();
                    break;

                case CharacterStateMachine.MovementStates.Sprinting:
                    FlyFast();
                    break;
            }

        }

        private void FlySlow()
        {
            Vector3 movement = _moveDirection.x * Time.deltaTime * _cameraTransform.right + _moveDirection.y * Time.deltaTime * _cameraTransform.forward;

            float slowDown = _spectatorMoveConfig.spectatorNormalFlightSpeed / _spectatorMoveConfig.spectatorSlowFlightSpeed;

            _characterRigidbody.AddForce(movement.normalized * slowDown, ForceMode.VelocityChange); 
        }

        private void FlyNormal()
        {
            Vector3 movement = _moveDirection.x * Time.deltaTime * _cameraTransform.right + _moveDirection.y * Time.deltaTime * _cameraTransform.forward;
            _characterRigidbody.AddForce(movement.normalized * _spectatorMoveConfig.spectatorNormalFlightSpeed, ForceMode.VelocityChange);
        }

        private void FlyFast()
        {
            Vector3 movement = _moveDirection.x * Time.deltaTime * _cameraTransform.right + _moveDirection.y * Time.deltaTime * _cameraTransform.forward;

            float speedUp = _spectatorMoveConfig.spectatorNormalFlightSpeed * _spectatorMoveConfig.spectatorFastFlightSpeed;

            _characterRigidbody.AddForce(movement.normalized * speedUp, ForceMode.VelocityChange);

        }

        private void FlyUp()
        {
            switch (_characterStateMachine.movementStates)
            {
                case CharacterStateMachine.MovementStates.Idle:
                    _characterRigidbody.AddForce(_spectatorMoveConfig.flyUpSpeed * Vector3.up, ForceMode.VelocityChange);
                    break;

                case CharacterStateMachine.MovementStates.Walking:
                    _characterRigidbody.AddForce(_spectatorMoveConfig.flyUpSpeed * Vector3.up, ForceMode.VelocityChange);
                    break;

                case CharacterStateMachine.MovementStates.Crouching:
                    _characterRigidbody.AddForce((_spectatorMoveConfig.flyUpSpeed / _spectatorMoveConfig.spectatorSlowFlightSpeed) * Vector3.up, ForceMode.VelocityChange);
                    break;

                case CharacterStateMachine.MovementStates.Sprinting:
                    _characterRigidbody.AddForce((_spectatorMoveConfig.flyUpSpeed * _spectatorMoveConfig.spectatorFastFlightSpeed) * Vector3.up, ForceMode.VelocityChange);
                    break;
            }

        }

        private void FlyDown()
        {
            switch (_characterStateMachine.movementStates)
            {
                case CharacterStateMachine.MovementStates.Idle:
                    _characterRigidbody.AddForce(_spectatorMoveConfig.flyDownSpeed * Vector3.down, ForceMode.VelocityChange);
                    break;

                case CharacterStateMachine.MovementStates.Walking:
                    _characterRigidbody.AddForce(_spectatorMoveConfig.flyDownSpeed * Vector3.down, ForceMode.VelocityChange);
                    break;

                case CharacterStateMachine.MovementStates.Crouching:
                    _characterRigidbody.AddForce((_spectatorMoveConfig.flyDownSpeed / _spectatorMoveConfig.spectatorSlowFlightSpeed) * Vector3.down, ForceMode.VelocityChange);
                    break;

                case CharacterStateMachine.MovementStates.Sprinting:
                    _characterRigidbody.AddForce((_spectatorMoveConfig.flyDownSpeed * _spectatorMoveConfig.spectatorFastFlightSpeed) * Vector3.down, ForceMode.VelocityChange);
                    break;
            }

        }
    }
}

