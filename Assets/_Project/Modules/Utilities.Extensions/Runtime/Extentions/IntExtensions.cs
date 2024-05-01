using UnityEngine;

namespace RpDev.Extensions
{
    public static class IntExtensions
    {
        public static Vector2Int ToVector2Int(this int self)
        {
            return new Vector2Int(self, self);
        }
    }
}