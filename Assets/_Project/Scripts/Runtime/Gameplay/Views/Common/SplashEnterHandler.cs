using RpDev.Services.AudioService;
using UnityEngine;

namespace Froust.Level.Views
{
    public class SplashEnterHandler : MonoBehaviour
    {

        [SerializeField] private ParticleSystem _splashParticles;
        [SerializeField] private AudioClipPack _splashAudio;
        [SerializeField] private LayerMask _waterMask;

        private void OnTriggerEnter(Collider other)
        {
            if (((1 << other.gameObject.layer) & _waterMask.value) == 0) return;
            
            _splashAudio.PlayRandomAsSfx();
            if(_splashParticles != null) _splashParticles.Play();
        }
    }
}
