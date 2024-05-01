using System;
using JetBrains.Annotations;
using UnityEngine;

namespace RpDev.Services.Persistence.PersistenceHandlers
{
    [UsedImplicitly]
    public class PlayerPrefsPersistenceHandler<T> : PersistenceHandler<T>
    {
        private readonly string _key;

        public PlayerPrefsPersistenceHandler(string key, bool useCompression = false)
            : base(useCompression)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentException("The key cannot be null or empty.", nameof(key));

            _key = key;
        }

        public override void Clear()
        {
            PlayerPrefs.DeleteKey(_key);
        }

        protected override bool TryGetPersistentData(out string data)
        {
            data = null;

            if (PlayerPrefs.HasKey(_key) == false)
                return false;

            data = PlayerPrefs.GetString(_key);

            if (data.Equals(string.Empty) == false)
                return true;

            Debug.LogError($"PlayerPrefs value for key {_key} is empty.");

            return false;
        }

        protected override void StorePersistentData(string serializedData)
        {
            PlayerPrefs.SetString(_key, serializedData);
        }
    }
}