using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace TTTSC.Player.Character.Controller
{
    [RequireComponent(typeof(CharacterStateMachine))]
    public class PlayerInputReceiver : MonoBehaviour
    {
        private float _lookX, _lookY;
    
        public event Action<Vector2, bool> MoveInputEvent, LookInputEvent;
        public event Action<bool> SprintInputEvent, CrouchInputEvent, JumpInputEvent;


        public PlayerInputSender playerInputEvents;

        private void OnEnable()
        {
            playerInputEvents = new PlayerInputSender();
        
            playerInputEvents.Enable();
            playerInputEvents.Controlls.Walk.performed += WalkInputReceiver;
            playerInputEvents.Controlls.LookX.performed += LookXInputReceiver;
            playerInputEvents.Controlls.LookY.performed += LookYInputReceiver;
            playerInputEvents.Controlls.Sprint.performed += SprintInputReceiver;
            playerInputEvents.Controlls.Jump.performed += JumpInputReceiver;
            playerInputEvents.Controlls.Crouch.performed += CrouchInputReceiver;
        }

        private void OnDisable()
        {
            playerInputEvents.Disable();
            playerInputEvents.Controlls.Walk.performed -= WalkInputReceiver;
            playerInputEvents.Controlls.LookX.performed -= LookXInputReceiver;
            playerInputEvents.Controlls.Sprint.performed -= SprintInputReceiver;
            playerInputEvents.Controlls.Jump.performed -= JumpInputReceiver;
            playerInputEvents.Controlls.Crouch.performed -= CrouchInputReceiver;
        }

        private void LookXInputReceiver(InputAction.CallbackContext ctx)
        {
            var value = ctx.ReadValue<float>();
            bool performing = value != 0;

            _lookX = value;
            Look(performing);
        }

        private void LookYInputReceiver(InputAction.CallbackContext ctx)
        {
            var value = ctx.ReadValue<float>();
            bool performing = value != 0;

            _lookY = value;
            Look(performing);
        }

        private void Look(bool performing)
        {
            var look = new Vector2(_lookX, _lookY);

            LookInputEvent?.Invoke(look, performing);
        }

        private void WalkInputReceiver(InputAction.CallbackContext ctx)
        {
            var value = ctx.ReadValue<Vector2>();

            bool performing = !(value == new Vector2(0,0));

            MoveInputEvent?.Invoke(value, performing);
        }

        private void SprintInputReceiver(InputAction.CallbackContext ctx)
        {
            SprintInputEvent?.Invoke(ctx.ReadValue<bool>());
        }

        private void CrouchInputReceiver(InputAction.CallbackContext ctx)
        {
            CrouchInputEvent?.Invoke(ctx.ReadValue<bool>());
        }

        private void JumpInputReceiver(InputAction.CallbackContext ctx)
        {
            JumpInputEvent?.Invoke(ctx.ReadValue<bool>());
        }
    }
}
