using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using TTTSC.Player.Character;
using TTTSC.Player.Character.Controller;

namespace TTTSC.Player.NetworkCharacter
{
    public class NetworkCharacterHover : NetworkBehaviour
    {
        [SerializeField]
        float _capsuleCastHight, _capsuleCastRadius;

        Vector3 _downVector;

        [SerializeField]
        private NetworkAliveReffrenceHub _aliveReffrenceHub;
        private CharacterMovementConfig _characterMovementConfig;
        private NetworkCharacterStateMachine _characterStateMachine;
        [SerializeField]
        private Transform _groundCheckOrigin;
        [SerializeField]
        LayerMask _layerMask;

        public float hoverForce { get; private set; }

        public float currentHoverHight;

        RaycastHit _hoverRayHit;

        [SerializeField]
        Mesh _capsuleMesh;

        bool _rayStatus;

        private void OnDrawGizmos()
        {

            _characterMovementConfig = _aliveReffrenceHub.characterMovementConfig;


            switch (_rayStatus)
            {
                case true:
                    Gizmos.color = Color.green;
                    break;
                case false:
                    Gizmos.color = Color.red;
                    break;
            }

            Gizmos.DrawMesh(_capsuleMesh, new Vector3(_groundCheckOrigin.position.x, (_groundCheckOrigin.position.y - _capsuleCastHight) - _hoverRayHit.distance, _groundCheckOrigin.position.z), Quaternion.identity, new Vector3(_capsuleCastRadius * 2, _capsuleCastHight / 2, _capsuleCastRadius * 2));

            //Gizmos.DrawSphere(new Vector3(_groundCheckOrigin.position.x, (_capsuleCastHight / 2) + _groundCheckOrigin.position.y , _groundCheckOrigin.position.z), 0.5f);
            //Gizmos.DrawSphere(new Vector3(_groundCheckOrigin.position.x, (-_capsuleCastHight / 2) + _groundCheckOrigin.position.y, _groundCheckOrigin.position.z), 0.5f);

            Gizmos.DrawLine(_groundCheckOrigin.transform.position, _groundCheckOrigin.transform.position + _downVector * _characterMovementConfig.groundCheckLength);



        }

        private void Awake()
        {
            _characterMovementConfig = _aliveReffrenceHub.characterMovementConfig;
            _characterStateMachine = _aliveReffrenceHub.characterStateMachine;
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            _downVector = transform.TransformDirection(Vector3.down);

            Vector3 characterVelocity = GetComponentInParent<Rigidbody>().velocity;

            _rayStatus = Physics.CapsuleCast(new Vector3(_groundCheckOrigin.position.x, (_capsuleCastHight / 2) + _groundCheckOrigin.position.y, _groundCheckOrigin.position.z),
                new Vector3(_groundCheckOrigin.position.x, (-_capsuleCastHight / 2) + _groundCheckOrigin.position.y, _groundCheckOrigin.position.z), _capsuleCastRadius, _downVector, out _hoverRayHit, _characterMovementConfig.groundCheckLength, _layerMask);

            switch (_rayStatus)
            {
                case true:

                    Vector3 otherObjectVelocity = Vector3.zero;
                    _characterStateMachine.characterState = NetworkCharacterStateMachine.CharacterStates.Grounded;

                    Rigidbody otherRigidbody = _hoverRayHit.rigidbody;

                    if (otherRigidbody != null)
                    {
                        otherObjectVelocity = otherRigidbody.velocity;
                    }

                    float characterDirectionalVelocity = Vector3.Dot(_downVector, characterVelocity);
                    float otherObjectDirectionalVelocity = Vector3.Dot(_downVector, otherObjectVelocity);

                    float realVelocity = characterDirectionalVelocity - otherObjectDirectionalVelocity;

                    float characterHightDiffrence = _hoverRayHit.distance - currentHoverHight;

                    hoverForce = (characterHightDiffrence * _characterMovementConfig.hoverStrenght) - (realVelocity * _characterMovementConfig.hoverDampening) * Time.deltaTime;


                    //Debug.Log("ray number " + ray + " found ground " + characterHightDiffrence);

                    break;

                case false:
                    _characterStateMachine.characterState = NetworkCharacterStateMachine.CharacterStates.InAir;

                    hoverForce = 0;

                    //Debug.Log("ray number " + ray + " did not found ground");

                    break;
            }

        }
    }
}

