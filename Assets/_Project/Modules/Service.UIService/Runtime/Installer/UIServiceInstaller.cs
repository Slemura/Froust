using RpDev.Di.Installer.VContainer;
using RpDev.Services.UI.Mediators;
using UnityEngine;
using UnityEngine.Serialization;
using VContainer;
using VContainer.Unity;

namespace RpDev.Services.UI.Installer
{
    [CreateAssetMenu(menuName = "Services/UIServiceInstaller" )]
    public class UIServiceInstaller : VContainerScriptableObjectInstaller
    {
        [FormerlySerializedAs("_uiSystemRoot")] [SerializeField] private UIService _uiServiceRoot;
        
        public override void Install(IContainerBuilder builder)
        {
            builder.RegisterComponentInNewPrefab(_uiServiceRoot, Lifetime.Singleton)
                .DontDestroyOnLoad()
                .AsImplementedInterfaces();

            builder.Register<UIMediatorFactory>(Lifetime.Singleton)
                .AsSelf();
        }
    }
}