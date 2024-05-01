using RpDev.Di.Installer.VContainer;
using VContainer;

namespace Froust.Runtime.PersistentData
{
    public class UserDataHandlerInstaller : VContainerInstaller
    {
        public override void Install(IContainerBuilder builder)
        {
            builder.Register<UserDataHandler>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();
        }
    }
}