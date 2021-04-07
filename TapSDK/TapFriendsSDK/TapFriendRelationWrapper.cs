using System;
using System.Collections.Generic;
using TapCommonSDK;
using UnityEngine;

namespace TapFriendsSDK
{
    public class TapFriendRelationWrapper
    {
        public List<TapUserRelationShip> wrapper;
        public TapFriendRelationWrapper(List<object> list)
        {
            if (list == null)
            {
                return;
            }
            this.wrapper = new List<TapUserRelationShip>();
            foreach (var tmp in list)
            {
                Dictionary<string, object> dic = tmp as Dictionary<string, object>;
                TapUserRelationShip relation = new TapUserRelationShip(dic);
                wrapper.Add(relation);
            }
        }
    }
}