using UnityEngine;

namespace TapTap.Common
{
    public class Platform
    {
        public static bool IsAndroid()
        {
            return Application.platform == RuntimePlatform.Android;
        }

        public static bool IsIOS()
        {
            return Application.platform == RuntimePlatform.IPhonePlayer;
        }
    }
}