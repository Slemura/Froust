using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Froust.Editor
{
    public static class SelectObjectsWithMissingComponent
    {
        [MenuItem("Tools/Select objects with missing components")]
        public static void Do()
        {
            var isInPrefabStage = UnityEditor.SceneManagement.PrefabStageUtility.GetCurrentPrefabStage() != null;

            var scene = isInPrefabStage
                ? UnityEditor.SceneManagement.PrefabStageUtility.GetCurrentPrefabStage().scene
                : SceneManager.GetActiveScene();

            var rootObjects = new List<GameObject>();
            scene.GetRootGameObjects(rootObjects);

            var result = new HashSet<Object>();

            foreach (var rootObject in rootObjects)
            {
                var gameObjects = rootObject.GetComponentsInChildren<Transform>(true)
                    .Select(_ => _.gameObject);

                foreach (var gameObject in gameObjects)
                {
                    if (gameObject.GetComponents<Component>().All(component => component != null))
                        continue;

                    result.Add(gameObject);
                }
            }

            Selection.objects = result.ToArray();
        }
    }
}