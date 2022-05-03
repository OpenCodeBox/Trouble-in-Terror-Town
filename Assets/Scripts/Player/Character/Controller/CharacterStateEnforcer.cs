using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TTTSC.Player.Character.Controller
{
    public class CharacterStateEnforcer : MonoBehaviour
    {
        [SerializeField]
        private CharacterReffrenceHub _reffrenceHub;


        [Header("Normal")]
        [SerializeField]
        private Vector3 _desieredStandingColliderPosition;
        [SerializeField]
        private float _desieredStandingColliderHight;

        [Header("Crouched")]
        [SerializeField]
        private Vector3 _desieredCrouchedColliderPosition;
        [SerializeField]
        private float _desieredCrouchedColliderHight;
        

        [SerializeField]
        private CapsuleCollider _characterEnviormentCollider;
        private CharacterMovementConfig _characterMovementConfig;
        private CharacterHover _characterHover;
        private CharacterStateMachine _characterStateMachine;


        // Start is called before the first frame update
        void Start()
        {
            _characterHover = GetComponent<CharacterHover>();
            _characterMovementConfig = GetComponent<CharacterMovementConfig>();
            _characterStateMachine = GetComponent<CharacterStateMachine>();
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            switch (_characterStateMachine.movementStates)
            {
                default:
                    CharacterDefault();
                    break;
                case CharacterStateMachine.MovementStates.Crouching:
                    CharacterCrouch();
                    break;
            }
        }

        private void CharacterCrouch()
        {
            _characterHover.currentHoverHight = _characterMovementConfig.crouchHeight;

            _characterEnviormentCollider.height = _desieredCrouchedColliderHight;
            _characterEnviormentCollider.center = _desieredCrouchedColliderPosition;
        }

        private void CharacterDefault()
        {
            _characterHover.currentHoverHight = _characterMovementConfig.desieredHoverHight;

            _characterEnviormentCollider.height = _desieredStandingColliderHight;
            _characterEnviormentCollider.center = _desieredStandingColliderPosition;

        }
    }
}

