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

        private bool _walkIsPerforming, _crouchIsPerforming, _sprintIsPerforming;

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

        private void Crouch(bool performing)
        {
            _crouchIsPerforming = performing;

            if (!_sprintIsPerforming)
                _characterStateMachine.movementStates = CharacterStateMachine.MovementStates.Crouching;
        }

        private void Sprint(bool performing)
        {
            _sprintIsPerforming = performing;


        }

        private void Update()
        {

            if (_walkIsPerforming && !_crouchIsPerforming && !_sprintIsPerforming)
                _characterStateMachine.movementStates = CharacterStateMachine.MovementStates.Walking;

            if (_sprintIsPerforming && !_crouchIsPerforming && _walkIsPerforming)
                _characterStateMachine.movementStates = CharacterStateMachine.MovementStates.Sprinting;


            if (!_walkIsPerforming && !_crouchIsPerforming && !_sprintIsPerforming)
            {
                _characterStateMachine.movementStates = CharacterStateMachine.MovementStates.Idle;
            }
        }
    }
}

