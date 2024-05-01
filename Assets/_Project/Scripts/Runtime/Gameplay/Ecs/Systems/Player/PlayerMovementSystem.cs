using Froust.Level.Components;
using Froust.Level.Configurations;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Froust.Level.Systems
{
    public class PlayerMovementSystem : IEcsRunSystem
    {
        private readonly EcsCustomInject<PlayerConfiguration> _playerSettings = default;
        private readonly EcsFilterInject<Inc<PlayerComponent, SpawnPositionComponent>> _filterPlayer = default;
        private readonly EcsFilterInject<Inc<PlayerInputComponent>> _filterPlayerInput = default;

        public void Run(IEcsSystems systems)
        {
            if (_filterPlayerInput.Value == null)
                return;

            foreach (var playerInputEntity in _filterPlayerInput.Value)
            {
                ref var playerInput = ref _filterPlayerInput.Pools.Inc1.Get(playerInputEntity);

                if (playerInput.Magnitude == 0) return;

                var movementVector = new Vector3(
                    _playerSettings.Value.PlayerMovementCurve.Evaluate(Mathf.Abs(playerInput.Horizontal)) * Mathf.Sign(playerInput.Horizontal),
                    0f,
                    _playerSettings.Value.PlayerMovementCurve.Evaluate(Mathf.Abs(playerInput.Vertical)) * Mathf.Sign(playerInput.Vertical)
                ).normalized;

                foreach (var playerEntity in _filterPlayer.Value)
                {
                    ref var player = ref _filterPlayer.Pools.Inc1.Get(playerEntity);
                    var force = movementVector * _playerSettings.Value.MovementSpeed;

                    player.View.AddForce(force);
                    
                    var targetRotation = Quaternion.LookRotation(movementVector);
                    player.View.SetRotation(Quaternion.Lerp(player.View.LocalRotation, targetRotation, _playerSettings.Value.RotationSpeed * Time.deltaTime));
                }
            }
        }
    }
}