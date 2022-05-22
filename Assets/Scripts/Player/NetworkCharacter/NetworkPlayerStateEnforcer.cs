using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TTTSC.Player.Character;
using TTTSC.Player.Character.PlayerCharacterInfo;
using Mirror;

namespace TTTSC.Player.NetworkedCharacter
{
    public class NetworkPlayerStateEnforcer : NetworkBehaviour
    {
        [SerializeField]
        private PlayerGhostReffrenceHub _playerGhostReffrenceHub;
        [SerializeField]
        private PlayerCharacterInfoData _playerInfoData;
        private Rigidbody _characterRigidbody;
        [SerializeField]
        [Tooltip("assign the 'alive' prefab here")]
        private GameObject _aliveBodyPrefab;
        [SerializeField]
        [Tooltip("assign the 'dead' prefab here")]
        private GameObject _spectatorBodyPrefab;

        private GameObject _aliveBody;
        private GameObject _spectatorBody;

        private void Start()
        {
            _playerInfoData = _playerGhostReffrenceHub.playerInfoData;
            _characterRigidbody = _playerGhostReffrenceHub.characterRigidbody;
            CheckPlayerState();
        }

        private void Update()
        {
            CheckPlayerState();
        }

        public void CheckPlayerState()
        {
            switch (_playerInfoData.currentPlayerPlayState)
            {
                case PlayerCharacterInfoData.playerPlayStates.Spectator:
                    SpawnSpectatorPlayerBody();
                    _characterRigidbody.useGravity = false;
                    break;
                case PlayerCharacterInfoData.playerPlayStates.Alive:
                    SpawnAlivePlayerBody();
                    _characterRigidbody.useGravity = true;
                    break;
            }
        }

        public void SpawnAlivePlayerBody()
        {
            _playerInfoData.helth = 100;
            if (_spectatorBody != null)
            {
                Destroy(_spectatorBody);
                Debug.Log("destroyed spectator body");
            }

            if (_aliveBody == null)
            {
                _aliveBody = Instantiate(_aliveBodyPrefab, transform.position, transform.rotation, transform);
                NetworkServer.Spawn(_aliveBody);
            }
        }

        public void SpawnSpectatorPlayerBody()
        {
            if (_aliveBody != null)
            {
                Destroy(_aliveBody);
                Debug.Log("destroyed alive body");
                SpawnDeadBody();
            }

            if (_spectatorBody == null)
            {

                _spectatorBody = Instantiate(_spectatorBodyPrefab, transform.position, transform.rotation, transform);
                NetworkServer.Spawn(_spectatorBody);
            }
        }

        void SpawnDeadBody()
        {
            //Add logic for spawning ragdoll and the dead body
        }
    }
}