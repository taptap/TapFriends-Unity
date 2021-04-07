using System.Collections.Generic;
using TapTap.Common;

namespace TapTap.Friends
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
        public object wrapper;
        public int getFollowingListCode;

        public TapGetFollowingListWrapper(string json)
        {
            Dictionary<string, object> dic = Json.Deserialize(json) as Dictionary<string, object>;
            this.getFollowingListCode = SafeDictionary.GetValue<int>(dic, "getFollowingList");
            if (this.getFollowingListCode == 0)
            {
                this.wrapper = SafeDictionary.GetValue<List<object>>(dic, "wrapper");
            }
            else
            {
                this.wrapper = SafeDictionary.GetValue<string>(dic, "wrapper");
            }
        }
    }

    public class TapGetFollowerListWrapper
    {
        public object wrapper;
        public int getFollowerListCode;

        public TapGetFollowerListWrapper(string json)
        {
            Dictionary<string, object> dic = Json.Deserialize(json) as Dictionary<string, object>;
            this.getFollowerListCode = SafeDictionary.GetValue<int>(dic, "getFollowerList");
            if (this.getFollowerListCode == 0)
            {
                this.wrapper = SafeDictionary.GetValue<List<object>>(dic, "wrapper");
            }
            else
            {
                this.wrapper = SafeDictionary.GetValue<string>(dic, "wrapper");
            }
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
        public object wrapper;
        public int getBlockListCode;

        public TapGetBlockListWrapper(string json)
        {
            Dictionary<string, object> dic = Json.Deserialize(json) as Dictionary<string, object>;
            this.getBlockListCode = SafeDictionary.GetValue<int>(dic, "getBlockList");
            if (this.getBlockListCode == 0)
            {
                this.wrapper = SafeDictionary.GetValue<List<object>>(dic, "wrapper");
            }
            else
            {
                this.wrapper = SafeDictionary.GetValue<string>(dic, "wrapper");
            }
        }
    }
}