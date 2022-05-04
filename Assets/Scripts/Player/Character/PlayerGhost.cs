using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TTTSC.Player.Character
{
    public class PlayerGhost : MonoBehaviour
    {
        [SerializeField]
        private PlayerGhostReffrenceHub playerGhostReffrenceHub;
        [SerializeField]
        public PlayerInfo.PlayerInfoData playerInfoData;


        // Start is called before the first frame update
        void Awake()
        {
            playerInfoData = ScriptableObject.CreateInstance<PlayerInfo.PlayerInfoData>();
        }

        // Update is called once per frame
        void Update()
        {

        }


    }
}
