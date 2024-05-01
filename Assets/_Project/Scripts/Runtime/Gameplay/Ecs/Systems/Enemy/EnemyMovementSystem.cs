using Froust.Level.Components;
using Froust.Level.Configurations;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Froust.Level.Systems
{
    public class EnemyMovementSystem : IEcsRunSystem
    {
        private readonly EcsCustomInject<LevelView> _levelData = default;

        private readonly
            EcsFilterInject<Inc<EnemyComponent, SpawnPositionComponent, ViewComponent>,
                Exc<CharacterPushedComponent, CharacterFallFlagComponent>> _filterEnemies = default;
        
        private readonly EcsFilterInject<Inc<PlayerCharacterDrownedComponent>> _filterDrowned = default;

        private int _lastGivenPointIndex = -1;
        private float _distance;

        public void Run(IEcsSystems systems)
        {
            if (_filterDrowned.Value.GetEntitiesCount() > 0)
                return;

            foreach (var enemyEntity in _filterEnemies.Value)
            {
                ref var enemy = ref _filterEnemies.Pools.Inc1.Get(enemyEntity);

                _distance = Vector3.Distance(enemy.View.transform.position, enemy.LastWaypoint);

                if (enemy.LastWaypoint == Vector3.zero || _distance < enemy.View.Agent.stoppingDistance)
                {
                    enemy.LastWaypoint = GetRandomWayPosition();

                    enemy.View.Agent.SetDestination(enemy.LastWaypoint);
                }
            }
        }

        private Vector3 GetRandomWayPosition()
        {
            var points = _levelData.Value.EnemyMovePoints.Points;

            if (points == null || points.Length == 0)
                return Vector3.zero;

            if (points.Length == 1)
                return points[0].position;

            int randomIndex;

            do
            {
                randomIndex = Random.Range(0, points.Length);
            } while (randomIndex == _lastGivenPointIndex);

            _lastGivenPointIndex = randomIndex;

            return points[randomIndex].position;
        }
    }
}