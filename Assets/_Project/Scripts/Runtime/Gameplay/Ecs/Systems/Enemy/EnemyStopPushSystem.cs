using Froust.Level.Components;
using Froust.Level.Configurations;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Froust.Level.Systems
{
    public class EnemyStopPushSystem : IEcsRunSystem
    {
        private readonly EcsCustomInject<EnemyConfiguration> _enemySettings = default;

        private readonly EcsFilterInject<Inc<EnemyComponent, SpawnPositionComponent, CharacterPushedComponent>, Exc<CharacterFallFlagComponent>> _filterEnemies = default;

        private readonly EcsPoolInject<CharacterPushedComponent> _pushedPool = default;

        public void Run(IEcsSystems systems)
        {
            foreach (var enemyEntity in _filterEnemies.Value)
            {
                ref var enemy = ref _filterEnemies.Pools.Inc1.Get(enemyEntity);
                ref var pushed = ref _filterEnemies.Pools.Inc3.Get(enemyEntity);

                if (Time.realtimeSinceStartup - pushed.StartTime < _enemySettings.Value.StopPushDelay)
                    return;

                enemy.View.Agent.enabled = true;
                enemy.View.Agent.SetDestination(enemy.LastWaypoint);

                enemy.View.SetupConstraints(RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ);

                _pushedPool.Value.Del(enemyEntity);
            }
        }
    }
}