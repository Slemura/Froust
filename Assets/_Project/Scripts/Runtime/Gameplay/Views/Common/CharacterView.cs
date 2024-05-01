using JetBrains.Annotations;
using UnityEngine;

namespace Froust.Level.Views
{
    public class CharacterView : RigidbodyView
    {
        [SerializeField][CanBeNull] private ParticleSystem _snowPuffParticles;
        
        public void ShowSnowPuff()
        {
            if (_snowPuffParticles != null) _snowPuffParticles.Play();
        }
    }
}