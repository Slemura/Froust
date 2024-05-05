using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Audio;
using VContainer;

namespace RpDev.Services.AudioService
{
    public class AudioService : MonoBehaviour, IDisposable
    {
        private AudioMixer _mixer;

        private const string MasterVolumeKey = "Master Volume";

        private readonly Dictionary<AudioClipType, AudioGroupInfo> _audioGroups = new();
        private readonly Dictionary<string, AudioClip> _audioClipMap = new();

        public void AddAudioMixer(AudioMixer audioMixer)
        {
            _mixer = audioMixer;
        }
        
        public void AddAudioClipPacks(AudioClipPack[] audioClipPacks)
        {
            PopulateAudioClipMap(audioClipPacks);
        }

        public void Init()
        {
            RegisterMixerGroup(AudioClipType.UI, false, false);
            RegisterMixerGroup(AudioClipType.Sfx, false, true);
            RegisterMixerGroup(AudioClipType.Music, true, true);

            AudioClipReference.PlaybackRequested += OnPlaybackRequested;
            AudioClipPackEntry.PlaybackRequested += OnPlaybackRequested;
        }

        public void EnableSound(bool state)
        {
            _mixer.SetFloat(MasterVolumeKey, state ? 0 : -80);
        }
        
        public void EnableUISounds(bool state)
        {
            _audioGroups[AudioClipType.UI].AudioSource.mute = !state;
        }

        public void EnableSfx(bool state)
        {
            _audioGroups[AudioClipType.Sfx].AudioSource.mute = !state;
        }

        public void EnableMusic(bool state)
        {
            _audioGroups[AudioClipType.Music].AudioSource.mute = !state;
        }

        public void EnableChannel(AudioClipType channel, bool status)
        {
            _audioGroups[channel].AudioSource.mute = !status;
        }

        public void Dispose()
        {
            foreach (var groupInfo in _audioGroups.Values)
                groupInfo.Dispose();

            AudioClipReference.PlaybackRequested -= OnPlaybackRequested;
            AudioClipPackEntry.PlaybackRequested -= OnPlaybackRequested;
        }
        
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        private void PopulateAudioClipMap(AudioClipPack[] audioClipPacks)
        {
            _audioClipMap.Clear();

            foreach (var audioClipPack in audioClipPacks)
            {
                foreach (var (id, audioClip) in audioClipPack.GetAudioClipMap())
                    _audioClipMap.Add(id, audioClip);
            }
        }

        private void OnPlaybackRequested(AudioPlaybackRequest request)
        {
            if (_audioGroups.TryGetValue(request.AudioClipType, out var audioGroupInfo) == false)
                throw new Exception($"Audio group '{request.AudioClipType}' not registered.");

            var audioSource = audioGroupInfo.AudioSource;

            switch (request.AudioClipType)
            {
                case AudioClipType.UI:
                    audioSource.PlayOneShot(
                        _audioClipMap[request.AudioClipId],
                        Mathf.Clamp01(request.Volume)
                    );
                    break;
                case AudioClipType.Sfx:
                    audioSource.PlayOneShot(
                        _audioClipMap[request.AudioClipId],
                        Mathf.Clamp01(request.Volume)
                    );
                    break;
                case AudioClipType.Music:
                    audioSource.clip = _audioClipMap[request.AudioClipId];

                    if (audioSource.isPlaying)
                        audioSource.Stop();

                    audioSource.volume = Mathf.Clamp01(request.Volume);
                    audioSource.Play();

                    break;
                case AudioClipType.Ambient:
                    break;
                case AudioClipType.Voice:
                    break;
                case AudioClipType.Footsteps:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void RegisterMixerGroup(AudioClipType type, bool loop, bool interruptOnSceneChange)
        {
            var groupName = Enum.GetName(typeof(AudioClipType), type);

            var matchingGroups = _mixer.FindMatchingGroups(groupName);

            Assert.IsTrue(matchingGroups.Length > 0, $"'{groupName}' mixer group not found.");

            var group = matchingGroups[0];

            var groupRoot = new GameObject(groupName);

            groupRoot.transform.SetParent(transform);

            var audioSource = groupRoot.AddComponent<AudioSource>();

            audioSource.outputAudioMixerGroup = group;
            audioSource.playOnAwake = false;
            audioSource.loop = loop;

            _audioGroups.Add(type, new AudioGroupInfo(
                audioSource,
                interruptOnSceneChange
            ));
        }
    }
}