using UnityEngine;

namespace TTTSC.Player.Character
{
    public class CharacterReffrenceHub : MonoBehaviour
    {
        public Rigidbody characterRigidbody;
        public PlayerStateEnforcer playerStateEnforcrer;
        public PlayerStateMachine playerStateMachine;
        public Controller.CharacterMovementConfig characterMovementConfig;
        public Controller.CharacterStateMachine characterStateMachine;
        public Controller.PlayerInputReceiver playerInputReceiver;
        public Controller.CharacterHover characterHover;
    }
}

