using Discord;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscordManager : MonoBehaviour
{

    Discord.Discord discord;
    ActivityManager activityManager;
    DateTimeOffset dateTimeOffset;

    private void OnEnable()
    {
        DontDestroyOnLoad(this);
        dateTimeOffset = DateTime.UtcNow;
        discord = new Discord.Discord(845470776173658132, (UInt64)Discord.CreateFlags.NoRequireDiscord);
        activityManager = discord.GetActivityManager();

        ClearPresence();
    }

    public void ClearPresence()
    {
        activityManager.ClearActivity((result) =>
        {
            if (result == Discord.Result.Ok)
            {
                Console.WriteLine("Activity cleared");
            }
            else
            {
                Console.WriteLine("Failed to clear activity");
            }
        });
    }

    public void SetPresence(string state, string gamemode, string map, string playersInGame, string maxPlayersInGame)
    {
        switch (state)
        {
            case "presence_InMainMenu":
                var activityInMainMenu = new Discord.Activity
                {
                    Details = "In Main Menu",
                    Assets =
                    {
                        LargeImage = "tttsc_icon",
#if UNITY_EDITOR
                        SmallImage = "tttsc_icon_unity",
                        SmallText = "Playing in Unity Editor",
#endif
                    },
                    Timestamps =
                    {
                        Start = dateTimeOffset.ToUnixTimeSeconds()
                    }
                };

                activityManager.UpdateActivity(activityInMainMenu, (res) =>
                {
                    switch (res)
                    {
                        case Result.Ok:
                            Debug.Log("discord activity has been updated");
                            break;
                        default:
                            Debug.LogError("discord activity failed to update");
                            break;
                    }
                });

                Debug.Log("discord presence has been set to In Main Menu");
                break;
            case "presence_JoiningGame":
                var activityJoiningGame = new Discord.Activity
                {
                    Details = "Joining Game",
                    Assets = 
                    {
                        LargeImage = "tttsc_icon",

                        #if UNITY_EDITOR
                            SmallImage = "tttsc_icon_unity",
                            SmallText = "Playing in Unity Editor"
                        #endif
                    }
                };

                activityManager.UpdateActivity(activityJoiningGame, (res) =>
                {
                    switch (res)
                    {
                        case Result.Ok:
                            Debug.Log("discord activity has been updated");
                            break;
                        default:
                            Debug.LogError("discord activity failed to update");
                            break;
                    }
                });

                Debug.Log("discord presence has been set to Joining Game");
                break;
            case "presence_InGame":
                var activityInGame = new Discord.Activity
                {
                    Details = "In Game (" + gamemode + ")",
                    State = " On " + map,
                    Party =
                    {
                        Id = "party_Id_test",
                        Size =
                        {
                            CurrentSize = int.Parse(playersInGame),
                            MaxSize = int.Parse(maxPlayersInGame)
                        }
                    },
                    Secrets =
                    {
                        Match = "foo matchSecret",
                        Join = "foo joinSecret",
                        Spectate = "foo spectateSecret",
                    },
                    Instance = true,
                    Assets =
                    {
                        LargeImage = "tttsc_icon",

                        #if UNITY_EDITOR
                            SmallImage = "tttsc_icon_unity",
                            SmallText = "Playing in Unity Editor"
                        #endif
                    }
                };

                activityManager.UpdateActivity(activityInGame, (res) =>
                {
                    switch (res)
                    {
                        case Result.Ok:
                            Debug.Log("discord activity has been updated");
                            break;
                        default:
                            Debug.LogError("discord activity failed to update");
                            break;
                    }
                });

                Debug.Log("discord presence has been set to In Game with following parameters: on map = " + map + ", players in game = " + playersInGame + ", max players in game = " + maxPlayersInGame);
                break;
        }
        


    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        discord.RunCallbacks();
    }
}
