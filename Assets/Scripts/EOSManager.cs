using System.Collections;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Generic;
using Mirror;
using UnityEngine;
using EpicTransport;
using UnityEngine.SceneManagement;

public class EOSManager : EOSSDKComponent
{
    public NetworkManager networkManager;

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

    [ContextMenu("Set NetworkAdress")]
    public void SetNetworkAddress()
    {
        networkManager = FindObjectOfType<NetworkManager>();
        Debug.Log("DeviceID (ProductID) of host is : " + localUserProductIdString);
        networkManager.networkAddress = localUserProductIdString;

        if (!Initialized)
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
                SetNetworkAddress();
                StopCoroutine("InitializationCheck");
            }

            yield return new WaitForSecondsRealtime(2);
        }

    }


}