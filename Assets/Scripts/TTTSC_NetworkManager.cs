using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using TTTSC.Player.NetworkedCharacter;

public class TTTSC_NetworkManager : NetworkManager
{
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

        //update player count
        _networkPresenceUtilities.UpdatePresence("presence_playersInGame", numPlayers.ToString());

        Debug.Log("ther is " + numPlayers + " players in game");
    }

    public override void OnServerDisconnect(NetworkConnection conn)
    {
        base.OnServerDisconnect(conn);

        //update player count
        _networkPresenceUtilities.UpdatePresence("presence_playersInGame", numPlayers.ToString());

        Debug.Log("Server disconnected");
    }



}
