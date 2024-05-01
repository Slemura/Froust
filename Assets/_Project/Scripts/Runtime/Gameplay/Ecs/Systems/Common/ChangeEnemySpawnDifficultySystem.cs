using Froust.Level.Configurations;
using Froust.Level.Model;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Froust.Level.Systems
{
    public class ChangeEnemySpawnDifficultySystem : IEcsRunSystem
    {
        private readonly EcsCustomInject<EnemyConfiguration> _enemySettings = default;
        private readonly EcsCustomInject<GameplayModel> _gameplayModel = default;

        private bool _isDifficultyIncreased;

        public void Run(IEcsSystems systems)
        {
            if (Time.realtimeSinceStartup - _gameplayModel.Value.LastTimeChangedDifficulty <
                _enemySettings.Value.DifficultyChangeInterval)
                return;

            var currentSpawnDelay = _gameplayModel.Value.CurrentSpawnDelay;
            var currentSpawnCount = _gameplayModel.Value.CurrentSpawnCount;

            if (_isDifficultyIncreased)
            {
                _isDifficultyIncreased = false;
                currentSpawnCount -= _enemySettings.Value.BaseSpawnCount * _enemySettings.Value.SpawnIncreasePercent;
                currentSpawnDelay +=
                    Mathf.Clamp(_enemySettings.Value.BaseSpawnInterval * _enemySettings.Value.IntervalIncreasePercent,
                        0.5f, 3);
            }
            else
            {
                _isDifficultyIncreased = true;
                currentSpawnCount += _enemySettings.Value.BaseSpawnCount * _enemySettings.Value.SpawnDecreasePercent;
                currentSpawnDelay -=
                    Mathf.Clamp(_enemySettings.Value.BaseSpawnInterval * _enemySettings.Value.IntervalDecreasePercent,
                        0.5f, 3);
            }

            _gameplayModel.Value.CurrentSpawnDelay = currentSpawnDelay;
            _gameplayModel.Value.CurrentSpawnCount = currentSpawnCount;
            _gameplayModel.Value.LastTimeChangedDifficulty = Time.realtimeSinceStartup;
        }
    }
}