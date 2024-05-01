// ReSharper disable UnusedMember.Global

using System;
using UnityEngine;

namespace RpDev.Extensions.Unity
{
    public static partial class Extensions
    {
        public static Vector2 ToVector2(this Vector3 vector)
        {
            return new Vector2(vector.x, vector.y);
        }

        public static Vector3 WithZ(this Vector3 vector, float z)
        {
            vector.z = z;
            return vector;
        }

        public static Vector3 WithY(this Vector3 vector, float y)
        {
            vector.y = y;
            return vector;
        }

        public static Vector3 Abs(this Vector3 vector)
        {
            return new Vector3(
                Mathf.Abs(vector.x),
                Mathf.Abs(vector.y),
                Mathf.Abs(vector.z)
            );
        }

        public static Vector3 OnScreen(this Vector3 vector, Camera camera)
        {
            return camera.WorldToScreenPoint(vector);
        }

        public static Vector3Int FloorToVector3Int(this Vector3 vector)
        {
            return ToVector3IntInternal(vector, Mathf.FloorToInt);
        }

        private static Vector3Int ToVector3IntInternal(this Vector3 vector, Func<float, int> func)
        {
            return new Vector3Int(func(vector.x), func(vector.y), func(vector.z));
        }

        public static Vector3 Floor(this Vector3 self)
        {
            return new Vector3(
                Mathf.Floor(self.x),
                Mathf.Floor(self.y),
                Mathf.Floor(self.z)
            );
        }

        public static Vector3 Round(this Vector3 self)
        {
            return new Vector3(
                Mathf.Round(self.x),
                Mathf.Round(self.y),
                Mathf.Round(self.z)
            );
        }

        public static Vector3 Ceil(this Vector3 self)
        {
            return new Vector3(
                Mathf.Ceil(self.x),
                Mathf.Ceil(self.y),
                Mathf.Ceil(self.z)
            );
        }

        // TODO вынести в модуль с методами расширения
        public static float MaxXYZ(this Vector3 self)
        {
            return Mathf.Max(self.x, self.y, self.z);
        }

        // TODO вынести в модуль с методами расширения
        public static float MaxXY(this Vector3 self)
        {
            return Mathf.Max(self.x, self.y);
        }

        // TODO вынести в модуль с методами расширения
        public static float MaxXZ(this Vector3 self)
        {
            return Mathf.Max(self.x, self.z);
        }

        // TODO вынести в модуль с методами расширения
        public static Vector3 SwapYZ(this Vector3 self)
        {
            return new Vector3(self.x, self.z, self.y);
        }
    }
}