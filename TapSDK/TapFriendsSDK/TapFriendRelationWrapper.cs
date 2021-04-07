using System;
using System.Collections.Generic;
using TapCommonSDK;
using UnityEngine;

namespace TapFriendsSDK
{
    public class TapFriendRelationWrapper
    {
        public List<TapFriendRelation> wrapper;
        public TapFriendRelationWrapper(List<object> list)
        {
            if (list == null)
            {
                return;
            }
            this.wrapper = new List<TapFriendRelation>();
            foreach (var tmp in list)
            {
                Dictionary<string, object> dic = tmp as Dictionary<string, object>;
                TapFriendRelation relation = new TapFriendRelation(dic);
                wrapper.Add(relation);
            }
        }
    }
}