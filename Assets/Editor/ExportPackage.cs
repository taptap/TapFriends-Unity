using System.IO;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    public class ExportPackage : UnityEditor.Editor
    {
        static void ExportUnityPackage(string moduleName)
        {
            string exportPath = Directory.GetParent(Application.dataPath).FullName + "/TapSDK-UnityPackage";

            if (!Directory.Exists(exportPath))
            {
                Directory.CreateDirectory(exportPath);
            }

            var path = exportPath + $"/{moduleName}.unitypackage";

            string[] resPaths = {$"Assets/{moduleName}"};

            var assetsPathNames = AssetDatabase.GetDependencies(resPaths);

            AssetDatabase.ExportPackage(assetsPathNames, path, ExportPackageOptions.Recurse);

        }

        static void PushUnityPackage()
        {
            string[] moduleNames = {"TapCommon", "TapBootstrap", "TapMoment", "TapDB", "TapLogin"};

            foreach (var module in moduleNames)
            {
                ExportUnityPackage(module);
            }
        }
    }
}