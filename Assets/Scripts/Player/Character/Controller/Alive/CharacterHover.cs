using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TTTSC.Player.Character.Controller
{
    public class CharacterHover : MonoBehaviour
    {
        [SerializeField]
        private CharacterReffrenceHub characterReffrenceHub;
        private CharacterMovementConfig _characterMovementConfig;
        private CharacterStateMachine _characterStateMachine;
        [SerializeField]
        private Transform _groundCheckOrigins;
        [SerializeField]
        float _groundCheckLength;
        [SerializeField]
        LayerMask _layerMask;
        public float hoverHight;
        [SerializeField]
        float _hoverStrenght, _hoverDampening;
        public float hoverForces { get; private set; }

        RaycastHit _hoverRayHits;

        bool _rayStatuses;

        private void OnDrawGizmos()
        {
            Gizmos.DrawLine(_groundCheckOrigins.transform.position, _groundCheckOrigins.transform.position + Vector3.down * _groundCheckLength);

            switch (_rayStatuses)
            {
                case true:
                    Gizmos.color = Color.green;
                    break;
                case false:
                    Gizmos.color = Color.red;
                    break;
            }
        }

        private void Awake()
        {
            _characterMovementConfig = characterReffrenceHub.characterMovementConfig;
            _characterStateMachine = characterReffrenceHub.characterStateMachine;
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            Vector3 downVector = transform.TransformDirection(Vector3.down);

            Vector3 characterVelocity = _characterMovementConfig.characterRigidbody.velocity;


            _rayStatuses = Physics.Raycast(_groundCheckOrigins.position, downVector, out _hoverRayHits, _groundCheckLength, _layerMask);

            switch (_rayStatuses)
            {
                case true:

                    Vector3 otherObjectVelocity = Vector3.zero;
                    _characterStateMachine.characterState = CharacterStateMachine.CharacterStates.Grounded;

                    Rigidbody otherRigidbody = _hoverRayHits.rigidbody;

                    if (otherRigidbody != null)
                    {
                        otherObjectVelocity = otherRigidbody.velocity;

                    }

                    float characterDirectionalVelocity = Vector3.Dot(downVector, characterVelocity);
                    float otherObjectDirectionalVelocity = Vector3.Dot(downVector, otherObjectVelocity);

                    float realVelocity = characterDirectionalVelocity - otherObjectDirectionalVelocity;

                    float characterHightDiffrence = _hoverRayHits.distance - hoverHight;

                    hoverForces = (characterHightDiffrence * _hoverStrenght) - (realVelocity * _hoverDampening) * Time.deltaTime;


                    //Debug.Log("ray number " + ray + " found ground " + characterHightDiffrence);

                    break;

                case false:
                    _characterStateMachine.characterState = CharacterStateMachine.CharacterStates.InAir;

                    hoverForces = 0;

                    //Debug.Log("ray number " + ray + " did not found ground");

                    break;
            }

        }
    }
}

