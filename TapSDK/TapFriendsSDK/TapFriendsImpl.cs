using System;
using UnityEngine;
using TapCommonSDK;
using System.Collections.Generic;

namespace TapFriendsSDK
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

        public void GetFollowingList(int from, int mutualAttention, int limit,
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