using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Mirror;

namespace TTTSC.Player.Character.Controller
{
    public class PlayerInputReceiver : NetworkBehaviour
    {
        private NetworkIdentity _networkIdentity;

        private float _lookX, _lookY;

        bool _sprintIsHeld, _crouchIsHeld, _jumpIsHeld;
        public event Action<Vector2, bool> MoveInputEvent, LookInputEvent;
        public event Action<bool> SprintInputEvent, CrouchInputEvent, JumpInputEvent;


        #region SpectatorControls
        public event Action<Vector2, bool> SpectatorScrollSpeedInputEvent;
        public event Action<bool> SpectatorFlyFastInputEvent, SpectatorFlySlowInputEvent, SpectatorFlyUpInputEvent, SpectatorFlyDownInputEvent;
        
        bool _spectatorSpeedUpIsHeld, _spectatorSlowDownIsHeld, _flyUpIsHeld, _flyDownIsHeld;
        #endregion

        public PlayerInputSender playerInputEvents;

        private void Awake()
        {
            playerInputEvents = new PlayerInputSender();

            playerInputEvents.Enable();

            _networkIdentity = GetComponent<NetworkIdentity>();

            _networkIdentity.AssignClientAuthority(_networkIdentity.connectionToClient);

            //
            #region GlobalControls

            playerInputEvents.GlobalControls.LookX.performed += LookXInputReceiver;
            playerInputEvents.GlobalControls.LookY.started += LookYInputReceiver;
            playerInputEvents.GlobalControls.Move.started += WalkInputReceiver;

            #endregion


            //
            #region AliveControls

            playerInputEvents.AliveControls.Sprint.started += SprintInputReceiver;
            playerInputEvents.AliveControls.Jump.started += JumpInputReceiver;
            playerInputEvents.AliveControls.Crouch.performed += CrouchInputReceiver;

            #endregion


            //
            #region SpectatorControls

            playerInputEvents.SpectatorControls.FlyFast.started += SpectatorFlyFastInputReceiver;
            playerInputEvents.SpectatorControls.FlySlow.started += SpectatorFlySlowInputReceiver;
            playerInputEvents.SpectatorControls.FlyUp.started += SpectatorFlyUpInputReceiver;
            playerInputEvents.SpectatorControls.FlyDown.performed += SpectatorFlyDownInputReceiver;

            #endregion
        }

