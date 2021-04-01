using System.IO;
using TapCommon.Editor;
using UnityEditor;
using UnityEditor.Callbacks;
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
            
            var proj = TapCommonCompile.ParseProjPath(path);
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
            
            File.WriteAllText(TapCommonCompile.GetProjPath(path), proj.WriteToString());

            Debug.Log("TapSDK Unity Sign Success!");
        }
    }
}