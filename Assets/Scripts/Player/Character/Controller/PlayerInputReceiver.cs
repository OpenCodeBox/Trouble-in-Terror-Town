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


        #region SpectatorControls
        public event Action<Vector2, bool> SpectatorScrollSpeedInputEvent;
        public event Action<bool, float> SpectatorSpeedUpInputEvent, SpectatorSlowDownEvent, FlyUpInputEvent, FlyDownInputEvent;
        bool _spectatorSpeedUpIsHeld, _spectatorSlowDownIsHeld, _flyUpIsHeld, _flyDownIsHeld;
        float _spectatorFlyFastStageValue, _spectatorFlySlowStageValue, _flyUpStageValue, _flyDownStageValue;
        #endregion

        public PlayerInputSender playerInputEvents;

        private void OnEnable()
        {
            playerInputEvents = new PlayerInputSender();

            playerInputEvents.Enable();

            //
            #region GlobalControls

            playerInputEvents.GlobalControls.LookX.performed += LookXInputReceiver;
            playerInputEvents.GlobalControls.LookY.performed += LookYInputReceiver;
            playerInputEvents.GlobalControls.Move.performed += WalkInputReceiver;

            #endregion


            //
            #region AliveControls


            playerInputEvents.AliveControls.Sprint.performed += SprintInputReceiver;
            playerInputEvents.AliveControls.Jump.performed += JumpInputReceiver;
            playerInputEvents.AliveControls.Crouch.performed += CrouchInputReceiver;

            #endregion


            //
            #region SpectatorControls

            playerInputEvents.SpectatorControls.FlyFast.performed += SpectatorFlyFast;
            playerInputEvents.SpectatorControls.FlySlow.performed += SpectatorFlySlow;
            playerInputEvents.SpectatorControls.FlyUp.performed += SpectatorFlyUp;
            playerInputEvents.SpectatorControls.FlyDown.performed += SpectatorFlyDown;

            #endregion
        }

        private void OnDisable()
        {
            playerInputEvents.Disable();

            //
            #region GlobalControls

            playerInputEvents.GlobalControls.LookX.performed -= LookXInputReceiver;
            playerInputEvents.GlobalControls.LookY.performed -= LookYInputReceiver;
            playerInputEvents.GlobalControls.Move.performed -= WalkInputReceiver;

            #endregion


            //
            #region AliveControls

            playerInputEvents.AliveControls.Sprint.performed -= SprintInputReceiver;
            playerInputEvents.AliveControls.Jump.performed -= JumpInputReceiver;
            playerInputEvents.AliveControls.Crouch.performed -= CrouchInputReceiver;

            #endregion


            //
            #region SpectatorControls

            playerInputEvents.SpectatorControls.FlyFast.performed -= SpectatorFlyFast;
            playerInputEvents.SpectatorControls.FlySlow.performed -= SpectatorFlySlow;
            playerInputEvents.SpectatorControls.FlyUp.performed -= SpectatorFlyUp;
            playerInputEvents.SpectatorControls.FlyDown.performed -= SpectatorFlyDown;

            #endregion
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
            //
            #region AliveControls

            playerInputEvents.AliveControls.Sprint.started += ctx => _sprintStageValue = 1;
            playerInputEvents.AliveControls.Sprint.performed += ctx => _sprintStageValue = 2;
            playerInputEvents.AliveControls.Sprint.canceled += ctx => _sprintStageValue = 0;

            SprintInputEvent?.Invoke(_sprintIsHeld, _sprintStageValue);

            playerInputEvents.AliveControls.Crouch.started += ctx => _crouchStageValue = 1;
            playerInputEvents.AliveControls.Crouch.performed += ctx => _crouchStageValue = 2;
            playerInputEvents.AliveControls.Crouch.canceled += ctx => _crouchStageValue = 0;

            CrouchInputEvent?.Invoke(_crouchIsHeld, _crouchStageValue);

            playerInputEvents.AliveControls.Jump.started += ctx => _jumpStageValue = 1;
            playerInputEvents.AliveControls.Jump.performed += ctx => _jumpStageValue = 2;
            playerInputEvents.AliveControls.Jump.canceled += ctx => _jumpStageValue = 0;
            
            JumpInputEvent?.Invoke(_jumpIsHeld, _jumpStageValue);
            #endregion


            //
            #region SpectatorControls

            playerInputEvents.SpectatorControls.FlyFast.started += ctx => _spectatorFlyFastStageValue = 1;
            playerInputEvents.SpectatorControls.FlyFast.performed += ctx => _spectatorFlyFastStageValue = 2;
            playerInputEvents.SpectatorControls.FlyFast.canceled += ctx => _spectatorFlyFastStageValue = 0;

            SpectatorSpeedUpInputEvent?.Invoke(_spectatorSpeedUpIsHeld, _spectatorFlyFastStageValue);

            playerInputEvents.SpectatorControls.FlySlow.started += ctx => _spectatorFlySlowStageValue = 1;
            playerInputEvents.SpectatorControls.FlySlow.performed += ctx => _spectatorFlySlowStageValue = 2;
            playerInputEvents.SpectatorControls.FlySlow.canceled += ctx => _spectatorFlySlowStageValue = 0;

            SpectatorSlowDownEvent?.Invoke(_spectatorSlowDownIsHeld, _spectatorFlySlowStageValue);

            playerInputEvents.SpectatorControls.FlyUp.started += ctx => _flyUpStageValue = 1;
            playerInputEvents.SpectatorControls.FlyUp.performed += ctx => _flyUpStageValue = 2;
            playerInputEvents.SpectatorControls.FlyUp.canceled += ctx => _flyUpStageValue = 0;

            FlyUpInputEvent?.Invoke(_flyUpIsHeld, _flyUpStageValue);

            playerInputEvents.SpectatorControls.FlyDown.started += ctx => _flyDownStageValue = 1;
            playerInputEvents.SpectatorControls.FlyDown.performed += ctx => _flyDownStageValue = 2;
            playerInputEvents.SpectatorControls.FlyDown.canceled += ctx => _flyDownStageValue = 0;

            FlyDownInputEvent?.Invoke(_flyDownIsHeld, _flyDownStageValue);
            #endregion
        }

        #region GlobalControls

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

        #endregion


        #region AliveControls

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

        #endregion

        #region SpectatorControls

        private void SpectatorFlyFast(InputAction.CallbackContext ctx)
        {
            float value = ctx.ReadValue<float>();

            _spectatorSpeedUpIsHeld = FloatBool(value, "==", 1);
        }

        private void SpectatorFlySlow(InputAction.CallbackContext ctx)
        {
            float value = ctx.ReadValue<float>();

            _spectatorSlowDownIsHeld = FloatBool(value, "==", 1);
        }

        private void SpectatorFlyUp(InputAction.CallbackContext ctx)
        {
            float value = ctx.ReadValue<float>();

            _flyUpIsHeld = FloatBool(value, "==", 1);
        }

        private void SpectatorFlyDown(InputAction.CallbackContext ctx)
        {
            float value = ctx.ReadValue<float>();

            _flyDownIsHeld = FloatBool(value, "==", 1);
        }

        #endregion
    }
}
