using Froust.Level.Components;
using Froust.Level.Configurations;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Froust.Level.Systems
{
    public class CreateEnemyViewSystem : IEcsRunSystem
    {
        private readonly EcsCustomInject<LevelView> _levelData = default;
        private readonly EcsPoolInject<ViewComponent> _viewPool = default;
        private readonly EcsCustomInject<EnemyConfiguration> _enemyConfiguration = default;
        
        private readonly EcsFilterInject<Inc<EnemyComponent, SpawnPositionComponent>, Exc<ViewComponent>> _filterEnemyWithoutView = default;
        private readonly EcsFilterInject<Inc<PlayerCharacterDrownedComponent>> _filterDrowned = default;

        public void Run(IEcsSystems systems)
        {
            if (_filterDrowned.Value.GetEntitiesCount() > 0)
                return;

            foreach (var enemyEntity in _filterEnemyWithoutView.Value)
            {
                ref var enemy = ref _filterEnemyWithoutView.Pools.Inc1.Get(enemyEntity);
                ref var enemySpawnPosition = ref _filterEnemyWithoutView.Pools.Inc2.Get(enemyEntity);
                
                var enemyCharacterConfiguration = _enemyConfiguration.Value.Characters[enemy.IsKing ? 1 : 0];
                
                var view = Object.Instantiate(enemyCharacterConfiguration.CharacterView, enemySpawnPosition.Value, Quaternion.identity, _levelData.Value.GameContainer);
                enemy.View = view;
                
                ref var viewComponent = ref _viewPool.Value.Add(enemyEntity);
                viewComponent.View = view;
                viewComponent.PushBackForce = enemyCharacterConfiguration.PushBackForce;
                viewComponent.ResponsePushForce = enemyCharacterConfiguration.ResponsePushForce;
            }
        }
    }
}