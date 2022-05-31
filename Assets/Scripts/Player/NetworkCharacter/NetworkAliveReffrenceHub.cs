using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using TTTSC.Player.Character.Controller;

namespace TTTSC.Player.NetworkCharacter
{
    public class NetworkAliveReffrenceHub : MonoBehaviour
    {
        public CharacterMovementConfig characterMovementConfig;
        public NetworkCharacterStateMachine characterStateMachine;
        public NetworkCharacterHover characterHover;
        public NetworkCharacterStateChanger characterStateChanger;
    }
}