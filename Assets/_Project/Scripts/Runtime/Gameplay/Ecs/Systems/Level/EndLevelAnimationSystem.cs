using Froust.Level.Components;
using Froust.Level.Configurations;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Froust.Level.Systems
{
    public class EndLevelAnimationSystem : IEcsRunSystem
    {
        private readonly EcsCustomInject<LevelView> _levelData = default;
        
        private readonly EcsFilterInject<Inc<PlayerComponent, PlayerCharacterDrownedComponent>, Exc<CharacterFallFlagComponent>> _filterPlayer = default;
        private readonly EcsFilterInject<Inc<EnemyComponent, SpawnPositionComponent, ViewComponent>> _filterEnemies = default;

        public void Run(IEcsSystems systems)
        {
            if (_filterPlayer.Value.GetEntitiesCount() <= 0) return;
            
            foreach (var playerEntity in _filterPlayer.Value)
            {
                ref var playerView = ref _filterPlayer.Pools.Inc1.Get(playerEntity).View;
                playerView.SetupConstraints(RigidbodyConstraints.None);
                playerView.AddRandomTorque(20);
            }

            foreach (var enemyEntity in _filterEnemies.Value)
            {
                ref var enemyView = ref _filterEnemies.Pools.Inc1.Get(enemyEntity).View;
                enemyView.Agent.enabled = false;
                enemyView.SetupConstraints(RigidbodyConstraints.None);
                enemyView.AddRandomTorque(20);
            }

            _levelData.Value.Ground.position += new Vector3(0, -Time.deltaTime * 15, 0);
        }
    }
}