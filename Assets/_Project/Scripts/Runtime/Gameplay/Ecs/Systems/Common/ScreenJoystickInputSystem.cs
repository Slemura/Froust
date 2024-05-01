using Froust.Level.Components;
using Froust.Level.Configurations;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Froust.Level.Systems
{
    public class ScreenJoystickInputSystem : IEcsRunSystem
    {
        private const float Acceleration = 100f;
        
        private readonly EcsCustomInject<LevelView> _levelView = default;

        private readonly EcsFilterInject<Inc<PlayerInputComponent>> _filterPlayerInput = default;

        public void Run(IEcsSystems systems)
        {
            if (_filterPlayerInput.Value == null)
                return;

            if(_levelView.Value.ScreenJoystick.Horizontal == 0 && _levelView.Value.ScreenJoystick.Vertical == 0)
                return;
            
            foreach (var playerInputEntity in _filterPlayerInput.Value)
            {
                ref var playerInput = ref _filterPlayerInput.Pools.Inc1.Get(playerInputEntity);
    
                playerInput.Horizontal = Mathf.Lerp(playerInput.Horizontal, _levelView.Value.ScreenJoystick.Horizontal,
                    Acceleration * Time.deltaTime);
                playerInput.Vertical = Mathf.Lerp(playerInput.Vertical, _levelView.Value.ScreenJoystick.Vertical,
                    Acceleration * Time.deltaTime);

                var movementVector = new Vector2(playerInput.Horizontal, playerInput.Vertical).normalized;
                playerInput.Magnitude = movementVector.magnitude;
            }
        }
    }
}