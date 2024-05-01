using RpDev.Extensions.Unity;
using UnityEngine;

namespace RpDev.Utilities
{
    [RequireComponent(typeof(RectTransform))]
    public class RectDrawer : MonoBehaviour
    {
        #region Set in Inspector

        [SerializeField] private Color _color = new Color(0.25f, 0.9f, 1f, 0.1f);
        [SerializeField] private bool _whenSelected = true;

        #endregion Set in Inspector

        private RectTransform _rectTransform;

        private void Start()
        {
            if (Application.isEditor == false)
                Destroy(this);
        }

        private void OnDrawGizmosSelected()
        {
            if (_whenSelected)
                Draw();
        }

        private void OnDrawGizmos()
        {
            if (_whenSelected == false)
                Draw();
        }

        private void Draw()
        {
            Gizmos.color = _color;

            if (_rectTransform == null)
                _rectTransform = GetComponent<RectTransform>();

            var worldRect = _rectTransform.GetWorldRect();

            Gizmos.DrawWireCube(worldRect.center, worldRect.size);
            Gizmos.DrawCube(worldRect.center, worldRect.size);
        }
    }
}