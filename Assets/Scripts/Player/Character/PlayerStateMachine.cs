using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TTTSC.Player
{
    public class PlayerStateMachine : MonoBehaviour
    {
        public enum playerGameStates
        {
            Dead,
            Alive
        };

        public playerGameStates currentPlayerGameState;

        public enum playerClass
        {
            Spectator,
            Preparing,
            Innocent,
            Detective,
            Traitor
        };

        public playerClass currentPlayerClass;
    }
}

