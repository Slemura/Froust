using UnityEngine;

namespace Froust.Level.Views
{
    [RequireComponent(typeof(Rigidbody))]
    public class RigidbodyView : MonoBehaviour
    {
        private const string CollisionTag = "Character";
        private const string LevelBoundariesTag = "LevelBoundaries";
        
        [SerializeField] private Rigidbody _rigidbody;
        
        private bool _outOfBoundaries;
        private bool _haveCollision;
        private Vector3 _otherPosition;

        public bool HaveCollision => _haveCollision;
        public Vector3 OtherPosition => _otherPosition;
        public Vector3 Velocity => _rigidbody.velocity;
        public bool OutOfBoundaries => _outOfBoundaries;
        
        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag(CollisionTag) != true) return;
            
            _haveCollision = true;
            _otherPosition = other.transform.position;
        }

        private void OnCollisionExit(Collision other)
        {
            _haveCollision = false;
        }
        
        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag(LevelBoundariesTag)) _outOfBoundaries = true;
        }
        
        public void SetupConstraints(RigidbodyConstraints constraints) => _rigidbody.constraints = constraints;
        public void AddForce(Vector3 force, ForceMode forceMode) => _rigidbody.AddForce(force, forceMode);
        public void AddForce(Vector3 force) => _rigidbody.AddForce(force);
        public void AddRandomTorque(float maxTorque) => _rigidbody.AddTorque(Random.insideUnitSphere * maxTorque);
    }
}