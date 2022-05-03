using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TTTSC.Player.Character.Controller
{
    public class CharacterStateChanger : MonoBehaviour
    {
        [SerializeField]
        private CharacterReffrenceHub _characterReffrenceHub;
        private PlayerInputReceiver _playerInputReceiver;
        private CharacterStateMachine _characterStateMachine;

        private bool _walkIsPerforming, _crouchIsHeld, _sprintIsPerforming;

        private void Awake()
        {
            _playerInputReceiver = _characterReffrenceHub.playerInputReceiver;
            _characterStateMachine = _characterReffrenceHub.characterStateMachine;
        }

        private void Start()
        {
            _playerInputReceiver.MoveInputEvent += Walk;
            _playerInputReceiver.CrouchInputEvent += Crouch;
            _playerInputReceiver.SprintInputEvent += Sprint;
        }

        private void Walk(Vector2 direction, bool performing)
        {
            _walkIsPerforming = performing;
        }

        private void Crouch(bool performing, float stageValue)
        {
            _crouchIsHeld = performing;
        }

        private void Sprint(bool held, float stageValue)
        {
            _sprintIsPerforming = held;
        }

        private void Update()
        {

            if (_crouchIsHeld && !_sprintIsPerforming)
                _characterStateMachine.movementStates = CharacterStateMachine.MovementStates.Crouching;

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
}

