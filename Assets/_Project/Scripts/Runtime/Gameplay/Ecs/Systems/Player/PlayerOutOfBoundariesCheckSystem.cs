using System;
using Froust.Level.Components;
using Froust.Level.Model;
using Froust.Level.Configurations;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Froust.Level.Systems
{
    public class PlayerOutOfBoundariesCheckSystem : IEcsRunSystem
    {
        private readonly EcsCustomInject<GameplayModel> _gameplayModel = default;
        
        private readonly EcsPoolInject<SpawnPositionComponent> _spawnPositionPool = default;
        private readonly EcsPoolInject<CharacterFallFlagComponent> _fallenCharactersPool = default;
        private readonly EcsFilterInject<Inc<PlayerComponent, SpawnPositionComponent>, Exc<CharacterFallFlagComponent>> _filterPlayer = default;

        public void Run(IEcsSystems systems)
        {
            foreach (var playerEntity in _filterPlayer.Value)
            {
                ref var player = ref _filterPlayer.Pools.Inc1.Get(playerEntity);
                
                if (player.View.OutOfBoundaries == true && player.View.transform.position.y > -10f)
                    player.View.AddForce(new Vector3(0, -2f, 0), ForceMode.Impulse);

                if (player.View.OutOfBoundaries == true && player.View.transform.position.y < -10f)
                {
                    _fallenCharactersPool.Value.Add(playerEntity);
                    _spawnPositionPool.Value.Del(playerEntity);
                    
                    _gameplayModel.Value.EndLevelTime = DateTime.UtcNow;
                }
            }
        }
    }
}