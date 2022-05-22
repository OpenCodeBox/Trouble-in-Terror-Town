using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class PresanceManager : MonoBehaviour
{
    SteamworksManager steamworksManager;
    DiscordManager discordManager;
    string valueName;
    string state;
    string gamemode;
    string map;
    string playersInGame;
    string maxPlayersInGame;
    string timeUntillGameEnds;

    GameEventDataSet gameEventDataSet;
    private void OnEnable()
    {
        DontDestroyOnLoad(this);
        steamworksManager = FindObjectOfType<SteamworksManager>();
        discordManager = FindObjectOfType<DiscordManager>();
    }


    public void SetPresence(GameEventDataSet dataSet)
    {
        Debug.Log("attempting to set presence");
        gameEventDataSet = dataSet;
        CheckDataSet(gameEventDataSet);
    }

    public void CheckDataSet(GameEventDataSet dataSet)
    {
        Debug.Log("Checing passed data");
        
        for (int data = 0; data < dataSet.gameEventData.Count; data++)
        {
            var currentData = dataSet.gameEventData[data];
            //task[data] = currentData;
            Debug.Log("there is " + dataSet.gameEventData.Count + " data set to check");


            switch (currentData.valueName)
            {
                case "presence_state":
                    valueName = currentData.valueName;
                    state = currentData.returnString;
                    if (state == null || state == "null")
                    {
                        Debug.Log("no state was passed");
                    }
                    else
                    {
                        Debug.Log("suceessfuly recived state " + state);
                    }
                    break;
                    
                    //
                case "presence_gamemode":
                    valueName = currentData.valueName;
                    gamemode = currentData.returnString;
                    if (gamemode == null || gamemode == "null")
                    {
                        Debug.Log("no gamemode was passed");
                    }
                    else
                    {
                        Debug.Log("suceessfuly recived current gamemode " + gamemode);
                    }
                    break;

                    //
                case "presence_map":
                    valueName = currentData.valueName;
                    map = currentData.returnString;
                    if (map == null || map == "null")
                    {
                        Debug.Log("no map was passed");
                    }
                    else
                    {
                        Debug.Log("suceessfuly recived map " + map);
                    }
                    break;

                    //
                case "presence_playersInGame":
                    valueName = currentData.valueName;
                    playersInGame = currentData.returnString;
                    if (playersInGame == null || playersInGame == "null")
                    {
                        Debug.Log("no player amout was passed");
                    }
                    else
                    {
                        Debug.Log("suceessfuly recived the current amout of players " + playersInGame);
                    }
                    break;

                    //
                case "presence_maxPlayersInGame":
                    valueName = currentData.valueName;
                    maxPlayersInGame = currentData.returnString;
                    if (maxPlayersInGame == null || maxPlayersInGame == "null")
                    {
                        Debug.Log("no state was passed");
                    }
                    else
                    {
                        Debug.Log("suceessfuly recived the max amout of players " + maxPlayersInGame);
                    }
                    break;
            }
        }

        //await Task.WhenAll(task);

        Debug.Log("all data has been successfully checked, attempting setting presence");
        SetSteamPeresance();
        discordManager.SetPresence(state, gamemode, map, playersInGame, maxPlayersInGame);
        UpdatePresenceStats();
        Debug.Log("attepting to update all platform presences");
    }

    private void UpdatePresenceStats()
    {
        steamworksManager.SetSteamPresance(valueName, gamemode);

        for (int data = 0; data < gameEventDataSet.gameEventData.Count; data++)
        {
            var currentData = gameEventDataSet.gameEventData[data];
            //task[data] = currentData;
            Debug.Log("there is " + gameEventDataSet.gameEventData.Count + " data set to check");


            switch (currentData.valueName)
            {

                //
                case "presence_gamemode":
                    valueName = currentData.valueName;
                    steamworksManager.SetSteamPresance(valueName, gamemode);
                    if (gamemode == null || gamemode == "null")
                    {
                        Debug.Log("no gamemode was passed");
                    }
                    else
                    {
                        Debug.Log("steam suceessfuly recived current gamemode " + gamemode);
                    }
                    break;

                //
                case "presence_map":
                    valueName = currentData.valueName;
                    steamworksManager.SetSteamPresance(valueName, map);
                    if (map == null || map == "null")
                    {
                        Debug.Log("no map was passed");
                    }
                    else
                    {
                        Debug.Log("steam suceessfuly recived map " + map);
                    }
                    break;

                //
                case "presence_playersInGame":
                    valueName = currentData.valueName;
                    steamworksManager.SetSteamPresance(valueName, playersInGame);
                    if (playersInGame == null || playersInGame == "null")
                    {
                        Debug.Log("no player amout was passed");
                    }
                    else
                    {
                        Debug.Log("steam suceessfuly recived the current amout of players " + playersInGame);
                    }
                    break;

                //
                case "presence_maxPlayersInGame":
                    valueName = currentData.valueName;
                    steamworksManager.SetSteamPresance(valueName, maxPlayersInGame);
                    if (maxPlayersInGame == null || maxPlayersInGame == "null")
                    {
                        Debug.Log("no state was passed");
                    }
                    else
                    {
                        Debug.Log("steam suceessfuly recived the max amout of players " + maxPlayersInGame);
                    }
                    break;
            }
        }
        Debug.Log("Updated steam presence stats");
    }

    public void SetSteamPeresance()
    {
        switch (state)
        {
            case "presence_InMainMenu":
                steamworksManager.SetSteamPresance("steam_display", "#InMainMenu");
                Debug.Log("steam presence has been set to In Main Menu");
                break;
            case "presence_JoiningGame":
                steamworksManager.SetSteamPresance("steam_display", "#JoiningGame");
                Debug.Log("steam presence has been set to Joining Game");
                break;
            case "presence_InGame":
                UpdatePresenceStats();
                steamworksManager.SetSteamPresance("steam_display", "#InGame");
                Debug.Log("steam presence has been set to In Game with following parameters: on map = " + map + ", players in game = " + playersInGame + ", max players in game = " + maxPlayersInGame);
                break;
        }
        
    }
}
