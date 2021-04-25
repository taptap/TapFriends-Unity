using System.Collections.Generic;
using TapTap.Common;
using UnityEngine;

namespace TapTap.Login
{
    public class TapLoginToken
    {
        public string kid;

        public string accessToken;

        public string tokenType;

        public string macKey;

        public string macAlgorithm;

        public TapLoginToken(string json)
        {
            var dic = Json.Deserialize(json) as Dictionary<string, object>;
            this.kid = SafeDictionary.GetValue<string>(dic, "kid");
            this.accessToken = SafeDictionary.GetValue<string>(dic, "access_token");
            this.tokenType = SafeDictionary.GetValue<string>(dic, "token_type");
            this.macKey = SafeDictionary.GetValue<string>(dic, "mac_key");
            this.macAlgorithm = SafeDictionary.GetValue<string>(dic, "mac_algorithm");
        }

        public TapLoginToken()
        {
            
        }

        public string ToJson()
        {
            return JsonUtility.ToJson(this);
        }
    }
}