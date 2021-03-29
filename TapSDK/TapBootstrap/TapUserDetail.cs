using System.Collections.Generic;
using UnityEngine;

namespace TapBootstrap
{
    public class TapUserDetail
    {
        public string userId;

        public string name;

        public string avatar;

        public string taptapUserId;

        public bool isGuest;

        public long gender;

        public TapUserCenterEntry userCenterEntry;

        public TapUserDetail()
        {
        }

        public TapUserDetail(string json)
        {
            var dic = TapCommon.Json.Deserialize(json) as Dictionary<string, object>;
            userId = TapCommon.SafeDictionary.GetValue<string>(dic, "user_id");
            name = TapCommon.SafeDictionary.GetValue<string>(dic, "name");
            avatar = TapCommon.SafeDictionary.GetValue<string>(dic, "avatar");
            taptapUserId = TapCommon.SafeDictionary.GetValue<string>(dic, "taptap_user_id");
            isGuest = TapCommon.SafeDictionary.GetValue<bool>(dic, "is_guest");
            gender = TapCommon.SafeDictionary.GetValue<long>(dic, "gender");

            var entry =
                TapCommon.SafeDictionary.GetValue<Dictionary<string, object>>(dic, "user_center_entries");
            if (entry != null)
            {
                userCenterEntry = new TapUserCenterEntry(entry);
            }
        }

        public string ToJSON()
        {
            return JsonUtility.ToJson(this);
        }
    }

    public class TapUserCenterEntry
    {
        public bool isMomentEnabled;

        public TapUserCenterEntry(Dictionary<string, object> dic)
        {
            isMomentEnabled = TapCommon.SafeDictionary.GetValue<bool>(dic, "is_moment_enabled");
        }

        public string ToJSON()
        {
            return JsonUtility.ToJson(this);
        }
    }
}