using DG.Tweening;
using UnityEngine;

namespace Froust.Level.Camera
{
    public class CameraShake : MonoBehaviour
    {
        [SerializeField] private float _magnitudeMultiplier = 1f;
        [SerializeField] private float _duration = 0.5f;
        [SerializeField] private int _vibrato = 15;

        private Vector3 _initialPosition;
        private Sequence _sequence;

        private void Awake()
        {
            _initialPosition = transform.localPosition;
        }

        public void Shake(float magnitude)
        {
            _sequence?.Kill();

            _sequence = DOTween.Sequence()
                .Append(
                    transform.DOShakePosition(
                        _duration,
                        Vector3.one * magnitude * _magnitudeMultiplier,
                        _vibrato
                    ))
                .Append(transform.DOLocalMove(_initialPosition, 0.1f))
                .SetLink(gameObject, LinkBehaviour.KillOnDestroy);
        }
    }
}