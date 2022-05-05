using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundConfigLoader : MonoBehaviour
{
    public class RoundConfig
    {
        public int perparingTime;
        public int roundTime;
        public bool hasteMode;
        public int hasteKillAddTime;
        public int[] enabledRoles;
        public int[] roleSpawnChance = new int[265];
        public int[] rolePlayerCount = new int[265];
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
