using Froust.Level.Components;
using Froust.Level.Configurations;
using Froust.Level.Views;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Froust.Level.Systems
{
    public class CreateSnowdriftViewSystem : IEcsRunSystem
    {
        private readonly EcsCustomInject<LevelView> _levelData = default;
        private readonly EcsCustomInject<EnemyConfiguration> _enemyConfiguration = default;
        private readonly EcsPoolInject<ViewComponent> _viewPool = default;

        private readonly EcsFilterInject<Inc<SnowdriftComponent, SpawnPositionComponent>, Exc<ViewComponent>>
            _filterSnowdriftWithoutView = default;

        public void Run(IEcsSystems systems)
        {
            foreach (var snowdriftEntity in _filterSnowdriftWithoutView.Value)
            {
                ref var snowdrift = ref _filterSnowdriftWithoutView.Pools.Inc1.Get(snowdriftEntity);
                ref var snowdriftSpawnPosition = ref _filterSnowdriftWithoutView.Pools.Inc2.Get(snowdriftEntity);

                var view = Object.Instantiate(_enemyConfiguration.Value.SnowdriftView, snowdriftSpawnPosition.Value, Quaternion.identity, _levelData.Value.GameContainer);
                snowdrift.ViewTransform = view.transform;
                snowdrift.ExistTime = 0;
                
                ref var viewComponent = ref _viewPool.Value.Add(snowdriftEntity);
                viewComponent.View = view;
            }
        }
    }
}