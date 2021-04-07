using System.Collections.Generic;
using TapCommonSDK;

namespace TapFriendsSDK
{
    public class TapAddFriendWrapper
    {
        public string wrapper;
        public int addFriendCode;

        public TapAddFriendWrapper(string json)
        {
            Dictionary<string, object> dic = Json.Deserialize(json) as Dictionary<string, object>;
            this.wrapper = SafeDictionary.GetValue<string>(dic, "wrapper");
            this.addFriendCode = SafeDictionary.GetValue<int>(dic, "addFriend");
        }
    }

    public class TapDeleteFriendWrapper
    {
        public string wrapper;
        public int deleteFriendCode;

        public TapDeleteFriendWrapper(string json)
        {
            Dictionary<string, object> dic = Json.Deserialize(json) as Dictionary<string, object>;
            this.wrapper = SafeDictionary.GetValue<string>(dic, "wrapper");
            this.deleteFriendCode = SafeDictionary.GetValue<int>(dic, "deleteFriend");
        }
    }

    public class TapGetFollowingListWrapper
    {
        public string wrapper;
        public int getFollowingListCode;

        public TapGetFollowingListWrapper(string json)
        {
            Dictionary<string, object> dic = Json.Deserialize(json) as Dictionary<string, object>;
            this.wrapper = SafeDictionary.GetValue<string>(dic, "wrapper");
            this.getFollowingListCode = SafeDictionary.GetValue<int>(dic, "getFollowingList");
        }
    }

    public class TapGetFansListWrapper
    {
        public string wrapper;
        public int getFansListCode;

        public TapGetFansListWrapper(string json)
        {
            Dictionary<string, object> dic = Json.Deserialize(json) as Dictionary<string, object>;
            this.wrapper = SafeDictionary.GetValue<string>(dic, "wrapper");
            this.getFansListCode = SafeDictionary.GetValue<int>(dic, "getFansList");
        }
    }

    public class TapBlockFriendWrapper
    {
        public string wrapper;
        public int blockFriendCode;

        public TapBlockFriendWrapper(string json)
        {
            Dictionary<string, object> dic = Json.Deserialize(json) as Dictionary<string, object>;
            this.wrapper = SafeDictionary.GetValue<string>(dic, "wrapper");
            this.blockFriendCode = SafeDictionary.GetValue<int>(dic, "blockFriend");
        }
    }

    public class TapUnBlockFriendWrapper
    {
        public string wrapper;
        public int unblockFriendCode;

        public TapUnBlockFriendWrapper(string json)
        {
            Dictionary<string, object> dic = Json.Deserialize(json) as Dictionary<string, object>;
            this.wrapper = SafeDictionary.GetValue<string>(dic, "wrapper");
            this.unblockFriendCode = SafeDictionary.GetValue<int>(dic, "unblockFriend");
        }
    }

    public class TapGetBlockListWrapper
    {
        public string wrapper;
        public int getBlockListCode;

        public TapGetBlockListWrapper(string json)
        {
            Dictionary<string, object> dic = Json.Deserialize(json) as Dictionary<string, object>;
            this.wrapper = SafeDictionary.GetValue<string>(dic, "wrapper");
            this.getBlockListCode = SafeDictionary.GetValue<int>(dic, "getBlockList");
        }
    }
}





































