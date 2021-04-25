using System.Collections.Generic;
using TapTap.Common;
using UnityEngine;
using UnityEngine.Assertions;

namespace TapTap.Bootstrap
{
    public class TapConfig
    {
        public string clientID;

        public RegionType regionType;

        public string clientSecret;

        public TapDBConfig dbConfig;

        public TapConfig(string clientID, string clientSecret, RegionType regionType)
        {
            this.clientID = clientID;
            this.clientSecret = clientSecret;
            this.regionType = regionType;
        }

        public TapConfig(string clientID, string clientSecret, RegionType regionType, string channel,
            string gameVersion)
        {
            this.clientID = clientID;
            this.clientSecret = clientSecret;
            this.regionType = regionType;
            dbConfig = new TapDBConfig(channel, gameVersion);
        }

        public string ToJson()
        {
            var dic = new Dictionary<string, object>
            {
                ["clientID"] = clientID,
                ["clientSecret"] = clientSecret,
                ["isCN"] = regionType == RegionType.CN,
                ["dbConfig"] = dbConfig?.ToJson()
            };
            return Json.Serialize(dic);
        }

        public class TapConfigBuilder
        {
            private string clientID;

            private string clientSecret;

            private RegionType regionType;

            private string channel;

            private string gameVersion;

            public TapConfigBuilder()
            {
            }

            public TapConfigBuilder ClientID(string clientID)
            {
                this.clientID = clientID;
                return this;
            }

            public TapConfigBuilder ClientSecret(string clientSecret)
            {
                this.clientSecret = clientSecret;
                return this;
            }

            public TapConfigBuilder RegionType(RegionType regionType)
            {
                this.regionType = regionType;
                return this;
            }

            public TapConfigBuilder TapDBConfig(string channel, string gameVersion)
            {
                this.channel = channel;
                this.gameVersion = gameVersion;
                return this;
            }

            public TapConfig Builder()
            {
                return new TapConfig(clientID, clientSecret, regionType, channel, gameVersion);
            }
        }
    }


    public class TapDBConfig
    {
        private string channel;

        private string gameVersion;

        public TapDBConfig(string channel, string gameVersion)
        {
            this.channel = channel;
            this.gameVersion = gameVersion;
        }

        public Dictionary<string, object> ToJson()
        {
            return new Dictionary<string, object>
            {
                ["channel"] = channel,
                ["gameVersion"] = gameVersion
            };
        }
    }
}