using System;
using System.Collections.Generic;
using TapTap.Common;
using UnityEngine;

namespace TapTap.Bootstrap
{
    [Obsolete("已弃用")]
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
            var dic = Json.Deserialize(json) as Dictionary<string, object>;
            userId = SafeDictionary.GetValue<string>(dic, "user_id");
            name = SafeDictionary.GetValue<string>(dic, "name");
            avatar = SafeDictionary.GetValue<string>(dic, "avatar");
            taptapUserId = SafeDictionary.GetValue<string>(dic, "taptap_user_id");
            isGuest = SafeDictionary.GetValue<bool>(dic, "is_guest");
            gender = SafeDictionary.GetValue<long>(dic, "gender");

            var entry =
                SafeDictionary.GetValue<Dictionary<string, object>>(dic, "user_center_entries");
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
            isMomentEnabled = SafeDictionary.GetValue<bool>(dic, "is_moment_enabled");
        }

        public string ToJSON()
        {
            return JsonUtility.ToJson(this);
        }
    }
}