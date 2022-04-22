using UnityEngine;

namespace TTTSC.Player.Character.Controller
{
    public class CharacterStateMachine : MonoBehaviour
    {
        public CharacterState characterState; 
        public MovementType movementType;
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
        public enum CharacterState
        {
            OnGround,
            InAir,
            InWater
        }

        public enum MovementType
        {
            Walk,
            Run,
            Crouch
        }

        public enum ActionState
        {
            InCar,
            InBoat,
            InProp
        }
    }
}
