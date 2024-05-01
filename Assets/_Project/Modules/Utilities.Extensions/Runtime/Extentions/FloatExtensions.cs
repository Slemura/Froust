using UnityEngine;

namespace RpDev.Extensions
{
    public static partial class Extensions
    {
        public static Vector3 ToVector3(this float value)
        {
            return new Vector3(value, value, value);
        }

        public static Vector2 ToVector2(this float value)
        {
            return new Vector2(value, value);
        }

        public static void Clamp(this ref float value, float min, float max)
        {
            value = Mathf.Clamp(value, min, max);
        }

        public static float Clamped(this float value, float min, float max)
        {
            return Mathf.Clamp(value, min, max);
        }

        /// <summary>
        /// Modifies value by clamping between 0 and 1
        /// </summary>
        public static void Clamp01(this ref float value)
        {
            value = Mathf.Clamp01(value);
        }

        /// <summary>
        /// Returns a new clamped value between 0 and 1  
        /// </summary>
        /// <returns>float</returns>
        public static float Clamped01(this float value)
        {
            return Mathf.Clamp01(value);
        }

        public static float OneMinus(this float value)
        {
            return 1 - value;
        }
    }
}