using VContainer;

namespace RpDev.Di.Installer.VContainer
{
    public abstract class VContainerInstaller
    {
        public abstract void Install(IContainerBuilder builder);
    }
}
