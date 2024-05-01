// ReSharper disable UnusedMember.Global

using System.Collections.Generic;
using System.Reflection;
using UnityEngine.UI;

namespace RpDev.Extensions.Unity
{
    public static partial class Extensions
    {
        public static IReadOnlyList<Toggle> GetToggles(this ToggleGroup toggleGroup)
        {
            return toggleGroup.GetNonPublicValue<FieldInfo>("m_Toggles")
                .GetValue(toggleGroup) as IReadOnlyList<Toggle>;
        }
    }
}