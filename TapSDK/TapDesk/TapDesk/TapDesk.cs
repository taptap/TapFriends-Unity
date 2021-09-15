using System.Collections.Generic;
using System.Threading.Tasks;

namespace TapTap.Desk
{
    public static class TapDesk
    {
        public static void Init(string serverUrl, string rootCategoryID, TapDeskCallback callback)
        {
            TapDeskImpl.GetInstance().Init(serverUrl, rootCategoryID, callback);
        }

        public static Task Login(string appId, string sessionToken)
        {
            return TapDeskImpl.GetInstance().Login(appId, sessionToken);
        }

        public static void AnonymousLogin(string uuid)
        {
            TapDeskImpl.GetInstance().AnonymousLogin(uuid);
        }

        public static void AnonymousLogin()
        {
            TapDeskImpl.GetInstance().AnonymousLogin();
        }

        public static void Resume()
        {
            TapDeskImpl.GetInstance().Resume();
        }

        public static void Pause()
        {
            TapDeskImpl.GetInstance().Pause();
        }

        public static Task<bool> FetchUnReadStatus()
        {
            return TapDeskImpl.GetInstance().FetchUnReadStatus();
        }

        public static void SetDefaultMetaData(Dictionary<string, object> metaData)
        {
            TapDeskImpl.GetInstance().SetDefaultMetaData(metaData);
        }

        public static void SetDefaultFieldsData(Dictionary<string, object> fieldsData)
        {
            TapDeskImpl.GetInstance().SetDefaultFieldsData(fieldsData);
        }

        public static string GetDeskWebUrl()
        {
            return TapDeskImpl.GetInstance().GetDeskWebUrl();
        }

        public static string GetDeskWebUrl(string path)
        {
            return TapDeskImpl.GetInstance().GetDeskWebUrl(path);
        }

        public static string GetDeskWebUrl(string path, Dictionary<string, object> metaData,
            Dictionary<string, object> fieldsData)
        {
            return TapDeskImpl.GetInstance().GetDeskWebUrl(path, metaData, fieldsData);
        }
    }
}