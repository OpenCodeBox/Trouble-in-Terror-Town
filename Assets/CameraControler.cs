using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

namespace TTTSC.Player.Character.Controller
{
    public class CameraControler : MonoBehaviour
    {
        private Transform _cameraTransfrom;
        [SerializeField]
        private Transform _cameraAnchor;
        private GameObject _cinemachineCamera;
        private CinemachineVirtualCamera _virtualCamera;
        private CinemachineObjectRotator _objectRotator;


        // Start is called before the first frame update
        void Start()
        {
            _cameraTransfrom = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Transform>();
            _cinemachineCamera = GameObject.FindGameObjectWithTag("CinemachineMainCamera");
            _virtualCamera = _cinemachineCamera.GetComponent<CinemachineVirtualCamera>();
            _objectRotator = _cinemachineCamera.GetComponent<CinemachineObjectRotator>();

            _virtualCamera.Follow = _cameraAnchor;
            _objectRotator.cameraTransform = _cameraTransfrom;
            _objectRotator.horizontalRotationTargets.Add(transform);
        }

    }
}