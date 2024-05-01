using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using RpDev.Services.AssetProvider.Abstractions;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Object = UnityEngine.Object;

namespace RpDev.Services.AssetProvider.Implementations
{
    public class AddressablesAssetProvider : IAssetProvider
    {
        private readonly Dictionary<string, AsyncOperationHandle> _cachedObjects = new Dictionary<string, AsyncOperationHandle>();

        public async UniTask<T> LoadAsset<T>(string address, CancellationToken cancellationToken) where T : Object
        {
            if (_cachedObjects.TryGetValue(address, out var cachedHandle))
            {
                if (cachedHandle.IsDone == false)
                    await cachedHandle.Task;

                return cachedHandle.Result as T;
            }

            var loadedObject = Addressables.LoadAssetAsync<T>(address);

            _cachedObjects.Add(address, loadedObject);

            await loadedObject;

            if (cancellationToken.IsCancellationRequested)
                cancellationToken.ThrowIfCancellationRequested();

            return loadedObject.Result;
        }
        
        public void UnloadAsset(string address)
        {
            if (_cachedObjects.TryGetValue(address, out var cachedHandle) == false) return;
            
            Addressables.Release(cachedHandle);
            _cachedObjects.Remove(address);
        }

        public void Dispose()
        {
            foreach (var resource in _cachedObjects.Values) Addressables.Release(resource);
            
            _cachedObjects.Clear();
        }
    }
}