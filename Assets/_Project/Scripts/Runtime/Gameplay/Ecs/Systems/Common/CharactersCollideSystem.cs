using Froust.Level.Components;
using Froust.Services;
using Froust.Level.Configurations;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Froust.Level.Systems
{
    public class CharactersCollideSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<ViewComponent, SpawnPositionComponent>, Exc<StartPushFlagComponent, CharacterPushedComponent>> _filterEnemies =
            default;
        
        private readonly EcsPoolInject<StartPushFlagComponent> _startPushPool = default;

        public void Run(IEcsSystems systems)
        {
            foreach (var enemyEntity in _filterEnemies.Value)
            {
                ref var viewComponent = ref _filterEnemies.Pools.Inc1.Get(enemyEntity);

                if (viewComponent.View.HaveCollision == true)
                {
                    HitStop.Stop();
                    
                    ref var startPushComponent = ref _startPushPool.Value.Add(enemyEntity);
                    
                    var direction = (viewComponent.View.transform.position - viewComponent.View.OtherPosition).normalized;
                    var pushForce = viewComponent.PushBackForce * (1f + Vector3.Dot(direction, viewComponent.View.Velocity.normalized));
                    
                    startPushComponent.PushForce = pushForce;
                    startPushComponent.PushDirection = direction;
                }
            }
        }
    }
}