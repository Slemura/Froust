using UnityEngine;

namespace Froust.Level.Camera
{
    public class CameraWiggle : MonoBehaviour
    {
        [SerializeField] private float _wiggleAmount = 0.1f;
        [SerializeField] private float _wiggleSpeed = 1.0f;

        private Vector3 _originalPosition;

        private void Start()
        {
            _originalPosition = transform.localPosition;
        }

        private void Update()
        {
            var xOffset = Mathf.PerlinNoise(Time.time * _wiggleSpeed, 0) * 2 - 1;
            var yOffset = Mathf.PerlinNoise(0, Time.time * _wiggleSpeed) * 2 - 1;
            var zOffset = Mathf.PerlinNoise(Time.time * _wiggleSpeed, Time.time * _wiggleSpeed) * 2 - 1;
            
            var offset = new Vector3(xOffset, yOffset, zOffset) * _wiggleAmount;
            
            transform.localPosition = _originalPosition + offset;
        }
    }
}