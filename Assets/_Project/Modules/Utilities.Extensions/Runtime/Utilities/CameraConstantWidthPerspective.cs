using UnityEngine;

namespace RpDev.Utilities
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Camera))]
    public class CameraConstantWidthPerspective : MonoBehaviour
    {
        [SerializeField] private Vector2Int _referenceResolution = new Vector2Int(1440, 2560);

        private Camera _camera;
        private float _targetAspectRatio;
        private float _initialFOV;

        private void Awake()
        {
            _camera = GetComponent<Camera>();

            _initialFOV = _camera.fieldOfView;

            _targetAspectRatio = (float)_referenceResolution.x / _referenceResolution.y;
        }

        private void Start()
        {
            UpdateFieldOfView();
        }

        private void Update()
        {
            if (Application.isEditor)
                UpdateFieldOfView();
        }

        private void UpdateFieldOfView()
        {
            float aspectRatio = (float)Screen.width / Screen.height;

            float newFOV = _initialFOV * (_targetAspectRatio / aspectRatio);

            _camera.fieldOfView = newFOV;
        }
    }
}