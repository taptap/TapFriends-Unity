using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace TapTap.Common
{
    public class TapCommonImpl : ITapCommon
    {
        private static readonly string TAP_COMMON_SERVICE = "TDSCommonService";
        
        private readonly AndroidJavaObject _proxyServiceImpl;
        
        private TapCommonImpl()
        {
            EngineBridge.GetInstance().Register("com.tds.common.wrapper.TDSCommonService",
                "com.tds.common.wrapper.TDSCommonServiceImpl");

            _proxyServiceImpl = new AndroidJavaObject("com.tds.common.wrapper.TDSCommonServiceImpl");
        }

        private static volatile TapCommonImpl _sInstance;

        private static readonly object Locker = new object();

        public static TapCommonImpl GetInstance()
        {
            lock (Locker)
            {
                if (_sInstance == null)
                {
                    _sInstance = new TapCommonImpl();
                }
            }

            return _sInstance;
        }

        public void SetXua()
        {
            try
            {
                var xua = new Dictionary<string, string>
                {
                    {Constants.VersionKey, Assembly.GetExecutingAssembly().GetName().Version.ToString()},
                    {Constants.PlatformKey, "Unity"}
                };

                var command = new Command.Builder()
                    .Service(TAP_COMMON_SERVICE)
                    .Method("setXUA")
                    .Args("setXUA", Json.Serialize(xua)).CommandBuilder();

                EngineBridge.GetInstance().CallHandler(command);
            }
            catch (Exception e)
            {
                Debug.Log($"exception:{e}");
            }
        }

        public void SetLanguage(string language)
        {
            var dic = new Dictionary<string, object> {{"language", language}};
            var command = new Command(TAP_COMMON_SERVICE, "setLanguage", false, dic);
            EngineBridge.GetInstance().CallHandler(command);
        }

        public void GetRegionCode(Action<bool> callback)
        {
            var command = new Command.Builder()
                .Service(TAP_COMMON_SERVICE)
                .Method("getRegionCode").Callback(true)
                .OnceTime(true)
                .CommandBuilder();
            EngineBridge.GetInstance().CallHandler(command, (result) =>
            {
                if (result.code != Result.RESULT_SUCCESS)
                {
                    return;
                }

                if (string.IsNullOrEmpty(result.content))
                {
                    return;
                }

                var wrapper = new CommonRegionWrapper(result.content);
                callback(wrapper.isMainland);
            });
        }

        public void IsTapTapInstalled(Action<bool> callback)
        {
            var command = new Command.Builder()
                .Service(TAP_COMMON_SERVICE)
                .Method("isTapTapInstalled")
                .Callback(true)
                .OnceTime(true)
                .CommandBuilder();

            EngineBridge.GetInstance().CallHandler(command, (result) =>
            {
                if (result.code != Result.RESULT_SUCCESS)
                {
                    callback(false);
                    return;
                }

                if (string.IsNullOrEmpty(result.content))
                {
                    callback(false);
                    return;
                }

                var dlc = Json.Deserialize(result.content) as Dictionary<string, object>;
                callback(SafeDictionary.GetValue<bool>(dlc, "isTapTapInstalled"));
            });
        }

        public void IsTapTapGlobalInstalled(Action<bool> callback)
        {
            var command = new Command.Builder()
                .Service(TAP_COMMON_SERVICE)
                .Method("isTapGlobalInstalled")
                .Callback(true)
                .OnceTime(true)
                .CommandBuilder();

            EngineBridge.GetInstance().CallHandler(command, (result) =>
            {
                if (result.code != Result.RESULT_SUCCESS)
                {
                    callback(false);
                    return;
                }

                if (string.IsNullOrEmpty(result.content))
                {
                    callback(false);
                    return;
                }

                var dlc = Json.Deserialize(result.content) as Dictionary<string, object>;
                callback(SafeDictionary.GetValue<bool>(dlc, "isTapGlobalInstalled"));
            });
        }

        public void UpdateGameInTapTap(string appId, Action<bool> callback)
        {
            var command = new Command.Builder()
                .Service(TAP_COMMON_SERVICE)
                .Method("updateGameInTapTap")
                .Args("appId", appId)
                .Callback(true)
                .OnceTime(true)
                .CommandBuilder();

            EngineBridge.GetInstance().CallHandler(command, (result) =>
            {
                if (result.code != Result.RESULT_SUCCESS)
                {
                    callback(false);
                    return;
                }

                if (string.IsNullOrEmpty(result.content))
                {
                    callback(false);
                    return;
                }

                var dlc = Json.Deserialize(result.content) as Dictionary<string, object>;
                callback(SafeDictionary.GetValue<bool>(dlc, "updateGameInTapTap"));
            });
        }

        public void UpdateGameInTapGlobal(string appId, Action<bool> callback)
        {
            var command = new Command.Builder()
                .Service(TAP_COMMON_SERVICE)
                .Method("updateGameInTapGlobal")
                .Args("appId", appId)
                .Callback(true)
                .OnceTime(true)
                .CommandBuilder();

            EngineBridge.GetInstance().CallHandler(command, result =>
            {
                if (result.code != Result.RESULT_SUCCESS)
                {
                    callback(false);
                    return;
                }

                if (string.IsNullOrEmpty(result.content))
                {
                    callback(false);
                    return;
                }

                var dlc = Json.Deserialize(result.content) as Dictionary<string, object>;
                callback(SafeDictionary.GetValue<bool>(dlc, "updateGameInTapGlobal"));
            });
        }

        public void OpenReviewInTapTap(string appId, Action<bool> callback)
        {
            var command = new Command.Builder()
                .Service(TAP_COMMON_SERVICE)
                .Method("openReviewInTapTap")
                .Args("appId", appId)
                .Callback(true)
                .OnceTime(true)
                .CommandBuilder();

            EngineBridge.GetInstance().CallHandler(command, result =>
            {
                if (result.code != Result.RESULT_SUCCESS)
                {
                    callback(false);
                    return;
                }

                if (string.IsNullOrEmpty(result.content))
                {
                    callback(false);
                    return;
                }

                var dlc = Json.Deserialize(result.content) as Dictionary<string, object>;
                callback(SafeDictionary.GetValue<bool>(dlc, "openReviewInTapTap"));
            });
        }

        public void OpenReviewInTapGlobal(string appId, Action<bool> callback)
        {
            var command = new Command.Builder()
                .Service(TAP_COMMON_SERVICE)
                .Method("openReviewInTapGlobal")
                .Args("appId", appId)
                .Callback(true)
                .OnceTime(true)
                .CommandBuilder();

            EngineBridge.GetInstance().CallHandler(command, result =>
            {
                if (result.code != Result.RESULT_SUCCESS)
                {
                    callback(false);
                    return;
                }

                if (string.IsNullOrEmpty(result.content))
                {
                    callback(false);
                    return;
                }

                var dlc = Json.Deserialize(result.content) as Dictionary<string, object>;
                callback(SafeDictionary.GetValue<bool>(dlc, "openReviewInTapGlobal"));
            });
        }

        public void SetLanguage(TapLanguage language)
        {
            var command = new Command.Builder()
                .Service(TAP_COMMON_SERVICE)
                .Method("setPreferredLanguage")
                .Args("preferredLanguage", (int) language)
                .CommandBuilder();

            EngineBridge.GetInstance().CallHandler(command);
        }

        public void ConsumptionProperties(string key, ITapPropertiesProxy proxy)
        {
            if (Platform.IsAndroid())
            {
                Debug.Log($"invoke Properties:{key} value:{proxy.GetProperties()}");
                _proxyServiceImpl?.Call("consumptionProperties", key, new TapPropertiesProxy(proxy));
            }
        }


        public class TapPropertiesProxy : AndroidJavaProxy
        {
            private readonly ITapPropertiesProxy _properties;

            public TapPropertiesProxy(ITapPropertiesProxy properties) :
                base(new AndroidJavaClass("com.tds.common.wrapper.TapPropertiesProxy"))
            {
                _properties = properties;
            }

            public override AndroidJavaObject Invoke(string methodName, object[] args)
            {
                return _properties != null
                    ? new AndroidJavaObject("java.lang.String", Json.Serialize(_properties.GetProperties()))
                    : base.Invoke(methodName, args);
            }
        }
    }
}