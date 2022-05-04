using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TTTSC.Player
{
    public class PlayerStateMachine : MonoBehaviour
    {
        public enum playerPlayStates
        {
            Spectator,
            Alive
        };

        public playerPlayStates currentPlayerPlayState;

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

