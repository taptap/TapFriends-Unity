using System;
using System.Collections.Generic;
using TapTap.Common;
using UnityEngine;

namespace TapTap.Friends
{
    public class TapFriendsImpl : ITapFriends
    {
        private TapFriendsImpl()
        {
            EngineBridge.GetInstance()
                .Register(TapFriendsConstants.TAP_FRIENDS_CLZ, TapFriendsConstants.TAP_FRIENDS_IMPL);
        }

        private static volatile TapFriendsImpl sInstance;

        private static readonly object locker = new object();

        public static TapFriendsImpl GetInstance()
        {
            lock (locker)
            {
                if (sInstance == null)
                {
                    sInstance = new TapFriendsImpl();
                }
            }

            return sInstance;
        }

        public void AddFriend(string userId, Action<TapError> action)
        {
            var command = new Command.Builder()
                .Service(TapFriendsConstants.TAP_FRIENDS_SERVICE)
                .Method("addFriend")
                .Args("addFriend", userId)
                .Callback(true)
                .OnceTime(true)
                .CommandBuilder();
            EngineBridge.GetInstance().CallHandler(command, result =>
            {
                if (!CheckResultLegal(result))
                {
                    action(new TapError(80080, "TapSDK AddFriend Error!"));
                    return;
                }

                var wrapper = new TapAddFriendWrapper(result.content);
                if (wrapper.addFriendCode == 0)
                {
                    action(null);
                    return;
                }

                action(TapError.SafeConstrucTapError(wrapper.wrapper));
            });
        }

        public void DeleteFriend(string userId, Action<TapError> action)
        {
            var command = new Command.Builder()
                .Service(TapFriendsConstants.TAP_FRIENDS_SERVICE)
                .Method("deleteFriend")
                .Args("deleteFriend", userId)
                .Callback(true)
                .OnceTime(true)
                .CommandBuilder();
            EngineBridge.GetInstance().CallHandler(command, result =>
            {
                if (!CheckResultLegal(result))
                {
                    action(new TapError(80080,
                        errorDescription: "TapSDK DeleteFriend Error!"));
                    return;
                }

                var wrapper = new TapDeleteFriendWrapper(result.content);
                if (wrapper.deleteFriendCode == 0)
                {
                    action(null);
                    return;
                }

                action(TapError.SafeConstrucTapError(wrapper.wrapper));
            });
        }

        public void BlockUser(string userId, Action<TapError> action)
        {
            var command = new Command.Builder()
                .Service(TapFriendsConstants.TAP_FRIENDS_SERVICE)
                .Method("blockUser")
                .Args("blockUser", userId)
                .Callback(true)
                .OnceTime(true)
                .CommandBuilder();
            EngineBridge.GetInstance().CallHandler(command, result =>
            {
                if (!CheckResultLegal(result))
                {
                    action(new TapError(80080,
                        errorDescription: "TapSDK blockUser Error!"));
                    return;
                }

                var wrapper = new TapBlockFriendWrapper(result.content);
                if (wrapper.blockFriendCode == 0)
                {
                    action(null);
                    return;
                }

                action(TapError.SafeConstrucTapError(wrapper.wrapper));
            });
        }

        public void UnblockUser(string userId, Action<TapError> action)
        {
            var command = new Command.Builder()
                .Service(TapFriendsConstants.TAP_FRIENDS_SERVICE)
                .Method("unblockUser")
                .Args("unblockUser", userId)
                .Callback(true)
                .OnceTime(true)
                .CommandBuilder();
            EngineBridge.GetInstance().CallHandler(command, result =>
            {
                if (!CheckResultLegal(result))
                {
                    action(new TapError(80080,
                        errorDescription: "TapSDK unblockUser Error!"));
                    return;
                }

                var wrapper = new TapUnBlockFriendWrapper(result.content);
                if (wrapper.unblockFriendCode == 0)
                {
                    action(null);
                    return;
                }

                action(TapError.SafeConstrucTapError(wrapper.wrapper));
            });
        }

