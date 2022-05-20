using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class NetworkingUtilities : NetworkBehaviour
{

    //public void SpawnAsChild(GameObject prefabToSpawn, NetworkIdentity parentClient, Quaternion rotation, GameObject parrent)
    [ContextMenu("Spawn As Child")]
    public void SpawnAsChild()
    {
        // generate a new unique assetId 
        //System.Guid newAssetId = System.Guid.NewGuid();
        //NetworkServer.Spawn(prefabToSpawn, parentClient.connectionToClient);

        
        
        // get the spawned object
        Debug.Log(NetworkServer.spawned.Count);
    }

}
