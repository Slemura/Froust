using DG.Tweening;
using Froust.Level.Components;
using Froust.Level.Configurations;
using Froust.Level.Model;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Froust.Level.Systems
{
    public class GroundSinkSystem : IEcsRunSystem
    {
        private readonly EcsCustomInject<LevelView> _levelView = default;
        
        private readonly EcsCustomInject<GameplayModel> _gameplayModel = default;
        private readonly EcsCustomInject<GroundConfiguration> _groundConfig = default;
        private readonly EcsCustomInject<GameAudioHandler> _audioHandler = default;
        private readonly EcsPoolInject<PlayerCharacterDrownedComponent> _drownedPool = default;
        
        private readonly EcsFilterInject<Inc<PlayerComponent, SpawnPositionComponent>, Exc<PlayerCharacterDrownedComponent>> _filterPlayer = default;

        private float _timeBeforeSinkCheck = 0;
        private float _lastSinkSoundPlayTime = 0;
        private int _lastEnemyCount = 0;
        
        public void Run(IEcsSystems systems)
        {
            if (_filterPlayer.Value.GetEntitiesCount() == 0)
                return;

            _timeBeforeSinkCheck -= Time.deltaTime;
            
            if(_lastEnemyCount == _gameplayModel.Value.CurrentEnemiesCount)
                return;
            
            _lastEnemyCount = _gameplayModel.Value.CurrentEnemiesCount;

            if (_timeBeforeSinkCheck > 0)
                return;
            
            _timeBeforeSinkCheck = _groundConfig.Value.RefreshDelay;
            
            UpdateGroundPosition();

            if (_gameplayModel.Value.CurrentEnemiesCount - _groundConfig.Value.IgnoreEnemyCount >
                _groundConfig.Value.MaxEnemyCount)
            {
                foreach (var playerEntity in _filterPlayer.Value) _drownedPool.Value.Add(playerEntity);
            }
        }

        private void UpdateGroundPosition()
        {
            var water = _levelView.Value.Water;

            var sinkLevelNormalize =
                Mathf.Clamp01(
                    (_gameplayModel.Value.CurrentEnemiesCount - _groundConfig.Value.IgnoreEnemyCount) /
                    (float)_groundConfig.Value.MaxEnemyCount);

            _gameplayModel.Value.IceDriftLevel.Value = sinkLevelNormalize;
            
            var waterPositionY = Mathf.Lerp(-4.5f, _groundConfig.Value.FailWaterPosition, sinkLevelNormalize);
            
            if (Mathf.Abs(water.position.y - waterPositionY) < float.Epsilon)
                return;
           
            if(water.position.y - waterPositionY < 0)
                PlayVfxAndSfxOnce();
            
            water.DOKill();
            water.DOMoveY(waterPositionY, 0.5f)
                .SetEase(_groundConfig.Value.SinkAnimationCurve)
                .SetEase(Ease.OutSine)
                .SetLink(water.gameObject, LinkBehaviour.KillOnDestroy);
        }

        private void PlayVfxAndSfxOnce()
        {
            if (Time.realtimeSinceStartup - _lastSinkSoundPlayTime > 1)
            {
                _lastSinkSoundPlayTime = Time.realtimeSinceStartup;
                _audioHandler.Value.PlayIceFoeSinkSound();
                _levelView.Value.IceFoeSplashParticles.Play();
            }
        }
    }
}