using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using TTTSC.Player.NetworkCharacter;

public class TTTSC_NetworkManager : NetworkManager
{
    [SerializeField]
    NetworkManagerValuieHolder _networkManagerValuieHolder;

    [SerializeField]
    NetworkPresenceUtilities _networkPresenceUtilities;


    public override void OnClientConnect(NetworkConnection conn)
    {
        base.OnClientConnect(conn);

        Debug.Log("Client connected to server");

        _networkPresenceUtilities.ClientEnterGame("Classic", networkSceneName, numPlayers.ToString(), "16");
    }

    public override void OnServerAddPlayer(NetworkConnection conn)
    {
        base.OnServerAddPlayer(conn);

        Debug.Log($"Server added a new player, there is now {numPlayers} players");

        AddPlayerObject(conn);

        //update player count
        _networkPresenceUtilities.UpdatePresence("presence_playersInGame", numPlayers.ToString());

        Debug.Log("ther is " + numPlayers + " players in game");
    }

    private void AddPlayerObject(NetworkConnection conn)
    {
        _networkManagerValuieHolder.AddPlayerObject(conn.connectionId, null, null);
        Debug.Log($"Added player object with connection id {conn.connectionId}");
    }

    public override void OnServerDisconnect(NetworkConnection conn)
    {
        base.OnServerDisconnect(conn);

        //update player count
        _networkPresenceUtilities.UpdatePresence("presence_playersInGame", numPlayers.ToString());

        RemovePlayerObject(conn);

        for (int playerObject = 0; playerObject < _networkManagerValuieHolder.playerObjectList.Count; playerObject++)
        {
            if (_networkManagerValuieHolder.playerObjectList[playerObject].connectionId == conn.connectionId)
            {
                _networkManagerValuieHolder.playerObjectList.RemoveAt(playerObject);
                break;
            }
        }

        Debug.Log("Server disconnected");
    }

    private void RemovePlayerObject(NetworkConnection conn)
    {
        _networkManagerValuieHolder.RemovePlayerObject(conn.connectionId);
        Debug.Log($"Removed player object with connection id {conn.connectionId}");
    }



}
