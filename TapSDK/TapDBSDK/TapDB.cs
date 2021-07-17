using System;

namespace TapTap.TapDB
{
    public class TapDB
    {
        public static void Init(string clientId, string channel, string gameVersion, bool isCN)
        {
            TapDBImpl.GetInstance().Init(clientId, channel, gameVersion, isCN);
        }

        public static void SetUser(string userId)
        {
            TapDBImpl.GetInstance().SetUser(userId);
        }

        public static void SetUser(string userId, string loginType)
        {
            TapDBImpl.GetInstance().SetUser(userId, loginType);
        }

        public static void SetName(string name)
        {
            TapDBImpl.GetInstance().SetName(name);
        }

        public static void SetLevel(int level)
        {
            TapDBImpl.GetInstance().SetLevel(level);
        }

        public static void SetServer(string server)
        {
            TapDBImpl.GetInstance().SetServer(server);
        }

        public static void OnCharge(string orderId, string product, long amount, string currencyType, string payment)
        {
            TapDBImpl.GetInstance().OnCharge(orderId, product, amount, currencyType, payment);
        }

        public static void OnCharge(string orderId, string product, long amount, string currencyType, string payment,
            string properties)
        {
            TapDBImpl.GetInstance().OnCharge(orderId, product, amount, currencyType, payment, properties);
        }

        [Obsolete("已弃用,请调用trackEvent(string eventName, Dictionary<string, object> properties)")]
        public static void OnEvent(string eventCode, string properties)
        {
            TapDBImpl.GetInstance().OnEvent(eventCode, properties);
        }

        public static void TrackEvent(string eventName, string properties)
        {
            TapDBImpl.GetInstance().TrackEvent(eventName, properties);
        }

        public static void RegisterStaticProperties(string properties)
        {
            TapDBImpl.GetInstance().RegisterStaticProperties(properties);
        }

        public static void UnregisterStaticProperty(string propertKey)
        {
            TapDBImpl.GetInstance().UnregisterStaticProperty(propertKey);
        }

        public static void RegisterDynamicProperties(IDynamicProperties properties)
        {
            TapDBImpl.GetInstance().RegisterDynamicProperties(properties);
        }

        public static void ClearStaticProperties()
        {
            TapDBImpl.GetInstance().ClearStaticProperties();
        }

        public static void DeviceInitialize(string properties)
        {
            TapDBImpl.GetInstance().DeviceInitialize(properties);
        }

        public static void DeviceUpdate(string properties)
        {
            TapDBImpl.GetInstance().DeviceUpdate(properties);
        }

        public static void DeviceAdd(string properties)
        {
            TapDBImpl.GetInstance().DeviceAdd(properties);
        }

        public static void UserInitialize(string properties)
        {
            TapDBImpl.GetInstance().UserInitialize(properties);
        }

        public static void UserUpdate(string properties)
        {
            TapDBImpl.GetInstance().UserUpdate(properties);
        }

        public static void UserAdd(string properties)
        {
            TapDBImpl.GetInstance().UserAdd(properties);
        }

        public static void EnableLog(bool enable)
        {
            TapDBImpl.GetInstance().EnableLog(enable);
        }

        public static void AdvertiserIDCollectionEnabled(bool enable)
        {
            TapDBImpl.GetInstance().AdvertiserIDCollectionEnabled(enable);
        }

        public static void CloseFetchTapTapDeviceId()
        {
            TapDBImpl.GetInstance().CloseFetchTapTapDeviceId();
        }

        public static void ClearUser()
        {
            TapDBImpl.GetInstance().ClearUser();
        }

        public static void GetTapTapDid(Action<string> action)
        {
            TapDBImpl.GetInstance().GetTapTapDid(action);
        }
    }
}