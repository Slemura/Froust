using UnityEngine;
using UnityEngine.UI;

namespace RpDev.Extensions.Unity
{
    public static partial class Extensions
    {
        public static void SetAlpha(this Graphic graphic, float alpha)
        {
            var color = graphic.color;
            color.a = alpha;
            graphic.color = color;
        }
    }
}