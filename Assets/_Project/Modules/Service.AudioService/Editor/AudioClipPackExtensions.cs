using UnityEditor;

namespace RpDev.Services.AudioService.Editor
{
    public static class AudioClipPackExtensions
    {
        [MenuItem("CONTEXT/AudioClipPack/Regenerate IDs", false, 200)]
        private static void RegenerateIDsInspector(MenuCommand menuCommand)
        {
            var audioClipPack = menuCommand.context as AudioClipPack;

            RegenerateIDs(audioClipPack);
        }

        private static void RegenerateIDs(AudioClipPack audioClipPack)
        {
            var entries = audioClipPack.Entries;

            foreach (var entry in entries)
                entry.RegenerateID();

            EditorUtility.SetDirty(audioClipPack);
            AssetDatabase.SaveAssets();
        }
    }
}