using System;
using System.IO;
using System.Text;
using UnityEngine;

namespace TapTap.Support
{
    public class TapSupportPersistence
    {
        public static readonly string DeskUuid = ".TapDesk_uuid";

        public static string GetUuid()
        {
            var cacheUuid = ReadText(DeskUuid);
            if (!string.IsNullOrEmpty(cacheUuid)) return cacheUuid;
            var uuid = Guid.NewGuid().ToString();
            WriteText(DeskUuid, uuid);
            return uuid;
        }

        public static void Save(string uuid)
        {
            WriteText(DeskUuid, uuid);
        }

        public static void WriteText(string filename, string text)
        {
            using (FileStream fs = File.Create(GetFileFullPath(filename)))
            {
                byte[] bytes = Encoding.UTF8.GetBytes(text);
                fs.Write(bytes, 0, bytes.Length);
            }
        }

        public static string ReadText(string filename)
        {
            string fileFullPath = GetFileFullPath(filename);
            if (!File.Exists(fileFullPath))
                return null;
            string str;
            using (FileStream fs = File.OpenRead(fileFullPath))
            {
                byte[] buffer = new byte[fs.Length];
                int num = fs.Read(buffer, 0, (int) fs.Length);
                str = Encoding.UTF8.GetString(buffer);
                buffer = null;
            }
            return str;
        }

        private static string GetFileFullPath(string filename)
        {
            return Path.Combine(Application.persistentDataPath, filename);
        }
    }
}