using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace TTTSC.Player.Character.Controller
{
    public class PlayerInputReceiver : MonoBehaviour
    {
        private float _lookX, _lookY;

        bool _sprintIsHeld, _crouchIsHeld, _jumpIsHeld;
        float _sprintStageValue, _crouchStageValue, _jumpStageValue;
        public event Action<Vector2, bool> MoveInputEvent, LookInputEvent;
        public event Action<bool, float> SprintInputEvent, CrouchInputEvent, JumpInputEvent;


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

        #region FloatBool function

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
        #endregion

        private void FixedUpdate()
        {
            playerInputEvents.Controlls.Sprint.started += ctx => _sprintStageValue = 1;
            playerInputEvents.Controlls.Sprint.performed += ctx => _sprintStageValue = 2;
            playerInputEvents.Controlls.Sprint.canceled += ctx => _sprintStageValue = 0;

            SprintInputEvent?.Invoke(_sprintIsHeld, _sprintStageValue);

            playerInputEvents.Controlls.Crouch.started += ctx => _crouchStageValue = 1;
            playerInputEvents.Controlls.Crouch.performed += ctx => _crouchStageValue = 2;
            playerInputEvents.Controlls.Crouch.canceled += ctx => _crouchStageValue = 0;

            CrouchInputEvent?.Invoke(_crouchIsHeld, _crouchStageValue);

            playerInputEvents.Controlls.Jump.started += ctx => _jumpStageValue = 1;
            playerInputEvents.Controlls.Jump.performed += ctx => _jumpStageValue = 2;
            playerInputEvents.Controlls.Jump.canceled += ctx => _jumpStageValue = 0;
            
            JumpInputEvent?.Invoke(_jumpIsHeld, _jumpStageValue);
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

            _sprintIsHeld = FloatBool(value, "==", 1);



        }

        private void CrouchInputReceiver(InputAction.CallbackContext ctx)
        {
            float value = ctx.ReadValue<float>();

            _crouchIsHeld = FloatBool(value, "==", 1);



        }

        private void JumpInputReceiver(InputAction.CallbackContext ctx)
        {
            float value = ctx.ReadValue<float>();

            _jumpIsHeld = FloatBool(value, "==", 1);

        }
    }
}
