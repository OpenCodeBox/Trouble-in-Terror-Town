using UnityEngine;

namespace TTTSC.Player.Character.Controller
{
    public class CharacterMovementConfig : MonoBehaviour
    {
        public Rigidbody characterRigidbody; // Rigidbody that will be used for moving character around

        [Header("On Ground")]
        public float moveForce; // This value controlls at what speed your character walks
        public float moveCounterForce; // This value controlls at what speed your character walks
        public float crouchMoveForce; // This value controlls how much slower are you while crouching (equasion: walkSpeed / crouchSpeedDecrease) 
        public float crouchMoveCounterForce; // This value controlls how much slower are you while crouching (equasion: walkSpeed / crouchSpeedDecrease) 
        public float sprintMoveForce; // This value controlls how much faster are you while sprinting (equasion: walkSpeed * sprintSpeedIncrease)
        public float sprintMoveCounterForce; // This value controlls h                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                             ow much faster are you while sprinting (equasion: walkSpeed * sprintSpeedIncrease)
        public float ladderClimbingForce; // This value controlls how fast dose the character climb ladders
        public float desieredHoverHight;
        public float crouchHeight; // This value controlls the hight of player when crouched
        public float crouchSmoothing; // This value controlls the transition speed of standing to crouch and viceversa
        public bool allowSprint; // bool for enabling sprint
        public float jumpForce; // This value controlls character's jump height

        [Header("Drag")]
        public float idleDrag;
        public float moveDrag;
        public float inAirDrag;
        
        [Header("Hover")]
        public float groundCheckLength;
        public float hoverStrenght;
        public float hoverDampening;

        [Header("Misc")]
        public float airControlForce; // This value controlls strenght of the force applied to the player when in the air
        public float airControlCounterForce; // This value controlls strenght of the force applied to the player when in the air
        public float lookVerticalSpeed; // This value controlls vertical looking speed
        public float lookHorizontalSpeed; // This value controlls horizontal looking speed
        public float aimVerticalSpeed; // This value controlls vertical looking speed while aiming down the sight
        public float aimHorizontalSpeed; // This value controlls horizontal looking speed while aiming down the sight
    }
} 