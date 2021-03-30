using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.iOS.Xcode;
using UnityEngine;

namespace TapCommon.Script.Editor
{
    public class TapCommonIOSProcessor : MonoBehaviour
    {
        // 添加标签，unity导出工程后自动执行该函数
        [PostProcessBuild(99)]
        public static void OnPostprocessBuild(BuildTarget buildTarget, string path)
        {
            if (buildTarget != BuildTarget.iOS) return;
            // 获得工程路径
            var projPath = PBXProject.GetPBXProjectPath(path);
            var proj = new PBXProject();
            proj.ReadFromString(File.ReadAllText(projPath));
            // 2019.3以上有多个target
#if UNITY_2019_3_OR_NEWER
            string unityFrameworkTarget = proj.GetUnityFrameworkTargetGuid();
            string target = proj.GetUnityMainTargetGuid();
#else
            var unityFrameworkTarget = proj.TargetGuidByName("Unity-iPhone");
            var target = proj.TargetGuidByName("Unity-iPhone");
#endif
            if (target == null)
            {
                Debug.LogError("TapCommon iOS Compile Error, Target is NUll!");
                return;
            }

            proj.AddBuildProperty(target, "OTHER_LDFLAGS", "-ObjC");
            proj.AddBuildProperty(unityFrameworkTarget, "OTHER_LDFLAGS", "-ObjC");

            proj.SetBuildProperty(target, "ENABLE_BITCODE", "NO");
            proj.SetBuildProperty(target, "ALWAYS_EMBED_SWIFT_STANDARD_LIBRARIES", "YES");
            proj.SetBuildProperty(target, "SWIFT_VERSION", "5.0");
            proj.SetBuildProperty(target, "CLANG_ENABLE_MODULES", "YES");
            proj.SetBuildProperty(unityFrameworkTarget, "ENABLE_BITCODE", "NO");
            proj.SetBuildProperty(unityFrameworkTarget, "ALWAYS_EMBED_SWIFT_STANDARD_LIBRARIES", "YES");
            proj.SetBuildProperty(unityFrameworkTarget, "SWIFT_VERSION", "5.0");
            proj.SetBuildProperty(unityFrameworkTarget, "CLANG_ENABLE_MODULES", "YES");

            proj.AddFrameworkToProject(unityFrameworkTarget, "CoreTelephony.framework", false);
            proj.AddFrameworkToProject(unityFrameworkTarget, "QuartzCore.framework", false);
            proj.AddFrameworkToProject(unityFrameworkTarget, "Security.framework", false);
            proj.AddFrameworkToProject(unityFrameworkTarget, "WebKit.framework", false);
            proj.AddFrameworkToProject(unityFrameworkTarget, "Photos.framework", false);
            proj.AddFrameworkToProject(unityFrameworkTarget, "AdSupport.framework", false);
            proj.AddFrameworkToProject(unityFrameworkTarget, "AssetsLibrary.framework", false);
            proj.AddFrameworkToProject(unityFrameworkTarget, "AVKit.framework", false);
            proj.AddFrameworkToProject(unityFrameworkTarget, "AuthenticationServices.framework", true);
            proj.AddFrameworkToProject(unityFrameworkTarget, "LocalAuthentication.framework", false);
            proj.AddFrameworkToProject(unityFrameworkTarget, "SystemConfiguration.framework", false);
            proj.AddFrameworkToProject(unityFrameworkTarget, "Accelerate.framework", false);
            proj.AddFrameworkToProject(unityFrameworkTarget, "SafariServices.framework", false);
            proj.AddFrameworkToProject(unityFrameworkTarget, "AVFoundation.framework", false);
            proj.AddFrameworkToProject(unityFrameworkTarget, "MobileCoreServices.framework", false);
            proj.AddFrameworkToProject(unityFrameworkTarget, "AppTrackingTransparency.framework", true);

            proj.AddFileToBuild(unityFrameworkTarget,
                proj.AddFile("usr/lib/libc++.tbd", "libc++.tbd", PBXSourceTree.Sdk));

            var resourcePath = Path.Combine(path, "TapCommonResource");

            var parentFolder = Directory.GetParent(Application.dataPath).FullName;

            Debug.Log($"ProjectFolder path:{parentFolder}");

            if (Directory.Exists(resourcePath))
            {
                Directory.Delete(resourcePath, true);
            }

            Directory.CreateDirectory(resourcePath);

            var remotePackagePath =
                TapFileHelper.FilterFile(parentFolder + "/Library/PackageCache/", "com.tapsdk.common@");

            var assetLocalPackagePath = TapFileHelper.FilterFile(parentFolder + "/Assets/", "TapCommon");

            var localPackagePath = TapFileHelper.FilterFile(parentFolder, "TapCommon");

            var tdsResourcePath = "";

            if (!string.IsNullOrEmpty(remotePackagePath))
            {
                tdsResourcePath = remotePackagePath;
            }
            else if (!string.IsNullOrEmpty(assetLocalPackagePath))
            {
                tdsResourcePath = assetLocalPackagePath;
            }
            else if (!string.IsNullOrEmpty(localPackagePath))
            {
                tdsResourcePath = localPackagePath;
            }

            if (string.IsNullOrEmpty(tdsResourcePath))
            {
                Debug.LogError("tdsResourcePath is NUll");
                return;
            }

            tdsResourcePath = $"{tdsResourcePath}/Plugins/iOS/Resource";

            Debug.Log($"Find TapCommonResource path:{tdsResourcePath}");

            if (Directory.Exists(tdsResourcePath))
            {
                TapFileHelper.CopyAndReplaceDirectory(tdsResourcePath, resourcePath);
            }

            var names = new List<string> {"TapCommonResource.bundle"};
            foreach (var name in names)
            {
                proj.AddFileToBuild(target,
                    proj.AddFile(Path.Combine(resourcePath, name), Path.Combine(resourcePath, name),
                        PBXSourceTree.Source));
            }

            File.WriteAllText(projPath, proj.WriteToString());
        }
    }
}