using Froust.Level.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Froust.Level.Systems
{
    public class PlayerAnimationSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<PlayerInputComponent>> _filterPlayerInput = default;
        private readonly EcsFilterInject<Inc<PlayerComponent, SpawnPositionComponent>> _filterPlayer = default;
        
        public void Run(IEcsSystems systems)
        {
            if (_filterPlayerInput.Value == null)
                return;

            foreach (var playerInputEntity in _filterPlayerInput.Value)
            {
                ref var playerInput = ref _filterPlayerInput.Pools.Inc1.Get(playerInputEntity);

                if (playerInput.Magnitude == 0) return;
                
                foreach (var playerEntity in _filterPlayer.Value)
                {
                    ref var player = ref _filterPlayer.Pools.Inc1.Get(playerEntity);
                    
                    player.View.UpdateAnimator(playerInput.Magnitude);
                }
            }
        }
    }
}