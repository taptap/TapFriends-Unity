using System.Collections.Generic;
using TapCommonSDK;
using UnityEngine;

namespace TapBootstrapSDK
{
    public class TapLoginWrapper
    {
        public string wrapper;

        public int loginCallbackCode;

        public TapLoginWrapper(string json)
        {
            Dictionary<string,object> dic = Json.Deserialize(json) as Dictionary<string,object>;
            this.wrapper = SafeDictionary.GetValue<string>(dic,"wrapper");
            this.loginCallbackCode = SafeDictionary.GetValue<int>(dic,"loginCallbackCode");
        }
        
    }

    public class TapUserStatusWrapper
    {
        public string wrapper;

        public int userStatusCallbackCode;

        public TapUserStatusWrapper(string json)
        {
            Dictionary<string, object> dic = Json.Deserialize(json) as Dictionary<string, object>;
            this.wrapper = SafeDictionary.GetValue<string>(dic, "wrapper");
            this.userStatusCallbackCode = SafeDictionary.GetValue<int>(dic, "userStatusCallbackCode");
        }
    }
    
    public class TapUserInfoWrapper
    {
        public string wrapper;

        public int getUserInfoCode;

        public TapUserInfoWrapper(string json)
        {
            Dictionary<string,object> dic = Json.Deserialize(json) as Dictionary<string,object>;
            this.wrapper = SafeDictionary.GetValue<string>(dic,"wrapper");
            this.getUserInfoCode = SafeDictionary.GetValue<int>(dic,"getUserInfoCode");
            Debug.Log("Parse Info:" + json);
        }
    }

    public class TapUserDetailInfoWrapper
    {
        public string wrapper;

        public int getUserDetailInfoCode;

        public TapUserDetailInfoWrapper(string json)
        {
            Dictionary<string,object> dic = Json.Deserialize(json) as Dictionary<string,object>;
            this.wrapper = SafeDictionary.GetValue<string>(dic,"wrapper");
            this.getUserDetailInfoCode = SafeDictionary.GetValue<int>(dic,"getUserDetailInfoCode");
            Debug.Log("Parse Detail Info:" + json);
        }
    }
}