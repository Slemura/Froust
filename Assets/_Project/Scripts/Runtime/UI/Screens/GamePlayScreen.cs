using Froust.Runtime.UI;
using RpDev.Services.UI;
using TMPro;
using UnityEngine;

namespace Froust.Runtime.Screens
{
    public class GamePlayScreen : UIScreen
    {
        [SerializeField] private WarningOverlay _warningOverlay;
        [SerializeField] private GameplayTimeCounterView _gameplayTimeCounterView;
        [SerializeField] private TMP_Text _enemyInfoTxt;
        [SerializeField] private SimpleToggleButton _musicButton;
        [SerializeField] private SimpleToggleButton _soundButton;
        
        public WarningOverlay WarningOverlay => _warningOverlay;
        public GameplayTimeCounterView LevelTimeCounterView => _gameplayTimeCounterView;

        public SimpleToggleButton MusicButton => _musicButton;
        public SimpleToggleButton SoundButton => _soundButton;

        public void SetupEnemyInfo(string enemyDestroyed)
        {
            _enemyInfoTxt.text = $" Enemy destroyed: {enemyDestroyed}";
        }
    }
}