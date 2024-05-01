using UnityEngine;

namespace Froust.Runtime.UI
{
    [RequireComponent(typeof(CanvasGroup))]
    public class WarningOverlay : MonoBehaviour
    {
        [SerializeField] private AnimationCurve _curve;
        [SerializeField] private float _smoothing = 1;

        private CanvasGroup _canvasGroup;

        public void UpdateWarningLevel(float warningNormalized)
        {
            var targetValue = warningNormalized;
            
            targetValue =  Mathf.Clamp(targetValue, 0, 0.75f);
            targetValue /= 0.75f;
            targetValue =  _curve.Evaluate(targetValue);

            _canvasGroup.alpha = Mathf.Lerp(_canvasGroup.alpha, targetValue, Time.deltaTime * _smoothing);
        }

        private void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
            _canvasGroup.alpha = 0;
        }
    }
}