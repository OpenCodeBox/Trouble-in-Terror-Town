using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerList : MonoBehaviour
{
    [SerializeField]
    GameObject playerPrefab;
    [SerializeField]
    Transform contnent;
    
    NetworkManagerValuieHolder _networkManagerValuieHolder;
    List<GameObject> _players;


    private void Start()
    {
        _networkManagerValuieHolder = GetComponentInParent<NetworkManagerValuieHolder>();
        _players = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        for (int player = 0; _players.Count < _networkManagerValuieHolder.playerObjectList.Count; player++)
        {
            GameObject playerObject;
            playerObject = Instantiate(playerPrefab, contnent);
            _players.Add(playerObject);

            TMP_Text text;
            text = playerObject.GetComponentInChildren<TMP_Text>();
            text.text = $"Player [{_networkManagerValuieHolder.playerObjectList[player].connectionId}]";
        }
        
    }
}
