using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TTTSC.Player.Character
{
    public class PlayerStateMachine : MonoBehaviour
    {
        public enum playerPlayStates
        {
            Alive,
            Spectator
        };

        public enum playerClass
        {
            Preparing,
            Innocent,
            Detective,
            Traitor
        };

    }
}

