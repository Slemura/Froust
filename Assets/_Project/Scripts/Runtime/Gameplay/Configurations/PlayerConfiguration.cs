using Froust.Level.Views;
using UnityEngine;

namespace Froust.Level.Configurations
{
    [CreateAssetMenu(menuName = "Configurations/PlayerConfiguration", fileName = "PlayerConfiguration")]
    public class PlayerConfiguration : ScriptableObject
    {
        [SerializeField] private PlayerView _playerView;
        [SerializeField] private float _movementSpeed;
        [SerializeField] private float _rotationSpeed;
        [SerializeField] private AnimationCurve _playerMovementCurve;
        
        public float MovementSpeed => _movementSpeed;
        public float RotationSpeed => _rotationSpeed;
        public AnimationCurve PlayerMovementCurve => _playerMovementCurve;
        public PlayerView PlayerView => _playerView;
    }
}