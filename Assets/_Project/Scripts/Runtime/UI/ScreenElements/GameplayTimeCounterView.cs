using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Froust.Runtime.UI
{
    [RequireComponent(typeof(TMP_Text))]
    public class GameplayTimeCounterView : MonoBehaviour
    {
        [SerializeField, Min(1)] private float _pushScale = 1.1f;

        private TMP_Text _textComponent;
        private Tweener _tween;
        private int _seconds;

        public void UpdateTimeInLevel(int seconds)
        {
            if (seconds == _seconds)
                return;

            _seconds = seconds;

            _textComponent.text = _seconds.ToString();

            if (_seconds == 0)
                return;

            _tween.Restart();
        }

        private void Awake()
        {
            _textComponent = GetComponent<TMP_Text>();

            _tween = transform.DOPunchScale(Vector3.one * (_pushScale - 1), 0.2f)
                .SetAutoKill(false)
                .SetLink(gameObject, LinkBehaviour.KillOnDestroy);
        }

        private void Start()
        {
            _textComponent.text = 0.ToString();
        }
    }
}