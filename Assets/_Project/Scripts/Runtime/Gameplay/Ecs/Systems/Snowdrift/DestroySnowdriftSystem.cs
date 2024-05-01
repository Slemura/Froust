using Froust.Level.Components;
using Froust.Level.Model;
using Froust.Level.Configurations;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Froust.Level.Systems
{
    public class DestroySnowdriftSystem : IEcsRunSystem
    {
        private readonly EcsWorldInject _world = default;

        private readonly EcsCustomInject<GameplayModel> _gameplayModel = default;

        private readonly EcsPoolInject<EnemyComponent> _enemyPool = default;
        private readonly EcsPoolInject<SpawnPositionComponent> _positionPool = default;

        private readonly EcsFilterInject<Inc<SnowdriftComponent, SpawnPositionComponent, ViewComponent, DestroySnowdriftFlagComponent>>
            _filterSnowdriftDestroy = default;

        public void Run(IEcsSystems systems)
        {
            foreach (var snowdriftEntity in _filterSnowdriftDestroy.Value)
            {
                ref var snowdrift = ref _filterSnowdriftDestroy.Pools.Inc1.Get(snowdriftEntity);

                var enemyEntity = _world.Value.NewEntity();

                ref var enemy = ref _enemyPool.Value.Add(enemyEntity);
                
                enemy.IsKing = snowdrift.IsKing;
                
                ref var spawnPosition = ref _positionPool.Value.Add(enemyEntity);

                spawnPosition.Value = snowdrift.ViewTransform.position;
                
                _gameplayModel.Value.SpawnedEnemiesCount++;
                _gameplayModel.Value.CurrentEnemiesCount++;
                Object.Destroy(snowdrift.ViewTransform.gameObject);
                _world.Value.DelEntity(snowdriftEntity);
            }
        }
    }
}