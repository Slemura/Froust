// ReSharper disable UnusedType.Global
// ReSharper disable UnusedMember.Global

using UnityEngine;

namespace RpDev.Extensions.Unity
{
    public static partial class Extensions
    {
        public static Vector2Int ToVector2Int(this Vector3Int self)
        {
            return new Vector2Int(self.x, self.y);
        }

        public static Vector3Int Abs(this Vector3Int self)
        {
            return new Vector3Int(
                Mathf.Abs(self.x),
                Mathf.Abs(self.y),
                Mathf.Abs(self.z)
            );
        }

        public static int MaxXYZ(this Vector3Int self)
        {
            return Mathf.Max(self.x, self.y, self.z);
        }

        public static int MaxXY(this Vector3Int self)
        {
            return Mathf.Max(self.x, self.y);
        }

        public static int MaxXZ(this Vector3Int self)
        {
            return Mathf.Max(self.x, self.z);
        }
    }
}