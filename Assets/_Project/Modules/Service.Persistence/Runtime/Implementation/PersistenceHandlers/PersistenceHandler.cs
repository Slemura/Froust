using System;
using System.IO;
using System.IO.Compression;
using Newtonsoft.Json;
using UnityEngine;

namespace RpDev.Services.Persistence.PersistenceHandlers
{
    public abstract class PersistenceHandler<T>
    {
        private readonly bool _useCompression;
        private readonly object _lockObject;

        protected PersistenceHandler(bool useCompression = false)
        {
            _useCompression = useCompression;
            _lockObject = new object();
        }

        public void Save(T target)
        {
            if (target is null)
                throw new ArgumentNullException(nameof(target), "The target cannot be null.");

            lock (_lockObject)
            {
                var serializedData = Serialize(target, _useCompression);

                StorePersistentData(serializedData);
            }
        }

        public bool TryLoad(out T value)
        {
            value = default;

            if (TryGetPersistentData(out var data) == false)
                return false;

            lock (_lockObject)
            {
                try
                {
                    value = Deserialize(data, _useCompression);
                }
                catch (Exception exception)
                {
                    Debug.LogError($"Failed to deserialize '{typeof(T)}': {exception.Message}");
                    return false;
                }
            }

            // TODO -> This should be done in a different way.
            return value is not IValidate validatable || validatable.IsValid;
        }

        public bool TryLoadInto(T target)
        {
            if (target is null)
                throw new ArgumentNullException(nameof(target), "The target cannot be null.");

            if (TryGetPersistentData(out var data) == false)
                return false;

            lock (_lockObject)
            {
                try
                {
                    Populate(data, target, _useCompression);
                }
                catch (Exception e)
                {
                    Debug.LogError(e);
                }
            }

            return true;
        }

        public abstract void Clear();

        protected abstract bool TryGetPersistentData(out string data);

        protected abstract void StorePersistentData(string data);

        private static string Serialize(T value, bool compressData)
        {
            string serializedData;

            if (compressData)
            {
                serializedData = JsonConvert.SerializeObject(value);
                serializedData = Compress(serializedData);
            }
            else
            {
                serializedData = JsonConvert.SerializeObject(value, Formatting.Indented);
            }

            return serializedData;
        }

        private static T Deserialize(string data, bool decompressData)
        {
            if (decompressData)
                data = Decompress(data);

            return JsonConvert.DeserializeObject<T>(data);
        }

        private static void Populate(string data, T target, bool decompressData)
        {
            if (decompressData)
                data = Decompress(data);

            JsonConvert.PopulateObject(data, target);
        }

        private static string Compress(string data)
        {
            var binaryData = System.Text.Encoding.UTF8.GetBytes(data);

            using (var compressedStream = new MemoryStream())
            {
                using (var gZipStream = new GZipStream(compressedStream, CompressionMode.Compress))
                {
                    gZipStream.Write(binaryData, 0, binaryData.Length);
                }

                return Convert.ToBase64String(compressedStream.ToArray());
            }
        }

        private static string Decompress(string compressedData)
        {
            var compressedBytes = Convert.FromBase64String(compressedData);

            using (var compressedStream = new MemoryStream(compressedBytes))
            {
                using (var gZipStream = new GZipStream(compressedStream, CompressionMode.Decompress))
                {
                    using (var decompressedStream = new MemoryStream())
                    {
                        gZipStream.CopyTo(decompressedStream);
                        decompressedStream.Position = 0;

                        using (var reader = new StreamReader(decompressedStream))
                        {
                            var originalString = reader.ReadToEnd();
                            return originalString;
                        }
                    }
                }
            }
        }
    }
}