using System;
using Froust.Level.Components;
using Froust.Level.Configurations;
using Froust.Level.Model;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Froust.Level.Systems
{
    public class EndGameSystem : IEcsRunSystem
    {
        private readonly EcsCustomInject<LevelView> _levelData = default;
        private readonly EcsCustomInject<GameplayModel> _gameplayModel = default;
        private readonly EcsCustomInject<GameAudioHandler> _gameAudioHandler = default;
        private readonly EcsFilterInject<Inc<PlayerComponent, CharacterFallFlagComponent>> _filterFallenPlayer = default;
        
        public void Run(IEcsSystems systems)
        {
            if (_filterFallenPlayer.Value.GetEntitiesCount() <= 0) return;
            
            foreach (Transform view in _levelData.Value.GameContainer) Object.Destroy(view.gameObject);
            
            _gameplayModel.Value.EndLevelTime = DateTime.UtcNow;
            _gameplayModel.Value.IsGameEnded = true;
            _gameAudioHandler.Value.PlayLooseSound();
        }
    }
}