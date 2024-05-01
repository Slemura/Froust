using UnityEngine;

namespace Froust.Services
{
    public class LevelPointsInfo : MonoBehaviour
    {
        [SerializeField] private Transform[] _points;
        
        public Transform[] Points => _points;

        #if UNITY_EDITOR
        [SerializeField] private float _drawRadius = 0.1f;
        
        private void OnDrawGizmos()
        {
            if (_points == null || _points.Length == 0)
                return;

            foreach (var point in _points)
            {
                Gizmos.color = Color.magenta;
                Gizmos.DrawWireSphere(point.position, _drawRadius);
            }
        }
        #endif
    }
}