using System;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using UnityEngine;

namespace Froust.Services
{
    public static class HitStop
    {
        private const float Duration = 0.085f;
        
        public static void Stop()
        {
            StopAsync().Forget();
        }

        private static async UniTaskVoid StopAsync()
        {
            Time.timeScale = 0f;

            await UniTask.Delay(TimeSpan.FromSeconds(Duration), DelayType.UnscaledDeltaTime);

            Time.timeScale = 1f;
        }
    }
}