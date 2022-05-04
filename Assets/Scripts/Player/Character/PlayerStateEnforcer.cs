using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TTTSC.Player
{
    public class PlayerStateEnforcer : MonoBehaviour
    {
        private PlayerStateMachine _playerStateMachine;
        [SerializeField][Tooltip("assign the 'alive' prefab here")]
        private GameObject alivePlayerPrefab;
        private GameObject spectatorPlayerPrefab;


        void CheckPlayerState()
        {
            switch (_playerStateMachine.currentPlayerPlayState)
            {
                case PlayerStateMachine.playerPlayStates.Spectator:
                    //add logic for turning player in to a spectator
                    break;
                case PlayerStateMachine.playerPlayStates.Alive:
                    //add logic for making player "alive"
                    //basicaly just reset player's stats
                    break;
            }
        }

        void CheckPlayerClass()
        {
            switch (_playerStateMachine.currentPlayerClass)
            {
                case PlayerStateMachine.playerClass.Spectator:
                    //this class is used when player excedes the max AFK time before being turned in to a spectator
                    //or when player just wants to spactate the game.
                    break;
                case PlayerStateMachine.playerClass.Preparing:
                    //this is the default class for everyone right before the round starts
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

        void SpawnPlayerBody()
        {

        }

        void SpawnPlayerRagdoll()
        {

        }
    }
}