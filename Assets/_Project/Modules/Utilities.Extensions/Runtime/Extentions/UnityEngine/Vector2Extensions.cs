using UnityEngine;

namespace RpDev.Extensions.Unity
{
    public static partial class Extensions
    {
        public static Vector2 InvertedX(this Vector2 vector)
        {
            vector.x *= -1;
            return vector;
        }

        public static Vector2 InvertedY(this Vector2 vector)
        {
            vector.y *= -1;
            return vector;
        }

        public static Vector2 Clamped01(this Vector2 vector)
        {
            vector.x = Mathf.Clamp01(vector.x);
            vector.y = Mathf.Clamp01(vector.y);

            return vector;
        }
    }
}