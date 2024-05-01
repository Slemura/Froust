using UnityEngine;

namespace RpDev.Extensions.Unity
{
    public static partial class Extensions
    {
        public static RectInt ToRectInt(this Rect rect)
        {
            return new RectInt(
                Mathf.RoundToInt(rect.xMin),
                Mathf.RoundToInt(rect.yMin),
                Mathf.RoundToInt(rect.width),
                Mathf.RoundToInt(rect.height)
            );
        }

        public static Rect WithY(this Rect rect, float y)
        {
            rect.y = y;
            return rect;
        }

        public static Rect WithX(this Rect rect, float x)
        {
            rect.x = x;
            return rect;
        }

        public static Rect WithWidth(this Rect rect, float width)
        {
            rect.width = width;
            return rect;
        }

        public static Rect WithHeight(this Rect rect, float height)
        {
            rect.height = height;
            return rect;
        }

        public static Rect WithPadding(this Rect rect, float padding)
        {
            return WithPadding(rect, padding, padding, padding, padding);
        }

        public static Rect WithPadding(this Rect rect, float left, float right, float top, float bottom)
        {
            rect.xMin += left;
            rect.xMax -= right;
            rect.yMin += top;
            rect.yMax -= bottom;

            return rect;
        }
    }
}