using System;
using System.Collections.Generic;
using UnityEngine.LowLevel;
using UnityEngine.PlayerLoop;

namespace RpDev.Extensions.Unity
{
    public static class UnityObjectExtensions
    {
        private static readonly object Lock = new object();

        // ReSharper disable once UnusedParameter.Global
        public static void AttachToPlayerLoop<T>(this T self, PlayerLoopSystem.UpdateFunction updateFunction)
        {
            var playerLoopSystem = new PlayerLoopSystem
            {
                type = typeof(Update), // TODO what
                updateDelegate = updateFunction
            };

            var currentPlayerLoop = PlayerLoop.GetCurrentPlayerLoop();

            lock (Lock)
            {
                if (Array.Exists(currentPlayerLoop.subSystemList, static sys => sys.type == typeof(T)))
                    return;

                var currentSubSystems = new List<PlayerLoopSystem>(currentPlayerLoop.subSystemList);

                if (currentSubSystems.Contains(playerLoopSystem))
                    return;

                currentSubSystems.Add(playerLoopSystem);

                currentPlayerLoop.subSystemList = currentSubSystems.ToArray();

                PlayerLoop.SetPlayerLoop(currentPlayerLoop);
            }
        }

        // ReSharper disable once UnusedParameter.Global
        public static void DetachFromPlayerLoop<T>(this T self)
        {
            var currentPlayerLoop = PlayerLoop.GetCurrentPlayerLoop();

            lock (Lock)
            {
                if (!Array.Exists(currentPlayerLoop.subSystemList, static sys => sys.type == typeof(T)))
                    return;

                var currentSubSystems = new List<PlayerLoopSystem>(currentPlayerLoop.subSystemList);

                currentSubSystems.RemoveAll(static sys => sys.type == typeof(T));
                currentPlayerLoop.subSystemList = currentSubSystems.ToArray();
                PlayerLoop.SetPlayerLoop(currentPlayerLoop);
            }
        }
    }
}