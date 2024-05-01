using UnityEngine;
using UnityEngine.AI;

namespace Froust.Level.Views
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class EnemyView : CharacterView
    {
        [SerializeField] private NavMeshAgent _agent;
        public NavMeshAgent Agent => _agent;
    }
}