using System;
using Froust.Runtime.Services.Preferences;
using Froust.Runtime.UI;
using VContainer;

namespace Froust.UI.Handlers
{
    public class SoundButtonHandler : IDisposable
    {
        private Preferences _preferences;
        private SimpleToggleButton _soundButton;
        
        [Inject]
        public void AddDependencies(Preferences preferences)
        {
            _preferences = preferences;
        }

        public void AddSoundButtonView(SimpleToggleButton musicButton)
        {
            _soundButton = musicButton;
            _soundButton.OnClicked += OnSoundButtonClicked;
            
            _soundButton.UpdateVisuals(_preferences.IsSoundEnabled.Value);
        }

        private void OnSoundButtonClicked()
        {
            _preferences.IsSoundEnabled.Value = !_preferences.IsSoundEnabled.Value;
            _soundButton.UpdateVisuals(_preferences.IsSoundEnabled.Value);
        }

        public void Dispose()
        {
            _soundButton.OnClicked -= OnSoundButtonClicked;
        }
    }
}