using System.Collections;
using System.Collections.Generic;
using System.Text;
using System;
using System.Threading.Tasks;
using Steamworks;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SteamworksManager : MonoBehaviour
{
    public UserInfo userInfo;
    public RawImage userAvatar;

    private void OnEnable()
    {
        DontDestroyOnLoad(this);
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

    public async void SetUserInfo(UserInfo _userInfo)
    {
        userInfo = _userInfo;
        
        userInfo.userName = SteamClient.Name;
        var avatar = await SteamFriends.GetLargeAvatarAsync(SteamClient.SteamId);
        userAvatar.texture = AvatarToTexture2D(avatar.Value);
        userInfo.steamId = $"{SteamClient.SteamId}";
        await Task.Yield();
        userInfo.profilePicture = userAvatar.texture;


        SceneManager.LoadScene("MainMenu");
    }

    // Start is called before the first frame update
    public void SetSteamPresance(string valueName, string presanceText)
    {
        Debug.Log("SetSteamPresance with key: " + valueName + " and content: " + presanceText);
        SteamFriends.SetRichPresence(valueName, presanceText);
    }

}
