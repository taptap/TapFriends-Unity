using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using UnityEngine;

namespace TapTap.Desk
{
    public class TapDeskImpl : ITapDesk
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

        private Timer _timer;


        private const long MINInterval = 5000L;

        private const long MAXInterval = 3000000L;

        private long _interval = MINInterval;

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

            _rootCategoryID = string.IsNullOrEmpty(rootCategoryID) ? "-" : rootCategoryID;
            _callback = callback;

            TapDeskHttpClient.GetInstance().Init(serverUrl);
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
            if (_timer == null)
            {
                _timer = new Timer(FetchUnReadStatus, null, 0, _interval);
            }
            else
            {
                _timer.Change(_interval, _interval);
            }
        }

        public void Pause()
        {
            _interval = MINInterval;
            _timer?.Change(-1, -1);
        }

        public void SetDefaultMetaData(Dictionary<string, object> metaData)
        {
            if (metaData == null)
            {
                return;
            }

            _metaData = metaData;
        }

        public void SetDefaultFieldsData(Dictionary<string, object> fieldsData)
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

            if (metaData == null || metaData.Count <= 0)
            {
                metaData = _metaData;
            }

            if (fieldsData == null || fieldsData.Count <= 0)
            {
                fieldsData = _fieldsData;
            }

            var metaStr = ConstructorExpandData(metaData);
            var fieldStr = ConstructorExpandData(fieldsData);
            var url = $"{_serverUrl}in-app/v1/categories/{_rootCategoryID}{path}";
            url = string.IsNullOrEmpty(_sessionToken)
                ? $"{url}#anonymous-id={_anonymousId}"
                : $"{url}#token={_sessionToken}";

            if (!string.IsNullOrEmpty(metaStr))
            {
                url = $"{url}&meta={metaStr}";
            }

            if (!string.IsNullOrEmpty(fieldStr))
            {
                url = $"{url}&fields={fieldStr}";
            }

            return url;
        }

        private async void FetchUnReadStatus(object state)
        {
            if (string.IsNullOrEmpty(_serverUrl))
            {
                _callback?.UnReadStatusChanged(false, new TapDeskException(-1, "ServerUrl is null!"));
            }

            try
            {
                var hasUnRead = await FetchUnReadStatus();

                if (hasUnRead)
                {
                    _interval = MINInterval;
                }
                else if (_interval < MAXInterval)
                {
                    _interval += _interval;
                }
                else
                {
                    _interval = MAXInterval;
                }

                _timer.Change(_interval, _interval);
                
                Debug.Log($"FetchUnReadStatus:{_interval}  {hasUnRead}\\n");
                Debug.Log($"Time:{DateTime.Now.ToString(CultureInfo.InvariantCulture)}\\n");
            }
            catch (Exception e)
            {
                if (e is TapDeskException exception)
                {
                    _callback?.UnReadStatusChanged(false, exception);
                }
                else
                {
                    _callback?.UnReadStatusChanged(false, new TapDeskException(-1, e.Message));
                }

                Debug.Log(e);
            }
        }

        public async Task<bool> FetchUnReadStatus()
        {
            var response = await TapDeskHttpClient.GetInstance()
                .Get(TapDeskApiConstants.UnRead_URL, ConstructorHeaders(), null);
            return response.Equals("true");
        }

        private Dictionary<string, object> ConstructorHeaders()
        {
            if (!string.IsNullOrEmpty(_sessionToken))
            {
                return new Dictionary<string, object>
                {
                    ["X-LC-Session"] = _sessionToken
                };
            }

            if (!string.IsNullOrEmpty(_anonymousId))
            {
                return new Dictionary<string, object>
                {
                    ["X-Anonymous-ID"] = _anonymousId
                };
            }

            throw new TapDeskException(-1, "Login First");
        }

        private static string ConstructorExpandData(ICollection data)
        {
            if (data == null || data.Count <= 0)
            {
                return "";
            }

            return HttpUtility.UrlDecode(Json.Serialize(data));
        }
    }
}