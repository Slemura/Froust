using System;
using UniRx;

namespace Froust.Level.Model
{
    public interface IReadonlyGameplayModel
    {
        public float GameplayTime { get; }
        public IObservable<int> StartLevelObservable { get; }
        public IReadOnlyReactiveProperty<int> DeadEnemyCountReadOnly { get; }
        public IReadOnlyReactiveProperty<float> IceDriftLevelReadOnly { get; }
    }
}