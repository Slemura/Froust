// ReSharper disable UnusedMember.Global

using UnityEngine;

namespace RpDev.Extensions.Unity
{
    public static partial class Extensions
    {
        public static Vector3[] GetWorldCorners(this RectTransform rectTransform)
        {
            Vector3[] corners = new Vector3[4];
            rectTransform.GetWorldCorners(corners);

            return corners;
        }

        public static Vector3[] GetLocalCorners(this RectTransform rectTransform)
        {
            Vector3[] corners = new Vector3[4];
            rectTransform.GetLocalCorners(corners);

            return corners;
        }

        public static Vector3 GetBottomLeftCornerWorld(this RectTransform rectTransform)
        {
            return rectTransform.GetWorldCorners()[0];
        }

        public static Vector3 GetTopLeftCornerWorld(this RectTransform rectTransform)
        {
            return rectTransform.GetWorldCorners()[1];
        }

        public static Vector3 GetTopRightCornerWorld(this RectTransform rectTransform)
        {
            return rectTransform.GetWorldCorners()[2];
        }

        public static Vector3 GetBottomRightCornerWorld(this RectTransform rectTransform)
        {
            return rectTransform.GetWorldCorners()[3];
        }

        public static Vector3 GetBottomLeftCornerLocal(this RectTransform rectTransform)
        {
            return rectTransform.GetLocalCorners()[0];
        }

        public static Vector3 GetTopLeftCornerLocal(this RectTransform rectTransform)
        {
            return rectTransform.GetLocalCorners()[1];
        }

        public static Vector3 GetTopRightCornerLocal(this RectTransform rectTransform)
        {
            return rectTransform.GetLocalCorners()[2];
        }

        public static Vector3 GetBottomRightCornerLocal(this RectTransform rectTransform)
        {
            return rectTransform.GetLocalCorners()[3];
        }

        public static Rect GetWorldRect(this RectTransform rectTransform)
        {
            Vector3[] corners = rectTransform.GetWorldCorners();

            Bounds bounds = new Bounds(corners[0], Vector3.zero);
            bounds.Encapsulate(corners[2]);

            return new Rect(new Vector2(bounds.min.x, bounds.min.y), bounds.size);
        }

        public static RectInt GetScreenRect(this RectTransform rectTransform, Camera camera)
        {
            Rect rect = rectTransform.GetWorldRect();

            Vector2 rectMax = camera.WorldToScreenPoint(rect.max);
            Vector2 rectMin = camera.WorldToScreenPoint(rect.min);

            Vector2 position = new Vector3(rectMin.x, rectMin.y);

            Vector2 size = new Vector2(
                rectMax.x - rectMin.x,
                rectMax.y - rectMin.y
            );

            return new Rect(position, size).ToRectInt();
        }
    }
}