using System;
using System.Collections.Generic;
using TapTap.Common;
using UnityEngine;

namespace TapTap.Bootstrap
{
    [Obsolete("已弃用")]
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
            Dictionary<string, object> dic = Json.Deserialize(json) as Dictionary<string, object>;
            userId = SafeDictionary.GetValue<string>(dic, "user_id");
            name = SafeDictionary.GetValue<string>(dic, "name");
            avatar = SafeDictionary.GetValue<string>(dic, "avatar");
            taptapUserId = SafeDictionary.GetValue<string>(dic, "taptap_user_id");
            isGuest = SafeDictionary.GetValue<bool>(dic, "is_guest");
            gender = SafeDictionary.GetValue<long>(dic, "gender");
        }

        public string ToJSON()
        {
            return JsonUtility.ToJson(this);
        }
    }
}