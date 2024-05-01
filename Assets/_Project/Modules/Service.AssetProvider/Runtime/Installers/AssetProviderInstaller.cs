using RpDev.Di.Installer.VContainer;
using RpDev.Services.AssetProvider.Abstractions;
using RpDev.Services.AssetProvider.Implementations;
using VContainer;

namespace RpDev.Services.AssetProvider.Installers
{
    public class AssetProviderInstaller : VContainerInstaller
    {
        public override void Install(IContainerBuilder builder)
        {
            builder.Register<IAssetProvider, AddressablesAssetProvider>(Lifetime.Singleton);
        }
    }
}