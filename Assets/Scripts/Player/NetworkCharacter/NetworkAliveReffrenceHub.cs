using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using TTTSC.Player.Character.Controller;

namespace TTTSC.Player.NetworkedCharacter
{
    public class NetworkAliveReffrenceHub : MonoBehaviour
    {
        public NetworkIdentity aliveNetworkIdentity;
        public CharacterMovementConfig networkCharacterMovementConfig;
        public NetworkCharacterStateMachine networkCharacterStateMachine;
        public NetworkCharacterHover networkCharacterHover;
        public NetworkCharacterStateChanger networkCharacterStateChanger;
    }
}

