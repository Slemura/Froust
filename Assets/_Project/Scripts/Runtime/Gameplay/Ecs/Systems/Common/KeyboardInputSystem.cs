using Froust.Level.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Froust.Level.Systems
{
    public class KeyboardInputSystem : IEcsRunSystem
    {
        private const float Acceleration = 100f;

        private readonly EcsFilterInject<Inc<PlayerInputComponent>> _filterPlayerInput = default;

        public void Run(IEcsSystems systems)
        {
            if (_filterPlayerInput.Value == null)
                return;

            foreach (var playerInputEntity in _filterPlayerInput.Value)
            {
                ref var playerInput = ref _filterPlayerInput.Pools.Inc1.Get(playerInputEntity);

                playerInput.Horizontal = Mathf.Lerp(playerInput.Horizontal, Input.GetAxis("Horizontal"),
                    Acceleration * Time.deltaTime);
                playerInput.Vertical = Mathf.Lerp(playerInput.Vertical, Input.GetAxis("Vertical"),
                    Acceleration * Time.deltaTime);

                var movementVector = new Vector2(playerInput.Horizontal, playerInput.Vertical).normalized;
                playerInput.Magnitude = movementVector.magnitude;
            }
        }
    }
}