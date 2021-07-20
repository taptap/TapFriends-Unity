using System;

namespace TapTap.Common
{
    public class TapCommon
    {
        public static void GetRegionCode(Action<bool> callback)
        {
            TapCommonImpl.GetInstance().GetRegionCode(callback);
        }

        public static void IsTapTapInstalled(Action<bool> callback)
        {
            TapCommonImpl.GetInstance().IsTapTapInstalled(callback);
        }

        public static void IsTapTapGlobalInstalled(Action<bool> callback)
        {
            TapCommonImpl.GetInstance().IsTapTapGlobalInstalled(callback);
        }

        public static void UpdateGameInTapTap(string appId, Action<bool> callback)
        {
            TapCommonImpl.GetInstance().UpdateGameInTapTap(appId, callback);
        }

        public static void UpdateGameInTapGlobal(string appId, Action<bool> callback)
        {
            TapCommonImpl.GetInstance().UpdateGameInTapGlobal(appId, callback);
        }

        public static void OpenReviewInTapTap(string appId, Action<bool> callback)
        {
            TapCommonImpl.GetInstance().OpenReviewInTapTap(appId, callback);
        }

        public static void OpenReviewInTapGlobal(string appId, Action<bool> callback)
        {
            TapCommonImpl.GetInstance().OpenReviewInTapGlobal(appId, callback);
        }

        public static void SetXua()
        {
            TapCommonImpl.GetInstance().SetXua();
        }

        public static void SetLanguage(TapLanguage language)
        {
            TapCommonImpl.GetInstance().SetLanguage(language);
        }

        public static void ConsumptionProperties(string key, ITapPropertiesProxy proxy)
        {
            TapCommonImpl.GetInstance().ConsumptionProperties(key, proxy);
        }
        
    }
}