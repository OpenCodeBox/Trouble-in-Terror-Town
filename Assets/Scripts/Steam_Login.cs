using System.Collections;
using System.Collections.Generic;
using System.Text;
using System;
using System.Threading.Tasks;
using Steamworks;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Steam_Login : MonoBehaviour
{
    [SerializeField]
    private string encryptedAppTicketHexString;
    private byte[] encryptedAppTicket;
    public UserInfo userInfo;
    [SerializeField]
    private RawImage test;
    [SerializeField]
    private SteamworksManager steamworksManager;

    public void SteamLogin()
    {
        SteamClient.Init(1860180, true);

        userInfo = Instantiate(new UserInfo());

        GetAppToken();
    }

    private async void GetAppToken()
    {
        encryptedAppTicket = await SteamUser.RequestEncryptedAppTicketAsync();

        encryptedAppTicketHexString = BitConverter.ToString(encryptedAppTicket);
        encryptedAppTicketHexString = encryptedAppTicketHexString.Replace("-","");

        await Task.Yield();

        steamworksManager.SetUserInfo(userInfo);
    }

}