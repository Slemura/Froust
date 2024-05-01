using UnityEngine;

namespace RpDev.Extensions.Unity
{
    public static partial class Extensions
    {
        public static void Enable(this Behaviour self)
        {
            self.enabled = true;
        }

        public static void Disable(this Behaviour component)
        {
            component.enabled = false;
        }
    }
}