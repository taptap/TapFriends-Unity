using System.IO;
using TapCommon.Editor;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

namespace TapBootstrap.Editor
{
    public static class TapBootstrapIOSProcessor
    {
        // 添加标签，unity导出工程后自动执行该函数
        [PostProcessBuild(100)]
        public static void OnPostprocessBuild(BuildTarget buildTarget, string path)
        {
            if (buildTarget != BuildTarget.iOS) return;
            // 获得工程路径
            var projPath = TapCommonCompile.GetProjPath(path);
            var proj = TapCommonCompile.ParseProjPath(projPath);
            var target = TapCommonCompile.GetUnityTarget(proj);

            if (TapCommonCompile.CheckTarget(target))
            {
                Debug.LogError("Unity-iPhone is NUll");
                return;
            }

            var parentFolder = Directory.GetParent(Application.dataPath).FullName;

            var plistFile = TapFileHelper.RecursionFilterFile(parentFolder + "/Assets/Plugins/", "TDS-Info.plist");
            
            if (!plistFile.Exists)
            {
                Debug.LogError("TapSDK Can't find TDS-Info.plist in Project/Assets/Plugins/!");
            }
            TapCommonCompile.HandlerPlist(path, plistFile.FullName);
            if (TapCommonCompile.HandlerBundle(path,
                Application.dataPath,
                "TapBootstrapResource",
                "com.tapsdk.bootstrap",
                "TapBootstrap",
                new[] {"TapBootstrapResource.bundle"},
                target, projPath, proj))
            {
                Debug.Log("TapBootstrap add Bundle Success!");
                return;
            }

            Debug.LogError("TapBootstrap add Bundle Failed!");
        }
    }
}