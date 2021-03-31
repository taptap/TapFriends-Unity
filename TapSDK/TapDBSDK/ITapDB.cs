namespace TapDBSDK
{
    public interface ITapDB
    {
        void Init(string clientId, string channel, string gameVersion, bool isCN);

        void SetUser(string userId);

        void SetUser(string userId, string loginType);

        void SetName(string name);

        void SetLevel(int level);

        void SetServer(string server);

        void OnCharge(string orderId, string product, long amount, string currencyType, string payment);

        void OnEvent(string eventCode, string properties);

        void Track(string eventName, string properties);

        void RegisterStaticProperties(string properties);

        void UnregisterStaticProperty(string propertKey);

        void ClearStaticProperties();

        void DeviceInitialize(string properties);

        void DeviceUpdate(string properties);

        void DeviceAdd(string properties);

        void UserInitialize(string properties);

        void UserUpdate(string properties);

        void UserAdd(string properties);

        void EnableLog(bool enable);

        void ClearUser();
    }
}