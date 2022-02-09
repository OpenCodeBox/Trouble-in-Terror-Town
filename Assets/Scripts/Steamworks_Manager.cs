using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Steamworks;

public class Steamworks_Manager : MonoBehaviour
{
    // Start is called before the first frame update
    public void SetSteamPresance(string valueName, string presanceText)
    {
        SteamFriends.SetRichPresence(valueName, presanceText);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
