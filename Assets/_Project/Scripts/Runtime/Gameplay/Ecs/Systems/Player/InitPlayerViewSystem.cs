using Froust.Level.Components;
using Froust.Level.Configurations;
using Froust.Level.Views;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Froust.Level.Systems
{
    public class InitPlayerViewSystem : IEcsInitSystem
    {
        private readonly EcsCustomInject<PlayerConfiguration> _playerConfiguration = default;
        
        private readonly EcsCustomInject<LevelView> _levelData = default;
        private readonly EcsPoolInject<ViewComponent> _viewPool = default;
        
        private readonly EcsFilterInject<Inc<PlayerComponent, SpawnPositionComponent>> _filterPlayer = default;

        public void Init(IEcsSystems systems)
        {
            foreach (var playerEntity in _filterPlayer.Value)
            {
                ref var player = ref _filterPlayer.Pools.Inc1.Get(playerEntity);
                ref var playerSpawnPosition = ref _filterPlayer.Pools.Inc2.Get(playerEntity);

                var view = Object.Instantiate(_playerConfiguration.Value.PlayerView, playerSpawnPosition.Value, Quaternion.identity, _levelData.Value.GameContainer);
                player.View = view;
                
                ref var character = ref _viewPool.Value.Add(playerEntity);
                character.View = view;
            }
        }
    }
}