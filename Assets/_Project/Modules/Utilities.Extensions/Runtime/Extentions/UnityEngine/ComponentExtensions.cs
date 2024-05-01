using UnityEngine;

namespace RpDev.Extensions.Unity
{
    public static partial class Extensions
    {
        public static bool Has<T>(this Component component) where T : Component
        {
            return component.TryGetComponent(out T _);
        }

        public static T Get<T>(this Component component) where T : Component
        {
            return component.GetComponent<T>();
        }

        public static T[] GetAll<T>(this Component component) where T : Component
        {
            return component.GetComponents<T>();
        }

        public static RectTransform GetRectTransform(this Component component)
        {
            return component.transform.ToRectTransform();
        }

        public static void DeactivateGameObject(this Component component)
        {
            component.gameObject.SetActive(false);
        }

        public static void ActivateGameObject(this Component component)
        {
            component.gameObject.SetActive(true);
        }

        public static void DestroyGameObject(this Component component)
        {
            Object.Destroy(component.gameObject);
        }

        public static T WithGameObjectName<T>(this T self, string name) where T : Component
        {
            self.gameObject.name = name;

            return self;
        }
    }
}