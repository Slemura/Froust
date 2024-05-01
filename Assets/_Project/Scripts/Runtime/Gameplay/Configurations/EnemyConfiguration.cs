using System.Collections.Generic;
using Froust.Level.Views;
using RpDev.Services.AudioService;
using UnityEngine;

namespace Froust.Level.Configurations
{
    [CreateAssetMenu(menuName = "Configurations/EnemyConfiguration", fileName = "EnemyConfiguration")]
    public class EnemyConfiguration : ScriptableObject
    {
        [Header("Characters settings")]
        
        [SerializeField] private CharacterConfiguration<EnemyView>[] _characters;
        [SerializeField] private SnowdriftView _snowdriftView;
        
        [Header("Snowdrift")] 
        [SerializeField] private float _snowdriftRiseTimeMin;
        [SerializeField] private float _snowdriftRiseTimeMax;
        [SerializeField] private float _kingSnowdriftRiseTimeMultiplier;
        
        [Header("Difficulty settings")]
        [SerializeField] [Min(0)] private float _initialSpawnDelay;
        [SerializeField] private int _startEnemyCount;

        [Header("Enemy spawn interval")]
        [SerializeField] private float _baseSpawnInterval;
        [SerializeField] [Range(0, 1)] private float _intervalIncreasePercent;
        [SerializeField] [Range(0, 1)] private float _intervalDecreasePercent;

        [Header("Enemy spawn count")]
        [SerializeField] private float _baseSpawnCount;
        [SerializeField] [Range(0, 1)] private float _spawnIncreasePercent;
        [SerializeField] [Range(0, 1)] private float _spawnDecreasePercent;
        
        [Header("Difficulty change")]
        [SerializeField] private float _difficultyChangeInterval;
        
        [Header("Static mob settings")]
        [SerializeField] private float _stopPushDelay;
        [SerializeField] private float _dieCoordinate;
        [SerializeField] private float _enemiesCountToSpawnKing = 10;
        
        public float InitialSpawnDelay => _initialSpawnDelay;
        public int StartEnemyCount => _startEnemyCount;
        public float BaseSpawnInterval => _baseSpawnInterval;
        public float IntervalIncreasePercent => _intervalIncreasePercent;
        public float IntervalDecreasePercent => _intervalDecreasePercent;
        public float BaseSpawnCount => _baseSpawnCount;
        public float SpawnIncreasePercent => _spawnIncreasePercent;
        public float SpawnDecreasePercent => _spawnDecreasePercent;
        public float DifficultyChangeInterval => _difficultyChangeInterval;
        public float DieCoordinate => _dieCoordinate;
        public float StopPushDelay => _stopPushDelay;
        public float EnemiesCountToSpawnKing => _enemiesCountToSpawnKing;
        public float SnowdriftRiseTimeMin => _snowdriftRiseTimeMin;
        public float SnowdriftRiseTimeMax => _snowdriftRiseTimeMax;
        public float KingSnowdriftRiseTimeMultiplier => _kingSnowdriftRiseTimeMultiplier;
        public IReadOnlyList<CharacterConfiguration<EnemyView>> Characters => _characters;
        public SnowdriftView SnowdriftView => _snowdriftView;
    }
}