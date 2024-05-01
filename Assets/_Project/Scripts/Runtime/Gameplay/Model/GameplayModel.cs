using System;
using UniRx;

namespace Froust.Level.Model
{
    public class GameplayModel : IReadonlyGameplayModel
    {
        private readonly ReactiveProperty<int> _deadEnemyCount = new ReactiveProperty<int>(0);
        private readonly ReactiveProperty<float> _iceDriftLevel = new ReactiveProperty<float>(1);
        private readonly Subject<int> _startLevelSubject = new Subject<int>();
        public bool IsGameEnded { get; set; }
        public float LastTimeChangedDifficulty { get; set; }
        public float LastTimeSpawnedEnemy { get; set; }
        public float CurrentSpawnDelay { get; set; }
        public float CurrentSpawnCount { get; set; }

        public DateTime StartLevelTime { get; set; }
        public DateTime EndLevelTime { get; set; }

        public int DestroyedEnemyCount { get; set; }
        public int SpawnedEnemiesWithSnowdriftCount { get; set; }
        public int SpawnedEnemiesCount { get; set; }
        public int CurrentEnemiesCount { get; set; }
        public float GameplayTime => (float)(DateTime.UtcNow - StartLevelTime).TotalSeconds;
        public IReactiveProperty<int> DeadEnemyCount => _deadEnemyCount;
        public IReactiveProperty<float> IceDriftLevel => _iceDriftLevel;
        public ISubject<int> StartLevelSubject => _startLevelSubject;
        public IReadOnlyReactiveProperty<int> DeadEnemyCountReadOnly => _deadEnemyCount;
        public IReadOnlyReactiveProperty<float> IceDriftLevelReadOnly => _iceDriftLevel;
        public IObservable<int> StartLevelObservable => _startLevelSubject;
    }
}