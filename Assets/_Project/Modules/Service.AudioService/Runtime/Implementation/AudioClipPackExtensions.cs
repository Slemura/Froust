using System.Collections.Generic;
using UnityEngine;

namespace RpDev.Services.AudioService
{
    public static class AudioClipPackExtensions
    {
        public static void PlayRandomAsSfx(this AudioClipPack self, float volume = 1f)
        {
            var entries = self.Entries;
            var entry = entries[Random.Range(0, entries.Count)];

            entry.PlayAsSfx(volume);
        }

        public static void PlayRandomAsMusic(this AudioClipPack self, float volume = 1f)
        {
            var entries = self.Entries;
            var entry = entries[Random.Range(0, entries.Count)];

            entry.PlayAsMusic(volume);
        }
    }
}