using Chronity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentCheck : MonoBehaviour
{
    void Awake()
    {
        Debug.Log("checking parrents");
        if (transform.parent == null)
        {
            Debug.Log("No parent, deleting object");
            GameObject.Destroy(gameObject);
        }
        else
        {
            Debug.Log("Parent is " + transform.parent.name);
        }
    }
}
