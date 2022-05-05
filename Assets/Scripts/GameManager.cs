using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private RoleLoader _roleLoader;
    private GameEventManager _gameEventManager;
    private RoundSystem _roundSystem;

    // Start is called before the first frame update
    void Start()
    {
        _gameEventManager = GetComponent<GameEventManager>();

        _gameEventManager.TriggerGameEvent("StartGame");

        _roundSystem = FindObjectOfType<RoundSystem>();
        
        
    }

    // Update is called once per frame
    void Update()
    {

    }
}
