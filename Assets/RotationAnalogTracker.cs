using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationAnalogTracker : MonoBehaviour
{
    [SerializeField]
    private Transform _trackedObject;

    [SerializeField]
    float _currentRotation = 0;

    [Header("Z+")][SerializeField]
    private bool _Facing_Z_Plus;
    [SerializeField]
    private float _lookDirectionForwardMin, _lookDirectionForwardMax;

    [Header("X+")]
    [SerializeField]
    private bool _Facing_X_Plus;
    [SerializeField]
    private float _lookDirectionRightMin, _lookDirectionRightMax;

    [Header("X-")]
    [SerializeField]
    private bool _Facing_X_Minus;
    [SerializeField]
    private float _lookDirectionLeftMin, _lookDirectionLeftMax;

    [Header("Z-")][SerializeField]
    private bool _Facing_Z_Minus;
    [SerializeField]
    private float _lookDirectionBackMin, _lookDirectionBackMax;


    bool IsInRange(float valueToCheck, float minimumValue, float maximumValue)
    {
        bool result;

        if(valueToCheck >= minimumValue && valueToCheck <= maximumValue)
        {
            result = true;
        }
        else
        {
            result = false;
        }

        return result;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        
        _currentRotation = _trackedObject.rotation.y;

        _Facing_Z_Plus = IsInRange(_trackedObject.rotation.y, _lookDirectionForwardMin, _lookDirectionForwardMax);
        if (!IsInRange(_trackedObject.rotation.y, _lookDirectionBackMin, _lookDirectionBackMax))
        {
            _Facing_Z_Minus = true;
        }
        else
        {
            _Facing_Z_Minus = false;
        }
        _Facing_X_Minus = IsInRange(_trackedObject.rotation.y, _lookDirectionLeftMin, _lookDirectionLeftMax);
        _Facing_X_Plus = IsInRange(_trackedObject.rotation.y, _lookDirectionRightMin, _lookDirectionRightMax);



    }
}
