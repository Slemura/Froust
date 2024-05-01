using System;
using UnityEditor;
using UnityEngine;

namespace RpDev.Services.AudioService.Editor
{
    [CustomPropertyDrawer(typeof(AudioClipReference))]
    public class AudioClipReferencePropertyDrawer : PropertyDrawer
    {
        private readonly GUIStyle _volumeLabelStyle = new(EditorStyles.label) { alignment = TextAnchor.MiddleRight };

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUIUtility.singleLineHeight * 3 + EditorGUIUtility.standardVerticalSpacing * 2 + 4;
        }

        // ReSharper disable once CognitiveComplexity
        public override void OnGUI(Rect position, SerializedProperty audioClipReferenceProperty, GUIContent label)
        {
            var serializedObject = audioClipReferenceProperty.serializedObject;
            var targetObject = serializedObject.targetObject;

            EditorGUI.BeginProperty(position, label, audioClipReferenceProperty);

            var audioClipPackProperty =
                audioClipReferenceProperty.FindPropertyRelative("_audioClipPack");

            EditorGUI.HelpBox(
                new Rect(position.x, position.y, position.width,
                    position.height - EditorGUIUtility.standardVerticalSpacing),
                string.Empty,
                MessageType.None
            );

            EditorGUI.LabelField(
                new Rect(position.x + 4, position.y, position.width / 2 - EditorGUIUtility.standardVerticalSpacing / 2,
                    EditorGUIUtility.singleLineHeight),
                label
            );

            SerializedProperty entriesProperty = null;

            if (audioClipPackProperty.objectReferenceValue == null)
            {
                EditorGUI.PropertyField(
                    new Rect(
                        position.x + 4,
                        position.y + EditorGUIUtility.singleLineHeight,
                        position.width - 8,
                        EditorGUIUtility.singleLineHeight),
                    audioClipPackProperty,
                    GUIContent.none
                );
            }
            else
            {
                EditorGUI.PropertyField(
                    new Rect(
                        position.x + 4,
                        position.y + EditorGUIUtility.singleLineHeight,
                        position.width / 2 - EditorGUIUtility.standardVerticalSpacing / 2 - 8,
                        EditorGUIUtility.singleLineHeight),
                    audioClipPackProperty,
                    GUIContent.none
                );

                var audioClipPack = (AudioClipPack)audioClipPackProperty.objectReferenceValue;

                if (audioClipPack != null)
                {
                    var serializedAudioClipPack = new SerializedObject(audioClipPack);
                    entriesProperty = serializedAudioClipPack.FindProperty("_entries");
                }
            }

            if (entriesProperty == null || entriesProperty.arraySize == 0)
            {
                if (audioClipPackProperty.objectReferenceValue != null)
                    EditorGUI.LabelField(
                        new Rect(
                            position.width / 2 + EditorGUIUtility.standardVerticalSpacing / 2 + 16,
                            position.y + EditorGUIUtility.singleLineHeight,
                            position.width / 2 - EditorGUIUtility.standardVerticalSpacing / 2 - 8,
                            EditorGUIUtility.singleLineHeight),
                        "Pack is empty."
                    );
            }
            else
            {
                var audioClipNames = new string[entriesProperty.arraySize];
                var audioClipIds = new string[entriesProperty.arraySize];

                for (var i = 0; i < audioClipNames.Length; i++)
                {
                    var audioClipPackEntryProperty = entriesProperty.GetArrayElementAtIndex(i);
                    audioClipNames[i] = audioClipPackEntryProperty.FindPropertyRelative("_name").stringValue;
                    audioClipIds[i] = audioClipPackEntryProperty.FindPropertyRelative("_id").stringValue;
                }

                var audioClipReferenceId = audioClipReferenceProperty.FindPropertyRelative("_id");
                var audioClipReferenceVolume =
                    audioClipReferenceProperty.FindPropertyRelative("_volume");

                var currentAudioClipIdIndex =
                    Mathf.Max(0, Array.IndexOf(audioClipIds, audioClipReferenceId.stringValue));

                var id = audioClipIds[EditorGUI.Popup(
                    new Rect(
                        position.width / 2 + EditorGUIUtility.standardVerticalSpacing / 2 + 16,
                        position.y + EditorGUIUtility.singleLineHeight,
                        position.width / 2 - EditorGUIUtility.standardVerticalSpacing / 2 - 2,
                        EditorGUIUtility.singleLineHeight
                    ),
                    currentAudioClipIdIndex,
                    audioClipNames
                )];

                audioClipReferenceId.stringValue = id;

                EditorGUI.LabelField(
                    new Rect(
                        position.x + 4,
                        position.y + EditorGUIUtility.singleLineHeight * 2 + EditorGUIUtility.standardVerticalSpacing,
                        position.width / 2 - EditorGUIUtility.standardVerticalSpacing / 2 - 8,
                        EditorGUIUtility.singleLineHeight),
                    "Volume",
                    _volumeLabelStyle
                );

                EditorGUI.Slider(
                    new Rect(
                        position.width / 2 + EditorGUIUtility.standardVerticalSpacing / 2 + 16,
                        position.y + EditorGUIUtility.singleLineHeight * 2 + EditorGUIUtility.standardVerticalSpacing,
                        position.width / 2 - EditorGUIUtility.standardVerticalSpacing / 2 - 2,
                        EditorGUIUtility.singleLineHeight
                    ),
                    audioClipReferenceVolume,
                    0,
                    1,
                    GUIContent.none
                );
            }

            EditorGUI.EndProperty();

            if (serializedObject.ApplyModifiedProperties())
                EditorUtility.SetDirty(targetObject);
        }
    }
}