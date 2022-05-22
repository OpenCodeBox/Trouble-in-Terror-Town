using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Steamworks;

public class SteamworksManager : MonoBehaviour
{
    private void OnEnable()
    {
        DontDestroyOnLoad(this);
    }

    // Start is called before the first frame update
    public void SetSteamPresance(string valueName, string presanceText)
    {
        Debug.Log("SetSteamPresance with key: " + valueName + " and content: " + presanceText);
        SteamFriends.SetRichPresence(valueName, presanceText);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
