using System.Collections;
using System.Collections.Generic;
using TTTSC.Player.Character.Controller;
using UnityEngine;

namespace TTTSC.Player.NetworkCharacter
{
    public class NetworkCharacterStateEnforcer : MonoBehaviour
    {

        [SerializeField]
        private Transform _characterEnviormentCollider;
        private CharacterMovementConfig _characterMovementConfig;
        private NetworkCharacterHover _characterHover;
        private NetworkCharacterStateMachine _characterStateMachine;


        // Start is called before the first frame update
        void Start()
        {
            _characterMovementConfig = GetComponent<CharacterMovementConfig>();
            _characterHover = GetComponent<NetworkCharacterHover>();
            _characterStateMachine = GetComponent<NetworkCharacterStateMachine>();
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            switch (_characterStateMachine.movementState)
            {
                default:
                    CharacterDefault();
                    break;
                case NetworkCharacterStateMachine.MovementStates.Crouching:
                    CharacterCrouch();
                    break;
            }
        }

        private void CharacterCrouch()
        {
            _characterHover.currentHoverHight = _characterMovementConfig.crouchHeight;

            _characterEnviormentCollider.localScale = new Vector3(_characterMovementConfig.crouchedColliderRadius, _characterMovementConfig.crouchedColliderHight, _characterMovementConfig.crouchedColliderRadius);
            _characterEnviormentCollider.localPosition = _characterMovementConfig.crouchedColliderPosition * transform.up;
        }

        private void CharacterDefault()
        {
            _characterHover.currentHoverHight = _characterMovementConfig.desieredHoverHight;

            _characterEnviormentCollider.localScale = new Vector3(_characterMovementConfig.standingColliderRadius, _characterMovementConfig.standingColliderHight, _characterMovementConfig.standingColliderRadius);
            _characterEnviormentCollider.localPosition = _characterMovementConfig.standingColliderPosition * transform.up;

        }
    }
}

