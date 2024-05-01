using UnityEngine;
using UnityEngine.AI;

namespace Froust.Level.Views
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(NavMeshAgent))]
    public class EnemyViewAnimator : MonoBehaviour
    {
        [SerializeField] private Transform _leanPivot;
        [SerializeField] private float _maxLeanAngle = 30f;
        [SerializeField] private float _leanSpeed = 5f;

        [Space] [Range(float.Epsilon, 1f)] [SerializeField]
        private float _animatorSpeed = 1f;

        private Animator _animator;
        private NavMeshAgent _agent;

        private static readonly int Speed = Animator.StringToHash("Speed");

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _agent = GetComponent<NavMeshAgent>();
            _animator.speed = _animatorSpeed;
        }

        private void Update()
        {
            UpdateAnimatorSpeed();

            if (_agent.velocity.magnitude < 0.1f)
            {
                RestoreLean();
            }
            else
            {
                Lean();
            }
        }

        private void UpdateAnimatorSpeed()
        {
            var agentSpeed = _agent.speed;
            var velocityMagnitude = _agent.velocity.magnitude;
            var animatorSpeed = velocityMagnitude / agentSpeed;

            _animator.SetFloat(Speed, animatorSpeed);
        }

        private void Lean()
        {
            var direction = _agent.velocity.normalized;
            var turnAngle = Vector3.SignedAngle(transform.forward, direction, Vector3.up);
            var leanAngle = Mathf.Clamp(turnAngle, -_maxLeanAngle, _maxLeanAngle);
            var leanRotation = Quaternion.Euler(0f, 0f, -leanAngle);

            _leanPivot.localRotation = Quaternion.Lerp(_leanPivot.localRotation, leanRotation, _leanSpeed * Time.deltaTime);
        }

        private void RestoreLean()
        {
            _leanPivot.rotation = Quaternion.Lerp(
                _leanPivot.rotation,
                Quaternion.LookRotation(_leanPivot.forward),
                _leanSpeed * Time.deltaTime
            );
        }
    }
}