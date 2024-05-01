using UnityEditor;
using UnityEngine;

namespace Froust.Editor
{
    public static class ScriptableObjectExtensions
    {
        [MenuItem("CONTEXT/ScriptableObject/Copy to Clipboard as JSON", false, 200)]
        private static void CopyToClipboardInspector(MenuCommand menuCommand)
        {
            var scriptableObject = menuCommand.context as ScriptableObject;
            CopyToClipboardAsJson(scriptableObject);
        }

        [MenuItem("Assets/Copy to Clipboard as JSON", false, 9999)]
        private static void CopyToClipboardProject()
        {
            var scriptableObject = Selection.activeObject;

            CopyToClipboardAsJson(scriptableObject);
        }

        [MenuItem("Assets/Copy to Clipboard as JSON", true)]
        private static bool ValidateCopyToClipboardProject()
        {
            var selectedObject = Selection.activeObject;
            return selectedObject is ScriptableObject;
        }

        private static void CopyToClipboardAsJson(Object selectedObject)
        {
            if (selectedObject is not ScriptableObject scriptableObject)
                return;

            var json = JsonUtility.ToJson(scriptableObject, true);
            GUIUtility.systemCopyBuffer = json;
            Debug.Log($"Copied {scriptableObject.name} to clipboard as JSON.");
        }
    }
}