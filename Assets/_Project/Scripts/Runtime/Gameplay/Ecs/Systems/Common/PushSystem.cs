using Froust.Level.Components;
using Froust.Level.Configurations;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Froust.Level.Systems
{
    public class PushSystem : IEcsRunSystem
    {
        private readonly EcsCustomInject<LevelView> _gameplayResources = default;
        private readonly EcsCustomInject<GameAudioHandler> _gameplayAudioConfiguration = default;
        private readonly EcsFilterInject<Inc<ViewComponent, SpawnPositionComponent, StartPushFlagComponent>, Exc<CharacterPushedComponent>> _filterCharacters =
            default;
        
        private readonly EcsPoolInject<StartPushFlagComponent> _startPushPool = default;
        private readonly EcsPoolInject<CharacterPushedComponent> _pushedPool = default;

        public void Run(IEcsSystems systems)
        {
            foreach (var characterEntity in _filterCharacters.Value)
            {
                ref var viewComponent = ref _filterCharacters.Pools.Inc1.Get(characterEntity);
                ref var startPush = ref _filterCharacters.Pools.Inc3.Get(characterEntity);

                var pushForce = startPush.PushDirection * startPush.PushForce;
                
                viewComponent.View.AddForce(pushForce, ForceMode.Impulse);
                viewComponent.View.ShowSnowPuff();
                
                _gameplayResources.Value.CameraShake.Shake(startPush.PushDirection.magnitude * 0.7f);
                
                _gameplayAudioConfiguration.Value.PlayRandomPunchSound();
                _gameplayAudioConfiguration.Value.PlayRandomScreamSound();
                
                _startPushPool.Value.Del(characterEntity);
                
                ref var pushed = ref _pushedPool.Value.Add(characterEntity);
                pushed.StartTime = Time.realtimeSinceStartup;
            }
        }
    }
}