        public void GetFollowingList(int from, bool mutualAttention, int limit,
            Action<List<TapUserRelationShip>, TapError> action)
        {
            var command = new Command.Builder()
                .Service(TapFriendsConstants.TAP_FRIENDS_SERVICE)
                .Method("getFollowingList")
                .Args("getFollowingList", from)
                .Args("mutualAttention", mutualAttention)
                .Args("limit", limit)
                .Callback(true)
                .OnceTime(true)
                .CommandBuilder();
            EngineBridge.GetInstance().CallHandler(command, result =>
            {
                if (!CheckResultLegal(result))
                {
                    action(null,
                        new TapError(80080,
                            errorDescription: "TapSDK getFollowingList Error!"));
                    return;
                }

                var wrapper = new TapGetFollowingListWrapper(result.content);
                if (wrapper.getFollowingListCode == 0)
                {
                    TapFriendRelationWrapper relationWrapper = new TapFriendRelationWrapper((List<object>)wrapper.wrapper);
                    action(relationWrapper.wrapper, null);
                    return;
                }

                action(null, TapError.SafeConstrucTapError((string)wrapper.wrapper));
            });
        }

        public void GetFollowerList(int from, int limit, Action<List<TapUserRelationShip>, TapError> action)
        {
            var command = new Command.Builder()
                .Service(TapFriendsConstants.TAP_FRIENDS_SERVICE)
                .Method("getFollowerList")
                .Args("getFollowerList", from)
                .Args("limit", limit)
                .Callback(true)
                .OnceTime(true)
                .CommandBuilder();
            EngineBridge.GetInstance().CallHandler(command, result =>
            {
                if (!CheckResultLegal(result))
                {
                    action(null,
                        new TapError(80080,
                            errorDescription: "TapSDK getFollowerList Error!"));
                    return;
                }

                var wrapper = new TapGetFollowerListWrapper(result.content);
                if (wrapper.getFollowerListCode == 0)
                {
                    TapFriendRelationWrapper relationWrapper = new TapFriendRelationWrapper((List<object>)wrapper.wrapper);
                    action(relationWrapper.wrapper, null);
                    return;
                }

                action(null, TapError.SafeConstrucTapError((string)wrapper.wrapper));
            });
        }

        public void GetBlockList(int from, int limit, Action<List<TapUserRelationShip>, TapError> action)
        {
            var command = new Command.Builder()
                .Service(TapFriendsConstants.TAP_FRIENDS_SERVICE)
                .Method("getBlockList")
                .Args("getBlockList", from)
                .Args("limit", limit)
                .Callback(true)
                .OnceTime(true)
                .CommandBuilder();
            EngineBridge.GetInstance().CallHandler(command, result =>
            {
                if (!CheckResultLegal(result))
                {
                    action(null,
                        new TapError(80080,
                            errorDescription: "TapSDK getBlockList Error!"));
                    return;
                }

                var wrapper = new TapGetBlockListWrapper(result.content);
                if (wrapper.getBlockListCode == 0)
                {
                    TapFriendRelationWrapper relationWrapper = new TapFriendRelationWrapper((List<object>)wrapper.wrapper);
                    action(relationWrapper.wrapper, null);
                    return;
                }

                action(null, TapError.SafeConstrucTapError((string)wrapper.wrapper));
            });
        }

