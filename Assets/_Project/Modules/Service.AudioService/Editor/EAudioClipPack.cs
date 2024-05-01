using System;
using RpDev.Services.AudioService;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace RpDev.Services.AudioService.Editor
{
    [CustomEditor(typeof(AudioClipPack))]
    public class EAudioClipPack : UnityEditor.Editor
    {
        private AudioClipPack _audioClipPack;
        private ReorderableList _entriesList;
        private SerializedProperty _entriesListProperty;

        private void OnEnable()
        {
            _audioClipPack = target as AudioClipPack;
            _entriesListProperty = serializedObject.FindProperty("_entries");

            CreateEntriesList();
        }

        private void CreateEntriesList()
        {
            _entriesList = new ReorderableList(serializedObject, _entriesListProperty, true, true, true, true)
            {
                drawHeaderCallback = rect => EditorGUI.LabelField(rect, _audioClipPack.name),
                onAddCallback = OnAddToList,
                drawElementCallback = OnDrawListElement,
                elementHeightCallback = index =>
                    EditorGUI.GetPropertyHeight(_entriesListProperty.GetArrayElementAtIndex(index))
            };
        }

        private void OnDrawListElement(Rect rect, int index, bool active, bool isActive)
        {
            var element = _entriesListProperty.GetArrayElementAtIndex(index);
            rect.y += 1.0f;
            rect.x += 10.0f;
            rect.width -= 10.0f;

            EditorGUI.PropertyField(
                new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight),
                element,
                true
            );
        }

        private void OnAddToList(ReorderableList list)
        {
            var entriesProperty = serializedObject.FindProperty("_entries");

            var insertIndex = entriesProperty.arraySize;

            entriesProperty.InsertArrayElementAtIndex(insertIndex);
            entriesProperty.GetArrayElementAtIndex(insertIndex)
                .FindPropertyRelative("_id").stringValue = Guid.NewGuid().ToString();
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            _entriesList.DoLayoutList();

            if (serializedObject.ApplyModifiedProperties())
                EditorUtility.SetDirty(_audioClipPack);
        }
    }
}