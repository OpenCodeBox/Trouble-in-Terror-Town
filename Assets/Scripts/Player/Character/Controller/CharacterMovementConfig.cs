using UnityEngine;

namespace TTTSC.Player.Character.Controller
{
    public class CharacterMovementConfig : MonoBehaviour
    {
        public Rigidbody characterRigidbody; // Rigidbody that will be used for moving character around
        public float moveSpeed; // This value controlls at what speed your character walks
        public float airControlStrength; // This value controlls strenght of the force applied to the player when in the air
        public float crouchSpeedDecrease; // This value controlls how much slower are you while crouching (equasion: walkSpeed / crouchSpeedDecrease) 
        public float sprintSpeedIncrease; // This value controlls how much faster are you while sprinting (equasion: walkSpeed * sprintSpeedIncrease)
        public float ladderClimbingSpeed; // This value controlls how fast dose the character climb ladders
        public float jumpPower; // This value controlls character's jump height
        public float crouchHeight; // This value controlls the hight of player when crouched
        public float crouchSmoothing; // This value controlls the transition speed of standing to crouch and viceversa
        public float lookVerticalSpeed; // This value controlls vertical looking speed
        public float lookHorizontalSpeed; // This value controlls horizontal looking speed
        public float aimVerticalSpeed; // This value controlls vertical looking speed while aiming down the sight
        public float aimHorizontalSpeed; // This value controlls horizontal looking speed while aiming down the sight
        public float stepSmoothing; // This value controlls the time value of lerp in CameraSmoother
        public float stepHeight; // This value controlls how high steps charecter takes
        public bool allowSprint; // bool for enabling sprint
    }
}