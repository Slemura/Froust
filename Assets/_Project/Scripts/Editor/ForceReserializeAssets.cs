using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Froust.Editor
{
    public class ForceReserializeAssets : UnityEditor.Editor
    {
        [MenuItem("Tools/Force Reserialize Assets")]
        private static void Do()
        {
            var result = EditorUtility.DisplayDialog(
                "Force Reserialize Assets",
                "Are you sure you want to reserialize all assets in the project?",
                "Yes",
                "Cancel"
            );

            if (result == false)
                return;

            var guids = AssetDatabase.FindAssets(string.Empty, new[] { "Assets" });

            var assetPaths = new HashSet<string>();

            for (int i = 0, length = guids.Length; i < length; i++)
            {
                if (string.IsNullOrEmpty(guids[i]))
                    continue;

                var assetPath = AssetDatabase.GUIDToAssetPath(guids[i]);

                if (string.IsNullOrEmpty(assetPath))
                    continue;

                assetPaths.Add(assetPath);

                Debug.Log(assetPath);
            }

            AssetDatabase.ForceReserializeAssets(assetPaths.ToArray());
            AssetDatabase.SaveAssets();

            Debug.Log($"{assetPaths.Count.ToString()} assets re-serialized.");
        }
    }
}