using System.Collections.Generic;
using TapTap.Common;

namespace TapTap.Bootstrap
{
    public class TapConfig
    {
        public readonly string ClientID;

        public readonly string ClientToken;

        public readonly RegionType RegionType;

        public readonly string ServerURL;

        public readonly TapDBConfig DBConfig;

        private TapConfig(string clientID, string clientToken, RegionType regionType, string serverUrl)
        {
            ClientID = clientID;
            ClientToken = clientToken;
            RegionType = regionType;
            ServerURL = serverUrl;
        }

        private TapConfig(string clientID, string clientToken, RegionType regionType, string serverUrl,
            bool enableTapDB, string channel,
            string gameVersion)
        {
            ClientID = clientID;
            ClientToken = clientToken;
            RegionType = regionType;
            ServerURL = serverUrl;
            DBConfig = new TapDBConfig(enableTapDB, channel, gameVersion);
        }

        private TapConfig(string clientID, string clientToken, RegionType regionType, string serverUrl,
            bool enableTapDB, string channel,
            string gameVersion, bool advertiserIDCollectionEnabled)
        {
            ClientID = clientID;
            ClientToken = clientToken;
            ServerURL = serverUrl;
            RegionType = regionType;
            DBConfig = new TapDBConfig(enableTapDB, channel, gameVersion, advertiserIDCollectionEnabled);
        }

        public string ToJson()
        {
            var dic = new Dictionary<string, object>
            {
                ["clientID"] = ClientID,
                ["clientToken"] = ClientToken,
                ["isCN"] = RegionType == RegionType.CN,
                ["dbConfig"] = DBConfig?.ToDic()
            };
            return Json.Serialize(dic);
        }

        public class Builder
        {
            private string _clientID;

            private string _clientToken;

            private string _serverURL;

            private RegionType _regionType;

            private bool _enableTapDB = true;


            private string _channel;

            private string _gameVersion;

            private bool _advertiserIDCollectionEnabled;

            public Builder()
            {
            }

            public Builder ClientID(string clientId)
            {
                _clientID = clientId;
                return this;
            }

            public Builder ClientToken(string secret)
            {
                _clientToken = secret;
                return this;
            }

            public Builder ServerURL(string serverURL)
            {
                _serverURL = serverURL;
                return this;
            }

            public Builder RegionType(RegionType type)
            {
                _regionType = type;
                return this;
            }

            public Builder EnableTapDB(bool enable)
            {
                _enableTapDB = enable;
                return this;
            }

            public Builder TapDBConfig(bool enable, string channel, string gameVersion)
            {
                _enableTapDB = enable;
                _channel = channel;
                _gameVersion = gameVersion;
                return this;
            }

            public Builder TapDBConfig(bool enable, string channel, string gameVersion,
                bool advertiserIDCollectionEnabled)
            {
                _enableTapDB = enable;
                _channel = channel;
                _gameVersion = gameVersion;
                _advertiserIDCollectionEnabled = advertiserIDCollectionEnabled;
                return this;
            }

            public TapConfig ConfigBuilder()
            {
                return new TapConfig(_clientID, _clientToken, _regionType, _serverURL, _enableTapDB, _channel,
                    _gameVersion,
                    _advertiserIDCollectionEnabled);
            }
        }
    }


    public class TapDBConfig
    {
        public readonly bool Enable;

        public readonly string Channel;

        public readonly string GameVersion;

        public readonly bool AdvertiserIDCollectionEnabled;

        public TapDBConfig(bool enable)
        {
            Enable = enable;
        }

        public TapDBConfig(bool enable, string channel, string gameVersion)
        {
            Enable = enable;
            Channel = channel;
            GameVersion = gameVersion;
        }

        public TapDBConfig(bool enable, string channel, string gameVersion, bool advertiserIDCollectionEnabled)
        {
            Enable = enable;
            Channel = channel;
            GameVersion = gameVersion;
            AdvertiserIDCollectionEnabled = advertiserIDCollectionEnabled;
        }

        public Dictionary<string, object> ToDic()
        {
            return new Dictionary<string, object>
            {
                ["channel"] = Channel,
                ["gameVersion"] = GameVersion,
                ["enable"] = Enable,
                ["advertiserIDCollectionEnabled"] = AdvertiserIDCollectionEnabled
            };
        }
    }
}