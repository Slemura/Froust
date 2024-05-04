using System;
using Froust.Runtime.UI;
using RpDev.Services.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Froust.Runtime.Screens
{
    public class GameOverScreen : UIScreen
    {
        [SerializeField] private TMP_Text _defaultText;
        [SerializeField] private TMP_Text _bestScoreText;
        [SerializeField] private TMP_Text _bestResultText;
        [SerializeField] private Button _startGameButton;
        [SerializeField] private SimpleToggleButton _musicButton;
        [SerializeField] private SimpleToggleButton _soundButton;
        public event Action OnStartGameClicked;
        public SimpleToggleButton MusicButton => _musicButton;
        public SimpleToggleButton SoundButton => _soundButton;

        public void SetupInfo(int currentStandSeconds, int bestScore)
        {
            if (currentStandSeconds > bestScore)
            {
                _defaultText.gameObject.SetActive(false);
                _bestScoreText.gameObject.SetActive(true);
                _bestResultText.gameObject.SetActive(false);
            }
            else
            {
                _defaultText.gameObject.SetActive(true);
                _bestScoreText.gameObject.SetActive(false);
                _bestResultText.gameObject.SetActive(bestScore > 0);
            }

            _defaultText.text = string.Format(_defaultText.text, currentStandSeconds);
            _bestScoreText.text = string.Format(_bestScoreText.text, currentStandSeconds);
            _bestResultText.text = string.Format(_bestResultText.text, bestScore);
        }

        private void Start()
        {
            _startGameButton.onClick.AddListener(StartGame);
        }

        private void StartGame()
        {
            OnStartGameClicked?.Invoke();
        }

        private void OnDestroy()
        {
            _startGameButton.onClick.RemoveListener(StartGame);
        }
    }
}