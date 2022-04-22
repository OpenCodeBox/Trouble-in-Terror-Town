using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TTTSC.Player.Character.Controller
{
    public class CharacterHover : MonoBehaviour
    {
        [SerializeField]
        CharacterMovementConfig characterConfig;
        [SerializeField]
        Transform _groundCheckOrigin;
        [SerializeField]
        float _groundCheckLength;
        [SerializeField]
        LayerMask _layerMask;
        public float hoverHight;
        [SerializeField]
        float _hoverStrenght, _hoverDampening;
        public float hoverForce { get; private set; }

        bool _rayStatus;
        
        private void OnDrawGizmos()
        {
            switch (_rayStatus)
            {
                case true:
                    Gizmos.color = Color.green;
                    break;
                case false:
                    Gizmos.color = Color.red;
                    break;
            }
            Gizmos.DrawLine(_groundCheckOrigin.position, _groundCheckOrigin.position + Vector3.down * _groundCheckLength);
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            Vector3 downVector = transform.TransformDirection(Vector3.down);

            Vector3 characterVelocity = characterConfig.characterRigidbody.velocity;


            _rayStatus = Physics.Raycast(_groundCheckOrigin.position, transform.TransformDirection(Vector3.down), out RaycastHit hit, _groundCheckLength, layerMask: _layerMask);

            switch (_rayStatus)
            {
                case true:

                    Vector3 otherObjectVelocity = Vector3.zero;


                    Rigidbody otherRigidbody = hit.rigidbody;

                    if (otherRigidbody != null)
                    {
                       otherObjectVelocity = otherRigidbody.velocity;

                    }

                    float characterDirectionalVelocity = Vector3.Dot(downVector, characterVelocity);
                    float otherObjectDirectionalVelocity = Vector3.Dot(downVector, otherObjectVelocity);

                    float realVelocity = characterDirectionalVelocity - otherObjectDirectionalVelocity;

                    float characterHightDiffrence = hit.distance - hoverHight;

                    hoverForce = (characterHightDiffrence * _hoverStrenght) - (realVelocity * _hoverDampening) * Time.deltaTime;


                    //Debug.Log("found ground " + characterHightDiffrence);

                    break;

                case false:

                    hoverForce = 0;

                    //Debug.Log("did not found ground");

                    break;
            }
        }
    }
}

