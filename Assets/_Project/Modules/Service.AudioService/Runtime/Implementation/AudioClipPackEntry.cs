using System;
using UnityEngine;

namespace RpDev.Services.AudioService
{
    [Serializable]
    public class AudioClipPackEntry
    {
        [SerializeField] private string _name;
        [SerializeField] private AudioClip _audioClip;
        [SerializeField] [HideInInspector] private string _id;

        public static event Action<AudioPlaybackRequest> PlaybackRequested;

        public string Name => _name;
        public AudioClip Clip => _audioClip;
        public string Id => _id;

        private void PlayAs(AudioClipType audioClipType, float volume = 1)
        {
            var request = new AudioPlaybackRequest(
                audioClipType,
                _id,
                volume,
                1
            );

            PlaybackRequested?.Invoke(request);
        }

        public void PlayAsUI(float volume = 1)
        {
            PlayAs(AudioClipType.UI, volume);
        }

        public void PlayAsSfx(float volume = 1)
        {
            PlayAs(AudioClipType.Sfx, volume);
        }

        public void PlayAsMusic(float volume)
        {
            PlayAs(AudioClipType.Music, volume);
        }

        #if UNITY_EDITOR
        public void RegenerateID()
        {
            _id = Guid.NewGuid().ToString();
        }
        #endif
    }
}