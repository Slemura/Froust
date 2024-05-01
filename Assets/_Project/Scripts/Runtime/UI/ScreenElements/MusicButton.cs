using System;
using UnityEngine;
using UnityEngine.UI;

namespace Froust.Runtime.UI
{
    [RequireComponent(typeof(Button))]
    public class MusicButton : MonoBehaviour
    {
        [SerializeField] private Image _onImage;
        [SerializeField] private Image _offImage;

        private Button _button;
        public event Action OnClicked;

        private void Awake()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(OnClick);
        }

        private void OnClick()
        {
            OnClicked?.Invoke();
        }

        public void UpdateVisuals(bool isSoundEnabled)
        {
            _onImage.gameObject.SetActive(isSoundEnabled);
            _offImage.gameObject.SetActive(!isSoundEnabled);
        }
    }
}