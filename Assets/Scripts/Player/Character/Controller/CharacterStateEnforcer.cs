using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TTTSC.Player.Character.Controller
{
    public class CharacterStateEnforcer : MonoBehaviour
    {
        [SerializeField]
        private CharacterReffrenceHub _reffrenceHub;

        private CharacterHover _characterHover;
        private CharacterStateMachine _characterStateMachine;


        // Start is called before the first frame update
        void Start()
        {
            _characterHover = GetComponent<CharacterHover>();
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
            _characterHover.hoverHight = 1f;
        }

        private void CharacterDefault()
        {
            _characterHover.hoverHight = 1.4f;
        }
    }
}

