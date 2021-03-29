using System.Collections.Generic;
using UnityEngine;

namespace TapBootstrap
{
    public class TapLoginWrapper
    {
        public string wrapper;

        public int loginCallbackCode;

        public TapLoginWrapper(string json)
        {
            Dictionary<string,object> dic = TapCommon.Json.Deserialize(json) as Dictionary<string,object>;
            this.wrapper = TapCommon.SafeDictionary.GetValue<string>(dic,"wrapper");
            this.loginCallbackCode = TapCommon.SafeDictionary.GetValue<int>(dic,"loginCallbackCode");
        }
        
    }

    public class TapUserStatusWrapper
    {
        public string wrapper;

        public int userStatusCallbackCode;

        public TapUserStatusWrapper(string json)
        {
            Dictionary<string, object> dic = TapCommon.Json.Deserialize(json) as Dictionary<string, object>;
            this.wrapper = TapCommon.SafeDictionary.GetValue<string>(dic, "wrapper");
            this.userStatusCallbackCode = TapCommon.SafeDictionary.GetValue<int>(dic, "userStatusCallbackCode");
        }
    }
    
    public class TapUserInfoWrapper
    {
        public string wrapper;

        public int getUserInfoCode;

        public TapUserInfoWrapper(string json)
        {
            Dictionary<string,object> dic = TapCommon.Json.Deserialize(json) as Dictionary<string,object>;
            this.wrapper = TapCommon.SafeDictionary.GetValue<string>(dic,"wrapper");
            this.getUserInfoCode = TapCommon.SafeDictionary.GetValue<int>(dic,"getUserInfoCode");
            Debug.Log("Parse Info:" + json);
        }
    }

    public class TapUserDetailInfoWrapper
    {
        public string wrapper;

        public int getUserDetailInfoCode;

        public TapUserDetailInfoWrapper(string json)
        {
            Dictionary<string,object> dic = TapCommon.Json.Deserialize(json) as Dictionary<string,object>;
            this.wrapper = TapCommon.SafeDictionary.GetValue<string>(dic,"wrapper");
            this.getUserDetailInfoCode = TapCommon.SafeDictionary.GetValue<int>(dic,"getUserDetailInfoCode");
            Debug.Log("Parse Detail Info:" + json);
        }
    }
}