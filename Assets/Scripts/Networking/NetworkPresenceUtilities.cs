using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Mirror;

public class NetworkPresenceUtilities : NetworkBehaviour
{
    TTTSC_NetworkManager _networkManager;
    GameEventManager _gameEventManager;
    [SerializeField]
    GameEventDataSet _gameEventDataSet;

    private void Start()
    {
        _networkManager = GetComponent<TTTSC_NetworkManager>();
        _gameEventManager = GetComponent<GameEventManager>();
        _gameEventDataSet = Instantiate(new GameEventDataSet());
        GameEventData _newEntry = new GameEventData();
    }

    public void CreateDataSet(GameEventDataSet dataSet, string state, string gamemode, string map, string playersInGame, string maxPlayersInGame)
    {
        Debug.Log("Checing passed data");


        for (int data = 1; data < 5; data++)
        {
            Debug.Log("there is " + data + " to add");

            switch (data)
            {
                case 1:

                    GameEventData stateEntry = new GameEventData();
                    stateEntry.valueName = "presence_state";
                    if (state == null || state == "null")
                    {
                        Debug.LogError("no state was passed");
                    }
                    else
                    {
                        stateEntry.valueType = GameEventData.returnValueTypeEnum.String;
                        stateEntry.returnString = state;
                        Debug.Log("suceessfuly recived state " + state);
                        dataSet.gameEventData.Add(stateEntry);
                    }


                    break;

                //
                case 2:
                    GameEventData gamemodeEntry = new GameEventData();
                    gamemodeEntry.valueName =  "presence_gamemode";
                    if (gamemode == null || gamemode == "null")
                    {
                        Debug.Log("no gamemode was passed");
                    }
                    else
                    {
                        gamemodeEntry.valueType = GameEventData.returnValueTypeEnum.String;
                        gamemodeEntry.returnString = gamemode;
                        Debug.Log("suceessfuly recived current gamemode " + gamemode);
                        dataSet.gameEventData.Add(gamemodeEntry);
                    }
                    break;

                //
                case 3:
                    GameEventData mapEntry = new GameEventData();
                    mapEntry.valueName = "presence_map";
                    if (map == null || map == "null")
                    {
                        Debug.LogError("no map was passed");
                    }
                    else
                    {
                        mapEntry.valueType = GameEventData.returnValueTypeEnum.String;
                        mapEntry.returnString = map;
                        Debug.Log("suceessfuly recived map " + map);
                        dataSet.gameEventData.Add(mapEntry);
                    }
                    break;

                //
                case 4:
                    GameEventData playersInGameEntry = new GameEventData();
                    playersInGameEntry.valueName = "presence_playersInGame";
                    if (playersInGame == null || playersInGame == "null")
                    {
                        Debug.LogError("no player amout was passed");
                    }
                    else
                    {
                        playersInGameEntry.valueType = GameEventData.returnValueTypeEnum.String;
                        playersInGameEntry.returnString = playersInGame;
                        Debug.Log("suceessfuly recived the current amout of players " + playersInGame);
                        dataSet.gameEventData.Add(playersInGameEntry);
                    }
                    break;

                //
                case 5:
                    GameEventData maxPlayersEntry = new GameEventData();
                    maxPlayersEntry.valueName = "presence_maxPlayersInGame";
                    if (maxPlayersInGame == null || maxPlayersInGame == "null")
                    {
                        Debug.LogError("no state was passed");
                    }
                    else
                    {
                        maxPlayersEntry.valueType = GameEventData.returnValueTypeEnum.String;
                        maxPlayersEntry.returnString = maxPlayersInGame;
                        Debug.Log("suceessfuly recived the max amout of players " + maxPlayersInGame);
                        dataSet.gameEventData.Add(maxPlayersEntry);
                    }
                    break;
            }
        }

        //await Task.WhenAll(task);

        Debug.Log("all data has been successfully edited");
    }

    public void UpdateDataSetValue(GameEventDataSet dataSet, string valueName, string value)
    {
        Debug.Log("Checing passed data");

        for (int data = 0; data < dataSet.gameEventData.Count; data++)
        {
            var currentData = dataSet.gameEventData[data];
            //task[data] = currentData;
            Debug.Log("there is " + dataSet.gameEventData.Count + " data set to check");
            if (currentData.valueName == valueName)
            {
                currentData.returnString = value;
                Debug.Log("suceessfuly updated " + valueName + " to " + value);
                break;
            }
        }
    }

    [Client]
    public void ClientEnterGame(string gamemode, string map, string playersInGame, string maxPlayersInGame)
    {
        Debug.Log("Checing passed data");

        CreateDataSet(_gameEventDataSet, "presence_InGame", gamemode, map, playersInGame, maxPlayersInGame);

        _gameEventManager.TriggerGameEvent("EnterGame");
    }

    [Client]
    public void ClientUpdatePresence(string valueName, string value)
    {
        UpdateDataSetValue(_gameEventDataSet, valueName, value);

        Debug.Log("Asking clients to update player count");
        _gameEventManager.TriggerGameEvent("UpdatePlayerCount");
    }

    [Client]
    public void UpdatePresence(string valueName, string value)
    {
        UpdateDataSetValue(_gameEventDataSet, valueName, value);
        
        Debug.Log("Asking clients to update player count");
        _gameEventManager.TriggerGameEvent("UpdatePlayerCount");
    }
}
