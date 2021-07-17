using System.Collections.Generic;
using TapTap.Common;
using UnityEngine;

namespace TapTap.Login
{
    public class AccessToken
    {
        public string kid;

        public string accessToken;

        public string tokenType;

        public string macKey;

        public string macAlgorithm;

        public AccessToken(string json)
        {
            var dic = Json.Deserialize(json) as Dictionary<string, object>;
            kid = SafeDictionary.GetValue<string>(dic, "kid");
            accessToken = SafeDictionary.GetValue<string>(dic, "access_token");
            tokenType = SafeDictionary.GetValue<string>(dic, "token_type");
            macKey = SafeDictionary.GetValue<string>(dic, "mac_key");
            macAlgorithm = SafeDictionary.GetValue<string>(dic, "mac_algorithm");
        }

        public AccessToken()
        {
            
        }

        public string ToJson()
        {
            return JsonUtility.ToJson(this);
        }
    }
}