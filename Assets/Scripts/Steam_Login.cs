using System.Collections;
using System.Collections.Generic;
using System.Text;
using System;
using System.Threading.Tasks;
using Steamworks;
using UnityEngine;
using UnityEngine.UI;

public class Steam_Login : MonoBehaviour
{
    public uint appIdUint;
    public GameObject EOSManagerPrefab;
    public EOSManager eOSManager;
    public string encryptedAppTicketHexString;
    public byte[] encryptedAppTicket;
    public UserInfo userInfo;
    public RawImage test;


    public void SteamEOSLogin()
    {
        SteamClient.Init(1860180, true);

        
        GetAppToken();
    }

    private async void GetAppToken()
    {
        encryptedAppTicket = await SteamUser.RequestEncryptedAppTicketAsync();

        //Debug.Log(encryptedAppTicket);

        StringBuilder sb = new StringBuilder();

        encryptedAppTicketHexString = BitConverter.ToString(encryptedAppTicket);
        encryptedAppTicketHexString = encryptedAppTicketHexString.Replace("-","");

        await Task.Yield();

        SetEOSUserInfo();
    }

    public async void SetEOSUserInfo()
    {
        userInfo.userName = SteamClient.Name;
        userInfo.usedPlatform = "steam";
        var avatar = await SteamFriends.GetLargeAvatarAsync(SteamClient.SteamId);
        test.texture = AvatarToTexture2D(avatar.Value);

        await Task.Yield();
        userInfo.profilePicture = test.texture;


        StartCoroutine("CreateEOSManager");
    }

    public Texture2D AvatarToTexture2D(Steamworks.Data.Image image)
    {
        // Create a new Texture2D
        Texture2D texture = new Texture2D((int)image.Width, (int)image.Height);

        
        // Flip image
        for (int x = 0; x < image.Width; x++)
        {
            for (int y = 0; y < image.Height; y++)
            {
                var pixels = image.GetPixel(x, y);
                texture.SetPixel(x, (int)image.Height - y, new UnityEngine.Color(pixels.r / 255.0f, pixels.g / 255.0f, pixels.b / 255.0f, pixels.a / 255.0f));
            }
        }

        texture.Apply();
        return texture;
    }

    private IEnumerator CreateEOSManager()
    {
        Debug.Log("waiting for app ticket");
        yield return new WaitForSeconds(3);
        Debug.Log("attempting login");
        Instantiate(EOSManagerPrefab);

        eOSManager = FindObjectOfType<EOSManager>();
        eOSManager.InitializeEOS(Epic.OnlineServices.Auth.LoginCredentialType.ExternalAuth, Epic.OnlineServices.ExternalCredentialType.SteamAppTicket,encryptedAppTicketHexString);
    }

}