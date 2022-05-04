using UnityEngine;

namespace TTTSC.Player.Character
{
    public class PlayerGhostReffrenceHub : MonoBehaviour
    {
        [Header("-----------Misc------------")]
        public Rigidbody characterRigidbody;
        public Transform cameraTransform;
        [Header("----------Scripts-----------")]
        public PlayerStateEnforcer playerStateEnforcrer;
        public PlayerStateMachine playerStateMachine;
        public Controller.PlayerInputReceiver playerInputReceiver;
    }
}

