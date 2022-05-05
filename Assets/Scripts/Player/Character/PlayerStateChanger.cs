using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TTTSC.Player.Character
{
    public class PlayerStateChanger : MonoBehaviour
    {
        private PlayerGhostReffrenceHub _playerGhostReffrenceHub;
        private GameManager _gameManager;
        private RoundSystem _roundSystem;
        public PlayerCharacterInfo.PlayerCharacterInfoData _playerInfoData;

        bool beforeRoundStart;

        // Start is called before the first frame update
        void Start()
        {
            _playerGhostReffrenceHub = GetComponent<PlayerGhostReffrenceHub>();
            _gameManager = _playerGhostReffrenceHub.gameManager;
            _roundSystem = _playerGhostReffrenceHub.roundSystem;
            _playerInfoData = _playerGhostReffrenceHub.playerInfoData;


            _playerInfoData.currentPlayerPlayState = PlayerCharacterInfo.PlayerCharacterInfoData.playerPlayStates.Alive;
        }

        // Update is called once per frame
        void Update()
        {
            /*
            if (!beforeRoundStart)
            {
                if (!_roundSystem.roundInProggress)
                {

                    beforeRoundStart = true;

                }
            }

            if (((DateTimeOffset)DateTime.UtcNow).ToUnixTimeSeconds() == _roundSystem.playerWaitTime)
            {
                if (!beforeRoundStart)
                {
                    _playerInfoData.currentPlayerPlayState = PlayerCharacterInfo.PlayerCharacterInfoData.playerPlayStates.Spectator;
                }
                else
                {
                    _playerInfoData.currentPlayerPlayState = PlayerCharacterInfo.PlayerCharacterInfoData.playerPlayStates.Alive;
                }
            }*/
            
        }
    }
}