        public void SearchUser(string userId, Action<TapUserRelationShip, TapError> action)
        {
            var command = new Command.Builder()
                .Service(TapFriendsConstants.TAP_FRIENDS_SERVICE)
                .Method("searchUser")
                .Args("searchUser", userId)
                .Callback(true)
                .OnceTime(true)
                .CommandBuilder();
            EngineBridge.GetInstance().CallHandler(command, result =>
            {
                if (!CheckResultLegal(result))
                {
                    action(null,
                        new TapError(code: 80080,
                            errorDescription: "TapSDK searchUser Error!"));
                    return;
                }
                
                var dic = Json.Deserialize(result.content) as Dictionary<string, object>;
                var error = SafeDictionary.GetValue<string>(dic, "error");
                if (string.IsNullOrEmpty(error))
                {
                    var resultJson = SafeDictionary.GetValue<string>(dic, "result");
                    var resultDic = Json.Deserialize(resultJson) as Dictionary<string, object>;
                    var relationShip = new TapUserRelationShip(resultDic);
                    action(relationShip, null);
                }
                else
                {
                    action(null, TapError.SafeConstrucTapError(error));
                }
            });
        }

        public void GenerateFriendInvitation(Action<string, TapError> action)
        {
            var command = new Command.Builder()
                .Service(TapFriendsConstants.TAP_FRIENDS_SERVICE)
                .Method("generateFriendInvitation")
                .Callback(true)
                .OnceTime(true)
                .CommandBuilder();
            EngineBridge.GetInstance().CallHandler(command, result =>
            {
                if (!CheckResultLegal(result))
                {
                    action(null,
                        new TapError(code: 80080,
                            errorDescription: "TapSDK generateFriendInvitation Error!"));
                    return;
                }
                
                var dic = Json.Deserialize(result.content) as Dictionary<string, object>;
                var error = SafeDictionary.GetValue<string>(dic, "error");
                if (string.IsNullOrEmpty(error))
                {
                    var resultStr = SafeDictionary.GetValue<string>(dic, "result");
                    action(resultStr, null);
                }
                else
                {
                    action(null, TapError.SafeConstrucTapError(error));
                }
            });
        }

        public void SendFriendInvitation(Action<bool, TapError> action)
        {
            var command = new Command.Builder()
                .Service(TapFriendsConstants.TAP_FRIENDS_SERVICE)
                .Method("sendFriendInvitation")
                .Callback(true)
                .OnceTime(true)
                .CommandBuilder();
            EngineBridge.GetInstance().CallHandler(command, result =>
            {
                if (!CheckResultLegal(result))
                {
                    action(false,
                        new TapError(code: 80080,
                            errorDescription: "TapSDK sendFriendInvitation Error!"));
                    return;
                }
                
                var dic = Json.Deserialize(result.content) as Dictionary<string, object>;
                var error = SafeDictionary.GetValue<string>(dic, "error");
                if (string.IsNullOrEmpty(error))
                {
                    var boolValue = SafeDictionary.GetValue<int>(dic, "result") == 1;
                    action(boolValue, null);
                }
                else
                {
                    action(false, TapError.SafeConstrucTapError(error));
                }
            });
        }

        public void RegisterMessageListener(ITapMessageListener listener)
        {
            var command = new Command.Builder()
                .Service(TapFriendsConstants.TAP_FRIENDS_SERVICE)
                .Method("registerMessageListener")
                .Callback(true)
                .OnceTime(false)
                .CommandBuilder();
            EngineBridge.GetInstance().CallHandler(command, result =>
            {
                if (!CheckResultLegal(result))
                {
                    listener.OnMessageWithCode(80080, null);
                    return;
                }
                
                var dic = Json.Deserialize(result.content) as Dictionary<string, object>;
                var code = SafeDictionary.GetValue<int>(dic, "messageCallbackCode");
                var json = SafeDictionary.GetValue<string>(dic, "wrapper");
                if (!string.IsNullOrEmpty(json))
                {
                    var message = Json.Deserialize(json) as Dictionary<string, object>;
                    listener.OnMessageWithCode(code, message);
                }
                else
                {
                    listener.OnMessageWithCode(code, null);
                }
            });
        }

        private bool CheckResultLegal(Result result)
        {
            if (result == null)
            {
                return false;
            }

            if (result.code != Result.RESULT_SUCCESS)
            {
                return false;
            }

            return !string.IsNullOrEmpty(result.content);
        }
    }
}