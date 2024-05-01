using UnityEngine;
using UnityEngine.UI;

namespace Froust.Audio
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Toggle))]
    public class UIToggleSound : UIBehaviourSound<Toggle>
    {
        [SerializeField] private bool _playOnToggleOn = true;
        [SerializeField] private bool _playOnToggleOff = true;

        protected override void AddListener()
        {
            Component.onValueChanged.AddListener(PlaySfx);
        }

        private void PlaySfx(bool isOn)
        {
            if (isOn && _playOnToggleOn)
            {
                PlaySfx();
                return;
            }

            if (_playOnToggleOff)
                PlaySfx();
        }
    }
}