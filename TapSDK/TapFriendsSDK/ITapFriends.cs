using System;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;

namespace TapFriendsSDK
{
    public interface ITapFriends
    {
        void AddFriend(string userId, Action<TapError> action);
        
        void DeleteFriend(string userId, Action<TapError> action);
        
        void BlockUser(string userId, Action<TapError> action);
        
        void UnBlockUser(string userId, Action<TapError> action);
        
        void GetFollowList(int from, int mutualAttention, int limit, Action<List<TapFriendRelation>, TapError> action);
        
        void GetFansList(int from, int limit, Action<List<TapFriendRelation>, TapError> action);
        
        void GetBlockList(int from, int limit, Action<List<TapFriendRelation>, TapError> action);
    }
}