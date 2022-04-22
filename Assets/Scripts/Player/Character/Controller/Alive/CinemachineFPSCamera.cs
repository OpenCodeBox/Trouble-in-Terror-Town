using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using TTTSC.Player.Character.Controller;

public class CinemachineFPSCamera : CinemachineExtension
{
    [SerializeField]
    float _maxXCamRotation = 80f;

    PlayerInputReceiver _playerInputReceiver;
    [SerializeField]
    private Transform _playerCharacterTransform;
    private Vector3 _startingRotation;
    private CharacterMovementConfig _characterConfig;
    private float _lookVertical, _lookHorizontal;

    protected override void Awake()
    {
        _playerInputReceiver = FindObjectOfType<PlayerInputReceiver>();
        _characterConfig = FindObjectOfType<CharacterMovementConfig>();
        _playerInputReceiver.LookInputEvent += LookInput;
        base.Awake();
    }

    private void LookInput(Vector2 lookInput, bool performing)
    {
        _lookHorizontal = lookInput.x * _characterConfig.lookHorizontalSpeed * Time.deltaTime;
        _lookVertical = lookInput.y * _characterConfig.lookVerticalSpeed * Time.deltaTime;

    }

    protected override void PostPipelineStageCallback(CinemachineVirtualCameraBase vcam, CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
    {
        if (vcam.Follow)
        {
            switch (stage)
            {
                case CinemachineCore.Stage.Aim:
                    
                    if (_startingRotation == null)
                    {
                        _startingRotation = transform.localRotation.eulerAngles;

                    }

                    _startingRotation.y -= _lookVertical;
                    _startingRotation.x += _lookHorizontal;
                    _startingRotation.y = Mathf.Clamp(_startingRotation.y, -_maxXCamRotation, _maxXCamRotation);
                    state.RawOrientation = Quaternion.Euler(_startingRotation.y, _startingRotation.x, 0f);

                    _playerCharacterTransform.rotation = Quaternion.Euler(0f, _startingRotation.x, 0f);

                    break;
            }
        }

    }
}
