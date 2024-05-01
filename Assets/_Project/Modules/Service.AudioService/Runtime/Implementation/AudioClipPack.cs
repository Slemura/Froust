using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace RpDev.Services.AudioService
{
    [CreateAssetMenu(menuName = "Resources/AudioClip Pack")]
    public class AudioClipPack : ScriptableObject
    {
        [SerializeField] private AudioClipPackEntry[] _entries;

        public IReadOnlyList<AudioClipPackEntry> Entries => _entries;

        private void CheckForDuplicateNames()
        {
            var duplicateNames = _entries.GroupBy(entry => entry.Name)
                .Where(group => group.Count() > 1)
                .Select(duplicate => duplicate.Key).ToArray();

            if (duplicateNames.Any())
                throw new Exception(
                    $"{nameof(AudioClipPack)} \"{name}\" contains duplicate names.\nNames:\n{string.Join(",\n", duplicateNames)}");
        }

        private void CheckForEmptyNames()
        {
            if (_entries.Any(entry => string.IsNullOrEmpty(entry.Name)))
                throw new Exception($"{nameof(AudioClipPack)} \"{name}\" contains entries with empty names.");
        }

        public Dictionary<string, AudioClip> GetAudioClipMap()
        {
            CheckForDuplicateNames();
            CheckForEmptyNames();

            return _entries.ToDictionary(entry => entry.Id, entry => entry.Clip);
        }
    }
}