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
        [SerializeField] private MusicButton _musicButton;
        
        public WarningOverlay WarningOverlay => _warningOverlay;
        public GameplayTimeCounterView LevelTimeCounterView => _gameplayTimeCounterView;

        public MusicButton MusicButton => _musicButton;

        public void SetupEnemyInfo(string enemyDestroyed)
        {
            _enemyInfoTxt.text = $" Enemy destroyed: {enemyDestroyed}";
        }
    }
}