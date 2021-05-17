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

        public TapUserRelationShip()
        {
            
        }
        
        public TapUserRelationShip(Dictionary<string, object> dic)
        {
            this.userId = SafeDictionary.GetValue<string>(dic, "userId");
            this.name = SafeDictionary.GetValue<string>(dic, "name");
            this.avatar = SafeDictionary.GetValue<string>(dic, "avatar");
            this.gender = SafeDictionary.GetValue<long>(dic, "gender");
            this.mutualAttention = SafeDictionary.GetValue<bool>(dic,  "mutualAttention");
            this.relationship = SafeDictionary.GetValue<string>(dic, "relationship");
        }
        
        public string ToJson()
        {
            return JsonUtility.ToJson(this);
        }
    }
}