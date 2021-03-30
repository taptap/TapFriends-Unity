using System.Collections.Generic;
using System.IO;
using UnityEditor.iOS.Xcode;
using UnityEngine;

namespace TapCommon.Editor
{
    public static class TapCommonCompile
    {
        public static string GetProjPath(string path)
        {
            return PBXProject.GetPBXProjectPath(path);
        }

        public static PBXProject ParseProjPath(string path)
        {
            var proj = new PBXProject();
            proj.ReadFromString(File.ReadAllText(path));
            return proj;
        }

        public static string GetUnityFrameworkTarget(PBXProject proj)
        {
#if UNITY_2019_3_OR_NEWER
            string target = proj.GetUnityFrameworkTargetGuid();
            return target;
#endif
            var target = proj.TargetGuidByName("Unity-iPhone");
            return target;
        }

        public static string GetUnityTarget(PBXProject proj)
        {
#if UNITY_2019_3_OR_NEWER
            string target = proj.GetUnityMainTargetGuid();
            return target;
#endif
            var target = proj.TargetGuidByName("Unity-iPhone");
            return target;
        }

        public static bool CheckTarget(string target)
        {
            return string.IsNullOrEmpty(target);
        }

        public static bool HandlerBundle(string path, string appDataPath, string resourceName, string modulePackageName,
            string moduleName, string[] bundleNames, string target, string projPath, PBXProject proj)
        {
            var resourcePath = Path.Combine(path, resourceName);

            var parentFolder = Directory.GetParent(appDataPath).FullName;

            Debug.Log($"ProjectFolder path:{parentFolder}");

            if (Directory.Exists(resourcePath))
            {
                Directory.Delete(resourcePath, true);
            }

            Directory.CreateDirectory(resourcePath);

            var remotePackagePath =
                TapFileHelper.FilterFile(parentFolder + "/Library/PackageCache/", $"{modulePackageName}@");

            var assetLocalPackagePath = TapFileHelper.FilterFile(parentFolder + "/Assets/", moduleName);

            var localPackagePath = TapFileHelper.FilterFile(parentFolder, moduleName);

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
                return false;
            }

            tdsResourcePath = $"{tdsResourcePath}/Plugins/iOS/Resource";

            Debug.Log($"Find {moduleName} path:{tdsResourcePath}");

            if (!Directory.Exists(tdsResourcePath))
            {
                Debug.LogError($"Can't Find {bundleNames}");
                return false;
            }

            TapFileHelper.CopyAndReplaceDirectory(tdsResourcePath, resourcePath);

            foreach (var name in bundleNames)
            {
                proj.AddFileToBuild(target,
                    proj.AddFile(Path.Combine(resourcePath, name), Path.Combine(resourcePath, name),
                        PBXSourceTree.Source));
            }

            File.WriteAllText(projPath, proj.WriteToString());
            return true;
        }

        public static bool HandlerPlist(string pathToBuildProject, string infoPlistPath)
        {
            //添加info
            var plistPath = pathToBuildProject + "/Info.plist";
            var plist = new PlistDocument();
            plist.ReadFromString(File.ReadAllText(plistPath));
            var rootDic = plist.root;

           var items = new List<string>()
            {
                "tapsdk",
                "tapiosdk",
            };
            var plistElementList = rootDic.CreateArray("LSApplicationQueriesSchemes");
            for (var i = 0; i < items.Count; i++)
            {
                plistElementList.AddString(items[i]);
            }

            if (!string.IsNullOrEmpty(infoPlistPath))
            {
                var dic = (Dictionary<string, object>) Plist.readPlist(infoPlistPath);
                var taptapId = "";

                foreach (var item in dic)
                {
                    if (item.Key.Equals("taptap"))
                    {
                        var taptapDic = (Dictionary<string, object>) item.Value;
                        foreach (var taptapItem in taptapDic)
                        {
                            if (taptapItem.Key.Equals("client_id"))
                            {
                                taptapId = "tt" + (string) taptapItem.Value;
                            }
                        }
                    }
                    else
                    {
                        //Copy TDS-Info.plist中的数据
                        rootDic.SetString(item.Key.ToString(), item.Value.ToString());
                    }
                }
                //添加url
                var dict = plist.root.AsDict();
                var array = dict.CreateArray("CFBundleURLTypes");
                var dict2 = array.AddDict();
                dict2.SetString("CFBundleURLName", "TapTap");
                var array2 = dict2.CreateArray("CFBundleURLSchemes");
                array2.AddString(taptapId);
            }

            File.WriteAllText(plistPath, plist.WriteToString());
            return true;
        }
    }
}