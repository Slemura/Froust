using JetBrains.Annotations;
using RpDev.Services.Preferences.Values;

namespace Froust.Runtime.Services.Preferences
{
    [UsedImplicitly]
    public class Preferences
    {
        public readonly PrefBool IsSoundEnabled = new PrefBool("sound_enabled", true);
        public readonly PrefBool IsMusicEnabled = new PrefBool("music_enabled", true);

        public void ResetAll()
        {
            IsSoundEnabled.SetToDefault();
            IsMusicEnabled.SetToDefault();
        }
    }
}