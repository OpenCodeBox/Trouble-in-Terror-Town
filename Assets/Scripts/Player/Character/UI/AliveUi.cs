using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace TTTSC.Player.Character
{
    public class AliveUi : MonoBehaviour
    {
        private PlayerGhostReffrenceHub _playerGhostReffrenceHub;
        private PlayerGhost _playerGhost;
        private PlayerInfo.PlayerInfoData _playerInfoData;
        [SerializeField]
        private TMP_Text _healthDisplay;
        private TMP_Text _roleDisplay;
        [SerializeField]
        private Slider _healthBar;

        // Start is called before the first frame update
        void Start()
        {
            _playerGhostReffrenceHub = GetComponentInParent<PlayerGhostReffrenceHub>();
            _playerGhost = _playerGhostReffrenceHub.playerGhost;
            _playerInfoData = _playerGhost.playerInfoData;
        }

        // Update is called once per frame
        void Update()
        {
            _healthDisplay.text = _playerInfoData.helth.ToString();
            _healthBar.value = _playerInfoData.helth;
        }
    }
}

