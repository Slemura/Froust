using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Froust.Editor
{
    public static class SetCurrentSceneAsInitial
    {
        private const string MenuName = "Start From Scene";

        [MenuItem("Tools/" + MenuName + "/Set Current Scene As Initial")]
        public static void Set()
        {
            var activeScene = SceneManager.GetActiveScene();
            EditorSceneManager.playModeStartScene = AssetDatabase.LoadAssetAtPath<SceneAsset>(activeScene.path);

            Debug.Log($"'{activeScene.name}' is set as Play Mode Start Scene.");
        }

        [MenuItem("Tools/" + MenuName + "/Reset Initial Scene")]
        public static void Reset()
        {
            EditorSceneManager.playModeStartScene = null;
            Debug.Log($"Play Mode Start Scene cleared.");
        }
    }
}