using System;
using Froust.Runtime.Services.Preferences;
using RpDev.Services.AudioService;
using VContainer.Unity;

namespace Froust
{
    public class GameAudioHandler : IInitializable, IDisposable
    {
        private readonly AudioService _audioService;
        private readonly Preferences _preferences;
        private AudioPackLibrary _audioPackLibrary;

        public AudioPackLibrary PackLibrary => _audioPackLibrary;
        
        public GameAudioHandler(AudioService audioService, Preferences preferences)
        {
            _audioService = audioService;
            _preferences = preferences;
        }

        public void Initialize()
        {
            _preferences.IsMusicEnabled.AddListener(OnMusicEnableChanged);
            _preferences.IsSoundEnabled.AddListener(OnSoundEnableChanged);
        }

        public void AddAudioLibrary(AudioPackLibrary audioPackLibrary)
        {
            _audioPackLibrary = audioPackLibrary;
            _audioService.AddAudioMixer(_audioPackLibrary.Mixer);
            _audioService.AddAudioClipPacks(new []
            {
                _audioPackLibrary.PunchAudioPack,
                _audioPackLibrary.ScreamAudioPack,
                _audioPackLibrary.SplashAudioPack,
                _audioPackLibrary.CommonAudioClipPack,
                _audioPackLibrary.MusicAudioPack
            });
            
            _audioService.Init();
            
            OnSoundEnableChanged(_preferences.IsSoundEnabled.Value);
            OnMusicEnableChanged(_preferences.IsMusicEnabled.Value);
            
            _audioPackLibrary.MusicAudioPack.PlayRandomAsMusic();
        }

        public void PlayRandomSplashSound()
        {
            _audioPackLibrary.SplashAudioPack.PlayRandomAsSfx();
        }

        public void PlayRandomPunchSound()
        {
            _audioPackLibrary.PunchAudioPack.PlayRandomAsSfx();
        }

        public void PlayRandomScreamSound()
        {
            _audioPackLibrary.ScreamAudioPack.PlayRandomAsSfx();
        }

        public void PlayIceFoeSinkSound()
        {
            _audioPackLibrary.IceFoeSinkSfx.PlayAsSfx();
        }

        public void PlayLooseSound()
        {
            _audioPackLibrary.GameOverSfx.PlayAsSfx();
        }

        public void Dispose()
        {
            _preferences.IsMusicEnabled.RemoveListener(OnMusicEnableChanged);
            _preferences.IsSoundEnabled.RemoveListener(OnSoundEnableChanged);
        }
        
        private void OnSoundEnableChanged(bool value)
        {
            _audioService.EnableChannel(AudioClipType.Sfx, value);
            _audioService.EnableChannel(AudioClipType.UI, value);
        }

        private void OnMusicEnableChanged(bool value)
        {
            _audioService.EnableChannel(AudioClipType.Music, value);
        }
    }
}