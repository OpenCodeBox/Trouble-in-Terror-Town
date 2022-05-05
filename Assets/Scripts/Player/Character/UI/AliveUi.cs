using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

namespace TTTSC.Player.Character
{
    public class AliveUi : MonoBehaviour
    {
        private PlayerGhostReffrenceHub _playerGhostReffrenceHub;
        private PlayerCharacterInfo.PlayerCharacterInfoData _playerInfoData;
        [SerializeField]
        private TMP_Text _healthDisplay;
        [SerializeField]
        private Slider _healthBar;
        [SerializeField]
        private TMP_Text _roleDisplay;
        private Image _roleBackground;
        private RoundSystem _roundSystem;
        [SerializeField]
        private TMP_Text _timer;

        // Start is called before the first frame update
        void Start()
        {
            _playerGhostReffrenceHub = GetComponentInParent<PlayerGhostReffrenceHub>();
            _playerInfoData = _playerGhostReffrenceHub.playerInfoData;
            _roundSystem = FindObjectOfType<RoundSystem>();
        }

        // Update is called once per frame
        void Update()
        {
            _healthDisplay.text = _playerInfoData.helth.ToString();
            _healthBar.value = _playerInfoData.helth;

            //UiClock();
        }

        private void UiClock()
        {
            var timeUntillStart = _roundSystem.currentTimer - ((DateTimeOffset)DateTime.UtcNow).ToUnixTimeSeconds();

            var unixMinutes = timeUntillStart / 60;
            var unixSeconds = timeUntillStart % 60;

            string minutes = null;
            string seconds = null;

            if (unixMinutes > 9)
            {
                minutes = unixMinutes.ToString();
            }
            else
            {
                minutes = "0" + unixMinutes.ToString();
            }

            if (unixSeconds > 9)
            {
                seconds = unixSeconds.ToString();
            }
            else
            {
                seconds = "0" + unixSeconds.ToString();
            }

            _timer.text = minutes.ToString() + ":" + seconds;
        }
    }
}

