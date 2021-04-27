using System.Collections.Generic;
using TapTap.Common;
using UnityEngine;

namespace TapTap.Bootstrap
{
    public class AccessToken
    {
        public string kid;

        public string accessToken;

        public string macAlgorithm;

        public string tokenType;

        public string macKey;

        public long expireIn;
        
        public AccessToken()
        {
            
        }

        public AccessToken(string json)
        {
            var dic = Json.Deserialize(json) as Dictionary<string,object>;
            kid = SafeDictionary.GetValue<string>(dic,"kid");
            accessToken = SafeDictionary.GetValue<string>(dic,"access_token");
            macAlgorithm = SafeDictionary.GetValue<string>(dic,"mac_algorithm");
            tokenType = SafeDictionary.GetValue<string>(dic,"token_type");
            macKey = SafeDictionary.GetValue<string>(dic,"mac_key");
            expireIn = SafeDictionary.GetValue<long>(dic,"expire_in");
        }

        public string ToJSON()
        {
            return JsonUtility.ToJson(this);
        }
    }
}