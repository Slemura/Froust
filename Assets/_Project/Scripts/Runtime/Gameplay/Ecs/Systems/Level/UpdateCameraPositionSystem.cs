using Froust.Level.Components;
using Froust.Level.Configurations;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Froust.Level.Systems
{
    public class UpdateCameraPositionSystem : IEcsRunSystem
    {
        private readonly EcsCustomInject<LevelView> _levelData = default;
        private readonly EcsCustomInject<CameraConfiguration> _cameraConfig = default;
        private readonly EcsFilterInject<Inc<CameraComponent>> _cameraFilter = default;
        
        public void Run(IEcsSystems systems)
        {
            foreach (var camera in _cameraFilter.Value)
            {
                ref var cameraComponent = ref _cameraFilter.Pools.Inc1.Get(camera);
                
                var cameraSinkRoot = cameraComponent.CameraSinkRoot;
                var cameraAdjustmentRoot = cameraComponent.CameraAdjustmentRoot;

                cameraSinkRoot.position = _levelData.Value.Water.position + cameraComponent.CameraSinkRootOffset;

                var time = Time.deltaTime * _cameraConfig.Value.CameraAdjustmentSpeed;

                cameraAdjustmentRoot.position = Vector3.Lerp(
                    cameraAdjustmentRoot.position,
                    cameraComponent.CameraAdjustmentRootDefaultPosition,
                    _cameraConfig.Value.CameraAdjustmentCurve.Evaluate(time)
                );                
            }
        }
    }
}