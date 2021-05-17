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

        public void SetLicencesCallback(ITapLicenseCallback callback)
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
                if (success.Equals("success"))
                {
                    callback.OnLicenseSuccess();
                }
            });
        }

        public void SetDLCCallback(ITapDlcCallback callback)
        {
            SetDLCCallbackInner(callback, false, null, false);
        }

        public void SetDLCCallback(ITapDlcCallback callback, bool checkOnce, string publicKey)
        {
            SetDLCCallbackInner(callback, checkOnce, publicKey, true);
        }

        private void SetDLCCallbackInner(ITapDlcCallback callback, bool checkOnce, string publicKey, bool isParams)
        {
            var command = new Command.Builder();
            command.Service(TapLicenseConstants.TAP_LICENSE_SERVICE);
            if (isParams)
            {
                command.Method("setDLCCallbackWithParams")
                    .Callback(true)
                    .Args("checkOnce", checkOnce)
                    .Args("publicKey", publicKey);
            }
            else
            {
                command.Method("setDLCCallback")
                    .Callback(true);
            }
            
            EngineBridge.GetInstance().CallHandler(command.CommandBuilder(), (result) =>
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
                var dlc = SafeDictionary.GetValue<string>(dic, "orderDLC");
                if (!string.IsNullOrEmpty(dlc))
                {
                    var statusCode = SafeDictionary.GetValue<int>(dic, "orderStatus");
                    callback.OnOrderCallBack(dlc, HandlePurchasedCode(statusCode));
                    return;
                }
                var code = SafeDictionary.GetValue<int >(dic, "queryCode");
                var queryListJson = SafeDictionary.GetValue<string>(dic, "queryResult");
                var queryListDic = Json.Deserialize(queryListJson) as Dictionary<string, object>;
                callback.OnQueryCallBack(HandleQueryCode(code), queryListDic);
            });
        }

        private static TapLicenseQueryCode HandleQueryCode(int code)
        {
            switch (code)
            {
                case 0:
                    return TapLicenseQueryCode.QUERY_RESULT_OK;
                case 1:
                    return TapLicenseQueryCode.QUERY_RESULT_NOT_INSTALL_TAPTAP;
                case 2:
                    return TapLicenseQueryCode.QUERY_RESULT_ERR;
                default:
                    return TapLicenseQueryCode.ERROR_CODE_UNDEFINED;
            }
        }
        
        private static TapLicensePurchasedCode HandlePurchasedCode(int code)
        {
            switch (code)
            {
                case 0:
                    return TapLicensePurchasedCode.DLC_NOT_PURCHASED;
                case 1:
                    return TapLicensePurchasedCode.DLC_PURCHASED;
                case -1:
                    return TapLicensePurchasedCode.DLC_RETURN_ERROR;
                default:
                    return TapLicensePurchasedCode.ERROR_CODE_UNDEFINED;
            }
        }

        public void Check()
        {
            var command = new Command.Builder()
                .Service(TapLicenseConstants.TAP_LICENSE_SERVICE)
                .Method("check")
                .CommandBuilder();
            EngineBridge.GetInstance().CallHandler(command);
        }

        public void QueryDLC(string[] skus)
        {
            var command = new Command.Builder()
                .Service(TapLicenseConstants.TAP_LICENSE_SERVICE)
                .Method("queryDLC")
                .Args("dlcList", skus)
                .CommandBuilder();
            EngineBridge.GetInstance().CallHandler(command);
        }

        public void PurchaseDLC(string sku)
        {
            var command = new Command.Builder()
                .Service(TapLicenseConstants.TAP_LICENSE_SERVICE)
                .Method("purchaseDLC")
                .Args("dlc", sku)
                .CommandBuilder();
            EngineBridge.GetInstance().CallHandler(command);
        }
    }
}















































