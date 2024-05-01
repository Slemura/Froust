using UnityEngine;
using UnityEngine.Audio;

namespace RpDev.Services.AudioService
{
    [CreateAssetMenu(menuName = "Audio/AudioPackLibrary")]
    public class AudioPackLibrary : ScriptableObject
    {
        [SerializeField] private AudioMixer _audioMixer;

        [Header("Sfx")] [SerializeField] private AudioClipPack _punchAudioPack;
        [SerializeField] private AudioClipPack _screamAudioPack;
        [SerializeField] private AudioClipPack _splashAudioPack;
        [SerializeField] private AudioClipPack _commonAudioClipPack;
        
        [SerializeField] private AudioClipReference _iceFoeSinkSfx;
        [SerializeField] private AudioClipReference _gameOverSfx;
        
        [Header("Music")] [SerializeField] private AudioClipPack _musicAudioPack;

        public AudioClipPack PunchAudioPack => _punchAudioPack;

        public AudioClipPack ScreamAudioPack => _screamAudioPack;

        public AudioClipPack SplashAudioPack => _splashAudioPack;

        public AudioClipPack CommonAudioClipPack => _commonAudioClipPack;

        public AudioMixer Mixer => _audioMixer;

        public AudioClipPack MusicAudioPack => _musicAudioPack;

        public AudioClipReference IceFoeSinkSfx => _iceFoeSinkSfx;

        public AudioClipReference GameOverSfx => _gameOverSfx;
    }
}