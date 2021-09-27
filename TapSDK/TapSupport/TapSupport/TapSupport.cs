using System.Collections.Generic;
using System.Threading.Tasks;

namespace TapTap.Support
{
    public static class TapSupport
    {
        public static void Init(string serverUrl, string rootCategoryID, TapSupportCallback callback)
        {
            TapSupportImpl.GetInstance().Init(serverUrl, rootCategoryID, callback);
        }

        private static Task Login(string appId, string sessionToken)
        {
            return TapSupportImpl.GetInstance().Login(appId, sessionToken);
        }

        public static void AnonymousLogin(string uuid)
        {
            TapSupportImpl.GetInstance().AnonymousLogin(uuid);
        }

        public static void AnonymousLogin()
        {
            TapSupportImpl.GetInstance().AnonymousLogin();
        }

        public static string GetAnonymousId()
        {
            return TapSupportImpl.GetInstance().GetAnonymousId();
        }

        public static void Resume()
        {
            TapSupportImpl.GetInstance().Resume();
        }

        public static void Pause()
        {
            TapSupportImpl.GetInstance().Pause();
        }

        public static Task<bool> FetchUnReadStatus()
        {
            return TapSupportImpl.GetInstance().FetchUnReadStatus();
        }

        public static void SetDefaultMetaData(Dictionary<string, object> metaData)
        {
            TapSupportImpl.GetInstance().SetDefaultMetaData(metaData);
        }

        public static void SetDefaultFieldsData(Dictionary<string, object> fieldsData)
        {
            TapSupportImpl.GetInstance().SetDefaultFieldsData(fieldsData);
        }

        public static string GetSupportWebUrl()
        {
            return TapSupportImpl.GetInstance().GetSupportWebUrl();
        }

        public static string GetSupportWebUrl(string path)
        {
            return TapSupportImpl.GetInstance().GetSupportWebUrl(path);
        }

        public static string GetSupportWebUrl(string path, Dictionary<string, object> metaData,
            Dictionary<string, object> fieldsData)
        {
            return TapSupportImpl.GetInstance().GetSupportWebUrl(path, metaData, fieldsData);
        }
    }
}