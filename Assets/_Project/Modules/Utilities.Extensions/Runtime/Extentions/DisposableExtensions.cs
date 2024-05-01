using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace RpDev.Extensions
{
    public static class DisposableExtensions
    {
        public static T LinkDestroyTo<T>(this T self, ICollection<IDisposable> disposables) where T : Component
        {
            disposables.Add(new GameObjectDestroyDisposable(self.gameObject));

            return self;
        }

        private sealed class GameObjectDestroyDisposable : IDisposable
        {
            private readonly GameObject _gameObject;

            public GameObjectDestroyDisposable(GameObject gameObject)
            {
                _gameObject = gameObject;
            }

            public void Dispose()
            {
                Object.Destroy(_gameObject);
            }
        }
    }
}