using Froust.Level.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Froust.Level.Systems
{
    public class EnemyDisableNavMeshSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<EnemyComponent, ViewComponent, StartPushFlagComponent>> _filterEnemy = default;
        
        public void Run(IEcsSystems systems)
        {
            foreach (var enemyEntity in _filterEnemy.Value)
            {
                ref var enemy = ref _filterEnemy.Pools.Inc1.Get(enemyEntity);
                enemy.View.Agent.enabled = false;
                enemy.View.SetupConstraints(RigidbodyConstraints.None);
            }
        }
    }
}