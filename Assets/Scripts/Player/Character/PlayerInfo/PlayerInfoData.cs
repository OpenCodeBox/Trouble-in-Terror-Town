using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TTTSC.Player.Character.PlayerInfo
{
    [CreateAssetMenu(menuName = "TTTSC/Player/Character/Player Info")]
    public class PlayerInfoData : ScriptableObject
    {
        public int helth;
        public int armor;

        public PlayerStateMachine.playerPlayStates currentPlayerPlayState;

        public PlayerStateMachine.playerClass currentPlayerClass;
    }
}