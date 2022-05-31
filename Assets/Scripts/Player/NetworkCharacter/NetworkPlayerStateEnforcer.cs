using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TTTSC.Player.Character;
using TTTSC.Player.Character.PlayerCharacterInfo;
using Mirror;

namespace TTTSC.Player.NetworkCharacter
{
    public class NetworkPlayerStateEnforcer : NetworkBehaviour
    {
        [SerializeField]
        private NetworkPlayerGhostReffrenceHub _playerGhostReffrenceHub;
        [SerializeField]
        private PlayerCharacterInfoData _playerInfoData;
        private Rigidbody _characterRigidbody;
        [SerializeField]
        [Tooltip("assign the 'alive' prefab here")]
        private GameObject _aliveBodyPrefab;
        [SerializeField]
        [Tooltip("assign the 'dead' prefab here")]
        private GameObject _spectatorBodyPrefab;
        [SerializeField]
        private NetworkManagerValuieHolder _networkManagerValuieHolder;

        private GameObject _aliveBody;
        private GameObject _spectatorBody;

        private void Start()
        {
            try
            {
                _playerInfoData = _playerGhostReffrenceHub.playerInfoData;
            }
            catch (System.Exception e)
            {
                Debug.LogError($"Error: unable to set _networkManager, {e}");
            }
            _characterRigidbody = _playerGhostReffrenceHub.characterRigidbody;
            _networkManagerValuieHolder = FindObjectOfType<NetworkManagerValuieHolder>();

            CheckPlayerState();
        }

        private void Update()
        {
            CheckPlayerState();
        }

        public void CheckPlayerState()
        {
            if (isServer)
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
        }

        [ClientRpc]
        private void SpawnAlivePlayerBody()
        {
            
            if (_spectatorBody != null)
            {
                Destroy(_spectatorBody);
                Debug.Log("destroyed spectator body");
            }

            if (_aliveBody == null)
            {

                Debug.Log("Intantiated alive body");
                _aliveBody = Instantiate(_aliveBodyPrefab, transform.position, transform.rotation, transform);

                if (isServer)
                {
                    NetworkServer.Spawn(_aliveBody, connectionToClient);
                    Debug.Log("_aliveBody spawned by server");
                }

                if (_networkManagerValuieHolder == null)
                {
                    Debug.LogError("_networkManagerValuieHolder is null");
                    return;
                }
                else if (_networkManagerValuieHolder.playerObjectList.Count! > 0) 
                {
                    Debug.LogError("playerObjectList has no entries, aborting action");
                    return;
                }

                for (int playerObject = 0; playerObject < _networkManagerValuieHolder.playerObjectList.Count; playerObject++)
                {
                    if (_networkManagerValuieHolder.playerObjectList[playerObject].connectionId == connectionToClient.connectionId)
                    {
                        PlayerObject currentPlayerObject = _networkManagerValuieHolder.playerObjectList[playerObject];

                        currentPlayerObject.playerGhost = transform;
                        currentPlayerObject.playerBody = _aliveBody.transform;

                        break;
                    }
                }
            }
            else
            {
                if (_playerInfoData == null)
                {
                    Debug.LogError("_playerInfoData is a null reference");
                    return;
                }

                _playerInfoData.helth = 100;
            }
        }

        [ClientRpc]
        public void SpawnSpectatorPlayerBody()
        {

            if (!isServer)
                return;


            _playerInfoData.helth = 100;
            if (_aliveBody != null)
            {
                Destroy(_aliveBody);
                Debug.Log("destroyed alive body");
                SpawnDeadBody();
            }

            if (_spectatorBody == null)
            {


                Debug.Log("Intantiated alive body");
                _spectatorBody = Instantiate(_spectatorBodyPrefab, transform.position, transform.rotation, transform);

                if (isServer)
                {
                    NetworkServer.Spawn(_spectatorBody, connectionToClient);
                    Debug.Log("_spectator spawned by server");
                }

                for (int playerObject = 0; playerObject < _networkManagerValuieHolder.playerObjectList.Count; playerObject++)
                {
                    if (_networkManagerValuieHolder.playerObjectList[playerObject].connectionId == connectionToClient.connectionId)
                    {
                        PlayerObject currentPlayerObject = _networkManagerValuieHolder.playerObjectList[playerObject];

                        currentPlayerObject.playerGhost = transform;
                        currentPlayerObject.playerBody = _spectatorBody.transform;

                        break;
                    }
                }
            }
        }

        void SpawnDeadBody()
        {
            //Add logic for spawning ragdoll and the dead body
        }
    }
}