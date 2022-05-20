using UnityEngine;
using Mirror;
    
namespace TTTSC.Player.NetworkedCharacter
{
    public class NetworkCharacterStateMachine : NetworkBehaviour
    {
        public CharacterStates characterState;
        [SyncVar(hook = nameof(HandleMovementStateChange))]
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

        [Command]
        private void HandleMovementStateChange(MovementStates oldMovementState, MovementStates newMovementState)
        {
            RpcSetMovementState(newMovementState);
        }

        [ClientRpc]
        private void RpcSetMovementState(MovementStates newMovementState)
        {
            movementState = newMovementState;
        }

        private void HandleCharacterStateChange(CharacterStates oldCharacterState, CharacterStates newCharacterState)
        {

        }
    }
}
