using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Object = UnityEngine.Object;

namespace RpDev.Services.AssetProvider.Abstractions
{
    public interface IAssetProvider : IDisposable
    {
        UniTask<T> LoadAsset<T>(string address, CancellationToken cancellationToken) where T : Object;
        void UnloadAsset(string address);
    }
}