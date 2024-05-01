using Froust.EntryPoint.States;
using Froust.Level.Model;
using RpDev.Di.Installer.VContainer;
using RpDev.Services.AsyncStateMachine.Installer;
using RpDev.Services.AsyncStateMachine.Tools;
using VContainer;

namespace Froust.EntryPoint.Installer
{
    public class AppCoreInstaller : VContainerInstaller
    {
        public override void Install(IContainerBuilder builder)
        {
            builder.Register<AppCore>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<GameplayResourcesModel>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();
            
            RegisterStateMachine(builder);
        }
        
        private void RegisterStateMachine(IContainerBuilder builder)
        {
            new StateMachineInstaller().Install(builder);
            
            builder.BindState<BootstrapState>();
            builder.BindState<GameplayState>();
            builder.BindState<GameOverState>();
            builder.BindState<MainMenuState>();
        }
    }
}