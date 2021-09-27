using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TapTap.Support
{
    public interface ITapSupport
    {
        void Init(string serverUrl, string rootCategoryID, TapSupportCallback callback);

        Task Login(string appId, string sessionToken);

        void AnonymousLogin(string uuid);

        void AnonymousLogin();

        void Resume();

        void Pause();

        Task<bool> FetchUnReadStatus();

        void SetDefaultMetaData(Dictionary<string, object> metaData);

        void SetDefaultFieldsData(Dictionary<string, object> fieldsData);

        string GetAnonymousId();

        string GetSupportWebUrl();

        string GetSupportWebUrl(string path);

        string GetSupportWebUrl(string path, Dictionary<string, object> metaData,
            Dictionary<string, object> fieldsData);
    }

    public class TapSupportConstants
    {
        public static readonly string PathHome = "/";
        public static readonly string PathCategory = "/categories/";
        public static readonly string PathTicketHistory = "/tickets";
        public static readonly string PathTicketNew = "/tickets/new?category_id=";
    }

    internal class TapSupportApiConstants
    {
        public static readonly string Login_URL = "api/2/users";
        public static readonly string UnRead_URL = "api/2/unread";
    }

    public class TapSupportCallback
    {
        public Action<bool, TapSupportException> UnReadStatusChanged { get; set; }
    }
}