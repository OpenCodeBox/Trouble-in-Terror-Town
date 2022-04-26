using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace TTTSC.Player.Character.Controller
{
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

        bool FloatBool(float a, string calculationOperator, float b)
        {
            bool result = false;

            switch (calculationOperator)
            {

                case "==":

                    if (a == b)
                    {
                        result = true;
                    }
                    else
                    {
                        result = false;
                    }
                    break;

                case "!=":

                    if (a != b)
                    {
                        result = true;
                    }
                    else
                    {
                        result = false;
                    }
                    break;

                case ">=":

                    if (a >= b)
                    {
                        result = true;
                    }
                    else
                    {
                        result = false;
                    }
                    break;

                case "<=":

                    if (a <= b)
                    {
                        result = true;
                    }
                    else
                    {
                        result = false;
                    }
                    break;

                case ">":

                    if (a > b)
                    {
                        result = true;
                    }
                    else
                    {
                        result = false;
                    }
                    break;

                case "<":

                    if (a < b)
                    {
                        result = true;
                    }
                    else
                    {
                        result = false;
                    }
                    break;
            }
            return result;
        }

        private void LookXInputReceiver(InputAction.CallbackContext ctx)
        {
            var value = ctx.ReadValue<float>();

            _lookX = value;
            Look(FloatBool(value, "!=", 0));
        }

        private void LookYInputReceiver(InputAction.CallbackContext ctx)
        {
            float value = ctx.ReadValue<float>();

            _lookY = value;
            Look(FloatBool(value, "!=", 0));
        }

        private void Look(bool performing)
        {
            var look = new Vector2(_lookX, _lookY);

            LookInputEvent?.Invoke(look, performing);
        }

        private void WalkInputReceiver(InputAction.CallbackContext ctx)
        {
            var value = ctx.ReadValue<Vector2>();

            bool performing = !(value == new Vector2(0, 0));

            MoveInputEvent?.Invoke(value, performing);
        }

        private void SprintInputReceiver(InputAction.CallbackContext ctx)
        {
            float value = ctx.ReadValue<float>();

            SprintInputEvent?.Invoke(FloatBool(value, "==", 1));
        }

        private void CrouchInputReceiver(InputAction.CallbackContext ctx)
        {
            float value = ctx.ReadValue<float>();

            CrouchInputEvent?.Invoke(FloatBool(value, "==", 1));
        }

        private void JumpInputReceiver(InputAction.CallbackContext ctx)
        {
            JumpInputEvent?.Invoke(ctx.ReadValue<bool>());
        }
    }
}
