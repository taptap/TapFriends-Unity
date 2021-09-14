using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TapTap.Desk
{
    public class TapDeskImpl : ITapDesk, IRuntimeHeaderInterceptor
    {
        private static readonly ITapDesk TapDesk = new TapDeskImpl();

        public static TapDeskImpl GetInstance() => (TapDeskImpl) TapDesk;

        private string _serverUrl;

        private string _rootCategoryID;

        private TapDeskCallback _callback;

        private string _anonymousId;

        private string _sessionToken;

        private Dictionary<string, object> _metaData = new Dictionary<string, object>();

        private Dictionary<string, object> _fieldsData = new Dictionary<string, object>();

        public string SessionToken()
        {
            return _anonymousId;
        }

        public string AnonymousId()
        {
            return _anonymousId;
        }

        public void Init(string serverUrl, string rootCategoryID, TapDeskCallback callback)
        {
            if (serverUrl.EndsWith("/"))
            {
                _serverUrl = serverUrl;
            }
            else
            {
                _serverUrl = serverUrl + "/";
            }

            _rootCategoryID = rootCategoryID;
            _callback = callback;

            TapDeskHttpClient.GetInstance().Init(serverUrl);
            TapDeskHttpClient.GetInstance().AddAddtionalHeader("X-Anonymous-ID", AnonymousId());
            TapDeskHttpClient.GetInstance().AddAddtionalHeader("X-LC-Session", SessionToken());
        }

        public async Task Login(string appId, string sessionToken)
        {
            var requestParams = new Dictionary<string, string>
            {
                ["platform"] = $"TDS#{appId}",
                ["authData"] = sessionToken
            };

            var response = await TapDeskHttpClient.GetInstance()
                .Post(TapDeskApiConstants.Login_URL, null, requestParams, null);
            if (Json.Deserialize(response) is Dictionary<string, string> responseDictionary)
            {
                _sessionToken = responseDictionary["sessionToken"];
            }

            throw new TapDeskException(-1, "Login Failed!");
        }

        public void AnonymousLogin(string uuid)
        {
            _sessionToken = null;
            _anonymousId = uuid;
            TapDeskPersistence.Save(uuid);
        }

        public void AnonymousLogin()
        {
            _sessionToken = null;
            if (string.IsNullOrEmpty(_anonymousId))
            {
                _anonymousId = TapDeskPersistence.GetUuid();
            }
        }

        public void Resume()
        {
            throw new System.NotImplementedException();
        }

        public void Pause()
        {
            throw new System.NotImplementedException();
        }

        public void SetMetaData(Dictionary<string, object> metaData)
        {
            if (metaData == null)
            {
                return;
            }

            _metaData = metaData;
        }

        public void SetFieldsData(Dictionary<string, object> fieldsData)
        {
            if (fieldsData == null)
            {
                return;
            }

            _fieldsData = fieldsData;
        }

        public string GetDeskWebUrl()
        {
            return GetDeskWebUrl(null);
        }

        public string GetDeskWebUrl(string path)
        {
            return GetDeskWebUrl(path, null, null);
        }

        public string GetDeskWebUrl(string path, Dictionary<string, object> metaData,
            Dictionary<string, object> fieldsData)
        {
            if (string.IsNullOrEmpty(path))
            {
                path = TapDeskConstants.PathHome;
            }
            return "";
        }
    }

    internal interface IRuntimeHeaderInterceptor
    {
        string SessionToken();

        string AnonymousId();
    }
}