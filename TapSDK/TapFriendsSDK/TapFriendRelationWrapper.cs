using System;
using System.Collections.Generic;
using UnityEngine;

namespace TapFriendsSDK
{
    public class TapFriendRelationWrapper<TapFriendRelation>
    {
        public List<TapFriendRelation> wrapper;

        public TapFriendRelationWrapper(string json)
        {
            JsonUtility.FromJsonOverwrite(json, this);
        }
    }
}