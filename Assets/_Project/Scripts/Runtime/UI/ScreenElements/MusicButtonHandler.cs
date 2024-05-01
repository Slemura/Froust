using System;
using Froust.Runtime.Services.Preferences;
using Froust.Runtime.UI;
using VContainer;

namespace Froust.UI.ScreenElements
{
    public class MusicButtonHandler : IDisposable
    {
        private Preferences _preferences;
        private MusicButton _musicButton;

        public MusicButtonHandler() {}
        
        [Inject]
        public void AddDependencies(Preferences preferences)
        {
            _preferences = preferences;
        }

        public void AddMusicButtonView(MusicButton musicButton)
        {
            _musicButton = musicButton;
            _musicButton.OnClicked += OnMusicButtonClicked;
            
            _musicButton.UpdateVisuals(_preferences.IsMusicEnabled.Value);
        }

        private void OnMusicButtonClicked()
        {
            _preferences.IsMusicEnabled.Value = !_preferences.IsMusicEnabled.Value;
            _musicButton.UpdateVisuals(_preferences.IsMusicEnabled.Value);
        }

        public void Dispose()
        {
            _musicButton.OnClicked -= OnMusicButtonClicked;
        }
    }
}