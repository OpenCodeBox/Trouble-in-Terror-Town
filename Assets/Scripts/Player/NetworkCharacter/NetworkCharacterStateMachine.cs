using UnityEngine;
using Mirror;
    
namespace TTTSC.Player.NetworkedCharacter
{
    public class NetworkCharacterStateMachine : NetworkBehaviour
    {
        [SyncVar]
        public CharacterStates characterState;
        [SyncVar]
        public MovementStates movementState;
        public bool ceilingDetected;
        public bool eligibleForStep;
        [Header("Ladder bools")]
        public bool onLadder;
        public bool topOnLadder;
        public bool bottomOnLadder;
        public bool enteredLadderFromBottom;
        public bool enteredLadderFromTop;

        [HideInInspector]
        public Transform topLadder;
        [HideInInspector]
        public Transform bottomLadder;

        //Grounded and InAir is currently set by CharacterHover script
        public enum CharacterStates
        {
            Grounded,
            InAir,
            InWater
        }

        public enum MovementStates
        {
            Idle,
            Walking,
            Crouching,
            Sprinting
        }

        public enum ActionState
        {
            InCar,
            InBoat,
            InProp
        }
    }
}
