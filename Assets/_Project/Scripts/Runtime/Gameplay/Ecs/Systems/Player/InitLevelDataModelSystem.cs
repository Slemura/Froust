using Froust.Level.Configurations;
using Froust.Level.Model;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Froust.Level.Systems
{
    public class InitLevelDataModelSystem : IEcsInitSystem
    {
        private readonly EcsCustomInject<GameplayModel> _gameplayModel = default;
        private readonly EcsCustomInject<EnemyConfiguration> _enemySettings = default;

        public void Init(IEcsSystems systems)
        {
            _gameplayModel.Value.LastTimeChangedDifficulty = 0;
            _gameplayModel.Value.LastTimeSpawnedEnemy = 0;
            _gameplayModel.Value.CurrentSpawnDelay = _enemySettings.Value.BaseSpawnInterval;
            _gameplayModel.Value.CurrentSpawnCount = (int)_enemySettings.Value.BaseSpawnCount;
            _gameplayModel.Value.SpawnedEnemiesCount = 0;
        }
    }
}