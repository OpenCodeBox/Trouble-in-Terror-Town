using System.Collections;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Generic;
using UnityEngine;
using EpicTransport;
using UnityEngine.SceneManagement;

public class EOSManager : EOSSDKComponent
{

    private void OnEnable()
    {
        DontDestroyOnLoad(this);
        StartCoroutine("InitializationCheck");
    }


    public void InitializeEOS(Epic.OnlineServices.Auth.LoginCredentialType loginCredentialType, Epic.OnlineServices.ExternalCredentialType externalCredentialType, string connectToken)
    {
        authInterfaceCredentialType = loginCredentialType;
        connectInterfaceCredentialType = externalCredentialType;
        SetConnectInterfaceCredentialToken(connectToken);
        Debug.Log("Set token");
        if(connectToken.Length > 3)
        Initialize();
    }

    IEnumerator InitializationCheck()
    {
        Debug.Log("Waiting for initialization");



        while (true)
        {
            if (initialized)
            {
                SceneManager.LoadScene("MainMenu");
                Debug.Log("initialized");
                StopCoroutine("InitializationCheck");
            }

            yield return new WaitForSecondsRealtime(2);
        }

    }


}