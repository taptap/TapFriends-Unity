using System.Collections.Generic;
using TapCommonSDK;
using UnityEngine;

namespace TapFriendsSDK
{
    public class TapFriendRelation
    {
        public string userId;
        public string name;
        public string avatar;
        public bool isGuest;
        public long gender;
        public int mutualFans;

        public TapFriendRelation(Dictionary<string, object> dic)
        {
            this.userId = SafeDictionary.GetValue<string>(dic, "userId");
            this.name = SafeDictionary.GetValue<string>(dic, "name");
            this.avatar = SafeDictionary.GetValue<string>(dic, "avatar");
            this.isGuest = SafeDictionary.GetValue<bool>(dic, "isGuest");
            this.gender = SafeDictionary.GetValue<long>(dic, "gender");
            this.mutualFans = SafeDictionary.GetValue<int>(dic, key: "mutualFans");
        }

        public string ToJson()
        {
            return JsonUtility.ToJson(this);
        }
    }
}