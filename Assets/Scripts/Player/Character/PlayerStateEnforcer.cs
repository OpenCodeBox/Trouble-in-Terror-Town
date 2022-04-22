using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TTTSC.Player
{
    public class PlayerStateEnforcer : MonoBehaviour
    {
        private PlayerStateMachine _playerStateMachine;
        [SerializeField][Tooltip("assign a body prefab to this")]
        private GameObject playerBodyPrefab;

        void CheckPlayerState()
        {
            switch (_playerStateMachine.currentPlayerGameState)
            {
                case PlayerStateMachine.playerGameStates.Dead:
                    //add logic for turning player in to a spectator
                    break;
                case PlayerStateMachine.playerGameStates.Alive:
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