using System.Collections.Generic;
using TapTap.Common;
using UnityEngine;

namespace TapTap.Friends
{
    public class TapUserRelationShip
    {
        public string userId;

        public string name;

        public string avatar;

        public long gender;

        public bool mutualAttention;

        public string relationship;

        public Dictionary<string, object> richPresence;

        public TapUserRelationShip()
        {
        }

        public TapUserRelationShip(Dictionary<string, object> dic)
        {
            userId = SafeDictionary.GetValue<string>(dic, "userId");
            name = SafeDictionary.GetValue<string>(dic, "name");
            avatar = SafeDictionary.GetValue<string>(dic, "avatar");
            gender = SafeDictionary.GetValue<long>(dic, "gender");
            mutualAttention = SafeDictionary.GetValue<bool>(dic, "mutualAttention");
            relationship = SafeDictionary.GetValue<string>(dic, "relationship");
            richPresence = Json.Deserialize(SafeDictionary.GetValue<string>(dic, "richPresence")) as Dictionary<string,object>;
            Debug.Log("richPresence:" + SafeDictionary.GetValue<string>(dic, "richPresence"));
            Debug.Log("richPresence:" + richPresence);

        }

        public string ToJson()
        {
            return Json.Serialize(this);
        }
    }
}