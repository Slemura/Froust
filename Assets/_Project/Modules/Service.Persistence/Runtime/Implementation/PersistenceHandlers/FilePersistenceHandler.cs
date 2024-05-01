using System;
using System.IO;
using JetBrains.Annotations;
using UnityEngine;

namespace RpDev.Services.Persistence.PersistenceHandlers
{
    [UsedImplicitly]
    public class FilePersistenceHandler<T> : PersistenceHandler<T>
    {
        private string FilePath => PersistentDataUtils.GetSaveFilePath(_fileName);
        private string BackupFilePath => PersistentDataUtils.GetBackupPath(_fileName);
        private string TempFilePath => PersistentDataUtils.GetTempPath(_fileName);

        private readonly string _fileName;

        public FilePersistenceHandler(string fileName, bool useCompression = false)
            : base(useCompression)
        {
            if (string.IsNullOrEmpty(fileName))
                throw new ArgumentException("File name cannot be null or empty.", nameof(fileName));

            _fileName = fileName;
        }

        public override void Clear()
        {
            File.Delete(FilePath);
            File.Delete(BackupFilePath);
            File.Delete(TempFilePath);
        }

        protected override bool TryGetPersistentData(out string data)
        {
            data = null;
            string filePath;

            if (File.Exists(FilePath))
            {
                filePath = FilePath;
            }
            else if (File.Exists(BackupFilePath))
            {
                filePath = BackupFilePath;
            }
            else
            {
                return false;
            }

            if (new FileInfo(filePath).Length != 0)
            {
                data = File.ReadAllText(filePath);
                return true;
            }

            Debug.LogWarning($"File '{filePath}' is empty.");

            return false;
        }

        protected override void StorePersistentData(string data)
        {
            try
            {
                File.WriteAllText(TempFilePath, data);

                if (File.Exists(FilePath))
                {
                    File.Replace(TempFilePath, FilePath, BackupFilePath, true);
                }
                else
                {
                    File.Move(TempFilePath, FilePath);
                }

                File.Delete(TempFilePath);
                File.Delete(BackupFilePath);
            }
            catch (Exception exception)
            {
                Debug.LogError($"Failed to write file '{FilePath}': {exception.Message}");
            }
        }
    }
}