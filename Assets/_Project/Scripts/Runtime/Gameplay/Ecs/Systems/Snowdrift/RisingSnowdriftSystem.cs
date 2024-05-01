using Froust.Level.Components;
using Froust.Level.Configurations;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Froust.Level.Systems
{
    public class RisingSnowdriftSystem : IEcsRunSystem
    {
        private readonly EcsPoolInject<DestroySnowdriftFlagComponent> _destroySnowdriftPool = default;
        private readonly EcsFilterInject<Inc<SnowdriftComponent, SpawnPositionComponent, ViewComponent>, Exc<DestroySnowdriftFlagComponent>> _filterSnowdrift = default;
        private readonly EcsFilterInject<Inc<PlayerCharacterDrownedComponent>> _filterDrowned = default;

        public void Run(IEcsSystems systems)
        {
            if (_filterDrowned.Value.GetEntitiesCount() > 0)
            {
                foreach (var snowdriftEntity in _filterSnowdrift.Value)
                {
                    _destroySnowdriftPool.Value.Add(snowdriftEntity);
                }
                return;
            }

            foreach (var snowdriftEntity in _filterSnowdrift.Value)
            {
                ref var snowdrift = ref _filterSnowdrift.Pools.Inc1.Get(snowdriftEntity);

                var snowdriftTransform = snowdrift.ViewTransform;
                var snowdriftRiseTime = snowdrift.RiseTime;
                
                snowdrift.ExistTime += Time.deltaTime;

                if (snowdrift.ExistTime > snowdriftRiseTime)
                    _destroySnowdriftPool.Value.Add(snowdriftEntity);

                var scaleFactor = snowdrift.ExistTime / snowdriftRiseTime * snowdrift.MaxScale;
                
                snowdriftTransform.transform.localScale = new Vector3(scaleFactor, scaleFactor, scaleFactor);
            }
        }
    }
}