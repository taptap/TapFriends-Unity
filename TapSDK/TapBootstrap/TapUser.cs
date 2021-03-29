using System.Collections.Generic;
using UnityEngine;

namespace TapBootstrap
{
    public class TapUser
    {
        public string userId;

        public string name;

        public string avatar;

        public string taptapUserId;

        public bool isGuest;

        public long gender;
        
        public TapUser()
        {
                
        }

        public TapUser(string json)
        {
            Dictionary<string, object> dic = TapCommon.Json.Deserialize(json) as Dictionary<string, object>;
            this.userId = TapCommon.SafeDictionary.GetValue<string>(dic, "user_id");
            this.name = TapCommon.SafeDictionary.GetValue<string>(dic, "name");
            this.avatar = TapCommon.SafeDictionary.GetValue<string>(dic, "avatar");
            this.taptapUserId = TapCommon.SafeDictionary.GetValue<string>(dic, "taptap_user_id");
            this.isGuest = TapCommon.SafeDictionary.GetValue<bool>(dic, "is_guest");
            this.gender = TapCommon.SafeDictionary.GetValue<long>(dic, "gender");
        }

        public string ToJSON()
        {
            return JsonUtility.ToJson(this);
        }
    }
}