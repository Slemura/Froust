using RpDev.Di.Installer.VContainer;
using RpDev.Services.AsyncStateMachine.Abstractions;
using RpDev.Services.AsyncStateMachine.Implementations;
using VContainer;

namespace RpDev.Services.AsyncStateMachine.Installer
{
    public class StateMachineInstaller : VContainerInstaller
    {
        public override void Install(IContainerBuilder builder)
        {
            builder.Register<IStateFactory, StateFactory>(Lifetime.Singleton);
            builder.Register(typeof(StateMachine<>), Lifetime.Transient);
        }
    }
}