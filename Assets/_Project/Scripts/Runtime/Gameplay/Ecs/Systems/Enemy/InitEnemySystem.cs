using Froust.Level.Components;
using Froust.Level.Configurations;
using Froust.Level.Model;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Froust.Level.Systems
{
    public class InitEnemySystem : IEcsInitSystem
    {
        private readonly EcsCustomInject<LevelView> _levelData = default;
        private readonly EcsCustomInject<EnemyConfiguration> _enemySettings = default;
        private readonly EcsCustomInject<GameplayModel> _gameplayModel = default;

        private readonly EcsWorldInject _world = default;

        private readonly EcsPoolInject<EnemyComponent> _enemyPool = default;
        private readonly EcsPoolInject<SpawnPositionComponent> _positionPool = default;

        private int _lastGivenPointIndex = -1;

        public void Init(IEcsSystems systems)
        {
            var startEnemyCount = _enemySettings.Value.StartEnemyCount;

            for (var i = 0; i < startEnemyCount; i++)
            {
                var enemyEntity = _world.Value.NewEntity();
                ref var enemy = ref _enemyPool.Value.Add(enemyEntity);

                ref var spawnPosition = ref _positionPool.Value.Add(enemyEntity);
                spawnPosition.Value = GetRandomSpawnPosition();

                _gameplayModel.Value.SpawnedEnemiesCount++;
                _gameplayModel.Value.CurrentEnemiesCount++;
            }
        }

        private Vector3 GetRandomSpawnPosition()
        {
            var points = _levelData.Value.LevelPoints.Points;

            if (points == null || points.Length == 0)
                return Vector3.zero;

            if (points.Length == 1)
                return points[0].position;

            int randomIndex;

            do
            {
                randomIndex = Random.Range(0, points.Length);
            } while (randomIndex == _lastGivenPointIndex);

            _lastGivenPointIndex = randomIndex;

            return points[randomIndex].position;
        }
    }
}