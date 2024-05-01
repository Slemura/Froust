using RpDev.ThirdParty.Input.Joysticks.Joysticks;
using UnityEditor;
using UnityEngine;

namespace RpDev.ThirdParty.Input.Joysticks.Editor
{
    #if UNITY_EDITOR
    [CustomEditor(typeof(FloatingJoystick))]
    public class FloatingJoystickEditor : JoystickEditor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (background != null)
            {
                RectTransform backgroundRect = (RectTransform)background.objectReferenceValue;
                backgroundRect.anchorMax = Vector2.zero;
                backgroundRect.anchorMin = Vector2.zero;
                backgroundRect.pivot = center;
            }
        }
    }

    #endif
}