using Froust.Level.Model;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Time = UnityEngine.Time;

namespace Froust.Level.Systems
{
    public class OnStartLevelSystem : IEcsInitSystem
    {
        private readonly EcsCustomInject<GameplayModel> _gameplayModel = default;
        
        public void Init(IEcsSystems systems)
        {
            _gameplayModel.Value.StartLevelSubject.OnNext((int)Time.realtimeSinceStartup);
        }
    }
}