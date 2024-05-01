using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceLocations;

namespace RpDev.Services.UI.Utils
{
    internal class UIScreenPrototypeProvider
    {
        private readonly Dictionary<Type, UIScreen> _prototypes = new Dictionary<Type, UIScreen>();

        public async UniTask LoadScreenPrototypesAsync()
        {
            var operation = Addressables.LoadResourceLocationsAsync(UIServiceConstants.UIScreenLabel);

            await operation.ToUniTask();

            var resourceLocations = operation.Result;

            foreach (var resourceLocation in resourceLocations)
                await RegisterScreenPrototype(resourceLocation);
        }

        internal UIScreen GetPrototype<TScreen>() where TScreen : UIScreen
        {
            if (_prototypes.TryGetValue(typeof(TScreen), out var prototype) == false)
                throw new Exception($"UI Screen '{typeof(TScreen).Name}' doesn't have a prototype.");

            return prototype;
        }

        internal UIScreen GetPrototypeAsync<TScreen>() where TScreen : UIScreen
        {
            if (_prototypes.TryGetValue(typeof(TScreen), out var prototype) == false)
                throw new Exception($"UI Screen '{typeof(TScreen).Name}' doesn't have a prototype.");

            return prototype;
        }

        private async UniTask RegisterScreenPrototype(IResourceLocation resourceLocation)
        {
            var operationHandle = Addressables.LoadAssetAsync<GameObject>(resourceLocation);

            await operationHandle.ToUniTask();

            var asset = operationHandle.Result;

            if (operationHandle.Status != AsyncOperationStatus.Succeeded)
            {
                Debug.LogException(new Exception($"Failed to load UI Screen asset: '{operationHandle.DebugName}'."));
                return;
            }

            if (asset.TryGetComponent(out UIScreen screenComponent) == false)
            {
                Debug.LogException(
                    new Exception($"UI Screen '{asset.name}' doesn't have a '{nameof(UIScreen)}' component."));
                return;
            }

            var screenType = screenComponent.GetType();

            if (_prototypes.TryAdd(screenType, screenComponent) == false)
                throw new Exception($"UI Screen '{screenType.Name}' has duplicate prototypes.");
        }
    }
}