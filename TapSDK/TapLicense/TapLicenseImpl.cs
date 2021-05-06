using System;
using System.Collections.Generic;
using TapTap.Common;
using UnityEngine;

namespace TapTap.License
{
    public class TapLicenseImpl: ITapLicense
    {
        private TapLicenseImpl()
        {
            EngineBridge.GetInstance().Register(TapLicenseConstants.TDS_LICENSE_SERVICE_CLZ, TapLicenseConstants.TDS_LICENSE_SERVICE_IMPL);
        }
        private static volatile TapLicenseImpl _sInstance;
        
        private static readonly object Locker = new object();

        public static TapLicenseImpl GetInstance()
        {
            lock (Locker)
            {
                if (_sInstance == null)
                {
                    _sInstance = new TapLicenseImpl();
                }
            }
            return _sInstance;
        }

        public void SetLicencesCallback(Action<bool> action)
        {
            var command = new Command.Builder()
                .Service(TapLicenseConstants.TAP_LICENSE_SERVICE)
                .Method("setLicenseCallback")
                .Callback(true)
                .CommandBuilder();
            EngineBridge.GetInstance().CallHandler(command, (result) =>
            {
                Debug.Log("result: " + result.ToJSON());
                if (result.code != Result.RESULT_SUCCESS)
                {
                    return;
                }

                if (string.IsNullOrEmpty(result.content))
                {
                    return;
                }
                var dic = Json.Deserialize(result.content) as Dictionary<string, object>;
                var success = SafeDictionary.GetValue<string>(dic, "login") as string;
                action(success.Equals("success"));
            });
        }

        public void SetDlcCallback(ITapDlcCallback callback)
        {
            SetDlcCallback(callback, false, null);
        }
        
        public void SetDlcCallback(ITapDlcCallback callback, bool checkOnce, string publicKey)
        {
            var command = new Command.Builder()
                .Service(TapLicenseConstants.TAP_LICENSE_SERVICE)
                .Method("setDLCCallbackWithParams")
                .Callback(true)
                .Args("checkOnce", checkOnce)
                .Args("publicKey", publicKey)
                .CommandBuilder();
            EngineBridge.GetInstance().CallHandler(command, (result) =>
            {
                Debug.Log("result: " + result.ToJSON());
                if (result.code != Result.RESULT_SUCCESS)
                {
                    return;
                }

                if (string.IsNullOrEmpty(result.content))
                {
                    return;
                }
                var dic = Json.Deserialize(result.content) as Dictionary<string, object>;
                var dlc = SafeDictionary.GetValue<string>(dic, "orderDLC") as string;
                if (!string.IsNullOrEmpty(dlc))
                {
                    int statusCode = SafeDictionary.GetValue<int>(dic, "orderStatus");
                    callback.OnOrderCallBack(dlc, statusCode);
                    return;
                }
                var code = SafeDictionary.GetValue<int >(dic, "queryCode");
                var queryListJson = SafeDictionary.GetValue<string>(dic, "queryResult");
                var queryListDic = Json.Deserialize(queryListJson) as Dictionary<string, object>;
                callback.OnQueryCallBack(code, queryListDic);
            });
        }

        public void Check()
        {
            var command = new Command.Builder()
                .Service(TapLicenseConstants.TAP_LICENSE_SERVICE)
                .Method("check")
                .CommandBuilder();
            EngineBridge.GetInstance().CallHandler(command);
        }

        public void QueryDlc(string[] dlcList)
        {
            var command = new Command.Builder()
                .Service(TapLicenseConstants.TAP_LICENSE_SERVICE)
                .Method("queryDLC")
                .Args("dlcList", dlcList)
                .CommandBuilder();
            EngineBridge.GetInstance().CallHandler(command);
        }

        public void PurchaseDlc(string dlc)
        {
            var command = new Command.Builder()
                .Service(TapLicenseConstants.TAP_LICENSE_SERVICE)
                .Method("purchaseDLC")
                .CommandBuilder();
            EngineBridge.GetInstance().CallHandler(command);
        }
    }
}















































