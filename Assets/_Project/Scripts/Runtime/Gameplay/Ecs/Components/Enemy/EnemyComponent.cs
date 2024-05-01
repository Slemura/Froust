using Froust.Level.Views;
using UnityEngine;

namespace Froust.Level.Components
{
    public struct EnemyComponent
    {
        public bool IsKing;
        public EnemyView View;
        public Vector3 LastWaypoint;
    }
}