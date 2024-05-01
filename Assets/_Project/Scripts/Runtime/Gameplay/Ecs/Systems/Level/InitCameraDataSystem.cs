using Froust.Level.Components;
using Froust.Level.Configurations;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Froust.Level.Systems
{
    public class InitCameraDataSystem : IEcsInitSystem
    {
        private readonly EcsWorldInject _world = default;
        private readonly EcsCustomInject<LevelView> _levelData = default;
        private readonly EcsPoolInject<CameraComponent> _cameraPool = default;
        
        public void Init(IEcsSystems systems)
        {
            var cameraSinkRoot = _levelData.Value.CameraSinkRoot;
            var cameraAdjustmentRoot = _levelData.Value.CameraAdjustmentRoot;

            var cameraEntity = _world.Value.NewEntity();
            ref var cameraComponent = ref _cameraPool.Value.Add(cameraEntity);
            
            cameraComponent.CameraSinkRootOffset = cameraSinkRoot.position - _levelData.Value.Water.position;
            cameraComponent.CameraAdjustmentRootDefaultPosition = cameraAdjustmentRoot.position;
            cameraComponent.CameraSinkRoot = cameraSinkRoot;
            cameraComponent.CameraAdjustmentRoot = cameraAdjustmentRoot;
        }
    }
}