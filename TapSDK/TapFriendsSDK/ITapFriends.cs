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
        
        void GetFollowingList(int from, bool mutualAttention, int limit, Action<List<TapUserRelationShip>, TapError> action);
        
        void GetFollowerList(int from, int limit, Action<List<TapUserRelationShip>, TapError> action);
        
        void GetBlockList(int from, int limit, Action<List<TapUserRelationShip>, TapError> action);

        void SearchUser(string userId, Action<TapUserRelationShip, TapError> action);

        void GenerateFriendInvitation(Action<string, TapError> action);

        void SendFriendInvitation(Action<bool, TapError> action);

        void RegisterMessageListener(ITapMessageListener listener);
    }
    
    public interface ITapMessageListener
    {
        void OnMessageWithCode(int code, Dictionary<string, object> extras);
    }
}















