using Froust.Level.Components;
using Froust.Level.Configurations;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Froust.Level.Systems
{
    public class EnemyDestroySystem : IEcsRunSystem
    {
        private readonly EcsWorldInject _world = default;
        private readonly EcsCustomInject<EnemyConfiguration> _enemySettings = default;

        private readonly EcsFilterInject<Inc<EnemyComponent, SpawnPositionComponent, CharacterFallFlagComponent>> _filterEnemyFallen = default;

        public void Run(IEcsSystems systems)
        {
            foreach (var enemyEntity in _filterEnemyFallen.Value)
            {
                ref var enemy = ref _filterEnemyFallen.Pools.Inc1.Get(enemyEntity);
                
                if (enemy.View.transform.position.y <= _enemySettings.Value.DieCoordinate)
                {
                    Object.Destroy(enemy.View.gameObject);
                    _world.Value.DelEntity(enemyEntity);
                }
            }
        }
    }
}