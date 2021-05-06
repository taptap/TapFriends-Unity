using System;
using System.Collections.Generic;

namespace TapTap.Common
{
    public class TapCommonImpl : ITapCommon
    {
        private static readonly TapCommonImpl SInstance = new TapCommonImpl();

        private static readonly string TAP_COMMON_SERVICE = "TDSCommonService";

        public static TapCommonImpl GetInstance()
        {
            return SInstance;
        }

        private TapCommonImpl()
        {
            EngineBridge.GetInstance().Register("com.tds.common.wrapper.TDSCommonService",
                "com.tds.common.wrapper.TDSCommonServiceImpl");
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

        public void OpenReviewInTapTapGlobal(string appId, Action<bool> callback)
        {
            var command = new Command.Builder()
                .Service(TAP_COMMON_SERVICE)
                .Method("openReviewInTapTapGlobal")
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
                callback(SafeDictionary.GetValue<bool>(dlc, "openReviewInTapTapGlobal"));
            });
        }
    }
}