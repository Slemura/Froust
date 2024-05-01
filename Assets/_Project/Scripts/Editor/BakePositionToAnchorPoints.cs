using UnityEditor;
using UnityEngine;

namespace Froust.Editor
{
    public static class BakePositionToAnchorPoints
    {
        [MenuItem("CONTEXT/RectTransform/Bake Position To Anchors")]
        private static void Do(MenuCommand command)
        {
            var rectTransform = (RectTransform)command.context;

            var canvas = rectTransform.GetComponentInParent<Canvas>();

            if (canvas == null)
                return;

            Undo.RecordObject(rectTransform, "Bake Position To Anchors");

            var parent = rectTransform.parent.GetComponentInParent<RectTransform>();

            if (parent == null)
                return;

            var transformPoint = parent.InverseTransformPoint(rectTransform.position);

            var parentRect = parent.rect;
            var anchorMin = rectTransform.anchorMin;
            var anchorMax = rectTransform.anchorMax;

            var anchorWidth = anchorMax.x - anchorMin.x;
            var anchorHeight = anchorMax.y - anchorMin.y;

            var targetAnchorX = Mathf.InverseLerp(parentRect.x, parentRect.x + parentRect.width, transformPoint.x);
            var targetAnchorY = Mathf.InverseLerp(parentRect.y, parentRect.y + parentRect.height, transformPoint.y);

            rectTransform.anchorMin = new Vector2(targetAnchorX - anchorWidth / 2, targetAnchorY - anchorHeight / 2);
            rectTransform.anchorMax = new Vector2(targetAnchorX + anchorWidth / 2, targetAnchorY + anchorHeight / 2);

            rectTransform.anchoredPosition = Vector2.zero;

            EditorUtility.SetDirty(rectTransform);
        }
    }
}