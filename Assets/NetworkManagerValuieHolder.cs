using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System.Linq;
public struct PlayerObject
{
    public int connectionId;
    public Transform playerGhost;
    public Transform playerBody;
}

public class NetworkManagerValuieHolder : NetworkBehaviour
{

    public UserInfo userInfo;

    public readonly SyncList<PlayerObject> playerObjectList = new SyncList<PlayerObject>();

    public override void OnStartClient()
    {
        playerObjectList.Callback += OnInventoryUpdated;

        // Process initial SyncList payload
        for (int index = 0; index < playerObjectList.Count; index++)
            OnInventoryUpdated(SyncList<PlayerObject>.Operation.OP_ADD, index, new PlayerObject(), playerObjectList[index]);
    }

    void OnInventoryUpdated(SyncList<PlayerObject>.Operation op, int index, PlayerObject oldPlayerObject, PlayerObject newPlayerObject)
    {
        switch (op)
        {
            case SyncList<PlayerObject>.Operation.OP_ADD:
                // index is where it was added into the list
                // newItem is the new item
                break;
            case SyncList<PlayerObject>.Operation.OP_INSERT:
                // index is where it was inserted into the list
                // newItem is the new item
                break;
            case SyncList<PlayerObject>.Operation.OP_REMOVEAT:
                // index is where it was removed from the list
                // oldItem is the item that was removed
                break;
            case SyncList<PlayerObject>.Operation.OP_SET:
                // index is of the item that was changed
                // oldItem is the previous value for the item at the index
                // newItem is the new value for the item at the index
                break;
            case SyncList<PlayerObject>.Operation.OP_CLEAR:
                // list got cleared
                break;
        }
    }


    [ClientRpc]
    public void AddPlayerObject(int connectionId, Transform playerGhost, Transform playerBody)
    {
        Debug.Log("AddPlayerObject");


        try
        {
            playerObjectList.Add(new PlayerObject() { connectionId = connectionId, playerGhost = playerGhost, playerBody = playerBody });
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Error: there is a proble with AddPlayerObject, {e}");
        }
        finally
        {
            Debug.Log($"added object with connection id {connectionId}");
        }
    }

    [ClientRpc]
    /// <summary>
    /// Searches for a specific connection Id and removes the entry associated with it
    /// </summary>
    /// <param name="conn"></param>
    /// <param name="connectionId"></param>
    public void RemovePlayerObject(int connectionId)
    {
        for (int playerObject = 0; playerObject < playerObjectList.Count; playerObject++)
        {
            if (playerObjectList[playerObject].connectionId == connectionId)
            {
                playerObjectList.RemoveAt(playerObject);
                break;
            }
        }
    }

    [ClientRpc]
    public void UpdatePlayerObject(int connectionId, Transform playerGhost, Transform playerBody)
    {
        for (int playerObject = 0; playerObject < playerObjectList.Count; playerObject++)
        {
            if (playerObjectList[playerObject].connectionId == connectionId)
            {
                RemovePlayerObject(connectionId);
                AddPlayerObject(connectionId, playerGhost, playerBody);

                break;
            }
        }
    }

}
