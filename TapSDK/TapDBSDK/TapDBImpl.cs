using System;
using System.Collections.Generic;
#if UNITY_IOS
using System.Runtime.InteropServices;
#endif
using TapTap.Common;
using UnityEngine;

namespace TapTap.TapDB
{
    public class TapDBImpl : ITapDB
    {
        private TapDBImpl()
        {
            EngineBridge.GetInstance().Register(TapDBConstants.TAPDB_CLZ, TapDBConstants.TAPDB_IMPL);

            _dbServiceImpl = new AndroidJavaObject(TapDBConstants.TAPDB_IMPL);
        }

        private static volatile TapDBImpl _sInstance;

        private static readonly object Locker = new object();

        private IDynamicProperties _dynamicProperties;

        private readonly AndroidJavaObject _dbServiceImpl;

        public static TapDBImpl GetInstance()
        {
            lock (Locker)
            {
                if (_sInstance == null)
                {
                    _sInstance = new TapDBImpl();
                }
            }

            return _sInstance;
        }

        public void Init(string clientId, string channel, string gameVersion, bool isCN)
        {
            var command = new Command.Builder()
                .Service(TapDBConstants.TAPDB_SERVICE)
                .Method("init")
                .Args("clientId", clientId)
                .Args("channel", channel)
                .Args("isCN", isCN)
                .Args("gameVersion", gameVersion).CommandBuilder();
            EngineBridge.GetInstance().CallHandler(command);
        }

        public void RegisterStaticProperties(string superProperties)
        {
            var command = new Command.Builder()
                .Service(TapDBConstants.TAPDB_SERVICE)
                .Method("registerStaticProperties")
                .Args("registerStaticProperties", superProperties)
                .CommandBuilder();

            EngineBridge.GetInstance().CallHandler(command);
        }

        public void UnregisterStaticProperty(string propertyName)
        {
            var dic = new Dictionary<string, object> {{"unregisterStaticProperty", propertyName}};
            var command = new Command(TapDBConstants.TAPDB_SERVICE, "unregisterStaticProperty", false, dic);
            EngineBridge.GetInstance().CallHandler(command);
        }

        public void ClearStaticProperties()
        {
            var command = new Command(TapDBConstants.TAPDB_SERVICE, "clearStaticProperties", false, null);
            EngineBridge.GetInstance().CallHandler(command);
        }

        public void ClearUser()
        {
            var command = new Command(TapDBConstants.TAPDB_SERVICE, "clearUser", false, null);
            EngineBridge.GetInstance().CallHandler(command);
        }

        public void SetUser(string userId)
        {
            var dic = new Dictionary<string, object> {{"userId", userId}};
            var command = new Command(TapDBConstants.TAPDB_SERVICE, "setUser", false, dic);
            EngineBridge.GetInstance().CallHandler(command);
        }

        public void SetUser(string userId, string loginType)
        {
            var dic = new Dictionary<string, object> {{"userId", userId}, {"loginType", loginType}};
            var command = new Command(TapDBConstants.TAPDB_SERVICE, "setUserWithParams", false, dic);
            EngineBridge.GetInstance().CallHandler(command);
        }

        public void SetName(string name)
        {
            var dic = new Dictionary<string, object> {{"name", name}};
            var command = new Command(TapDBConstants.TAPDB_SERVICE, "setName", false, dic);
            EngineBridge.GetInstance().CallHandler(command);
        }

        public void SetLevel(int level)
        {
            var dic = new Dictionary<string, object> {{"level", level}};
            var command = new Command(TapDBConstants.TAPDB_SERVICE, "setLevel", false, dic);
            EngineBridge.GetInstance().CallHandler(command);
        }

        public void SetServer(string server)
        {
            var dic = new Dictionary<string, object> {{"server", server}};
            var command = new Command(TapDBConstants.TAPDB_SERVICE, "setServer", false, dic);
            EngineBridge.GetInstance().CallHandler(command);
        }

        public void OnCharge(string orderId, string product, long amount, string currencyType, string payment)
        {
            var dic = new Dictionary<string, object>
            {
                {"orderId", orderId},
                {"product", product},
                {"amount", amount},
                {"currencyType", currencyType},
                {"payment", payment}
            };
            var command = new Command(TapDBConstants.TAPDB_SERVICE, "onCharge", false, dic);
            EngineBridge.GetInstance().CallHandler(command);
        }

        public void OnCharge(string orderId, string product, long amount, string currencyType, string payment,
            string properties)
        {
            var dic = new Dictionary<string, object>
            {
                {"orderId", orderId},
                {"product", product},
                {"amount", amount},
                {"currencyType", currencyType},
                {"payment", payment},
                {"properties", properties}
            };
            var command = new Command(TapDBConstants.TAPDB_SERVICE, "onChargeWithProperties", false, dic);
            EngineBridge.GetInstance().CallHandler(command);
        }


        public void OnEvent(string eventCode, string properties)
        {
            var dic = new Dictionary<string, object>
            {
                {"eventCode", eventCode}, {"properties", properties}
            };
            var command = new Command(TapDBConstants.TAPDB_SERVICE, "onEvent", false, dic);
            EngineBridge.GetInstance().CallHandler(command);
        }

