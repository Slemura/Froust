using UnityEngine;
using Object = UnityEngine.Object;

namespace RpDev.Extensions.Unity
{
    public static partial class Extensions
    {
        public static void Activate(this GameObject gameObject)
        {
            gameObject.SetActive(true);
        }

        public static void Deactivate(this GameObject gameObject)
        {
            gameObject.SetActive(false);
        }

        public static bool Has<T>(this GameObject gameObject) where T : Component
        {
            return gameObject.TryGetComponent(out T _);
        }

        public static T Get<T>(this GameObject gameObject) where T : Component
        {
            return gameObject.GetComponent<T>();
        }

        public static T[] GetAll<T>(this GameObject gameObject) where T : Component
        {
            return gameObject.GetComponents<T>();
        }

        public static T Instantiate<T>(this GameObject gameObject) where T : Component
        {
            return Object.Instantiate(gameObject).GetComponent<T>();
        }

        public static RectTransform GetRectTransform(this GameObject gameObject)
        {
            return gameObject.transform.ToRectTransform();
        }

        #if UNITY_EDITOR
        public static void DestroyChildrenImmediate(this GameObject obj)
        {
            obj.transform.DestroyChildrenImmediate();
        }
        #endif
    }
}