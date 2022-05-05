using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundSystem : MonoBehaviour
{
    public long currentTimer;

    [Header("Time to wait for player before starting the game")]
    public int waitTime;

    public long playerWaitTime;


    [Tooltip("preparing time in seconds")]
    public int preparingTime;

    public long roundStartTime { get; private set; }

    public int roundTime;

    public long roundEndTime { get; private set; }

    [Tooltip("time that will be added to the round when someone gets killed")]
    public int hasteAddTime;

    public long realTimeUnix;

    public bool roundInProggress;

    public void StartWaiting()
    {
        DateTime currenetTime = DateTime.UtcNow;
        playerWaitTime = ((DateTimeOffset)currenetTime).ToUnixTimeSeconds() + waitTime;
        //Debug.Log("waiting for players will end at " + playerWaitTime.ToString());

        currentTimer = playerWaitTime;
    }

    public void StartPreparing()
    {
        DateTime currenetTime = DateTime.UtcNow;
        roundStartTime = ((DateTimeOffset)currenetTime).ToUnixTimeSeconds() + preparingTime;
        //Debug.Log("preparing will end at " + roundStartTime.ToString());
        roundInProggress = true;

        currentTimer = roundStartTime;
    }

    private void FixedUpdate()
    {
        DateTime currenetTime = DateTime.UtcNow;

        realTimeUnix = ((DateTimeOffset)currenetTime).ToUnixTimeSeconds();

        if (realTimeUnix == playerWaitTime)
        {
            
            StartPreparing();
        }

        if (roundInProggress && realTimeUnix == roundStartTime)
        {
            Debug.Log("prep time ended staring round");
            StartRound();
        }
        else if (roundInProggress && realTimeUnix == roundEndTime)
        {
           roundInProggress = true;
        }
    }

    public void StartRound()
    {
        DateTime currenetTime = DateTime.UtcNow;
        roundEndTime = ((DateTimeOffset)currenetTime).ToUnixTimeSeconds() + roundTime;
        Debug.Log("round will end at at " + roundEndTime);

        currentTimer = roundEndTime;
    }

    public void StartHasteRound()
    {

    }

    public void HasteAddTime()
    {
        roundEndTime += hasteAddTime;
    }
}
