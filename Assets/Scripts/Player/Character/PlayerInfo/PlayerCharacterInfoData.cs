using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TTTSC.Player.Character.PlayerCharacterInfo
{
    [CreateAssetMenu(menuName = "TTTSC/Player/Character/Player Info")]
    public class PlayerCharacterInfoData : ScriptableObject
    {
        public int helth;
        public int armor;
        
        
        public enum playerPlayStates
        {
            Alive,
            Spectator
        };

        public playerPlayStates currentPlayerPlayState;


        public int playerRole;
    }
}