using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

namespace TTTSC.Player.Character.Controller
{
    public class CinemachineObjectRotator : MonoBehaviour
    {
        public bool enableTargetRotation;

        CinemachineVirtualCamera _virtualCamera;
        PlayerInputReceiver _playerInputReceiver;
        public TargetRotationModes rotateWithCameraRotationMode;
        public TargetRotationModes hotizontalRotationMode;
        public TargetRotationModes verticalRotationMode;
        public Transform cameraTransform;
        public List<Transform> horizontalRotationTargets, verticalRotationTargets, rotateWithCameraTargets;
        CinemachinePOV _cinemachinePOV;
        protected void Awake()
        {
            _virtualCamera = GetComponent<CinemachineVirtualCamera>();
            _cinemachinePOV = _virtualCamera.GetCinemachineComponent<CinemachinePOV>();
            _playerInputReceiver = FindObjectOfType<PlayerInputReceiver>();
        }

        public enum TargetRotationModes
        {
            disabled,
            rotateFollowTarget,
            rotateLookTarget,
            rotateSpecificTargets
        }

        private void LateUpdate()
        {
            float rotationX;
            float rotationY;

            switch (enableTargetRotation)
            {
                case true:
                    rotationX = _cinemachinePOV.m_HorizontalAxis.Value;
                    switch (hotizontalRotationMode)
                    {
                        case TargetRotationModes.rotateFollowTarget:
                            _virtualCamera.Follow.rotation = Quaternion.Euler(_virtualCamera.Follow.up * rotationX);
                            break;
                        case TargetRotationModes.rotateLookTarget:
                            _virtualCamera.LookAt.rotation = Quaternion.Euler(_virtualCamera.LookAt.up * rotationX);
                            break;
                        case TargetRotationModes.rotateSpecificTargets:
                            foreach (Transform target in horizontalRotationTargets)
                            {
                                target.rotation = Quaternion.Euler(transform.up * rotationX);
                            }
                            break;
                    }

                    rotationY = _cinemachinePOV.m_VerticalAxis.Value;
                    switch (verticalRotationMode)
                    {
                        case TargetRotationModes.rotateFollowTarget:
                            _virtualCamera.Follow.rotation = Quaternion.Euler(_virtualCamera.Follow.right * rotationY);
                            break;
                        case TargetRotationModes.rotateLookTarget:
                            _virtualCamera.LookAt.rotation = Quaternion.Euler(_virtualCamera.LookAt.right * rotationY);
                            break;
                        case TargetRotationModes.rotateSpecificTargets:
                            foreach (Transform target in verticalRotationTargets)
                            {
                                target.rotation = Quaternion.Euler(target.right * rotationY);
                            }
                            break;
                    }


                    switch (rotateWithCameraRotationMode)
                    {
                        case TargetRotationModes.rotateFollowTarget:
                            _virtualCamera.Follow.rotation = cameraTransform.rotation;
                            break;
                        case TargetRotationModes.rotateLookTarget:
                            _virtualCamera.LookAt.rotation = cameraTransform.rotation;
                            break;
                        case TargetRotationModes.rotateSpecificTargets:
                            foreach (Transform target in rotateWithCameraTargets)
                            {
                                target.rotation = cameraTransform.rotation;
                            }
                            break;
                    }
                    break;
            }
        }
    }
}
