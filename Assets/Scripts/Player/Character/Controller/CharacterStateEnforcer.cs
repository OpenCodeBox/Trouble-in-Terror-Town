using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TTTSC.Player.Character.Controller
{
    public class CharacterStateEnforcer : MonoBehaviour
    {
        [SerializeField]
        private PlayerGhostReffrenceHub _aliveReffrenceHub;

        [SerializeField]
        private CapsuleCollider _characterEnviormentCollider;
        private CharacterMovementConfig _characterMovementConfig;
        private GroundCheck _characterHover;
        private CharacterStateMachine _characterStateMachine;


        // Start is called before the first frame update
        void Start()
        {
            _aliveReffrenceHub = GetComponentInParent<PlayerGhostReffrenceHub>();
            _characterHover = GetComponent<GroundCheck>();
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

            _characterEnviormentCollider.height = _characterMovementConfig.crouchedColliderHight;
            _characterEnviormentCollider.center = _characterMovementConfig.crouchedColliderPosition * transform.up;
        }

        private void CharacterDefault()
        {
            _characterHover.currentHoverHight = _characterMovementConfig.desieredHoverHight;

            _characterEnviormentCollider.height = _characterMovementConfig.standingColliderHight;
            _characterEnviormentCollider.center = _characterMovementConfig.standingColliderPosition * transform.up;
            
        }
    }
}

