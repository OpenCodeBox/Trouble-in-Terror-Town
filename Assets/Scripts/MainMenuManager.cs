using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField]
    GameEventManager gameEventManager;

    private void OnEnable()
    {
        gameEventManager = FindObjectOfType<GameEventManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Triggering game event");
        gameEventManager.TriggerGameEvent("EnterMainMenu");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
