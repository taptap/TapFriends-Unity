using TapCommonSDK;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace TapFriendsSDK
{
    public class TapFriends
    {
        public static void AddFriend(string userId, Action<TapError> action)
        {
            TapFriendsImpl.GetInstance().AddFriend(userId, action);
        }
        public static void DeleteFriend(string userId, Action<TapError> action)
        {
            TapFriendsImpl.GetInstance().DeleteFriend(userId, action);
        }
        public static void BlockUser(string userId, Action<TapError> action)
        {
            TapFriendsImpl.GetInstance().BlockUser(userId, action);
        }
        public static void UnBlockUser(string userId, Action<TapError> action)
        {
            TapFriendsImpl.GetInstance().UnBlockUser(userId, action);
        }
        public static void GetFollowList(int from, int mutualAttention, int limit,
            Action<List<TapFriendRelation>, TapError> action)
        {
            TapFriendsImpl.GetInstance().GetFollowList(from, mutualAttention, limit, action);
        }
        public static void GetFansList(int from, int limit, Action<List<TapFriendRelation>, TapError> action)
        {
            TapFriendsImpl.GetInstance().GetFansList(from, limit, action);
        }
        public static void GetBlockList(int from, int limit, Action<List<TapFriendRelation>, TapError> action)
        {
            TapFriendsImpl.GetInstance().GetBlockList(from, limit, action);
        }
    }
}