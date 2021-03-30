using TapCommon.Scripts.Editor;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

namespace TapLogin.Scripts.Editor
{
    public class TapLoginIOSProcessor : MonoBehaviour
    {
        [PostProcessBuild(100)]
        public static void OnPostprocessBuild(BuildTarget buildTarget, string path)
        {
            if (buildTarget != BuildTarget.iOS) return;
            
            var unityAppControllerPath = path + "/Classes/UnityAppController.mm";
            var unityAppController = new TapFileHelper(unityAppControllerPath);
            unityAppController.WriteBelow(@"#import <OpenGLES/ES2/glext.h>", @"#import <TapLoginSDK/TapLoginHelper.h>");
            unityAppController.WriteBelow(
                @"id sourceApplication = options[UIApplicationOpenURLOptionsSourceApplicationKey], annotation = options[UIApplicationOpenURLOptionsAnnotationKey];",
                @"if(url){[TapLoginHelper handleTapTapOpenURL:url];}");
            Debug.Log("TapLogin Change AppControler File!");
        }
    }
}