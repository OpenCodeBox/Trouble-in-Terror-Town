using System.Collections.Generic;
using UnityEngine;

namespace TTTSC.Player.Character
{
    public class PlayerGhostReffrenceHub : MonoBehaviour
    {
        [Header("-----------Misc------------")]
        public Rigidbody characterRigidbody;
        public Transform cameraTransform;
        public PlayerCharacterInfo.PlayerCharacterInfoData playerInfoData;
        
        [Header("----------Scripts-----------")]
        public PlayerStateEnforcer playerStateEnforcrer;
        public PlayerStateMachine playerStateMachine;
        public Controller.PlayerInputReceiver playerInputReceiver;
        public GameManager gameManager;
        public RoundSystem roundSystem;

        void Awake()
        {
            playerInfoData = ScriptableObject.CreateInstance<PlayerCharacterInfo.PlayerCharacterInfoData>();
            gameManager = FindObjectOfType<GameManager>();
            roundSystem = FindObjectOfType<RoundSystem>();
        }
    }
}