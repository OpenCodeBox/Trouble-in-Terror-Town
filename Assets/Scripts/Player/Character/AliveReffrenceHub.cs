using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TTTSC.Player.Character
{
    public class AliveReffrenceHub : MonoBehaviour
    {


        public Controller.CharacterMovementConfig characterMovementConfig;
        public Controller.CharacterStateMachine characterStateMachine;
        public Controller.GroundCheck characterHover;
        public Controller.CharacterStateChanger characterStateChanger;
    }
}

