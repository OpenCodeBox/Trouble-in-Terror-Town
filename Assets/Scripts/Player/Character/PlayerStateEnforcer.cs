using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TTTSC.Player.Character
{
    public class PlayerStateEnforcer : MonoBehaviour
    {
        [SerializeField]
        private PlayerGhostReffrenceHub _playerGhostReffrenceHub;
        [SerializeField]
        private PlayerInfo.PlayerInfoData _playerInfoData;
        private PlayerGhost _playerGhost;
        private Rigidbody _characterRigidbody;
        [SerializeField][Tooltip("assign the 'alive' prefab here")]
        private GameObject _aliveBodyPrefab;
        [SerializeField][Tooltip("assign the 'dead' prefab here")]
        private GameObject _spectatorBodyPrefab;

        private GameObject _aliveBody;
        private GameObject _spectatorBody;

        private void Start()
        {
            _playerGhost = _playerGhostReffrenceHub.playerGhost;
            _playerInfoData = _playerGhost.playerInfoData;
            _characterRigidbody = _playerGhostReffrenceHub.characterRigidbody;
            CheckPlayerState();
        }

        void CheckPlayerState()
        {
            switch (_playerInfoData.currentPlayerPlayState)
            {
                case PlayerStateMachine.playerPlayStates.Spectator:
                    SpawnSpectatorPlayerBody();
                    _characterRigidbody.useGravity = false;
                    break;
                case PlayerStateMachine.playerPlayStates.Alive:
                    SpawnAlivePlayerBody();
                    _characterRigidbody.useGravity = true;
                    break;
            }
        }

        void CheckPlayerClass()
        {
            switch (_playerInfoData.currentPlayerClass)
            {
                case PlayerStateMachine.playerClass.Preparing:
                    //this is the default class for everyone before the round starts
                    break;
                case PlayerStateMachine.playerClass.Innocent:
                    //TODO
                    break;
                case PlayerStateMachine.playerClass.Detective:
                    //TODO
                    break;
                case PlayerStateMachine.playerClass.Traitor:
                    //TODO
                    break;
            }
        }

        void SpawnAlivePlayerBody()
        {
            _playerInfoData.helth = 100;
            if (_spectatorBody != null)
            {
                Destroy(_spectatorBody);
            }
            _aliveBody = Instantiate(_aliveBodyPrefab, transform.position, transform.rotation);

            _aliveBody.transform.SetParent(transform);
        }

        void SpawnSpectatorPlayerBody()
        {
            if (_aliveBody != null)
            {
                Destroy(_aliveBody);
                SpawnDeadBody();
            }

            _spectatorBody = Instantiate(_spectatorBodyPrefab, transform.position, transform.rotation);
            _spectatorBody.transform.SetParent(transform);
        }

        void SpawnDeadBody()
        {
            //Add logic for spawning ragdoll and the dead body
        }
    }
}