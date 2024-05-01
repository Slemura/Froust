using UnityEngine;
using VContainer;

namespace RpDev.Di.Installer.VContainer
{
    public abstract class VContainerScriptableObjectInstaller : ScriptableObject
    {
        public abstract void Install(IContainerBuilder builder);
    }
}