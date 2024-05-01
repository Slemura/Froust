using UnityEngine;

namespace Froust.Level.Configurations
{
    [CreateAssetMenu(menuName = "Configurations/GroundConfiguration", fileName = "GroundConfiguration")]
    public class GroundConfiguration : ScriptableObject
    {
        [SerializeField] private int _ignoreEnemyCount;
        [SerializeField] private int _maxEnemyCount;
        [SerializeField] private float _refreshDelay;
        [SerializeField] private float _failWaterPosition;
        
        [SerializeField] private float _groundFallY;
        [SerializeField] private float _groundDefaultPositionY;
        [SerializeField] private float _groundFallDuration;
        [SerializeField] private AnimationCurve _sinkAnimationCurve;

        public int IgnoreEnemyCount => _ignoreEnemyCount;
        public int MaxEnemyCount => _maxEnemyCount;
        public float RefreshDelay => _refreshDelay;
        public float FailWaterPosition => _failWaterPosition;
        public float GroundFallY => _groundFallY;
        public float GroundFallDuration => _groundFallDuration;
        public AnimationCurve SinkAnimationCurve => _sinkAnimationCurve;
        public float GroundDefaultPositionY => _groundDefaultPositionY;
    }
}