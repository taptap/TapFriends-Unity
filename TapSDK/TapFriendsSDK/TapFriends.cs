using TapCommonSDK;
using System;
using System.Collections.Generic;

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
        public static void UnblockUser(string userId, Action<TapError> action)
        {
            TapFriendsImpl.GetInstance().UnblockUser(userId, action);
        }
        public static void GetFollowingList(int from, int mutualAttention, int limit,
            Action<List<TapFriendRelation>, TapError> action)
        {
            TapFriendsImpl.GetInstance().GetFollowingList(from, mutualAttention, limit, action);
        }
        public static void GetFollowerList(int from, int limit, Action<List<TapFriendRelation>, TapError> action)
        {
            TapFriendsImpl.GetInstance().GetFollowerList(from, limit, action);
        }
        public static void GetBlockList(int from, int limit, Action<List<TapFriendRelation>, TapError> action)
        {
            TapFriendsImpl.GetInstance().GetBlockList(from, limit, action);
        }
    }
}