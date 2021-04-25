using System.Collections.Generic;
using TapTap.Common;
using UnityEngine;

namespace TapTap.Login
{
    public class Profile
    {
        public string name;

        public string avatar;

        public string openid;

        public string unionid;

        public Profile(string json)
        {
            var dic = Json.Deserialize(json) as Dictionary<string, object>;
            name = SafeDictionary.GetValue<string>(dic, "name");
            avatar = SafeDictionary.GetValue<string>(dic, "avatar");
            openid = SafeDictionary.GetValue<string>(dic, "openid");
            unionid = SafeDictionary.GetValue<string>(dic, "unionid");
        }

        public string ToJson()
        {
            return JsonUtility.ToJson(this);
        }
    }
}