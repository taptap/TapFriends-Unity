using System.Collections.Generic;
using TapTap.Common;

namespace TapTap.Bootstrap
{
    public class TapConfig
    {
        private readonly string _clientID;

        private readonly RegionType _regionType;

        private readonly string _clientSecret;

        private readonly TapDBConfig _dbConfig;

        private TapConfig(string clientID, string clientSecret, RegionType regionType)
        {
            _clientID = clientID;
            _clientSecret = clientSecret;
            _regionType = regionType;
        }

        private TapConfig(string clientID, string clientSecret, RegionType regionType, bool enableTapDB, string channel,
            string gameVersion)
        {
            _clientID = clientID;
            _clientSecret = clientSecret;
            _regionType = regionType;
            _dbConfig = new TapDBConfig(enableTapDB, channel, gameVersion);
        }

        public string ToJson()
        {
            var dic = new Dictionary<string, object>
            {
                ["clientID"] = _clientID,
                ["clientSecret"] = _clientSecret,
                ["isCN"] = _regionType == RegionType.CN,
                ["dbConfig"] = _dbConfig?.ToDic()
            };
            return Json.Serialize(dic);
        }

        public class TapConfigBuilder
        {
            private string _clientID;

            private string _clientSecret;

            private RegionType _regionType;

            private bool _enableTapDB;

            private string _channel;

            private string _gameVersion;

            public TapConfigBuilder()
            {
            }

            public TapConfigBuilder ClientID(string clientId)
            {
                _clientID = clientId;
                return this;
            }

            public TapConfigBuilder ClientSecret(string secret)
            {
                _clientSecret = secret;
                return this;
            }

            public TapConfigBuilder RegionType(RegionType type)
            {
                _regionType = type;
                return this;
            }

            public TapConfigBuilder EnableTapDB(bool enable)
            {
                _enableTapDB = enable;
                return this;
            }
            
            public TapConfigBuilder TapDBConfig(bool enable, string channel, string gameVersion)
            {
                _enableTapDB = enable;
                _channel = channel;
                _gameVersion = gameVersion;
                return this;
            }

            public TapConfig Builder()
            {
                return new TapConfig(_clientID, _clientSecret, _regionType, _enableTapDB, _channel, _gameVersion);
            }
        }
    }


    public class TapDBConfig
    {
        private readonly bool _enable;

        private readonly string _channel;

        private readonly string _gameVersion;

        public TapDBConfig(bool enable)
        {
            _enable = enable;
        }

        public TapDBConfig(bool enable, string channel, string gameVersion)
        {
            _enable = enable;
            _channel = channel;
            _gameVersion = gameVersion;
        }

        public Dictionary<string, object> ToDic()
        {
            return new Dictionary<string, object>
            {
                ["channel"] = _channel,
                ["gameVersion"] = _gameVersion,
                ["enable"] = _enable
            };
        }
    }
}