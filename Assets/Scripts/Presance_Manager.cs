using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Presance_Manager : MonoBehaviour
{
    public void SetAllPresences(GameEventDataSet dataSet)
    {
        string gamemode = null;

        for (int data = 0; data < dataSet.gameEventData.Count; data++)
        {
            var currentData = dataSet.gameEventData[data];
            switch (currentData.valueName)
            {
                case "presence_gamemode":
                    gamemode = currentData.returnString;
                    break;
            }
        }


        Debug.Log("event test " + gamemode);
    }

    public void SetSteamPeresance(string valueName, string presanceText)
    {
    
    }

    public void SetDiscordPresence()
    {

    }
}
