using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using TTTSC.Player.Character.Controller;

public class TestPlayer : MonoBehaviour
{
    public PlayerInputReceiver playerInputReceiver;

    // Start is called before the first frame update
    void Start()
    {
        playerInputReceiver = GetComponent<PlayerInputReceiver>();

        playerInputReceiver.CrouchInputEvent += test;
    }
    
    private void test(bool test)
    {
        Debug.Log("test");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
