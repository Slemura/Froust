using Froust.Level.Components;
using Froust.Level.Configurations;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Froust.Level.Systems
{
    public class SnowdriftPositionGeneratorSystem : IEcsRunSystem
    {
        private readonly EcsCustomInject<LevelView> _levelData = default;
        private readonly EcsPoolInject<SpawnPositionComponent> _positionPool = default;
        private readonly EcsFilterInject<Inc<SnowdriftComponent>, Exc<SpawnPositionComponent>> _filterSnowdriftWithoutPosition = default;

        private int _lastGivenPointIndex = -1;

        public void Run(IEcsSystems systems)
        {
            foreach (var snowdriftEntity in _filterSnowdriftWithoutPosition.Value)
            {
                ref var spawnPosition = ref _positionPool.Value.Add(snowdriftEntity);
                spawnPosition.Value = GetRandomSpawnPosition();
            }
        }

        private Vector3 GetRandomSpawnPosition()
        {
            var points = _levelData.Value.LevelPoints.Points;

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