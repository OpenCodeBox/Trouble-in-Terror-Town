using System.Collections.Generic;
using TTTSC.Player.Character;
using TTTSC.Player.Character.Controller;
using TTTSC.Player.Character.PlayerCharacterInfo;
using UnityEngine;

namespace TTTSC.Player.NetworkCharacter
{
    public class NetworkPlayerGhostReffrenceHub : MonoBehaviour
    {
        [Header("-----------Misc------------")]
        public Rigidbody characterRigidbody;
        public Transform cameraTransform;
        public PlayerCharacterInfoData playerInfoData;
        
        [Header("----------Scripts-----------")]
        public NetworkPlayerStateEnforcer playerStateEnforcrer;
        public PlayerStateMachine playerStateMachine;
        public PlayerInputReceiver playerInputReceiver;
        public GameManager gameManager;
        public RoundSystem roundSystem;

        void Awake()
        {
            playerInfoData = ScriptableObject.CreateInstance<PlayerCharacterInfoData>();
            gameManager = FindObjectOfType<GameManager>();
            roundSystem = FindObjectOfType<RoundSystem>();
        }
    }
}