using System;
using DG.Tweening;
using Froust.Runtime.UI;
using RpDev.Services.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Froust.Runtime.Screens
{
    public class MainMenuScreen : UIScreen
    {
        [SerializeField] private TMP_Text _infoText;
        [SerializeField] private Transform _snowman;
        [SerializeField] private TMP_Text _bestResultText;
        [SerializeField] private Button _startGameButton;
        [SerializeField] private GameObject _controlsInfo;
        [SerializeField] private Color _warningColor;
        [SerializeField] private MusicButton _musicButton;
        
        public event Action OnStartGameClicked;

        public MusicButton MusicButton => _musicButton;

        protected override void Awake()
        {
            base.Awake();
            _startGameButton.onClick.AddListener(StartGameClick);

            _controlsInfo.SetActive(SystemInfo.deviceType != DeviceType.Handheld);
        }

        public void SetupInfo(int bestScore)
        {
            _bestResultText.gameObject.SetActive(bestScore > 0);
            _bestResultText.text = string.Format(_bestResultText.text, bestScore);

            _infoText.transform.DORotate(new Vector3(0, 0, -5), 1f)
                .SetEase(Ease.InOutSine)
                .SetLoops(-1, LoopType.Yoyo)
                .SetLink(gameObject, LinkBehaviour.KillOnDestroy);

            _infoText.DOColor(_warningColor, 1f)
                .From(Color.white)
                .SetEase(Ease.InOutSine)
                .SetLoops(-1, LoopType.Yoyo)
                .SetLink(gameObject, LinkBehaviour.KillOnDestroy);

            _snowman.DORotate(new Vector3(0, 0, -10), 1f)
                .SetEase(Ease.InOutSine)
                .SetLoops(-1, LoopType.Yoyo)
                .SetLink(gameObject, LinkBehaviour.KillOnDestroy);

            _snowman.DOScale(new Vector3(-1.15f, 1.15f, 1.15f), 1f)
                .SetEase(Ease.InOutSine)
                .SetLoops(-1, LoopType.Yoyo)
                .SetLink(gameObject, LinkBehaviour.KillOnDestroy);
        }

        private void StartGameClick()
        {
            OnStartGameClicked?.Invoke();
        }

        private void OnDestroy()
        {
            _startGameButton.onClick.RemoveListener(StartGameClick);
        }
    }
}