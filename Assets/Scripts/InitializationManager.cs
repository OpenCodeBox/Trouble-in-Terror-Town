using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class InitializationManager : MonoBehaviour
{
    public string[] startArguments;
    [SerializeField]
    private Steam_Login steamworks_Login;
    public GameEventManager gameEventManager;


    void OnEnable()
    {
        startArguments = Environment.GetCommandLineArgs();
    }

    private void OnGUI()
    {
        if (GUILayout.Button("Log in with steam"))
        {
            steamworks_Login.SteamLogin();
        }

        /*
        if (GUILayout.Button("Log in with epic"))
        {
            steamworks_Login.SteamLogin();
        }*/
    }

#if !UNITY_EDITOR
    private void PlatformLogin()
    {
        for (int argument = 0; argument < startArguments.Length; argument++)
        {
            Debug.Log(startArguments[argument] + " is in possition " + argument);

            switch (startArguments[1])
            {
                case "-SteamLogin":
                    steamworks_Login.SteamLogin();
                    break;
                case "-XboxLogin":
                    Debug.Log("Loging in with xbox");
                    break;
            }
        }
    }




    private void AppMode()
    {
        for (int argument = 0; argument < startArguments.Length; argument++)
        {
            Debug.Log(startArguments[argument] + "is in possition" + argument);

            switch (startArguments[2])
            {
                case "-OculusVR":
                    Debug.Log("Starting in oculusVR mode");
                    break;
                case "-SteamVR":
                    Debug.Log("Starting in steamVR mode");
                    break;


            }
        }

    }
#endif

}
