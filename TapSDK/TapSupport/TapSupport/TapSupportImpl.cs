using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace TapTap.Support
{
    public class TapSupportImpl : ITapSupport
    {
        private static readonly ITapSupport TapSupport = new TapSupportImpl();

        public static TapSupportImpl GetInstance() => (TapSupportImpl) TapSupport;

        private string _serverUrl;

        private string _rootCategoryID;

        private TapSupportCallback _callback;

        private string _anonymousId;

        private string _sessionToken;

        private Dictionary<string, object> _metaData = new Dictionary<string, object>();

        private Dictionary<string, object> _fieldsData = new Dictionary<string, object>();

        private Timer _timer;

        private bool _firstReadStatus = true;

        private bool _cacheHasReadStatus;

        private const long MINInterval = 10000L;

        private const long MAXInterval = 3000000L;

        private long _interval = MINInterval;

        public void Init(string serverUrl, string rootCategoryID, TapSupportCallback callback)
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
            _callback = new TapSupportCallback
            {
                UnReadStatusChanged = (hasRead, exception) =>
                {
                    if (_firstReadStatus)
                    {
                        _firstReadStatus = false;
                        _cacheHasReadStatus = hasRead;
                        callback?.UnReadStatusChanged(_cacheHasReadStatus, exception);
                        return;
                    }

                    if (_cacheHasReadStatus == hasRead)
                    {
                        return;
                    }

                    _cacheHasReadStatus = hasRead;
                    callback?.UnReadStatusChanged(_cacheHasReadStatus, exception);
                }
            };

            TapSupportHttpClient.GetInstance().Init(serverUrl);
        }

        public async Task Login(string appId, string sessionToken)
        {
            var requestParams = new Dictionary<string, string>
            {
                ["platform"] = $"TDS#{appId}",
                ["authData"] = sessionToken
            };

            var response = await TapSupportHttpClient.GetInstance()
                .Post(TapSupportApiConstants.Login_URL, null, requestParams, null);
            if (!(Json.Deserialize(response) is Dictionary<string, string> responseDictionary))
                throw new TapSupportException(-1, "Login Failed!");
            _sessionToken = responseDictionary["sessionToken"];
        }

        public void AnonymousLogin(string uuid)
        {
            if (string.IsNullOrEmpty(uuid))
            {
                AnonymousLogin();
                return;
            }

            _sessionToken = null;
            _anonymousId = uuid;
            TapSupportPersistence.Save(uuid);
            Pause();
        }

        public void AnonymousLogin()
        {
            _sessionToken = null;
            if (string.IsNullOrEmpty(_anonymousId))
            {
                _anonymousId = TapSupportPersistence.GetUuid();
            }

            Pause();
        }

        public void Resume()
        {
            _interval = MINInterval;
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

        public string GetSupportWebUrl()
        {
            return GetSupportWebUrl(null);
        }

        public string GetSupportWebUrl(string path)
        {
            return GetSupportWebUrl(path, null, null);
        }

        public string GetSupportWebUrl(string path, Dictionary<string, object> metaData,
            Dictionary<string, object> fieldsData)
        {
            if (string.IsNullOrEmpty(path))
            {
                path = TapSupportConstants.PathHome;
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
                _callback?.UnReadStatusChanged(false, new TapSupportException(-1, "ServerUrl is null!"));
            }

            try
            {
                var hasUnRead = await FetchUnReadStatus();

                HandlerIntervalTime(hasUnRead);

                _callback?.UnReadStatusChanged(hasUnRead, null);

                Debug.Log($"FetchUnReadStatus:{_interval}  {hasUnRead}\\n");
                Debug.Log($"Time:{DateTime.Now.ToString(CultureInfo.InvariantCulture)}\\n");
            }
            catch (Exception e)
            {
                HandlerIntervalTime(false);

                if (e is TapSupportException exception)
                {
                    _callback?.UnReadStatusChanged(false, exception);
                }
                else
                {
                    _callback?.UnReadStatusChanged(false, new TapSupportException(-1, e.Message));
                }
            }
        }


        private void HandlerIntervalTime(bool hasUnRead)
        {
            if (hasUnRead)
            {
                _interval = MINInterval;
            }
            else if (_interval < MAXInterval)
            {
                _interval += MINInterval;
            }
            else
            {
                _interval = MAXInterval;
            }

            _timer.Change(_interval, _interval);
        }


        public async Task<bool> FetchUnReadStatus()
        {
            var response = await TapSupportHttpClient.GetInstance()
                .Get(TapSupportApiConstants.UnRead_URL, ConstructorHeaders(), null);
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

            throw new TapSupportException(-1, "Login First");
        }

        private static string ConstructorExpandData(ICollection data)
        {
            if (data == null || data.Count <= 0)
            {
                return "";
            }
            return UnityWebRequest.EscapeURL(Json.Serialize(data));
        }
    }
}