using RpDev.Services.AudioService;
using RpDev.Extensions.Unity;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Froust.Audio
{
    public abstract class UIBehaviourSound<TComponent> : MonoBehaviour where TComponent : UIBehaviour
    {
        [SerializeField] private AudioClipReference _sfx;

        protected TComponent Component;

        private void Awake()
        {
            Component = GetComponent<TComponent>();
        }

        private void Start()
        {
            if (string.IsNullOrEmpty(_sfx.Id))
            {
                #if UNITY_EDITOR
                string message = $"<color=#00FFFF>{transform.GetFullPath()}</color>: sfx is not assigned.";
                #else
				string message = $"{gameObject.name}: sfx is not assigned.";
                #endif

                Debug.LogWarning(message, gameObject);

                Destroy(this);
            }

            AddListener();
        }

        protected abstract void AddListener();

        protected void PlaySfx()
        {
            _sfx.PlayAsUI();
        }
    }
}