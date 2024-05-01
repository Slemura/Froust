using System;
using Froust.Level.Components;
using Froust.Level.Configurations;
using Froust.Level.Model;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Froust.Level.Systems
{
    public class InitSnowdriftSystem : IEcsRunSystem
    {
        private readonly EcsCustomInject<GameplayModel> _gameplayModel = default;
        private readonly EcsCustomInject<EnemyConfiguration> _enemyConfiguration = default;
        
        private readonly EcsWorldInject _world = default;
        private readonly EcsPoolInject<SnowdriftComponent> _snowdriftPool = default;
        private readonly EcsFilterInject<Inc<PlayerCharacterDrownedComponent>> _filterDrowned = default;

        private float _currentSpawnCount;

        public void Run(IEcsSystems systems)
        {
            if (_filterDrowned.Value.GetEntitiesCount() > 0)
                return;

            var secondsFromStartLevel = (DateTime.UtcNow - _gameplayModel.Value.StartLevelTime).TotalSeconds;
            
            if (secondsFromStartLevel < _enemyConfiguration.Value.InitialSpawnDelay || Time.realtimeSinceStartup - _gameplayModel.Value.LastTimeSpawnedEnemy < _gameplayModel.Value.CurrentSpawnDelay)
                return;

            _currentSpawnCount = Mathf.RoundToInt(_gameplayModel.Value.CurrentSpawnCount);
            
            for (var i = 0; i < _currentSpawnCount; i++)
            {
                var snowdriftEntity = _world.Value.NewEntity();
                ref var snowdriftComponent = ref _snowdriftPool.Value.Add(snowdriftEntity);
                
                var riseTime = Random.Range(_enemyConfiguration.Value.SnowdriftRiseTimeMin, _enemyConfiguration.Value.SnowdriftRiseTimeMax);
                
                snowdriftComponent.IsKing = _gameplayModel.Value.SpawnedEnemiesWithSnowdriftCount % _enemyConfiguration.Value.EnemiesCountToSpawnKing == 0;
                snowdriftComponent.MaxScale = snowdriftComponent.IsKing ? 2 : 1;
                snowdriftComponent.RiseTime = riseTime * (snowdriftComponent.IsKing ? _enemyConfiguration.Value.KingSnowdriftRiseTimeMultiplier : 1);
                _gameplayModel.Value.SpawnedEnemiesWithSnowdriftCount++;
            }

            _gameplayModel.Value.LastTimeSpawnedEnemy = Time.realtimeSinceStartup;
        }
    }
}