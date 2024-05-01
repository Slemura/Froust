// ReSharper disable UnusedType.Global
// ReSharper disable UnusedMember.Global

using System;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace RpDev.Extensions.Unity
{
    public static partial class Extensions
    {
        public static RectTransform ToRectTransform(this Transform transform)
        {
            if (transform is RectTransform rectTransform)
                return rectTransform;

            throw new Exception($"Transform \"{transform.gameObject}\" is not a RectTransform");
        }

        public static void DestroyChildren(this Transform transform)
        {
            foreach (Transform child in transform)
                Object.Destroy(child.gameObject);
        }

        #if UNITY_EDITOR
        public static string GetFullPath(this Transform transform)
        {
            var root = transform.root;
            return $"{root.name}/{AnimationUtility.CalculateTransformPath(transform, root)}";
        }

        public static void DestroyChildrenImmediate(this Transform transform)
        {
            while (transform.childCount != 0)
                Object.DestroyImmediate(transform.GetChild(0).gameObject);
        }
        #endif
    }
}