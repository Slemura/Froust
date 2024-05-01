using Froust.Level.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Froust.Level.Systems
{
    public class InitPlayerSystem : IEcsInitSystem
    {
        private readonly EcsWorldInject _world = default;

        private readonly EcsPoolInject<PlayerComponent> _playerPool = default;
        private readonly EcsPoolInject<SpawnPositionComponent> _positionPool = default;
        private readonly EcsPoolInject<PlayerInputComponent> _playerInputPool = default;

        public void Init(IEcsSystems systems)
        {
            var playerEntity = _world.Value.NewEntity();
            _playerPool.Value.Add(playerEntity);
            
            ref var spawnPosition = ref _positionPool.Value.Add(playerEntity);
            spawnPosition.Value = new Vector3(0, 1, 0);

            var playerInputEntity = _world.Value.NewEntity();
            _playerInputPool.Value.Add(playerInputEntity);
        }
    }
}