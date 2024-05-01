using UnityEngine;

namespace Froust.Level.Views
{
    [RequireComponent(typeof(Animator))]
    public class PlayerView : CharacterView
    {
        private static readonly int Speed = Animator.StringToHash("Speed");
        
        [SerializeField] private Animator _animator;
        
        public Quaternion LocalRotation => transform.localRotation;
        
        public void SetRotation(Quaternion quaternion)
        {
            transform.localRotation = quaternion;
        }
        
        public void UpdateAnimator(float magnitude)
        {
            _animator.SetFloat(Speed, magnitude);
        }
    }
}