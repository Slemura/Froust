using UnityEngine;

namespace RpDev.Utilities
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Camera))]
    public class CameraConstantWidthOrthographic : MonoBehaviour
    {
        [SerializeField] private Vector2Int _referenceResolution = new Vector2Int(1440, 2560);

        private Camera _camera;

        private float _initialSize;
        private float _targetAspectRatio;

        private void Awake()
        {
            _camera = GetComponent<Camera>();

            _initialSize = _camera.orthographicSize;
            _targetAspectRatio = (float)_referenceResolution.x / _referenceResolution.y;
        }

        private void Start()
        {
            UpdateOrthographicSize();
        }

        private void Update()
        {
            if (Application.isEditor)
                UpdateOrthographicSize();
        }

        private void UpdateOrthographicSize()
        {
            float aspectRatio = (float)Screen.width / Screen.height;

            if (_targetAspectRatio > aspectRatio)
                _camera.orthographicSize = _initialSize * (_targetAspectRatio / aspectRatio);
        }
    }
}