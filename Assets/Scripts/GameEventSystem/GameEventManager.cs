using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventManager : MonoBehaviour
{
    public List<GameEventManagerList> GameEvents;

    public void TriggerGameEvent(string eventName)
    {

        for (int gameEvent=0; gameEvent< GameEvents.Count; gameEvent++)
        {
            if (GameEvents[gameEvent].gameEventName == eventName)
            {
                Debug.Log("attempting to invoke " + eventName);
                GameEvents[gameEvent].GameEventSender.Invoke();
            }
        }
    }
}
