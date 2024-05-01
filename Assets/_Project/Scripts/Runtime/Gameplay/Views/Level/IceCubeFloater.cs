using UnityEngine;

namespace Froust.Common
{
    public class IceCubeFloater : MonoBehaviour
    {
        [SerializeField] private float _wiggleAmount = 0.1f;
        [SerializeField] private float _wiggleSpeed = 1.0f;
        [SerializeField] private float _seedOffsetRange = 10000;

        private Vector3 _originalPosition;
        private float _offset;

        private void Awake()
        {
            _originalPosition = transform.localPosition;

            _offset = Random.Range(-_seedOffsetRange, _seedOffsetRange);
        }

        private void Update()
        {
            var yOffset = Mathf.PerlinNoise(0, (Time.time + _offset) * _wiggleSpeed) * 2 - 1;

            var offset = new Vector3(0, yOffset, 0) * _wiggleAmount;

            transform.localPosition = _originalPosition + offset;
        }
    }
}