        public void TrackEvent(string eventName, string properties)
        {
            var dic = new Dictionary<string, object> {{"eventName", eventName}, {"properties", properties}};
            var command = new Command(TapDBConstants.TAPDB_SERVICE, "trackEvent", false, dic);
            EngineBridge.GetInstance().CallHandler(command);
        }

        public void DeviceInitialize(string properties)
        {
            EngineBridge.GetInstance().CallHandler(new Command.Builder()
                .Service(TapDBConstants.TAPDB_SERVICE)
                .Method("deviceInitialize")
                .Args("deviceInitialize", properties)
                .Callback(true)
                .CommandBuilder());
        }

        public void DeviceUpdate(string properties)
        {
            EngineBridge.GetInstance().CallHandler(new Command.Builder()
                .Service(TapDBConstants.TAPDB_SERVICE)
                .Method("deviceUpdate")
                .Args("deviceUpdate", properties)
                .CommandBuilder());
        }

        public void DeviceAdd(string properties)
        {
            EngineBridge.GetInstance().CallHandler(new Command.Builder()
                .Service(TapDBConstants.TAPDB_SERVICE)
                .Method("deviceAdd")
                .Args("deviceAdd", properties)
                .CommandBuilder());
        }

        public void UserInitialize(string properties)
        {
            EngineBridge.GetInstance().CallHandler(new Command.Builder()
                .Service(TapDBConstants.TAPDB_SERVICE)
                .Method("userInitialize")
                .Args("userInitialize", properties)
                .CommandBuilder());
        }

        public void UserUpdate(string properties)
        {
            EngineBridge.GetInstance().CallHandler(new Command.Builder()
                .Service(TapDBConstants.TAPDB_SERVICE)
                .Method("userUpdate")
                .Args("userUpdate", properties)
                .CommandBuilder());
        }

        public void UserAdd(string properties)
        {
            EngineBridge.GetInstance().CallHandler(new Command.Builder()
                .Service(TapDBConstants.TAPDB_SERVICE)
                .Method("userAdd")
                .Args("userAdd", properties)
                .CommandBuilder());
        }

        public void EnableLog(bool enable)
        {
            EngineBridge.GetInstance().CallHandler(new Command.Builder()
                .Service(TapDBConstants.TAPDB_SERVICE)
                .Method("enableLog")
                .Args("enableLog", enable)
                .CommandBuilder());
        }

        public void CloseFetchTapTapDeviceId()
        {
            if (!Platform.IsAndroid()) return;
            EngineBridge.GetInstance().CallHandler(new Command.Builder()
                .Service(TapDBConstants.TAPDB_SERVICE)
                .Method("closeFetchTapTapDeviceId")
                .CommandBuilder());
        }

        public void GetTapTapDid(Action<string> action)
        {
            if (!Platform.IsAndroid()) return;
            var command = new Command.Builder()
                .Service(TapDBConstants.TAPDB_SERVICE)
                .Method("getTapTapDID")
                .Callback(true)
                .OnceTime(true)
                .CommandBuilder();

            EngineBridge.GetInstance().CallHandler(command, result =>
            {
                if (result.code != Result.RESULT_SUCCESS)
                {
                    action(null);
                    return;
                }

                action(result.content);
            });
        }

        public void AdvertiserIDCollectionEnabled(bool enable)
        {
#if UNITY_IOS
            EngineBridge.GetInstance().CallHandler(new Command.Builder()
                .Service(TapDBConstants.TAPDB_SERVICE)
                .Method("advertiserIDCollectionEnabled")
                .Args("advertiserIDCollectionEnabled", enable)
                .CommandBuilder());
#endif
        }

        public void RegisterDynamicProperties(IDynamicProperties properties)
        {
            if (Platform.IsAndroid())
            {
                if (_dbServiceImpl == null)
                {
                    return;
                }

                _dbServiceImpl.Call("registerDynamicProperties", new TapDBDynamicProperties(properties));
            }
            else if (Platform.IsIOS())
            {
                _dynamicProperties = properties;
#if UNITY_IOS
                registerDynamicProperties(DynamicPropertiesDelegate);
#endif
                EngineBridge.GetInstance().CallHandler(new Command.Builder().Service(TapDBConstants.TAPDB_SERVICE)
                    .Method("registerDynamicProperties")
                    .CommandBuilder());
            }
        }

#if UNITY_IOS
        private delegate string TapDBDynamicPropertiesDelegate();

        [AOT.MonoPInvokeCallbackAttribute(typeof(TapDBDynamicPropertiesDelegate))]
        static string DynamicPropertiesDelegate()
        {
            return _sInstance._dynamicProperties == null
                ? null
                : Json.Serialize(_sInstance._dynamicProperties.GetDynamicProperties());
        }

        [DllImport("__Internal")]
        private static extern void registerDynamicProperties(TapDBDynamicPropertiesDelegate propertiesDelegate);
#endif
    }

    public class TapDBDynamicProperties : AndroidJavaProxy
    {
        private readonly IDynamicProperties _properties;

        public TapDBDynamicProperties(IDynamicProperties properties) :
            base(new AndroidJavaClass("com.tds.tapdb.wrapper.TapDBDynamicProperties"))
        {
            _properties = properties;
        }

        public override AndroidJavaObject Invoke(string methodName, object[] args)
        {
            return _properties != null
                ? new AndroidJavaObject("java.lang.String", Json.Serialize(_properties.GetDynamicProperties()))
                : base.Invoke(methodName, args);
        }
    }
}