        private void OnDisable()
        {
            playerInputEvents.Disable();

            //
            #region GlobalControls

            playerInputEvents.GlobalControls.LookX.performed -= LookXInputReceiver;
            playerInputEvents.GlobalControls.LookY.started -= LookYInputReceiver;
            playerInputEvents.GlobalControls.Move.started -= WalkInputReceiver;

            #endregion


            //
            #region AliveControls


            playerInputEvents.AliveControls.Sprint.started -= SprintInputReceiver;
            playerInputEvents.AliveControls.Jump.started -= JumpInputReceiver;
            playerInputEvents.AliveControls.Crouch.performed -= CrouchInputReceiver;

            #endregion


            //
            #region SpectatorControls

            playerInputEvents.SpectatorControls.FlyFast.started -= SpectatorFlyFastInputReceiver;
            playerInputEvents.SpectatorControls.FlySlow.started -= SpectatorFlySlowInputReceiver;;
            playerInputEvents.SpectatorControls.FlyUp.started -= SpectatorFlyUpInputReceiver;
            playerInputEvents.SpectatorControls.FlyDown.performed -= SpectatorFlyDownInputReceiver;

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

        #region GlobalControls

        
        [Client]
        private void LookXInputReceiver(InputAction.CallbackContext ctx)
        {
            var value = ctx.ReadValue<float>();

            _lookX = value;
            LookPerformed(FloatBool(value, "!=", 0));
        }


        [Client]
        private void LookYInputReceiver(InputAction.CallbackContext ctx)
        {
            float value = ctx.ReadValue<float>();

            _lookY = value;

            LookPerformed(FloatBool(value, "!=", 0));
        }

        [Client]
        private void LookPerformed(bool performing)
        {
            if (isLocalPlayer)
            {
                var look = new Vector2(_lookX, _lookY);

                LookInputEvent?.Invoke(look, performing);
            }
        }

        [Client]
        private void WalkInputReceiver(InputAction.CallbackContext ctx)
        {
            var value = ctx.ReadValue<Vector2>();

            bool performing = !(value == new Vector2(0, 0));

            CmdWalkInputReceiver(value, performing);
        }

        [Command]
        private void CmdWalkInputReceiver(Vector2 value, bool performing)
        {
            MoveInputEvent?.Invoke(value, performing);
        }
        
        #endregion


        #region AliveControls

        #region SprintInputReciver
        [Client]
        private void SprintInputReceiver(InputAction.CallbackContext ctx)
        {
            float value = ctx.ReadValue<float>();

            if (isLocalPlayer)
            {
                CmdSprintInput(FloatBool(value, "==", 1));
            }

        }
        
        [Command]
        private void CmdSprintInput(bool performed)
        {
            SprintInputEvent?.Invoke(performed);
        }

        #endregion

        #region CrouchInputReciver

        
        private void CrouchInputReceiver(InputAction.CallbackContext ctx)
        {
            float value = ctx.ReadValue<float>();

            Debug.Log("CrouchInputReceiver: " + value);

            Debug.Log("Sending crouch input to server");
            CrouchInputEvent?.Invoke(FloatBool(value, "==", 1));

        }


        #endregion

        #region JumpInputReciver
        
        [Client]
        private void JumpInputReceiver(InputAction.CallbackContext ctx)
        {
            float value = ctx.ReadValue<float>();

            _jumpIsHeld = FloatBool(value, "==", 1);

            if (isLocalPlayer)
            {
                CmdJumpInput(FloatBool(value, "==", 1));
            }
        }

        [Command]
        private void CmdJumpInput(bool performed)
        {
            JumpInputEvent?.Invoke(performed);
        }
        

        #endregion

        #endregion

        #region SpectatorControls


        #region SpectatorSpeedUp
        [Client]
        private void SpectatorFlyFastInputReceiver(InputAction.CallbackContext ctx)
        {
            float value = ctx.ReadValue<float>();

            if (isLocalPlayer)
            {
                CmdSpectatorFlyFast(FloatBool(value, "==", 1));
            }
        }
        
        [Command]
        private void CmdSpectatorFlyFast(bool performed)
        {
            SpectatorFlyFastInputEvent?.Invoke(performed);
        }

        #endregion


        #region SpectatorFlySlow
        private void SpectatorFlySlowInputReceiver(InputAction.CallbackContext ctx)
        {
            float value = ctx.ReadValue<float>();

            CmdSpectatorFlySlow(FloatBool(value, "==", 1));
        }

        [Command]
        private void CmdSpectatorFlySlow(bool performed)
        {
            SpectatorFlySlowInputEvent?.Invoke(performed);
        }

        #endregion


        #region SpectatorFlyUp

        [Client]
        private void SpectatorFlyUpInputReceiver(InputAction.CallbackContext ctx)
        {
            float value = ctx.ReadValue<float>();

            CmdSpectatorFlyUp(FloatBool(value, "==", 1));
        }

        [Command]
        private void CmdSpectatorFlyUp(bool performed)
        {
            SpectatorFlyUpInputEvent?.Invoke(performed);
        }

        #endregion

        
        #region SpectatorFlyDown
        private void SpectatorFlyDownInputReceiver(InputAction.CallbackContext ctx)
        {
            float value = ctx.ReadValue<float>();

            CmdSpectatorFlyDown(FloatBool(value, "==", 1));
        }

        private void CmdSpectatorFlyDown(bool performed)
        {
            SpectatorFlyDownInputEvent?.Invoke(performed);
        }

        #endregion
        
        #endregion
    }
}
