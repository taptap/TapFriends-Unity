using TapCommon.Editor;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

namespace TapFriends.Editor
{
    public class TapFriendsIOSProcessor
    {
        [PostProcessBuild(105)]
        public static void OnPostprocessBuild(BuildTarget buildTarget, string path)
        {
            if (buildTarget != BuildTarget.iOS) return;
            var projPath = TapCommonCompile.GetProjPath(path);
            var proj = TapCommonCompile.ParseProjPath(projPath);
            var target = TapCommonCompile.GetUnityTarget(proj);
            var unityFrameworkTarget = TapCommonCompile.GetUnityFrameworkTarget(proj);
            if (TapCommonCompile.CheckTarget(target))
            {
                Debug.LogError("Unity-iPhone is NUll");
                return;
            }
            //待续...
            
        }
    }
}