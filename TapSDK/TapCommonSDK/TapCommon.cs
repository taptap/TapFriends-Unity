using System;
using System.Threading.Tasks;

namespace TapTap.Common
{
    public class TapCommon
    {
        public static void Init(TapConfig config)
        {
            TapCommonImpl.GetInstance().Init(config);
        }

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

        public static void RegisterProperties(string key, ITapPropertiesProxy proxy)
        {
            TapCommonImpl.GetInstance().RegisterProperties(key, proxy);
        }

        public static void AddHost(string host, string replaceHost)
        {
            TapCommonImpl.GetInstance().AddHost(host, replaceHost);
        }

        public static Task<bool> UpdateGameAndFailToWebInTapTap(string appId)
        {
            return TapCommonImpl.GetInstance().UpdateGameAndFailToWebInTapTap(appId);
        }

        public static Task<bool> UpdateGameAndFailToWebInTapGlobal(string appId)
        {
            return TapCommonImpl.GetInstance().UpdateGameAndFailToWebInTapGlobal(appId);
        }

        public static Task<bool> UpdateGameAndFailToWebInTapTap(string appId, string webUrl)
        {
            return TapCommonImpl.GetInstance().UpdateGameAndFailToWebInTapTap(appId, webUrl);
        }

        public static Task<bool> UpdateGameAndFailToWebInTapGlobal(string appId, string webUrl)
        {
            return TapCommonImpl.GetInstance().UpdateGameAndFailToWebInTapGlobal(appId, webUrl);
        }

        public static Task<bool> OpenWebDownloadUrlOfTapTap(string appId)
        {
            return TapCommonImpl.GetInstance().OpenWebDownloadUrlOfTapTap(appId);
        }

        public static Task<bool> OpenWebDownloadUrlOfTapGlobal(string appId)
        {
            return TapCommonImpl.GetInstance().OpenWebDownloadUrlOfTapGlobal(appId);
        }

        public static Task<bool> OpenWebDownloadUrl(string url)
        {
            return TapCommonImpl.GetInstance().OpenWebDownloadUrl(url);
        }
    }
}