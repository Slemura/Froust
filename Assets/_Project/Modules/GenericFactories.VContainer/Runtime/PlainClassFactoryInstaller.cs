using RpDev.Di.Installer.VContainer;
using RpDev.Services.GenericFactories.VContainer;
using VContainer;

namespace RpDev.GenericFactories.VContainer
{
    public class PlainClassFactoryInstaller : VContainerInstaller
    {
        public override void Install(IContainerBuilder builder)
        {
            builder.Register<PlainClassPlainClassFactory>(Lifetime.Singleton).AsImplementedInterfaces();
        }
    }
}