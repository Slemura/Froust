using System.IO;
using System.Text.RegularExpressions;
using UnityEngine;

namespace RpDev.Services.Persistence
{
    public static class PersistentDataUtils
    {
        private const string SaveFileExtension = ".sav";
        private const string BackupFileExtension = ".bak";
        private const string TempFileExtension = ".tmp";

        public static string GetSaveFilePath(string fileName)
        {
            return GetPath(fileName, SaveFileExtension);
        }

        public static string GetBackupPath(string fileName)
        {
            return GetPath(fileName, BackupFileExtension);
        }

        public static string GetTempPath(string fileName)
        {
            return GetPath(fileName, TempFileExtension);
        }

        private static string GetPath(string fileName, string extension)
        {
            var separator = Path.DirectorySeparatorChar.ToString();
            var rawPath = Path.Combine(Application.persistentDataPath, fileName + extension);
            var fixedPath = Regex.Replace(rawPath, "[/\\\\]", separator);

            return fixedPath;
        }
    }
}