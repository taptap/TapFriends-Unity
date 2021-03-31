using System.Collections.Generic;
using TapCommonSDK;
using UnityEngine;

namespace TapBootstrapSDK
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
            Dictionary<string, object> dic = Json.Deserialize(json) as Dictionary<string, object>;
            this.userId = SafeDictionary.GetValue<string>(dic, "user_id");
            this.name = SafeDictionary.GetValue<string>(dic, "name");
            this.avatar = SafeDictionary.GetValue<string>(dic, "avatar");
            this.taptapUserId = SafeDictionary.GetValue<string>(dic, "taptap_user_id");
            this.isGuest = SafeDictionary.GetValue<bool>(dic, "is_guest");
            this.gender = SafeDictionary.GetValue<long>(dic, "gender");
        }

        public string ToJSON()
        {
            return JsonUtility.ToJson(this);
        }
    }
}