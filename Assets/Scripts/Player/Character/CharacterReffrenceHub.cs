using UnityEngine;

namespace TTTSC.Player.Character
{
    public class CharacterReffrenceHub : MonoBehaviour
    {
        [Header("-----------Misc------------")]
        public Rigidbody characterRigidbody;
        [Header("----------Scripts-----------")]
        public PlayerStateEnforcer playerStateEnforcrer;
        public PlayerStateMachine playerStateMachine;
        public Controller.CharacterMovementConfig characterMovementConfig;
        public Controller.CharacterStateMachine characterStateMachine;
        public Controller.PlayerInputReceiver playerInputReceiver;
        public Controller.CharacterHover characterHover;
        public Controller.CharacterStateChanger characterStateChanger;
    }
}

