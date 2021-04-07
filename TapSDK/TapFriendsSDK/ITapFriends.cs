using System;
using System.Collections.Generic;

namespace TapTap.Friends
{
    public interface ITapFriends
    {
        void AddFriend(string userId, Action<TapError> action);
        
        void DeleteFriend(string userId, Action<TapError> action);
        
        void BlockUser(string userId, Action<TapError> action);
        
        void UnblockUser(string userId, Action<TapError> action);
        
        void GetFollowingList(int from, int mutualAttention, int limit, Action<List<TapUserRelationShip>, TapError> action);
        
        void GetFollowerList(int from, int limit, Action<List<TapUserRelationShip>, TapError> action);
        
        void GetBlockList(int from, int limit, Action<List<TapUserRelationShip>, TapError> action);
    }
}