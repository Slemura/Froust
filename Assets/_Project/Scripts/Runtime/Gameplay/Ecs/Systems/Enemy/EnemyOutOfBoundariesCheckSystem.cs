using Froust.Level.Components;
using Froust.Level.Model;
using Froust.Level.Configurations;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Froust.Level.Systems
{
    public class EnemyOutOfBoundariesCheckSystem : IEcsRunSystem
    {
        private readonly EcsCustomInject<GameplayModel> _gameplayModel = default;

        private readonly
            EcsFilterInject<Inc<EnemyComponent, SpawnPositionComponent, ViewComponent>,
                Exc<CharacterFallFlagComponent>> _filterEnemies = default;

        private readonly EcsPoolInject<CharacterPushedComponent> _pushedPool = default;
        private readonly EcsPoolInject<CharacterFallFlagComponent> _fallenPool = default;

        public void Run(IEcsSystems systems)
        {
            foreach (var enemyEntity in _filterEnemies.Value)
            {
                ref var enemy = ref _filterEnemies.Pools.Inc1.Get(enemyEntity);

                if (enemy.View.OutOfBoundaries == true)
                {
                    _pushedPool.Value.Del(enemyEntity);
                    _fallenPool.Value.Add(enemyEntity);
                    
                    _gameplayModel.Value.DeadEnemyCount.Value++;
                    _gameplayModel.Value.CurrentEnemiesCount--;
                }
            }
        }
    }
}