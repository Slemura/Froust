using Froust.Level.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Froust.Level.Systems
{
    public class PlayerPushBackSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<ViewComponent, SpawnPositionComponent, StartPushFlagComponent>, Exc<CharacterPushedComponent>> _filterCharacters =
            default;

        private readonly EcsFilterInject<Inc<PlayerComponent>> _filterPlayer = default;
        
        public void Run(IEcsSystems systems)
        {
            foreach (var enemyEntity in _filterCharacters.Value)
            {
                ref var startPush = ref _filterCharacters.Pools.Inc3.Get(enemyEntity);
                ref var viewComponent = ref _filterCharacters.Pools.Inc1.Get(enemyEntity);
                
                foreach (var playerEntity in _filterPlayer.Value)
                {
                    ref var player = ref _filterPlayer.Pools.Inc1.Get(playerEntity);
                    player.View.AddForce(-startPush.PushDirection * viewComponent.ResponsePushForce, ForceMode.Impulse);
                }
            }
        }
    }
}