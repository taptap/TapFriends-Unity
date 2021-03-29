using System.Collections.Generic;
using UnityEngine;

namespace TapBootstrap
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
            Dictionary<string,object> dic = TapCommon.Json.Deserialize(json) as Dictionary<string,object>;
            kid = TapCommon.SafeDictionary.GetValue<string>(dic,"kid");
            accessToken = TapCommon.SafeDictionary.GetValue<string>(dic,"access_token");
            macAlgorithm = TapCommon.SafeDictionary.GetValue<string>(dic,"mac_algorithm");
            tokenType = TapCommon.SafeDictionary.GetValue<string>(dic,"token_type");
            macKey = TapCommon.SafeDictionary.GetValue<string>(dic,"mac_key");
            expireIn = TapCommon.SafeDictionary.GetValue<long>(dic,"expire_in");
        }

        public string ToJSON()
        {
            return JsonUtility.ToJson(this);
        }
    }
}