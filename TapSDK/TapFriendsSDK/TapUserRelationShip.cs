using System;
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

        public bool online;

        public long time;

        public Dictionary<string, object> richPresence;

        public bool IsFollowed()
        {
            if (string.IsNullOrEmpty(relationship))
            {
                return false;
            }

            if (relationship.Length<1)
            {
                return false;
            }
            int index = relationship.Length - 1;
            return relationship.ToCharArray()[index] == '1';
        }

        public bool IsFollower()
        {
            if (string.IsNullOrEmpty(relationship))
            {
                return false;
            }

            if (relationship.Length<2)
            {
                return false;
            }

            int index = relationship.Length - 2;
            return relationship.ToCharArray()[index] == '1';
        }

        public bool IsBlocked()
        {
            if (string.IsNullOrEmpty(relationship))
            {
                return false;
            }

            if (relationship.Length<3)
            {
                return false;
            }

            int index = relationship.Length - 3;
            return relationship.ToCharArray()[index] == '1';
        }
        
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
            online = SafeDictionary.GetValue<bool>(dic, "online");
            time = SafeDictionary.GetValue<long>(dic, "time");
            var richPresenceJson = dic["richPresence"];
            if (richPresenceJson is string json)
            {
                richPresence = Json.Deserialize(json) as Dictionary<string, object>;
                return;
            }
            richPresence = SafeDictionary.GetValue<Dictionary<string, object>>(dic, "richPresence");
        }

        public string ToJson()
        {
            return Json.Serialize(this);
        }
    }
}