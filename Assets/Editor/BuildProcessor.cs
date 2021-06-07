using System.Collections.Generic;
using System.IO;
using System.Linq;
using TapTap.Common.Editor;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.iOS.Xcode;
using UnityEngine;

namespace Editor
{
    public class BuildProcessor : MonoBehaviour
    {
        [PostProcessBuild(999)]
        public static void OnPostprocessBuild(BuildTarget buildTarget, string path)
        {
            if (buildTarget != BuildTarget.iOS)
            {
                return;
            }

            var proj = TapCommonCompile.ParseProjPath(TapCommonCompile.GetProjPath(path));
            var target = TapCommonCompile.GetUnityTarget(proj);
            var unityFrameworkTarget = TapCommonCompile.GetUnityFrameworkTarget(proj);
            // 企业证书
            proj.SetBuildProperty(target, "CODE_SIGN_IDENTITY",
                "iPhone Distribution: Shanghai Xindong Enterprise Development Co., Ltd.");
            proj.SetBuildProperty(target, "PROVISIONING_PROFILE_SPECIFIER", "Everything 2020");
            proj.SetBuildProperty(target, "PROVISIONING_PROFILE", "6a542e15-b177-4e10-a884-31e7c51c4857");
            proj.SetBuildProperty(target, "CODE_SIGN_IDENTITY[sdk=iphoneos*]",
                "iPhone Distribution: Shanghai Xindong Enterprise Development Co., Ltd.");
            // Dev
            // proj.SetBuildProperty(target, "CODE_SIGN_IDENTITY", "iPhone Developer: GU YUNZE (QNV4UFK7C2)");
            // proj.SetBuildProperty(target, "PROVISIONING_PROFILE_SPECIFIER", "TDSDemo_Dev");
            // proj.SetBuildProperty(target, "PROVISIONING_PROFILE", "1d50d1da-2c85-4c25-afa7-08ba332c2388");
            // proj.SetBuildProperty(target, "CODE_SIGN_IDENTITY[sdk=iphoneos*]", "iPhone Developer: GU YUNZE (QNV4UFK7C2)");
            // proj.SetBuildProperty(target, "DEVELOPMENT_TEAM", "NTC4BJ542G");
            proj.SetBuildProperty(target, "PRODUCT_BUNDLE_IDENTIFIER", "com.tdssdk.demo");

            proj.SetBuildProperty(unityFrameworkTarget, "CODE_SIGN_STYLE", "Manual");

            var parentFolder = Directory.GetParent(Application.dataPath).FullName;
            
            var plistFile = TapFileHelper.RecursionFilterFile(parentFolder + "/Assets/Plugins/", "TDS-Info.plist");

            if (!plistFile.Exists)
            {
                Debug.LogError("TapSDK Can't find TDS-Info.plist in Project/Assets/Plugins/!");
            }

            SetPlist(path, plistFile.FullName);

            File.WriteAllText(TapCommonCompile.GetProjPath(path), proj.WriteToString());

            Debug.Log("TapSDK Unity Sign Success!");
        }

        private static void SetPlist(string pathToBuildProject, string infoPlistPath)
        {
            //添加info
            var plistPath = pathToBuildProject + "/Info.plist";
            var plist = new PlistDocument();
            plist.ReadFromString(File.ReadAllText(plistPath));

            if (string.IsNullOrEmpty(infoPlistPath)) return;

            //添加url
            var dict = plist.root.AsDict();
            var array = dict.CreateArray("CFBundleURLTypes");

            var tapIdList = new List<string>(3)
            {
                "ttuZ8Yy6cSXVOR6AMRPj", "tt0RiAlMny7jiz086FaU", "ttKFV9Pm9ojdmWkkRJeb"
            };

            for (var i = 0; i < tapIdList.Capacity; i++)
            {
                var dict2 = array.AddDict();
                dict2.SetString("CFBundleURLName", "TapTap");
                var array2 = dict2.CreateArray("CFBundleURLSchemes");
                array2.AddString(tapIdList[i]);
            }

            Debug.Log("TapSDK change plist Success");
            File.WriteAllText(plistPath, plist.WriteToString());
        }
    }
}