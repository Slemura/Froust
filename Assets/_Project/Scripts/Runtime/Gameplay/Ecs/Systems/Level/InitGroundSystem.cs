using Froust.Level.Configurations;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Froust.Level.Systems
{
    public class InitGroundSystem : IEcsInitSystem
    {
        private readonly EcsCustomInject<LevelView> _levelData = default;
        private readonly EcsCustomInject<GroundConfiguration> _groundConfig = default;
        public void Init(IEcsSystems systems)
        {
            _levelData.Value.Ground.position = new Vector3(0, _groundConfig.Value.GroundDefaultPositionY, 0);
        }
    }
}