using UnityEngine;

namespace Froust.Level.Configurations
{
    [CreateAssetMenu(menuName = "Configurations/CameraConfiguration", fileName = "CameraConfiguration")]
    public class CameraConfiguration : ScriptableObject
    {
        [SerializeField] private float _cameraAdjustmentSpeed = 0.5f;
        [SerializeField] private AnimationCurve _cameraAdjustmentCurve;

        public float CameraAdjustmentSpeed => _cameraAdjustmentSpeed;
        public AnimationCurve CameraAdjustmentCurve => _cameraAdjustmentCurve;
    }
}
